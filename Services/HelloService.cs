public class HelloService : IHelloService
{
    public string GetHelloWorld()
    {
        return "Hello World!";
    }
}

public interface IHelloService
{
    string GetHelloWorld();
}