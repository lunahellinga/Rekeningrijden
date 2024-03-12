resource "keycloak_realm" "kw_realm" {
  realm             = "kwetter"
  enabled           = true
  display_name      = "Kwetter"
  display_name_html = "<b>Kwetter</b>"

  login_theme              = "base"
  edit_username_allowed    = true
  registration_allowed     = true
  reset_password_allowed   = true
  remember_me              = true
  login_with_email_allowed = true
  duplicate_emails_allowed = false

  access_code_lifespan = "1h"

  ssl_required    = "external"
  password_policy = "upperCase(1) and length(8) and forceExpiredPasswordChange(365) and notUsername"
  #  attributes      = {
  #    mycustomAttribute = "myCustomValue"
  #  }

  #  smtp_server {
  #    host = "smtp.example.com"
  #    from = "example@example.com"
  #
  #    auth {
  #      username = "tom"
  #      password = "password"
  #    }
  #  }

  internationalization {
    supported_locales = [
      "en",
      "de",
      "fr"
    ]
    default_locale = "en"
  }

  security_defenses {
    headers {
      x_frame_options                     = "DENY"
      content_security_policy             = "frame-src 'self'; frame-ancestors 'self'; object-src 'none';"
      content_security_policy_report_only = ""
      x_content_type_options              = "nosniff"
      x_robots_tag                        = "none"
      x_xss_protection                    = "1; mode=block"
      strict_transport_security           = "max-age=31536000; includeSubDomains"
    }
    brute_force_detection {
      permanent_lockout                = false
      max_login_failures               = 5
      wait_increment_seconds           = 60
      quick_login_check_milli_seconds  = 1000
      minimum_quick_login_wait_seconds = 60
      max_failure_wait_seconds         = 900
      failure_reset_time_seconds       = 43200
    }
  }
}

resource "keycloak_group" "kw_group" {
  realm_id = keycloak_realm.kw_realm.id
  name     = "kwetter-user"
}

resource "keycloak_user" "kw_user" {
  realm_id       = keycloak_realm.kw_realm.id
  username       = "test-user"
  enabled        = true
  email          = "test-user@kwetter.com"
  email_verified = true
  first_name     = "John"
  last_name      = "Smith"

  initial_password {
    value = "Password!"
  }
}

resource "keycloak_user_groups" "kw_user_groups" {
  realm_id = keycloak_realm.kw_realm.id
  user_id  = keycloak_user.kw_user.id

  group_ids = [
    keycloak_group.kw_group.id
  ]
}

resource "keycloak_openid_client_scope" "kw_groups" {
  realm_id               = keycloak_realm.kw_realm.id
  name                   = "kw-groups"
  include_in_token_scope = true
  gui_order              = 1
}

resource "keycloak_openid_group_membership_protocol_mapper" "kw_groups" {
  realm_id        = keycloak_realm.kw_realm.id
  client_scope_id = keycloak_openid_client_scope.kw_groups.id
  name            = "kw-groups"
  claim_name      = "groups"
  full_path       = false
}

resource "keycloak_openid_client" "kw_openid_client" {
  realm_id  = keycloak_realm.kw_realm.id
  client_id = "kwetter-client"

  name    = "kwetter client"
  enabled = true

  access_type = "PUBLIC"
  #  TODO: CORS
  login_theme = "keycloak"

  valid_redirect_uris = [
    "http://localhost:3000/", "http://localhost:3000/home", "http://localhost:3000/timeline",
    "https://kwetter.kind.cluster/", "https://kwetter.kind.cluster/home", "https://kwetter.kind.cluster/timeline"
  ]
  web_origins           = ["+"]
  standard_flow_enabled = true

  extra_config = {
    "key1" = "value1"
    "key2" = "value2"
  }
}

#resource "keycloak_openid_client_scope" "kw_client_scope" {
#  realm_id = keycloak_realm.kw_realm.id
#  name     = "kwetter-client-scope"
#  description            = "Default scope for Kwetter users"
#  include_in_token_scope = true
#  gui_order              = 1
#}
#

resource "keycloak_openid_client_default_scopes" "kw_client_default_scope" {
  client_id = keycloak_openid_client.kw_openid_client.id
  realm_id  = keycloak_realm.kw_realm.id

  default_scopes = [
    "profile",
    "email",
    "roles",
    "web-origins",
  ]
}

#resource "keycloak_role" "kw_client_role" {
#  realm_id    = keycloak_realm.kw_realm.id
#  client_id   = keycloak_openid_client.kw_openid_client.id
#  name        = "user"
#  description = "Kwetter User"
#  attributes = {
#    key = "value"
#  }
#}
#
#resource "keycloak_openid_user_client_role_protocol_mapper" "user_client_role_mapper" {
#  realm_id        = keycloak_realm.kw_realm.id
#  client_scope_id = keycloak_openid_client_scope.kw_groups.id
#  name            = "user-client-role-mapper"
#  claim_name      = "foo"
#}

# TODO: Figure out how to add the users email and groups to scope