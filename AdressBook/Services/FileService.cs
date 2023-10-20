using AdressBook.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AdressBook.Services;

internal class FileService
{
    private static readonly string filePath = @"C:\Skoluppgifter\Kontaktuppgifter\contacts.json";   // sökvägen till filen där kontakterna sparas

    public static void SaveToFile(string contentAsJson)    // metod för att spara innehållet i JSON format
    {
        try
        {
            using var sw = new StreamWriter(filePath);
            sw.WriteLine(contentAsJson);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }


    public static string ReadFromFile()     // metod för att läsa innehållet från filen
    {
        try
        {
            if (File.Exists(filePath))
            {
                using var sr = new StreamReader(filePath);
                return sr.ReadToEnd();
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;


    } 
}

