using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        // Initialize execution loop variables
        string userInput = "";
        bool executionLoopFlag = true;
        // Continue executing until user types "exit"
        while (executionLoopFlag == true)
        {
            // Prompt user for input
            Console.Write("Enter a message to write to file (type 'exit' to quit): ");
            // Read a line from the console and write to file if not "exit"
            userInput = Console.ReadLine();
            // Check if user wants to exit the program, case-insensitive comparison
            if (String.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase) == true)
            {
                // User wants to exit the program, set flag to false to break the loop
                Console.WriteLine("Exiting program.");
                executionLoopFlag = false;
            }
            else
            {
                // User input is not "exit", write the message to file and read it back to print
                WriteMessageToFile(userInput);
                ReadFileAndPrint();
            }
        }
    }

    static string GetOutputFilePath()
    {
        // Get the directory of the executing assembly and combine it with "output.txt" to get the full file path
        string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        Console.WriteLine($"Executing directory: {filePath}");
        return Path.Combine(filePath, "output.txt");
    }

    static void WriteMessageToFile(string message)
    {
        // Get the output file path and create a time-stamped message to write to the file
        string filePath = GetOutputFilePath();
        string timeStampedMessage = $"{DateTime.Now}: {message}{Environment.NewLine}";
        // Attempt to append the time-stamped message to the file, handling potential exceptions that may occur during file operations
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
        // Get the output file path and attempt to read its content, handling potential exceptions that may occur during file operations
        string filePath = GetOutputFilePath();
        try
        {
            // Read the content of the file and print it to the console
            string content = File.ReadAllText(filePath);
            Console.WriteLine("Current file content:");
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