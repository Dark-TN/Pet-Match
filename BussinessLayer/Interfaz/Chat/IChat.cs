using EntityLayer.Negocio.Usuario;
using EntityLayer.Peticion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaz.Chat
{
    interface IChat
    {
        ORespuesta SelectListaMatch(OUsuario pmtPeticion);
    }
}
