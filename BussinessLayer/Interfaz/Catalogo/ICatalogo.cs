using EntityLayer.Peticion;
using System.Collections.Generic;

namespace BussinessLayer.Interfaz.Catalogo
{
    interface ICatalogo
    {
        ORespuesta SelectCatalogoSexo();
        ORespuesta SelectCatalogoSexoMascota();
        ORespuesta SelectCatalogoTipoMascota();
    }
}
