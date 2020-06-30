using EntityLayer.Negocio.Login;
using EntityLayer.Negocio.Usuario;
using EntityLayer.Peticion;
using System.Collections.Generic;

namespace BussinessLayer.Interfaz.Login
{
    interface ILogin
    {
        ORespuesta ValidarCorreoElectronico(OLogin pmtPeticion);
        ORespuesta Login(OLogin pmtPeticion);
    }
}
