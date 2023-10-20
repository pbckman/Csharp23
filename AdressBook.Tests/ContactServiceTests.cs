using AdressBook.Models;
using AdressBook.Services;
using System.ComponentModel.DataAnnotations;

namespace AdressBook.Tests
{
    public class ContactServiceTests
    {
        [Fact]
        public void GetContactByEmail_ShouldReturnContactFromList_ReturnTrue()
        {
            // arrange
            ContactService contactService = new ContactService();
            string email = "test@domain.se";
            string filePath = @"C:\Skoluppgifter\Kontaktuppgifter\contacts.json";

            // act
            Contact result = contactService.GetContactByEmail(filePath, email);

            // assert
            Assert.Equal(email, result.Email);

        }
    }
}