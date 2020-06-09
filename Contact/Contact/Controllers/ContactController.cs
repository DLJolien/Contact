using Contact.Database;
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
            return View();
        }
    }
}
