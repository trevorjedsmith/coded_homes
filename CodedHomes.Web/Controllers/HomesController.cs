using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CodedHomes.Web.Filters;
using CodedHomes.Models;
using CodedHomes.Web.ViewModels;
using CodedHomes.Data;
using System.IO;

namespace CodedHomes.Web.Controllers
{
    [Authorize]
    public class HomesController : Controller
    {
        private ApplicationUnit _unit = new ApplicationUnit();
        //
        // GET: /Homes/
        [AllowAnonymous]
        public ActionResult Index()
        {
            HomesListViewModel vm = new HomesListViewModel();
            var query = this._unit.Homes.GetAll().OrderByDescending(s => s.Price);
            vm.Homes = query.ToList();
            return View("Index",vm);
        }

        public ActionResult New()
        {
            HomeViewModel vm = new HomeViewModel();
            vm.IsNew = true;

            return View("Home", vm);
        }


        [ActionName("Edit")]
        public ActionResult Get(int id)
        {
            HomeViewModel vm = new HomeViewModel();
            vm.Home = this._unit.Homes.GetById(id);
            if (vm.Home != null)
            {
                return View("Home", vm);
            }

            return View("NotFound");
        }

        [HttpPost()]
        public ActionResult UploadImage(HttpPostedFileBase image, int id)
        {
            JsonResult result;
            Home home;
            Random rand = new Random();
            string unique;
            string ext;
            string fileName;
            string path;

            unique = rand.Next(1000000).ToString();

            ext = Path.GetExtension(image.FileName).ToLower();

            fileName = string.Format("{0}-{1}{2}", id, unique, ext);

            path = Path.Combine(
                HttpContext.Server.MapPath("~/img/homes/"), fileName);

            if (ext == ".png" || ext == ".jpg")
            {
                home = _unit.Homes.GetById(id);

                if (home != null)
                {
                    home.ImageName = fileName;
                    _unit.Homes.Update(home);
                    _unit.SaveChanges();

                    image.SaveAs(path);
                    result = this.Json(new
                    {
                        imageUrl = string.Format("{0}{1}",
                        "/img/homes", fileName)
                    });
                }
                else
                {
                    result = this.Json(new
                    {
                        status = "error",
                        statusText =
                            string.Format("There is no home with the Id of " +
                                "'{0}' in the system.", id)
                    });
                }
            }
            else
            {
                result = this.Json(new
                {
                    status = "error",
                    statusText = "Unsupported image type. Only .png or " +
                        ".jpg files are acceptable."
                });
            }

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            this._unit.Dispose();
            base.Dispose(disposing);
        }

    }
}
