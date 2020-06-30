using System.Web.Mvc;
using BussinessLayer.Data.Catalogo;
using EntityLayer.Negocio.Catalogo;
using System.Collections.Generic;
using EntityLayer.Peticion;

namespace MVC.Controllers
{
    public class CatalogoController : Controller
    {
        readonly DCatalogo _D;
        public CatalogoController()
        {
            _D = new DCatalogo();
        }
        [HttpPost]
        [ActionName("SelectCatalogoSexoMascota")]
        public JsonResult SelectCatalogoSexoMascota()
        {
            ORespuesta tst = _D.SelectCatalogoSexoMascota();
            return Json(tst.Respuesta);
        }
        [HttpPost]
        [ActionName("SelectCatalogoTipoMascota")]
        public JsonResult SelectCatalogoTipoMascota()
        {
            ORespuesta tst = _D.SelectCatalogoTipoMascota();
            return Json(tst.Respuesta);
        }
    }
}