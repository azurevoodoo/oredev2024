# Include common variables
. .\Variables.ps1

function Invoke-SqlQuery {
    param (
        [string] $Query,
        [hashtable] $Variables = @{}
    )
    Invoke-Sqlcmd `
        -Server $Server `
        -Database $Database `
        -Username $User `
        -Password $Password `
        -TrustServerCertificate `
        -OutputAs DataRows `
        -Variable $Variables `
        -Query $Query
}

# Create the index.md file

'# DevOpsDocs

## Tables
' | Out-File -FilePath index.md

# Get the tables and their columns

Invoke-SqlQuery `
  -Query 'SELECT    object_id AS TableId,
                    SCHEMA_NAME(schema_id) AS SchemaName,
                    Name AS TableName
            FROM sys.tables'  `
            | ForEach-Object {
                "### $($_.SchemaName).$($_.TableName)"
                ''
                '#### Columns'
                ''
                '| Name |'
                '|------|'

                Invoke-SqlQuery `
                    -Variables  @{
                        'TableId' = $_.TableId
                        } `
                    -Query 'SELECT Name
                                FROM sys.columns
                                WHERE object_id = $(TableId)'
                    | ForEach-Object {
                        "| $($_.Name) |" 
                     }
            } | Out-File -FilePath index.md -Append