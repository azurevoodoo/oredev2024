namespace Document.Helpers;

public static class Queries
{
    public const string GetSchemaSQL = 
                        $"""
                        DECLARE @True   bit = 1,
                                @False  bit = 0

                        DECLARE @Table TABLE(
                            TableId        int,
                            SchemaName     sysname,
                            TableName      sysname,
                            TableFullName  nvarchar(MAX)
                        )

                        DECLARE @Column TABLE(
                            TableId            int,
                            TableFullName      nvarchar(MAX),
                            ColumnId           int,
                            ColumnName         sysname,
                            TypeName           nvarchar(MAX),
                            IsIdentity         bit,
                            IsPrimaryKey       bit,
                            IsComputed         bit,
                            ComputedDefinition nvarchar(MAX),
                            DefaultValue       nvarchar(MAX)
                        )

                        SELECT DB_NAME() AS DatabaseName

                        INSERT INTO @Table
                        OUTPUT  inserted.SchemaName,
                                inserted.TableName,
                                inserted.TableFullName
                        SELECT  t.object_id                 AS TableId,
                                s.name                      AS SchemaName,
                                t.name                      AS TableName,
                                s.name + '.' + t.name       AS TableFullName
                            FROM sys.tables t
                                INNER JOIN sys.schemas s on s.schema_id = t.schema_id

                        INSERT INTO @Column
                        OUTPUT  inserted.TableFullName,
                                inserted.ColumnName,
                                inserted.TypeName,
                                inserted.IsIdentity,
                                inserted.IsPrimaryKey,
                                inserted.IsComputed,
                                inserted.ComputedDefinition,
                                inserted.DefaultValue


                        SELECT  c.object_id                             AS TableId,
                                t.TableFullName                         AS TableFullName,
                                c.column_id                             AS ColumnId,
                                c.name                                  AS ColumnName,
                                tn.TypeName                             AS TypeName,
                                c.is_identity                           AS IsIdentity,
                                pk.is_primary_key                       AS IsPrimaryKey,
                                c.is_computed                           AS IsComputed,
                                cc.definition                           AS ComputedDefinition,
                                object_definition(c.default_object_id)  AS DefaultValue
                            FROM @Table t 
                                INNER JOIN sys.columns c on c.object_id = t.TableId
                                CROSS APPLY (
                                    SELECT  TypeName
                                            +
                                            CASE
                                                WHEN tn.TypeName IN ('varchar', 'nvarchar', 'varbinary', 'sql_variant')
                                                    THEN    '(' 
                                                            +
                                                            CASE c.max_length
                                                                WHEN -1 THEN 'MAX'
                                                                ELSE
                                                                    CAST(
                                                                        c.max_length
                                                                        /
                                                                        CASE tn.TypeName
                                                                            WHEN 'nvarchar'
                                                                                THEN 2
                                                                            ELSE 1
                                                                        END
                                                                        AS nvarchar(max)
                                                                    )
                                                            END
                                                            +
                                                            ')'
                                                WHEN tn.TypeName  IN ('decimal', 'numeric')
                                                    THEN    '('
                                                            +
                                                            CAST(c.precision AS nvarchar(MAX))
                                                            +
                                                            ', '
                                                            +
                                                            CAST(c.scale AS nvarchar(MAX))
                                                            +
                                                            ')'
                                                ELSE ''
                                            END
                                            AS TypeName
                                        FROM (
                                            SELECT TYPE_NAME(user_type_id) AS TypeName
                                        ) tn
                                ) tn
                                CROSS APPLY (
                                    SELECT CASE
                                                WHEN EXISTS(SELECT 1
                                                                FROM sys.index_columns ic
                                                                    INNER JOIN sys.indexes i ON i.index_id = ic.index_id
                                                                WHERE   ic.object_id = t.TableId
                                                                        AND
                                                                        ic.column_id = c.column_id
                                                                        AND
                                                                        i.is_primary_key = @True)
                                                    THEN @True
                                                ELSE @False
                                            END AS is_primary_key
                                ) pk
                                CROSS APPLY(
                                    SELECT CASE c.is_computed
                                        WHEN @True
                                            THEN (
                                                    SELECT cc.definition
                                                        FROM sys.computed_columns cc
                                                        WHERE   cc.object_id = t.TableId AND
                                                                cc.column_id = c.column_id
                                                )
                                            ELSE ''
                                        END AS definition
                                ) cc


                        SELECT ct.TableFullName AS ForeignTableFullName,
                            cc.ColumnName    AS ForeignColumnName,
                            pt.SchemaName    AS SchemaName,
                            pt.TableFullName AS TableFullName,
                            cp.ColumnName    AS ColumnName
                            
                            FROM @Table pt
                                INNER JOIN sys.foreign_keys fk ON pt.TableId = fk.parent_object_id
                                INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                                INNER JOIN @Column cp ON    fkc.parent_column_id = cp.ColumnId
                                                            AND
                                                            pt.TableId = cp.TableId
                                INNER JOIN @Table ct ON     fk.referenced_object_id = ct.TableId
                                INNER JOIN @Column cc ON    cc.TableId = ct.TableId AND
                                                            cc.ColumnId = fkc.referenced_column_id
                        """;
}
