# Include common variables
. .\Variables.ps1

# Start the SQL Server container image
[string] $Tool = (Get-Command docker,podman -ErrorAction Ignore).Source
&$Tool pull mcr.microsoft.com/mssql/server:2022-latest
&$Tool run `
   -it --rm `
   -e "ACCEPT_EULA=Y"`
   -e "MSSQL_SA_PASSWORD=$Password" `
   -e "MSSQL_COLLATION=Finnish_Swedish_CI_AS" `
   -e "MSSQL_PID=Express" `
   -p 1433:1433 `
   --name $Server `
   --hostname $Server `
   mcr.microsoft.com/mssql/server:2022-latest
