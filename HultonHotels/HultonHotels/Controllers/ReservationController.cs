using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HultonHotels.ViewModels;

namespace HultonHotels.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index()
        {

            var vm = new ReservationViewModel();
            vm.HandleRequest();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(ReservationViewModel vm)
        {
            vm.HandleRequest();
            return View(vm);
        }
    }
}