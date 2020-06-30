using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BussinessLayer.Interfaz.Catalogo;
using DataLayer.Aplicacion;
using EntityLayer.Negocio.Catalogo;
using EntityLayer.Peticion;

namespace BussinessLayer.Data.Catalogo
{
    public class DCatalogo : ICatalogo
    {
        private readonly String cadenaConexionLocal;
        public DCatalogo()
        {
            cadenaConexionLocal = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        }
        public ORespuesta SelectCatalogoSexo()
        {
            ORespuesta Ls = new ORespuesta();
            try
            {
                Hashtable Parametros = new Hashtable();
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_select_catalogo_sexo", Parametros, cadenaConexionLocal);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {                       
                            OCatalogo _Dato = new OCatalogo();
                            _Dato.ID = Int32.Parse(row["idSexo"].ToString());
                            _Dato.Descripcion = row["descripcion"].ToString();
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
        public ORespuesta SelectCatalogoSexoMascota()
        {
            ORespuesta Ls = new ORespuesta();
            try
            {
                Hashtable Parametros = new Hashtable();
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_select_catalogo_sexo_mascota", Parametros, cadenaConexionLocal);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            OCatalogo _Dato = new OCatalogo();
                            _Dato.ID = Int32.Parse(row["idSexo"].ToString());
                            _Dato.Descripcion = row["descripcion"].ToString();
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

        public ORespuesta SelectCatalogoTipoMascota()
        {
            ORespuesta Ls = new ORespuesta();
            try
            {
                Hashtable Parametros = new Hashtable();
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_select_catalogo_tipo_mascota", Parametros, cadenaConexionLocal);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            OCatalogo _Dato = new OCatalogo();
                            _Dato.ID = Int32.Parse(row["idTipo"].ToString());
                            _Dato.Descripcion = row["descripcion"].ToString();
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
