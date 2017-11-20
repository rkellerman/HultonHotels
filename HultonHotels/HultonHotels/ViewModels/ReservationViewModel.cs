using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using HultonHotels.Models;
using HultonHotels.Utilities;
using HultonHotels.ViewModels.Objects;

namespace HultonHotels.ViewModels
{
    public class ReservationViewModel
    {
        public int CurrentUserId { get; set; } = -1;
        public List<ReservationObject> Items { get; set; }
        public List<ReservationObject> MyItems { get; set; }    
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
        public bool IsMyReservationsAreaVisible { get; set; }

        public string ByPass { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; } = 10;

        public List<int> Pagination { get; set; }

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
                case "delete":
                    Delete();
                    break;
                default:
                    break;

            }
        }

        private void Delete()
        {
            var mgr = new ReservationManager();

            if (string.IsNullOrEmpty(ByPass))
            {
                Entity.HotelId = int.Parse(EventArgument);
                Entity.RoomNo = int.Parse(PrevEventArgument);


                mgr.Delete(Entity);
            }

            EventArgument = string.Empty;
            PrevEventArgument = string.Empty;
            ByPass = string.Empty;

            Get();
            ListMode();
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
            IsMyReservationsAreaVisible = false;

            Mode = "select";
        }

        private void Get()
        {

            var mgr = new ReservationManager();
            var ret = mgr.Get(SearchEntity);
            Items = ret[0];
            MyItems = ret[1];

            if (!string.IsNullOrEmpty(EventArgument))
            {
                CurrentPage = int.Parse(EventArgument);
            }

            TotalPages = (int)Math.Ceiling((double)Items.Count / (double)ItemsPerPage) - 1;
            if (TotalPages < 0) TotalPages = 0;

            if (CurrentPage > TotalPages)
            {
                CurrentPage = 0;
            }
            else if (CurrentPage < 0)
            {
                CurrentPage = TotalPages;
            }

            Items = Items.Skip(ItemsPerPage * CurrentPage).Take(ItemsPerPage).ToList();

            Pagination = PaginationHelper.CreatePaginationWithEllipsis(CurrentPage, TotalPages, 6);

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
            IsMyReservationsAreaVisible = true;

            Mode = "List";
        }
    }
}