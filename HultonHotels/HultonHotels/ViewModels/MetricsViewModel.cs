using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HultonHotels.Models;

namespace HultonHotels.ViewModels
{
    public class MetricsViewModel
    {
        public MetricsObject SearchEntity { get; set; }
        public MetricsObject Entity { get; set; }
        public MetricsObject PrevEntity { get; set; }
        public string EventCommand { get; set; }
        public string EventArgument { get; set; }
        public string PrevEventArgument { get; set; }

        public bool IsValid { get; set; }
        public string Mode { get; set; }

        public bool IsDetailAreaVisible { get; set; }
        public bool IsSearchAreaVisible { get; set; }
        public bool IsListAreaVisible { get; set; }
        public bool IsPopUpAreaVisible { get; set; }

        public MetricsViewModel()
        {
            Init();
            
            SearchEntity = new MetricsObject();
            Entity = new MetricsObject();
            EventCommand = "list";
        }

        private void Init()
        {
            EventCommand = "list";
            EventArgument = string.Empty;
            ListMode();
        }

        private void ListMode()
        {
            IsListAreaVisible = true;
            Mode = "list";
        }

        public void HandleRequest()
        {
            switch (EventCommand.ToLower())
            {
                case "list":
                    Get();
                    break;
                default:
                    break;

            }
        }

        private void Get()
        {
            var mgr = new MetricsManager();
            Entity = mgr.Get(Entity);

            return;

        }
    }
}