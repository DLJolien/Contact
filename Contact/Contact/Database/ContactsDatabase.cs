using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Contact
{
    
        public interface IContactDatabase
        {
        Contact Insert(Contact contact);
            IEnumerable<Contact> GetMovies();
        Contact GetContact(int id);
            void Delete(int id);
            void Update(int id, Contact movie);
        }

    public class MovieDatabase : IContactDatabase
    {
        private int _counter;
        private readonly List<Contact> _contacts;

        public MovieDatabase()
        {
            if (_contacts == null)
            {
                _contacts = new List<Contact>();
            }
        }

        public Contact GetContact(int id)
        {
            return _contacts.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Contact> GetMovies()
        {
            return _contacts;
        }

        public Contact Insert(Contact contact)
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

        public void Update(int id, Contact updatedContact)
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
