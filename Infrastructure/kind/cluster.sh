#!/usr/bin/env bash

set -e

# CONSTANTS

readonly KIND_NODE_IMAGE=kindest/node:v1.27.1
readonly DNSMASQ_DOMAIN=kind.cluster
#readonly DNSMASQ_DOMAIN=oibss.nl
readonly DNSMASQ_CONF=kind.k8s.conf

# FUNCTIONS

log() {
  echo "---------------------------------------------------------------------------------------"
  echo $1
  echo "---------------------------------------------------------------------------------------"
}

wait_ready() {
  local NAME=${1:-pods}
  local TIMEOUT=${2:-5m}
  local SELECTOR=${3:---all}

  log "WAIT $NAME ($TIMEOUT) ..."

  kubectl wait -A --timeout=$TIMEOUT --for=condition=ready $NAME $SELECTOR
}

wait_pods_ready() {
  local TIMEOUT=${1:-5m}

  wait_ready pods $TIMEOUT --field-selector=status.phase!=Succeeded
}

wait_nodes_ready() {
  local TIMEOUT=${1:-5m}

  wait_ready nodes $TIMEOUT
}

network() {
  local NAME=${1:-kind}

  log "NETWORK (kind) ..."

  if [ -z $(docker network ls --filter name=^$NAME$ --format="{{ .Name }}") ]; then
    docker network create $NAME --subnet=172.42.0.0/16
    echo "Network $NAME created"
  else
    echo "Network $NAME already exists, skipping"
  fi
}

proxy() {
  local NAME=$1
  local TARGET=$2

  if [ -z $(docker ps --filter name=^proxy-gcr$ --format="{{ .Names }}") ]; then
    docker run -d --name $NAME --restart=always --net=kind -e REGISTRY_PROXY_REMOTEURL=$TARGET registry:2
    echo "Proxy $NAME (-> $TARGET) created"
  else
    echo "Proxy $NAME already exists, skipping"
  fi
}

proxies() {
  log "REGISTRY PROXIES ..."

  proxy proxy-docker-hub https://registry-1.docker.io
  proxy proxy-quay https://quay.io
  proxy proxy-gcr https://gcr.io
  proxy proxy-k8s-gcr https://k8s.gcr.io
  proxy proxy-github https://github.com
  proxy proxy-azure https://dev.azure.com/
}

get_service_lb_ip() {
  kubectl get svc -n $1 $2 -o jsonpath='{.status.loadBalancer.ingress[0].ip}'
}

get_subnet() {
  docker network inspect -f '{{(index .IPAM.Config 0).Subnet}}' $1
}

subnet_to_ip() {
  echo $1 | sed "s@0.0/16@$2@"
}

root_ca() {
  log "ROOT CERTIFICATE ..."

  mkdir -p .ssl

  if [[ -f ".ssl/root-ca.pem" && -f ".ssl/root-ca-key.pem" ]]; then
    echo "Root certificate already exists, skipping"
  else
    openssl genrsa -out .ssl/root-ca-key.pem 2048
    openssl req -x509 -new -nodes -key .ssl/root-ca-key.pem -days 3650 -sha256 -out .ssl/root-ca.pem -subj "/CN=kube-ca"
    echo "Root certificate created"
  fi
}

install_ca() {
  log "INSTALL CERTIFICATE AUTHORITY ..."

  #  sudo mkdir -p /usr/local/share/ca-certificates/oibss.nl
  #
  #  sudo cp -f .ssl/root-ca.pem /usr/local/share/ca-certificates/oibss.nl/ca.crt

  sudo mkdir -p /usr/local/share/ca-certificates/kind.cluster

  sudo cp -f .ssl/root-ca.pem /usr/local/share/ca-certificates/kind.cluster/ca.crt

  sudo update-ca-certificates
}

cluster() {
  local NAME=${1:-kind}

  log "CLUSTER ..."

  docker pull $KIND_NODE_IMAGE

  #  oidc-issuer-url: https://keycloak.kind.cluster/auth/realms/master
  kind create cluster --name $NAME --image $KIND_NODE_IMAGE --config=./extracted/kind-cluster.yml
}

cilium() {
  log "CILIUM ..."

  helm upgrade --install --wait --timeout 15m --atomic --namespace kube-system --create-namespace \
    --repo https://helm.cilium.io cilium cilium -f ./extracted/cilium-values.yml
}

cert_manager() {
  log "CERT MANAGER ..."

  helm upgrade --install --wait --timeout 15m --atomic --namespace cert-manager --create-namespace \
    --repo https://charts.jetstack.io cert-manager cert-manager -f ./extracted/cert-manager-values.yml
}

cert_manager_ca_secret() {
  kubectl delete secret -n cert-manager root-ca || true
  kubectl create secret tls -n cert-manager root-ca --cert=.ssl/root-ca.pem --key=.ssl/root-ca-key.pem
}

cert_manager_ca_issuer() {
  kubectl apply -n cert-manager -f ./extracted/cert-manager-cluster-issuer.yml
}

metallb() {
  log "METALLB ..."

  local KIND_SUBNET=$(get_subnet kind)
  local METALLB_START=$(subnet_to_ip $KIND_SUBNET 0.200)
  local METALLB_END=$(subnet_to_ip $KIND_SUBNET 0.250)
  local METALLB_RANGE=$METALLB_START-$METALLB_END
  echo METALLB_RANGE
  helm upgrade --install --wait --timeout 15m --atomic --namespace metallb-system --create-namespace \
    --repo https://metallb.github.io/metallb metallb metallb -f ./extracted/metallb-values.yml

  kubectl apply -f ./extracted/metallb-ip-pool.yml
  kubectl apply -f ./extracted/metallb-advertisement.yml
}

ingress() {
  log "INGRESS-NGINX ..."

  helm upgrade --install --wait --timeout 15m --atomic --namespace ingress-nginx --create-namespace \
    --repo https://kubernetes.github.io/ingress-nginx ingress-nginx ingress-nginx -f ./extracted/nginx-ingress-values.yml
}

dnsmasq() {
  log "DNSMASQ ..."

  local INGRESS_LB_IP=$(get_service_lb_ip ingress-nginx ingress-nginx-controller)

  echo "address=/$DNSMASQ_DOMAIN/$INGRESS_LB_IP" | sudo tee /etc/dnsmasq.d/$DNSMASQ_CONF
}

restart_service() {
  log "RESTART $1 ..."

  sudo systemctl restart $1
}

cleanup() {
  log "CLEANUP ..."

  kind delete cluster || true
  sudo rm -f /etc/dnsmasq.d/$DNSMASQ_CONF
  sudo rm -rf /usr/local/share/ca-certificates/kind.cluster
  sudo rm -rf /usr/local/share/ca-certificates/oibss.nl
}

# RUN

cleanup
network
proxies
root_ca
install_ca
cluster
cilium
cert_manager
cert_manager_ca_secret
cert_manager_ca_issuer
metallb
ingress
dnsmasq
restart_service dnsmasq

# DONE

log "CLUSTER READY !"

echo "HUBBLE UI: https://hubble-ui.$DNSMASQ_DOMAIN"
