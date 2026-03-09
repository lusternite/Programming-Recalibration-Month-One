using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        // Initialize execution loop variables
        string userInput = "";
        // Continue executing until user types "exit"
        while (true)
        {
            // Prompt user for input
            Console.Write("Enter a directory path to scan (type 'exit' to quit): ");
            // Read a line from the console and scan directory if not "exit"
            userInput = Console.ReadLine();
            // Check if user wants to exit the program, case-insensitive comparison
            if (String.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase) == true)
            {
                // User wants to exit the program, set flag to false to break the loop
                Console.WriteLine("Exiting program.");
                break;
            }
            else
            {
                // User input is not "exit", attempt to scan the specified directory for .txt files, handling potential exceptions that may occur during directory access
                try
                {
                    var files = Directory
                        .GetFiles(userInput, "*.txt")
                        .Select(f => new FileInfo(f))
                        .OrderByDescending(f => f.Length);
                    // Check if any .txt files were found in the specified directory
                    if (files.Any())
                    {
                        // .txt files found, print their names, sizes, and last modified dates to the console sorted by size
                        Console.WriteLine("Scanned .txt files ordered by size:");
                        foreach (FileInfo file in files)
                        {
                            Console.WriteLine($"{file.Name} - {file.Length} bytes - Modified: {file.LastWriteTime}");
                        }
                        // Prompt user to export the report
                        Console.WriteLine("Would you like to export the report? (y/n)");
                        userInput = Console.ReadLine();
                        if (String.Equals(userInput, "y", StringComparison.OrdinalIgnoreCase) == true)
                        {
                            // User wants to export the report, call the method to export the file report to a CSV file
                            ExportFileReport(files);
                        } else
                        {
                            Console.WriteLine("Input other than y was entered, skipping export.");
                        }
                    } else
                    {
                        // No .txt files found in the specified directory, inform the user
                        Console.WriteLine("No .txt files found in the specified directory.");
                    }
                }
                // Handle potential exceptions that may occur during directory access, such as unauthorized access or directory not found
                catch (Exception ex) 
                {
                    Console.WriteLine($"An error occurred while accessing the directory: {ex.Message}");
                }
            }
        }
    }

    static string GetOutputFilePath()
    {
        // Get the directory of the executing assembly and return it
        return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
    }

    static string GetOutputFilePath(string outputFileName)
    {
        // Get the directory of the executing assembly and combine it with "output.txt" to get the full file path
        string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        Console.WriteLine($"Executing directory: {filePath}");
        return Path.Combine(filePath, outputFileName);
    }

    static void ExportFileReport(IOrderedEnumerable<FileInfo> files)
    {
        // Get the output file path
        string filePath = GetOutputFilePath("report.csv");
        try
        {
            // Check if the report.csv file already exists in the output directory, and if not, create it and write the header line
            if (Directory.GetFiles(GetOutputFilePath(), "report.csv").Length == 0)
            {
                File.AppendAllText(filePath, $"FileName,SizeBytes,LastModified");
            }
            // Append a header line to the report.csv file indicating the directory that was scanned for .txt files
            File.AppendAllText(filePath, $"{Environment.NewLine}Report of .txt files in directory: {files.First().Directory.FullName}"); 
            // Append the file information for each .txt file to the report.csv file
            foreach (var file in files)
            {
                File.AppendAllText(filePath, $"{Environment.NewLine}{file.Name},{file.Length},{file.LastWriteTime}");
            }
            Console.WriteLine("Report exported to file report.csv successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while writing the report to the file: " + ex.Message);
        }
    }

    static void WriteMessageToFile(string message)
    {
        // Get the output file path and create a time-stamped message to write to the file
        string filePath = GetOutputFilePath("output.txt");
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

    static void ReadFileAndPrint(string fileName)
    {
        // Get the output file path and attempt to read its content, handling potential exceptions that may occur during file operations
        string filePath = GetOutputFilePath(fileName);
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