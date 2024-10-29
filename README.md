# DevOpsDocs -  Øredev 2024

Sample code from DevOpsDocs Transforming Tediousness into Joy presented at Øredev 2024

## Prerequisites

1. .NET 8s SDK
1. Run `dotnet tool restore` to have all tools used available
1. Run `git submodule init` and `git submodule update` to ensure Static Docable theme available.

Some of the sample PowerShell scripts use environment variables for storing Azure tenant id and subscription id.

```PowerShell
$ENV:AZURE_TENANT_ID='{your tenant}'
$ENV:AZURE_CONTAINER_REGISTRY='{your container registry}'
```

## Azure

Example creating a static documentation website created by inventorying an Azure tenant for resources using the [ari](https://www.nuget.org/packages/ari) .NET tool and [Statiq Web](https://www.statiq.dev/web) web framework.

## Bicep

Example creating a static documentation website by inventorying a container registry for Bicep modules using the [bri](https://www.nuget.org/packages/bri) .NET tool and [Statiq Web](https://www.statiq.dev/web) framework.

## DotNet

Example creating a static documentation website by scanning C# source code using the [Statiq Docs](https://www.statiq.dev/docs) framework.
Also an example of adding .NET project dependency information to your documentation using the [dpi](https://www.nuget.org/packages/dpi) .NET tool.

## SQL

Proof of concept axample creating Markdown document containing a database tables and columns. Using [SQL Server PowerShell module](https://learn.microsoft.com/en-us/powershell/module/sqlserver/?view=sqlserver-ps) (`Install-Module -Name SqlServer`) [Invoke-Sqlcmd](https://learn.microsoft.com/en-us/powershell/module/sqlserver/invoke-sqlcmd?view=sqlserver-ps) cmdlet.