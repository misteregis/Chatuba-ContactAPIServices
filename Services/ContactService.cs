using ContactAPIServices.Interfaces;
using ContactAPIServices.Models;

namespace ContactAPIServices.Services
{
    public class ContactService : IContactService
    {
        private readonly List<Contact> _contacts = new()
        {
            new Contact
            {
                Id = 1,
                Name = "John",
                Email = "john.smith@example.com"
            },
            new Contact
            {
                Id = 2,
                Name = "Carlos",
                Email = "carlos@gmail.com"
            }
        };

        public IEnumerable<Contact> GetContacts() => _contacts.ToList();

        public void AddContact(Contact contact)
        {
            _contacts.Add(contact);
        }

        public Contact GetContact(int id)
        {
            return _contacts.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateContact(Contact contact)
        {
            Contact model = GetContact(contact.Id);

            model.Name = contact.Name;
            model.Email = contact.Email;
        }

        public void DeleteContact(int id)
        {
            _contacts.Remove(GetContact(id));
        }

        public bool ContactExists(int id) => _contacts.Any(c => c.Id == id);
    }
}