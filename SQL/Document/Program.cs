await using var connection = new SqlConnection("Server=127.0.0.1;Database=DevOpsDocs;User Id=sa;Password=Øredev%!2024;TrustServerCertificate=true");

await connection.OpenAsync();

using var multi = await connection.QueryMultipleAsync(Queries.GetSchemaSQL);

var (
        databaseName,
        tables,
        columns,
        relations
    ) = (
        await multi.ReadFirstAsync<string>(),
        (await multi.ReadAsync<Table>()).ToDictionary(key=>key.TableFullName),
        (await multi.ReadAsync<Column>()).ToLookup(key=>key.TableFullName),
        (await multi.ReadAsync<Relation>()).ToArray()
    );

var targetFile = "index.md";
if (File.Exists(targetFile))
{
    File.Delete(targetFile);
}

await using var stream = File.OpenWrite(targetFile);

await using var sw = new StreamWriter(stream);

sw.WriteLine($"# {databaseName}");
sw.WriteLine();

sw.WriteLine(Table.MarkdownHeader);
    
foreach(var table in tables.Values)
{
    sw.WriteLine(table.Markdown);
}

sw.WriteLine();

foreach(var (tableFullName, _) in tables)
{
    sw.WriteLine($"## {tableFullName}");
    sw.WriteLine();

    sw.WriteLine("### Columns");
    sw.WriteLine();

    sw.WriteLine(Column.MarkdownHeader);
    foreach(var column in columns[tableFullName])
    {
        sw.WriteLine(column.Markdown);
    }
}

sw.WriteLine(
    $"""

    ## Relations
    
    ```mermaid
    flowchart TD
    subgraph {databaseName}
        {string.Join($"{Environment.NewLine}    ", tables.Keys)}
    end

    {string.Join(
        Environment.NewLine,
        (
            from relation in relations
            select $"{relation.ForeignTableFullName}--{relation.ForeignColumnName}→{relation.ColumnName}-->{relation.TableFullName}"
        )
    )}
    ```
    """
);
