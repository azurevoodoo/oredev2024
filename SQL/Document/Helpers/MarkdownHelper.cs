
namespace Document.Helpers;

public static class MarkdownHelper
{
    public static string ToYesNo(this bool value)
        => value ? "Yes" : "No";
    public static string GetColumns(params string[] values)
        => GetColumns(false, values);
    public static string  GetColumns(bool header, params string[] values)
        => values.Aggregate(
                new StringBuilder(),
                (sb, value) => sb.Append($"| {value, -22} "),
                sb => sb.Append('|')
                        .Append(
                            header
                            ? values.Aggregate(
                                new StringBuilder()
                                    .AppendLine(),
                                (sbh, _) => sbh
                                                .Append('|')
                                                .Append("".PadRight(24, '-')),
                                sbh => sbh.Append('|')
                            )
                            : string.Empty
                        )
                        .ToString()
            );
}
