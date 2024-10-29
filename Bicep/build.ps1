if (!(Test-Path -Path ./BicepDocs/input)) {
    New-Item -Path ./BicepDocs/input -ItemType Directory | Out-Null
}

'dotnet bri inventory $ENV:AZURE_CONTAINER_REGISTRY ./BicepDocs/input'
dotnet bri inventory $ENV:AZURE_CONTAINER_REGISTRY ./BicepDocs/input