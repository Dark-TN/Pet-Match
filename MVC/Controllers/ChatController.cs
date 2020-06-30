using BussinessLayer.Data.Chat;
using EntityLayer.Negocio.Usuario;
using EntityLayer.Peticion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ChatController : Controller
    {
        // GET: Lugares
        readonly DChat _D;
        public ChatController()
        {
            _D = new DChat();
        }
        public ActionResult Index()
        {
            return View("~/Views/Chat/Index.cshtml", Session["Usuario"]);
        }
        [HttpPost]
        [ActionName("SelectListaMatch")]
        public JsonResult SelectListaMatch()
        {
            return Json(_D.SelectListaMatch((OUsuario)Session["Usuario"]));
        }
    }
}