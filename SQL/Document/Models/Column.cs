namespace Document.Models;

public record Column(
    string TableFullName,
    string ColumnName,
    string TypeName,
    bool IsIdentity,
    bool IsPrimaryKey,
    bool IsComputed,
    string ComputedDefinition,
    string DefaultValue
)
{
    public static string MarkdownHeader { get; } = GetColumns(
        header: true,
        "Name",
        "Type",
        "Primary Key",
        "Default value",
        "Computed"
    );

    public string Markdown { get; } = GetColumns(
        ColumnName,
        TypeName,
        IsPrimaryKey.ToYesNo(),
        DefaultValue,
        string.Join(
            ' ',
            IsComputed.ToYesNo(),
            ComputedDefinition
        )
    );
}
