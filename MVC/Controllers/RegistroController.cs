using System.IO;
using System.Web.Mvc;
using BussinessLayer.Data.Registro;
using EntityLayer.Negocio.Registro;
using EntityLayer.Peticion;

namespace MVC.Controllers
{
    public class RegistroController : Controller
    {
        readonly DRegistro _D;
        public RegistroController()
        {
            _D = new DRegistro();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ValidarCorreoElectronicoDisponible")]
        public JsonResult ValidarCorreoElectronicoDisponible(ORegistro PmtPeticion)
        {
            return Json(_D.ValidarCorreoElectronicoDisponible(PmtPeticion).Respuesta);
        }
        [HttpPost]
        [ActionName("Registrar")]
        public JsonResult Registrar(ORegistro PmtPeticion)
        {
            ORespuesta data = _D.Registrar(PmtPeticion);
            if (data.Exitoso)
            {
                PmtPeticion.URLImagen = data.Respuesta[0].ToString() + Path.GetExtension(PmtPeticion.UploadImagen.FileName);
                PmtPeticion.UploadImagen.SaveAs(Path.Combine(Server.MapPath("~/Recursos/ImagenesPerfil"), PmtPeticion.URLImagen));
            }
            return Json(data);
        }
}
}