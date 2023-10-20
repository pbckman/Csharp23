using AdressBook.Models;
using System.Reflection.Metadata;

namespace AdressBook.Interface;

public interface IContactService
{
    public void CreateContact(CreateContactRequest createContact);

    Contact GetContactByEmail(string filePath, string email);

    public void DeleteContact(string email);


    public void UpdateContact(string? email);

    public IEnumerable<Contact> GetContacts();

    

    

}
