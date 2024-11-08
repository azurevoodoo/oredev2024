namespace Document.Models;
public record Table(
    string SchemaName,
    string TableName,
    string TableFullName
)
{
    public static string MarkdownHeader { get; } = GetColumns(
        header: true,
        "SchemaName",
        "TableName"
    );

    public string Markdown { get; } = GetColumns(
        SchemaName,
        TableName
    );
}