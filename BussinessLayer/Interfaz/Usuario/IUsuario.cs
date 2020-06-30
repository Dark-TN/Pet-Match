using EntityLayer.Negocio.Usuario;
using EntityLayer.Peticion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaz.Usuario
{
    interface IUsuario
    {
        ORespuesta ModificarPassword(OUsuario pmtPeticion, OUsuario pmtSesion);
        ORespuesta ModificarDatos(OUsuario pmtPeticion, OUsuario pmtSesion);
        ORespuesta SelectListaMascotas(OUsuario pmtPeticion);
        ORespuesta InsertLike(OUsuario pmtUsuario1, OUsuario pmtUsuario2);
    }
}
