using BussinessLayer.Data.Usuario;
using EntityLayer.Negocio.Usuario;
using EntityLayer.Peticion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class UsuarioController : Controller
    {
        readonly DUsuario _D;
        public UsuarioController()
        {
            _D = new DUsuario();
        }
        public ActionResult Index()
        {
            return View("~/Views/Usuario/Index.cshtml", Session["Usuario"]);
        }
        [HttpPost]
        [ActionName("DatosUsuario")]
        public JsonResult DatosUsuario()
        {
            return Json(Session["Usuario"]);
        }
        [HttpPost]
        [ActionName("ModificarPassword")]
        public JsonResult ModificarPassword(OUsuario pmtPeticion)
        {
            OUsuario usuario = (OUsuario)Session["Usuario"];
            return Json(_D.ModificarPassword(pmtPeticion, usuario));
        }
        [HttpPost]
        [ActionName("ModificarDatos")]
        public ActionResult ModificarDatos(OUsuario pmtPeticion)
        {
            OUsuario usuario = (OUsuario)Session["Usuario"];
            if (pmtPeticion.UploadImagen == null)
            {
                pmtPeticion.URLImagen = usuario.URLImagen;
            }
            ORespuesta response = _D.ModificarDatos(pmtPeticion, usuario);
            if (response.Exitoso)
            {
                if (pmtPeticion.UploadImagen != null)
                {
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Proyecto/MVC/"), usuario.URLImagen));
                    pmtPeticion.UploadImagen.SaveAs(Path.Combine(Server.MapPath("~/Proyecto/MVC/"), ((OUsuario)response.Respuesta[0]).URLImagen));
                    Session["Usuario"] = (OUsuario)response.Respuesta[0];
                }
            }
            return PartialView("~/Views/Shared/_MenuUsuario.cshtml", Session["Usuario"]);
        }
        [HttpPost]
        [ActionName("SelectListaMascotas")]
        public JsonResult SelectListaMascotas(OUsuario pmtPeticion)
        {
            return Json(_D.SelectListaMascotas(pmtPeticion));
        }
        [HttpPost]
        [ActionName("InsertLike")]
        public JsonResult InsertLike(OUsuario pmtPeticion)
        {
            OUsuario usuario = (OUsuario)Session["Usuario"];
            return Json(_D.InsertLike(usuario, pmtPeticion));
        }
    }
}