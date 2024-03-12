#!/usr/bin/env bash

set -e

# CONSTANTS

readonly DNSMASQ_DOMAIN=kind.cluster

# FUNCTIONS

log(){
  echo "---------------------------------------------------------------------------------------"
  echo $1
  echo "---------------------------------------------------------------------------------------"
}

argocd(){
  log "ARGOCD ..."

  helm upgrade --install --wait --timeout 15m --atomic --namespace argocd --create-namespace \
    --repo https://argoproj.github.io/argo-helm argocd argo-cd -f ./extracted/argocd-values.yml
}

# RUN

argocd

# DONE

log "ARGOCD READY !"

echo "ARGOCD: https://argocd.$DNSMASQ_DOMAIN"
