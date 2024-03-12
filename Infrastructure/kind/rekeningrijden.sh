#!/usr/bin/env bash

set -e

# FUNCTIONS

deploy(){
  kubectl apply -n argocd -f ArgoCD/$1.yaml

  kubectl delete secret -A -l owner=helm,name=$1
}

wait_for_pods(){
  kubectl wait --for=condition=Ready pods --all --all-namespaces --timeout=300s
}
# RUN

#wait_for_pods
deploy repo
deploy rabbitmq
deploy car-service
deploy coordinate-service
deploy router-api
deploy payment-service