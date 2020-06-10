using Contact.Database;
using Contact.Domain;
using Contact.Models;
using ContactWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
                Email = contactToDisplay.Email,
                Avatar = contactToDisplay.Avatar
            };
            return View(vm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(ContactCreateViewModel vm)
        {
            byte[] file;

            if (vm.Avatar != null)
            {
                file = GetBytesFromFile(vm.Avatar);
            }
            else
            {
                file = new byte[] { };
            }

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
                    Description = vm.Description,
                    Avatar = file
                };


                _contactDatabase.Insert(newContact);
                return RedirectToAction("Index");
            }
            
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ContactPerson contactToUpdate = _contactDatabase.GetContact(id);
            ContactEditViewModel vm = new ContactEditViewModel()
            {
                FirstName = contactToUpdate.FirstName,
                SecondName = contactToUpdate.SecondName,
                Birthdate = contactToUpdate.Birthdate,
                Description = contactToUpdate.Description,
                PhoneNumber = contactToUpdate.PhoneNumber,
                Address = contactToUpdate.Address,
                Email = contactToUpdate.Email
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Update(ContactEditViewModel vm)
        {
            ContactPerson updatedPerson = new ContactPerson()
            {
                Id = vm.Id,
                FirstName = vm.FirstName,
                SecondName = vm.SecondName,
                Birthdate = vm.Birthdate,
                Address = vm.Address,
                PhoneNumber = vm.PhoneNumber,
                Email = vm.Email,
                Description = vm.Description
            };
            _contactDatabase.Update(updatedPerson.Id, updatedPerson);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ContactPerson contactToDelete = _contactDatabase.GetContact(id);
            ContactDeleteViewModel vm = new ContactDeleteViewModel()
            {
                Id = contactToDelete.Id,
                FirstName = contactToDelete.FirstName,
                SecondName = contactToDelete.SecondName,              
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _contactDatabase.Delete(id);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public Byte[] GetBytesFromFile(IFormFile file)
        {
            var extension = new FileInfo(file.FileName).Extension;
            if (extension == ".jpg" || extension == ".png" || extension == ".PNG")
            {
                using var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
            else
            {
                return new byte[] { };
            }
        }
        
    }
}
