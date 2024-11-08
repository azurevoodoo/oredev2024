namespace Document.Models;

public record Relation(
    string ForeignTableFullName,
    string ForeignColumnName,
    string SchemaName,
    string TableFullName,
    string ColumnName
);