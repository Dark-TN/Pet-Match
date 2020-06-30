using System.Web.Mvc;
using System.Web.UI;
using BussinessLayer.Data.Login;
using EntityLayer.Negocio.Login;
using EntityLayer.Negocio.Registro;
using EntityLayer.Negocio.Usuario;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        readonly DLogin _D;
        OUsuario Usuario = new OUsuario();
        public LoginController()
        {
            _D = new DLogin();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ValidarCorreoElectronico")]
        public JsonResult ValidarCorreoElectronico(OLogin PmtPeticion)
        {
            return Json(_D.ValidarCorreoElectronico(PmtPeticion).Respuesta);
        }
        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login(OLogin PmtPeticion)
        {
            Usuario = (OUsuario)_D.Login(PmtPeticion).Respuesta[0];
            if (Usuario.Correcto)
            {
                ViewData.Clear();
                Session["Usuario"] = Usuario;
                return View("~/Views/Usuario/Index.cshtml", Session["Usuario"]);
            }
            else
            {
                ViewBag.Mensaje = string.Format("bootbox.alert('<center><label>El correo electr&oacute;nico o la contraseña son incorrectos.</label></center>');");
                return View("~/Views/Login/Index.cshtml");
            }
        }
    }
}