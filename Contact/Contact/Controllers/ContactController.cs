using Contact.Database;
using Contact.Domain;
using Contact.Models;
using ContactWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactWeb
{
    public class ContactController : Controller
    {
        private readonly IContactDatabase _contactDatabase;
        public ContactController(IContactDatabase contactDatabase)
        {
            _contactDatabase = contactDatabase;
        }
        public IActionResult Index()
        {
            IEnumerable<ContactPerson> contacts = _contactDatabase.GetContacts();
            List<ContactListViewModel> vmList = new List<ContactListViewModel>();
            foreach (ContactPerson contact in contacts)
            {
                vmList.Add(new ContactListViewModel(){
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    SecondName = contact.SecondName,
                });
            }
            return View(vmList);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            ContactPerson contactToDisplay = _contactDatabase.GetContact(id);
            ContactDetailViewModel vm = new ContactDetailViewModel()
            {
                FirstName = contactToDisplay.FirstName,
                SecondName = contactToDisplay.SecondName,
                Birthdate = contactToDisplay.Birthdate,
                Description = contactToDisplay.Description,
                PhoneNumber = contactToDisplay.PhoneNumber,
                Address = contactToDisplay.Address,
                Email = contactToDisplay.Email
            };
            return View(vm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(ContactCreateViewModel vm)
        {
            if (!TryValidateModel(vm))
            {
                return View(vm);
            }
            else
            {
                ContactPerson newContact = new ContactPerson()
                {
                    FirstName = vm.FirstName,
                    SecondName = vm.SecondName,
                    Birthdate = vm.Birthdate,
                    Address = vm.Address,
                    PhoneNumber = vm.PhoneNumber,
                    Email = vm.Email,
                    Description = vm.Description
                };
                _contactDatabase.Insert(newContact);
                return RedirectToAction("Index");
            }
            
        }
    }
}
