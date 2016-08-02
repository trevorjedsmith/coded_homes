using CodedHomes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace CodedHomes.Web.ViewModels
{
    public class HomesListViewModel : ViewModelBase
    {
        public IList<Home> Homes { get; set; }

        public string HomesJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                //camelcasecontractresolver
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var homes = JsonConvert.SerializeObject(this.Homes, settings);
                return homes;
            }
        }

        public string ImageUrlPrefix
        {
            get
            {
                return "/img/homes/";
            }
        }
    }
}