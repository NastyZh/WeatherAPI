namespace Weather;

public class ConsoleLogger:ILoggerr
{

    public void Log(string x)
    {
        Console.WriteLine($"{DateTime.Now}:{x}");
    }
}

