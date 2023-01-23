using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactAPIServices.Models;

namespace ContactAPIServices.Interfaces
{
    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
        void AddContact(Contact contact);
        Contact GetContact(int id);
        void UpdateContact(Contact contact);
        void DeleteContact(int id);
        bool ContactExists(int id);
    }
}