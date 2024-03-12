# Setup

Based on the steps [here](https://cloud.google.com/kubernetes-engine/docs/deploy-app-cluster).

```shell
# Domain name via transip.nl
export IP_ADDRESS=34.102.255.52
export DOMAIN_NAME=oibss.nl
export CLUSTER_NAME=autopilot-cluster-1
export PROJECT_ID=rekeningrijden-fontys
```

1. Sign in

 ```shell
gcloud auth login
gcloud config set project $PROJECT_ID
```

2. Create the cluster

```shell
gcloud config set project $PROJECT_ID
gcloud container clusters create-auto $CLUSTER_NAME --region=europe-west4
gcloud container clusters get-credentials $CLUSTER_NAME --region europe-west4
 ```

3. Set up static ip

```shell
gcloud compute addresses create web-ip --global
gcloud compute addresses list
gcloud compute addresses describe web-ip --format='value(address)' --global
  ```

4. Cert manager

```shell
#kubectl apply -f google/ingress.yml
helm repo add jetstack https://charts.jetstack.io
helm repo update
helm upgrade --install --wait --timeout 10m --atomic --create-namespace --namespace cert-manager --set global.leaderElection.namespace=cert-manager cert-manager jetstack/cert-manager --reuse-values -f google/cert-manager-values.yml
```
```shell
kubectl apply -f google/cert-letsencrypt-staging.yml
```
```shell
helm upgrade --install --wait --timeout 10m --atomic --namespace default --create-namespace --repo https://charts.bitnami.com/bitnami keycloak keycloak -f google/keycloak-values.yml
```
```shell
kubectl apply -f google/empty-secret.yml
kubectl apply -f google/ingress-base.yml
```
Wait for ingress to be created and keycloak to be reachable on http.
```shell
kubectl apply -f google/ingress-certify.yml
```
Wait until the certificate resolves. This can take quite a while (10+ min)

```shell
./google/keycloak_users.sh
```
```shell
export TF_STATE=./google/terraform/tf-state/keycloak.tfstate
terraform -chdir=./google/terraform/keycloak init && terraform -chdir=./google/terraform/keycloak apply -auto-approve -state=$TF_STATE
```

```shell
  helm upgrade --install --wait --timeout 15m --atomic --namespace default --create-namespace --repo https://argoproj.github.io/argo-helm argocd argo-cd -f ./google/argocd-values.yml
```

```shell
kubectl apply -f google/cert-letsencrypt-prod.yml 
kubectl apply -f google/ingress-delivery.yml
```

Wait for all endpoints to work and to be able to sign in to ArgoCD. Keep in mind that the same cookies are used for Keycloak and ArgoCD, so you might get 

```shell
kubectl apply -f argocd/setup/repo.yaml
```
```shell
kubectl apply -f argocd/rabbitmq-cluster.yaml
```

Wait until rabbitmq is running

```shell
kubectl apply -f argocd/
```
# Stuff

Helm including scripts: https://medium.com/google-cloud/installing-helm-in-google-kubernetes-engine-7f07f43c536e
Cert
manager: https://cert-manager.io/docs/tutorials/getting-started-with-cert-manager-on-google-kubernetes-engine-using-lets-encrypt-for-ingress-ssl/
Google managed certs: https://cloud.google.com/kubernetes-engine/docs/how-to/managed-certs