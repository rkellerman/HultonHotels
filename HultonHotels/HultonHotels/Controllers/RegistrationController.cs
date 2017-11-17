using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HultonHotels.ViewModels;

namespace HultonHotels.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            var vm = new RegistrationViewModel();
            vm.HandleRequest();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(RegistrationViewModel vm)
        {

            vm.HandleRequest();
            return View(vm);
        }
    }
}