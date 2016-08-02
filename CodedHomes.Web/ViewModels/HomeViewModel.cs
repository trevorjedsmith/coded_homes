using CodedHomes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodedHomes.Web.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public Home Home { get; set; }
        public bool IsNew { get; set; }

        public string ImageUrlPrefix
        {
            get
            {
                return "/img/homes/";
            }
        }

        public HomeViewModel()
        {
            this.Home = new Home();
        }
    }
}