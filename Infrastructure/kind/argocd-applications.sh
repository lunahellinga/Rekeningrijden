#!/usr/bin/env bash

set -e

# FUNCTIONS

deploy(){
  kubectl apply -n argocd -f argocd/$1.yaml

  kubectl delete secret -A -l owner=helm,name=$1
}

# RUN

deploy cilium
deploy cert-manager
deploy ingress-nginx
deploy keycloak
deploy argocd
deploy rbac-manager
deploy node-problem-detector
deploy polaris