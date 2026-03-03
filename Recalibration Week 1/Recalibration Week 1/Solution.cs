using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        WriteMessageToFile("Hello, is anybody there?");
        WriteMessageToFile("It was nice meeting you.");
        WriteMessageToFile("I hope to see you again soon.");
        ReadFileAndPrint();
    }

    private static int add(int a, int b)
    {
        return a + b;
    }

    static void WriteMessageToFile(string message)
    {
        string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/output.txt";
        Console.WriteLine($"Executing directory: {filePath}");
        string timeStampedMessage = $"{DateTime.Now}: {message}{Environment.NewLine}";
        try
        {
            File.AppendAllText(filePath, timeStampedMessage);
            Console.WriteLine("Message written to file successfully.");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"ArgumentNullException: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"ArgumentException: {ex.Message}");
        }
        catch (PathTooLongException ex)
        {
            Console.WriteLine($"PathTooLongException: {ex.Message}");
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"DirectoryNotFoundException: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"IOException: {ex.Message}");
        }
    }

    static void ReadFileAndPrint()
    {
        string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/output.txt";
        Console.WriteLine($"Executing directory: {filePath}");
        try
        {
            string content = File.ReadAllText(filePath);
            Console.WriteLine("File content:");
            Console.WriteLine(content);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"FileNotFoundException: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"UnauthorizedAccessException: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"IOException: {ex.Message}");
        }
    }
}