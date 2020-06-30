using BussinessLayer.Interfaz.Chat;
using DataLayer.Aplicacion;
using EntityLayer.Negocio.Usuario;
using EntityLayer.Peticion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Data.Chat
{
    public class DChat: IChat
    {
        private readonly String cadenaConexionLocal;
        public DChat()
        {
            cadenaConexionLocal = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        }

        public ORespuesta SelectListaMatch(OUsuario pmtPeticion)
        {
            ORespuesta Ls = new ORespuesta();
            try
            {
                Hashtable Parametros = new Hashtable()
                {
                    {"@idUsuario", pmtPeticion.ID}
                };
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_select_lista_match", Parametros, cadenaConexionLocal);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            OUsuario _Dato = new OUsuario();
                            _Dato.Nombre = row["nombre"].ToString();
                            _Dato.URLImagen = row["imagen"].ToString();
                            Ls.Respuesta.Add(_Dato);
                        }
                    }
                }
                return Ls;
            }
            catch (SqlException e)
            {
                return Ls;
            }
            catch (Exception e)
            {
                return Ls;
            }
        }
    }
}
