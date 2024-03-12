terraform {
  required_providers {
    keycloak = {
      source  = "mrparkers/keycloak"
      version = "4.3.1"
    }
  }
}

# configure keycloak provider
provider "keycloak" {
  client_id = "admin-cli"
  username  = "admin"
  password  = "@6JKMT@qsFLu"
  url       = "https://keycloak.oibss.nl"
  base_path = ""
  root_ca_certificate = ""
  tls_insecure_skip_verify = "true"
}

locals {
  realm_id = "master"
  groups   = ["argocd-dev", "argocd-admin", "grafana-dev", "grafana-admin", "kube-dev", "kube-admin"]
  user_groups = {
    user-dev = {
      groups    = ["argocd-dev", "grafana-dev", "kube-dev"]
      gitlab_id = 1
    }
    user-admin = {
      groups    = ["argocd-admin", "grafana-admin", "kube-admin"]
      gitlab_id = 2
    }
  }
}

# create groups
resource "keycloak_group" "groups" {
  for_each = toset(local.groups)
  realm_id = local.realm_id
  name     = each.key
}

# create users
resource "keycloak_user" "users" {
  for_each       = local.user_groups
  realm_id       = local.realm_id
  username       = each.key
  enabled        = true
  email          = "${each.key}@domain.com"
  email_verified = true
  first_name     = each.key
  last_name      = each.key

  initial_password {
    value = each.key
  }

  attributes = {
    gitlab_id = each.value.gitlab_id
  }
}

# configure use groups membership
resource "keycloak_user_groups" "user_groups" {
  for_each  = local.user_groups
  realm_id  = local.realm_id
  user_id   = keycloak_user.users[each.key].id
  group_ids = [for g in each.value.groups : keycloak_group.groups[g].id]
}

# create groups openid client scope
resource "keycloak_openid_client_scope" "groups" {
  realm_id               = local.realm_id
  name                   = "groups"
  include_in_token_scope = true
  gui_order              = 1
}

resource "keycloak_openid_group_membership_protocol_mapper" "groups" {
  realm_id        = local.realm_id
  client_scope_id = keycloak_openid_client_scope.groups.id
  name            = "groups"
  claim_name      = "groups"
  full_path       = false
}
