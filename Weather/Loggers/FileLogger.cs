namespace Weather;

public class FileLogger:ILoggerr
{
    private readonly string _filePath = "logs.txt";
    public void Log(string message)
    {

            using (var writer = File.AppendText(_filePath))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }

    }
}