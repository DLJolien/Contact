using Contact.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Contact.Database
{
  
        public interface IContactDatabase
        {
        ContactPerson Insert(ContactPerson contact);
            IEnumerable<ContactPerson> GetContacts();
        ContactPerson GetContact(int id);
            void Delete(int id);
            void Update(int id, ContactPerson contact);
        }

    public class ContactDatabase : IContactDatabase
    {
        private int _counter;
        private readonly List<ContactPerson> _contacts;

        public ContactDatabase()
        {
            if (_contacts == null)
            {
                _contacts = new List<ContactPerson>();
            }
        }

        public ContactPerson GetContact(int id)
        {
            return _contacts.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ContactPerson> GetContacts()
        {
            return _contacts;
        }

        public ContactPerson Insert(ContactPerson contact)
        {
            _counter++;
            contact.Id = _counter;
            _contacts.Add(contact);
            return contact;
        }

        public void Delete(int id)
        {
            var contact = _contacts.SingleOrDefault(x => x.Id == id);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }

        public void Update(int id, ContactPerson updatedContact)
        {
            var contact = _contacts.SingleOrDefault(x => x.Id == id);
            if (contact != null)
            {
                contact.FirstName = updatedContact.FirstName;
                contact.SecondName = updatedContact.SecondName;
                contact.Description = updatedContact.Description;
                contact.Address = updatedContact.Address;
                contact.PhoneNumber = updatedContact.PhoneNumber;
                contact.Id = updatedContact.Id;
                contact.Email = updatedContact.Email;
            }
        }
    }
}
