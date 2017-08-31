using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto_KB.Models;
using System.IO;

namespace Projeto_KB.Controllers

{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        // Envia as imagens para o servidor
        public void uploadnow(HttpPostedFileWrapper upload)
        {
            if(upload!=null)
            {
                string ImageName = upload.FileName;
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images"), ImageName);
                upload.SaveAs(path);
            }
        }
        // Vista parcial para se verificar se as imagens estão no servidor
        public ActionResult uploadPartial()
        {
            var appData = Server.MapPath("~/Content/Images");
            var images = Directory.GetFiles(appData).Select(x => new Image
            {
                UrlImage = Url.Content("/Content/Images/" + Path.GetFileName(x))
            });
            return View(images);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}