namespace Sample.Core;

public record Greeter(
    ILog Log
)
{
    public void Hello(string name = "world")
        => Log.WriteLine($"Hello {name}!");
}
