﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HultonHotels.ViewModels;

namespace HultonHotels.Controllers
{
    public class MetricsController : Controller
    {
        // GET: Metrics
        public ActionResult Index()
        {
            var vm = new MetricsViewModel();
            vm.HandleRequest();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(MetricsViewModel vm)
        {
            vm.HandleRequest();
            return View(vm);
        }
    }
}