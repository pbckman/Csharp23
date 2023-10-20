using AdressBook.Interface;
using AdressBook.Models;

namespace AdressBook.Services;

public class MenuService : IMenuService
{
    private readonly IContactService _contactService = new ContactService(); // måste finnas med för att göra det möjligt att komma åt kontakter och metoder i ContactService classen
    public void MainMenu()    
    {
        var exit = false;
        do
        {
            Console.Clear();
            Console.WriteLine("     Huvudmeny   ");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("1. Lägg till kontakt");
            Console.WriteLine("2. Ta bort kontakt");
            Console.WriteLine("3. Uppdatera kontaktinfo");
            Console.WriteLine("4. Sortera kontakter");
            Console.WriteLine("5. Visa alla kontakter");
            Console.WriteLine("6. Visa detaljerad kontaktinfo");
            Console.WriteLine("7. Avsluta program");

            var option = Console.ReadLine();

            switch (option)     // olika metoder för valen från huvudmenyn för att ta sig vidare till nästa steg/alternativ i programmet
            {
                case "1":
                    CreateMenu();
                    break;

                case "2":
                    DeleteMenu();
                    break;

                case "3":
                    UpdateMenu();
                    break;

                case "4":
                    SortMenu();
                    break;

                case "5":
                    ListAllMenu();
                    break;

                case "6":
                    ShowDetailedMenu();
                    break;

                case "7":
                    exit = true;
                    break;

                default:
                    break;
            }

        }
        while (exit == false);
    }
    public void CreateMenu()   // skapar en ny användare, många Clears för att jag ville ha det rent och snyggt för användaren vid inmatning
    {
        Console.Clear();
        Console.WriteLine("Skapa en ny kontakt");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~");

        var contact = new CreateContactRequest();

        Console.WriteLine("Ange förnamn: ");
        contact.FirstName = Console.ReadLine()!.Trim();
        Console.Clear();

        Console.WriteLine("Skapa en ny kontakt");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Ange efternamn: ");
        contact.LastName = Console.ReadLine()!.Trim();
        Console.Clear();

        Console.WriteLine("Skapa en ny kontakt");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Ange e-mail: ");
        contact.Email = Console.ReadLine()!.Trim();
        Console.Clear();

        Console.WriteLine("Skapa en ny kontakt");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Ange telefonnummer: ");
        contact.PhoneNumber = Console.ReadLine()!.Trim();
        Console.Clear();

        Console.WriteLine("Skapa en ny kontakt");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Ange adress: ");
        contact.Adress = Console.ReadLine()!.Trim();
        Console.Clear();

        _contactService.CreateContact(contact);
        Console.WriteLine("En ny kontakt har lagts till.");
        Console.ReadKey();
    }

    public void ListAllMenu()   // visar alla kontakter i filen _contacts via metoden GetContacts från ContactService
    {
        Console.Clear();
        Console.WriteLine("Visa alla kontakter");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~");
        
        foreach (var contact in _contactService.GetContacts())    // skriver ut förnamn, efternamn och mail för varje kontakt i listan 
            Console.WriteLine($"{contact.FirstName} {contact.LastName} <{contact.Email}>");
        Console.ReadKey();
    }

    public void DeleteMenu()    // tar bort kontakt via angiven email
    {
        Console.Clear();
        Console.WriteLine("Ange email för kontakten du vill ta bort: ");
        string? email = Console.ReadLine();

        _contactService.DeleteContact(email!);
        Console.ReadKey();

       
    }

    public void UpdateMenu()   // möjlighet till att uppdatera info för kontakt genom att ange email
    {
        Console.Clear();
        Console.WriteLine("Ange email för kontakten du vill uppdatera: ");
        string? email = Console.ReadLine();

        _contactService.UpdateContact(email);
        Console.ReadKey();
    }

    public void SortMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Hur vill du sortera kontakter?");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("1. Sortera kontakter på förnamn");
            Console.WriteLine("2. Sortera kontakter på efternamn");
            Console.WriteLine("3. Sortera kontakter på email");
            Console.WriteLine("4. Sortera kontakter på adress");
            Console.WriteLine("5. Gå tillbaka till huvudmeny");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Kontakter sorterade efter förnamn");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    SortByFirstName(_contactService.GetContacts());
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Kontakter sorterade efter efternamn");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    SortByLastName(_contactService.GetContacts());
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("Kontakter sorterade efter email");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    SortByEmail(_contactService.GetContacts());
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine("Kontakter sorterade efter adress");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    SortByAdress(_contactService.GetContacts());
                    break;

                case "5":
                    MainMenu();
                    break;

                default:
                    Console.WriteLine("Ogiltigt val. Välj igen.");
                    break;
            }
        }
        

    }

    public void SortByFirstName(IEnumerable<Contact> contacts)   // metoder för att sortera kontakter på förnamn, efternamn, email, adress
    {
        var sortedContacts = contacts.OrderBy(x => x.FirstName).ToList();

        foreach (var contact in sortedContacts)
        {
            Console.WriteLine($"{contact.FirstName} {contact.LastName} <{contact.Email}>");
        }

        Console.ReadKey();
    }

    public void SortByLastName(IEnumerable<Contact> contacts)
    {
        var sortedContacts = contacts.OrderBy(x => x.LastName).ToList();

        foreach (var contact in sortedContacts)
        {
            Console.WriteLine($"{contact.FirstName} {contact.LastName} <{contact.Email}>");
        }

        Console.ReadKey();
    }

    public void SortByEmail(IEnumerable<Contact> contacts)
    {
        var sortedContacts = contacts.OrderBy(x => x.Email).ToList();

        foreach (var contact in sortedContacts)
        {
            Console.WriteLine($"{contact.FirstName} {contact.LastName} <{contact.Email}>");
        }

        Console.ReadKey();
    }

    public void SortByAdress(IEnumerable<Contact> contacts)
    {
        var sortedContacts = contacts.OrderBy(x => x.Adress).ToList();

        foreach (var contact in sortedContacts)
        {
            Console.WriteLine($"{contact.Adress}, {contact.FirstName} {contact.LastName} <{contact.Email}>");
        }

        Console.ReadKey();
    }

    public void ShowDetailedMenu()              // metod för detaljerad vy av kontakt
    {
        Console.Clear();
        Console.WriteLine("Ange email för kontakten du vill se detaljerad info om: ");
        string? email = Console.ReadLine();
        string filePath = @"C:\Skoluppgifter\Kontaktuppgifter\contacts.json";

        Contact contact = _contactService.GetContactByEmail(filePath, email!);

        if (contact != null)
        {
            Console.WriteLine($"Förnamn: {contact.FirstName}, Efternamn:{contact.LastName}, Email:{contact.Email}, Telefonnummer:{contact.PhoneNumber}, Adress:{contact.Adress}");
        }
        else
        {
            Console.WriteLine("Ingen kontakt hittades med den angivna emailen.");
        }
        Console.ReadKey();
    }
}
