using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HultonHotels.Models;
using HultonHotels.ViewModels.Objects;

namespace HultonHotels.ViewModels
{
    public class ReservationViewModel
    {
        public List<ReservationObject> Items { get; set; }
        public ReservationObject SearchEntity { get; set; }
        public ReservationObject Entity { get; set; }
        public ReservationObject PrevEntity { get; set; }
        public string EventCommand { get; set; }
        public string EventArgument { get; set; }
        public string PrevEventArgument { get; set; }

        public bool IsValid { get; set; }
        public string Mode { get; set; }

        public bool IsDetailAreaVisible { get; set; }
        public bool IsSearchAreaVisible { get; set; }
        public bool IsListAreaVisible { get; set; }
        public bool IsPopUpAreaVisible { get; set; }

        public ReservationViewModel()
        {
            Init();

            SearchEntity = new ReservationObject();
            Items = new List<ReservationObject>();
            Entity = new ReservationObject();
            PrevEntity = new ReservationObject();
            EventCommand = "list";
        }

        public void HandleRequest()
        {
            switch (EventCommand.ToLower())
            {
                case "list":
                case "search":
                    Get();
                    break;
                default:
                    break;

            }
        }

        private void Get()
        {

            var mgr = new ReservationManager();
            Items = mgr.Get(SearchEntity);

        }

        private void Init()
        {
            EventCommand = "list";
            EventArgument = string.Empty;
            ListMode();

        }

        private void ListMode()
        {
            IsValid = true;

            IsListAreaVisible = true;
            IsSearchAreaVisible = true;
            IsDetailAreaVisible = false;
            IsPopUpAreaVisible = false;

            Mode = "List";
        }
    }
}