#!/usr/bin/env bash

set -e

# CONSTANTS

kubectl_config() {
  local ID_TOKEN=$(curl --insecure -X POST https://keycloak.oibss.nl/.cluster/realms/master/protocol/openid-connect/token \
    -d grant_type=password \
    -d client_id=kube \
    -d client_secret=kube-client-secret \
    -d username=$1 \
    -d password=$1 \
    -d scope=openid \
    -d response_type=id_token | jq -r '.id_token')

  local REFRESH_TOKEN=$(curl --insecure -X POST https://keycloak.oibss.nl/.cluster/realms/master/protocol/openid-connect/token \
    -d grant_type=password \
    -d client_id=kube \
    -d client_secret=kube-client-secret \
    -d username=$1 \
    -d password=$1 \
    -d scope=openid \
    -d response_type=id_token | jq -r '.refresh_token')

  local CA_DATA=$(cat google/letsencrypt-staging/letsencrypt-stg-root-x1.pem | base64 | tr -d '\n')

  kubectl config set-credentials $1 \
    --auth-provider=oidc \
    --auth-provider-arg=client-id=kube \
    --auth-provider-arg=client-secret=kube-client-secret \
    --auth-provider-arg=idp-issuer-url=https://keycloak.kind.cluster/realms/master \
    --auth-provider-arg=id-token=$ID_TOKEN \
    --auth-provider-arg=refresh-token=$REFRESH_TOKEN \
    --auth-provider-arg=idp-certificate-authority-data=$CA_DATA

  kubectl config set-context $1 --cluster=kind-kind --user=$1
}

kubectl_config user-admin
kubectl_config user-dev
