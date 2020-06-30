using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using BussinessLayer.Interfaz.Login;
using DataLayer.Aplicacion;
using EntityLayer.Negocio.Login;
using EntityLayer.Negocio.Usuario;
using EntityLayer.Peticion;

namespace BussinessLayer.Data.Login
{
    public class DLogin : ILogin
    {
        private readonly String cadenaConexionLocal;
        public DLogin()
        {
            cadenaConexionLocal = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        }
        public ORespuesta ValidarCorreoElectronico(OLogin pmtPeticion)
        {
            ORespuesta Ls = new ORespuesta();
            Ls.Respuesta.Add(false);
            try
            {
                Hashtable Parametros = new Hashtable()
                {
                    {"@email", pmtPeticion.CorreoElectronico}
                };
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_select_validar_correo_electronico", Parametros, cadenaConexionLocal);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (Int32.Parse(row["resultado"].ToString()) != 0)
                            {
                                Ls.Respuesta[0] = true;
                            }
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
        public ORespuesta Login(OLogin pmtPeticion)
        {
            ORespuesta Ls = new ORespuesta();   
            try
            {
                Hashtable Parametros = new Hashtable()
                {
                    {"@email", pmtPeticion.CorreoElectronico}
                };
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_select_login", Parametros, cadenaConexionLocal);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            OUsuario _Dato = new OUsuario();
                            _Dato.CorreoElectronico = pmtPeticion.CorreoElectronico;
                            _Dato.PasswordCifrada = (byte[])row["password"];
                            _Dato.Salt = (byte[])row["salt"];
                            _Dato.IDEstatus = Int32.Parse(row["idEstatus"].ToString());
                            _Dato.ID = Int32.Parse(row["idUsuario"].ToString());
                            _Dato.Nombre = row["nombre"].ToString();
                            _Dato.Direccion = row["direccion"].ToString();
                            _Dato.IDTipoMascota = Int32.Parse(row["idTipo"].ToString());
                            _Dato.IDSexo = Int32.Parse(row["idSexo"].ToString());
                            _Dato.NombreMascota = row["nombreMascota"].ToString();
                            _Dato.URLImagen = row["imagen"].ToString();
                            _Dato.Raza = row["raza"].ToString();
                            Ls.Respuesta.Add(_Dato);
                        }
                    }
                }
                if (((OUsuario)Ls.Respuesta[0]).IDEstatus == 1)
                {
                    if (((OUsuario)Ls.Respuesta[0]).PasswordValida(pmtPeticion.Password))
                    {
                        ((OUsuario)Ls.Respuesta[0]).Correcto = true;
                    }
                }
                ((OUsuario)Ls.Respuesta[0]).Salt = new byte[0];
                ((OUsuario)Ls.Respuesta[0]).PasswordCifrada = new byte[0];
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
