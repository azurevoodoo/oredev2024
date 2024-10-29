if(!(Test-Path './AzureDocs/input'))
{
    New-Item -ItemType Directory -Path './AzureDocs/input' | Out-Null
}

'dotnet ari inventory $env:AZURE_TENANT_ID ./AzureDocs/input --skip-tenant-overview'
dotnet ari inventory $env:AZURE_TENANT_ID ./AzureDocs/input --skip-tenant-overview