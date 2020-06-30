using EntityLayer.Negocio.Registro;
using EntityLayer.Peticion;
using System.Collections.Generic;

namespace BussinessLayer.Interfaz.Registro
{
    interface IRegistro
    {
        ORespuesta ValidarCorreoElectronicoDisponible(ORegistro pmtPeticion);
        ORespuesta Registrar(ORegistro pmtPeticion);
    }
}
