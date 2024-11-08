# Include common variables
. .\Variables.ps1

# Build the project
dotnet build ./DevOpsDocs

# Publish the database
[string] $BinPath = Resolve-Path ./DevOpsDocs/bin/Debug
[string] $DacPacPath = Join-Path $BinPath 'DevOpsDocs.dacpac'
dotnet sqlpackage `
    /Action:Publish `
    -p:ScriptDatabaseCompatibility=true `
    -p:BlockOnPossibleDataLoss=false `
    /TargetConnectionString:"Server=$Server;Database=$Database;User Id=$User;Password=$Password;TrustServerCertificate=true" `
    /ReferencePaths:Master="$BinPath" `
    /SourceFile:$DacPacPath
 