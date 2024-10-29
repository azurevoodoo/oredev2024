using Markdig.Extensions.AutoIdentifiers;

namespace TestWeb;

public class DefaultAutoIdentifierExtension : AutoIdentifierExtension
{
    public DefaultAutoIdentifierExtension()
        : base(AutoIdentifierOptions.Default)
    {

    }
}
