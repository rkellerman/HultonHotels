using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using HultonHotels.Models;
using HultonHotels.ViewModels.Objects;

namespace HultonHotels.ViewModels
{
    public class ReservationViewModel
    {
        public int CurrentUserId { get; set; }
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
                case "login":
                    Login();
                    Get();
                    break;
                case "select":
                    Select();
                    break;
                case "save":
                    Save();
                    Get();
                    break;
                default:
                    break;

            }
        }

        

        private void Save()
        {
            var mgr = new ReservationManager();
            mgr.Save(Entity, SearchEntity.Email);

            
        }

        
        private void Login()
        {
            if (!string.IsNullOrEmpty(SearchEntity.Email))
            {
                var mgr = new ReservationManager();
                CurrentUserId = mgr.Login(SearchEntity.Email);
                SearchEntity.CustomerId = CurrentUserId;
            }
        }

        private void Select()
        {
            var mgr = new ReservationManager();
            Entity = mgr.Select(EventArgument, PrevEventArgument);

            SelectMode();

        }

        private void SelectMode()
        {
            IsListAreaVisible = false;
            IsSearchAreaVisible = false;
            IsDetailAreaVisible = true;
            IsPopUpAreaVisible = false;

            Mode = "select";
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