using System;
using System.IO;

namespace Artwork_Database
{
    public class FileReader
    {
        public static string[] ReadFile(string filepath)
        {       //  Catches added as per research into exceptions 
            try
            {
                return File.ReadAllLines(filepath);
            } catch (FileNotFoundException)
            {
                Console.WriteLine("The specified file was not found");
                throw;
            } catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to access this file");
                throw;
            } catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                throw;
            }
        }


    }

}
