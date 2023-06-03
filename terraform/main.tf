terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.59.0"
    }
  }
}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
}

resource "azurerm_resource_group" "rg" {
  name     = "resource-group"
  location = "westeurope"
}

resource "azurerm_service_plan" "main_service_plan" {
  name                = "service-plan"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type             = "Linux"
  sku_name            = "P1v2"
}

resource "azurerm_linux_web_app" "main" {
  name                = "webapp-hometask-deployment-module"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id     = azurerm_service_plan.main_service_plan.id
  https_only          = true

  app_settings = {
    WEBSITE_RUN_FROM_PACKAGE       = 1
    SCM_DO_BUILD_DURING_DEPLOYMENT = true
  }
  
  site_config {
    minimum_tls_version = "1.2"

    application_stack {
      dotnet_version = "7.0"
    }
  }
}
