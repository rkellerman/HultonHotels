using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HultonHotels.Models;

namespace HultonHotels.ViewModels
{
    public class RegistrationViewModel
    {
        public List<Customer> Customers { get; set; }
        public Customer SearchEntity { get; set; }
        public Customer Entity { get; set; }
        public Customer PrevEntity { get; set; }
        public string EventCommand { get; set; }
        public string EventArgument { get; set; }
        public string PrevEventArgument { get; set; }

        public bool IsValid { get; set; }
        public string Mode { get; set; }

        public bool IsDetailAreaVisible { get; set; }
        public bool IsSearchAreaVisible { get; set; }
        public bool IsListAreaVisible { get; set; }
        public bool IsPopUpAreaVisible { get; set; }


        public RegistrationViewModel()
        {
            Init();

            Customers = new List<Customer>();
            SearchEntity = new Customer();
            Entity = new Customer();
            EventCommand = "add";
        }

        public void HandleRequest()
        {
            switch (EventCommand.ToLower())
            {
                case "add":
                    Add();
                    break;
                case "save":
                    Save();
                    break;
                default:
                    break;
                
            }
        }

        private void Add()
        {
            IsValid = true;
            Entity = new Customer();

            AddMode();
        }

        private void AddMode()
        {
            Mode = "add";
        }

        private void Edit()
        {
            var mgr = new RegistrationManager();
            Entity = mgr.Get(Int32.Parse(EventArgument));
            EditMode();
        }

        private void EditMode()
        {
            Mode = "edit";
        }

        public void Save()
        {
            var mgr = new RegistrationManager();

            if (Mode == "add")
            {
                mgr.Insert(Entity);
            }
            else
            {
                mgr.Update(PrevEventArgument, Entity);
            }

            if (!IsValid)
            {
                if (Mode == "add")
                {
                    AddMode();
                }
                else
                {
                    EditMode();
                }
            }
        }

        public void Init()
        {
            EventCommand = "add";
            EventArgument = string.Empty;
            AddMode();
        }
    }
}