using AdressBook.Interface;
using AdressBook.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AdressBook.Services;

public class ContactService : IContactService
{
    public List<Contact> _contacts = new List<Contact>();   // lista för att lagra kontakter

    public void CreateContact(CreateContactRequest request)   // metod för att skapa en ny kontakt
    {
        _contacts.Add(new Contact        // skapar en ny kontakt och lägger till den i listan
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Adress = request.Adress,
        });

        FileService.SaveToFile(JsonConvert.SerializeObject(_contacts));     // gör listan till JSON-format och sparar den i en fil
            
    }

    public void GetContact()   // metod för att hämta kontakter från fil
    {
        var content = FileService.ReadFromFile();
        if (!string.IsNullOrEmpty(content))
            _contacts = JsonConvert.DeserializeObject<List<Contact>>(content)!;
    }

    public void DeleteContact (string email)  // metod för att ta bort en kontakt genom att ange email
    {
        string filePath = @"C:\Skoluppgifter\Kontaktuppgifter\contacts.json";
        var contactToRemove = GetContactByEmail(filePath, email);     // hämtar kontakten som ska tas bort

        if (contactToRemove != null)
        {
            _contacts.Remove(contactToRemove);                     // tar bort kontakten från listan
            Console.WriteLine("Kontakten har tagits bort.");

            FileService.SaveToFile(JsonConvert.SerializeObject(_contacts));  //uppdaterar filen efter borttagning av kontakt och sparar den till filen
        }
        else
        {
            Console.WriteLine("Hittade ingen kontakt med angiven email.");
        }
    }

    public IEnumerable<Contact> GetContacts()  // metod för att hämta alla kontakter från _contacts 
    {
        var content = FileService.ReadFromFile();
        return _contacts = JsonConvert.DeserializeObject<List<Contact>>(content)!;
    }



    public Contact GetContactByEmail(string filePath, string email)
    { 
        try
        {
            if (File.Exists(filePath))
            {
                using var sr = new StreamReader(filePath);
                string jsonContent = sr.ReadToEnd();
                List<Contact> contacts = JsonConvert.DeserializeObject<List<Contact>>(jsonContent);

                return contacts.FirstOrDefault(c => c.Email == email);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return null;
        //var contactsFromFile = FileService.ReadFromFile();  // tillägg för test
        //return contactsFromFile.FirstOrDefault(contact => contact.Email == email);         // metod för att hämta en kontakt från filen genom att ange email
    }

    public void UpdateContact (string? email)      // metod för att uppdatera information för en kontakt
    {
        GetContact();

        var contactToUpdate = _contacts.FirstOrDefault(contact => contact.Email == email);       // genom email så hämtas den kontakt som ska uppdateras

        if (contactToUpdate != null)
        {
            Console.WriteLine($"Förnamn: {contactToUpdate.FirstName}");    // visar befintliga kontaktuppgifter
            Console.WriteLine($"Efternamn: {contactToUpdate.LastName}");
            Console.WriteLine($"E-postadress: {contactToUpdate.Email}");
            Console.WriteLine($"Telefonnummer: {contactToUpdate.PhoneNumber}");
            Console.WriteLine($"Adress: {contactToUpdate.Adress}");

            Console.WriteLine("Ange nytt förnamn (tryck Enter om ingen ändring önskas): ");   
            string? newFirstName = Console.ReadLine();    // tar input från användaren och uppdaterar FirstName om nytt namn skrivs in och hoppar vidare till nästa fråga om man trycker på enter istället
            if (!string.IsNullOrEmpty(newFirstName))
            {
                contactToUpdate.FirstName = newFirstName;
            }

            Console.WriteLine("Ange nytt efternamn (tryck Enter om ingen ändring önskas): ");
            string? newLastName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newLastName))
            {
                contactToUpdate.LastName = newLastName;
            }

            Console.WriteLine("Ange ny e-postadress (tryck Enter om ingen ändring önskas): ");
            string? newEmail = Console.ReadLine();
            if (!string.IsNullOrEmpty(newEmail))
            {
                contactToUpdate.Email = newEmail;
            }

            Console.WriteLine("Ange nytt telefonnummer (tryck Enter om ingen ändring önskas): ");
            string? newPhoneNumber = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPhoneNumber))
            {
                contactToUpdate.PhoneNumber = newPhoneNumber;
            }

            Console.WriteLine("Ange ny adress (tryck Enter om ingen ändring önskas): ");
            string? newAddress = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAddress))
            {
                contactToUpdate.Adress = newAddress;
            }

            FileService.SaveToFile(JsonConvert.SerializeObject(_contacts));          // sparar uppdatreingar till filen _contacts
            Console.WriteLine("Kontaktuppgifter uppdaterade.");

        }
        else
        {
            Console.WriteLine("Ingen kontakt hittades med den angivna emailen.");
        }

        Console.ReadKey();

    }


}
