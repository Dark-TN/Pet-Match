using BussinessLayer.Interfaz.Usuario;
using DataLayer.Aplicacion;
using EntityLayer.Negocio.Usuario;
using EntityLayer.Peticion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Data.Usuario
{
    public class DUsuario : IUsuario
    {
        private readonly String cadenaConexionLocal;
        public DUsuario()
        {
            cadenaConexionLocal = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        }
        public ORespuesta ModificarPassword(OUsuario pmtPeticion, OUsuario pmtSesion)
        {
            ORespuesta Ls = new ORespuesta();
            OUsuario usuario = new OUsuario();
            Hashtable Parametros = new Hashtable()
                {
                    {"@email", pmtSesion.CorreoElectronico}
                };
            DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_select_login", Parametros, cadenaConexionLocal);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        usuario.PasswordCifrada = (byte[])row["password"];
                        usuario.Salt = (byte[])row["salt"];
                    }
                }
            }
            if (usuario.PasswordValida(pmtPeticion.Password))
            {
                Parametros = new Hashtable()
                    {
                        {"@email", pmtSesion.CorreoElectronico},
                        {"@password", usuario.HashPasswordConSalt(Encoding.UTF8.GetBytes(pmtPeticion.NuevaPassword), usuario.Salt)}
                    };
                ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_update_password_usuario", Parametros, cadenaConexionLocal);
                Ls.Exitoso = true;
                return Ls;
            }
            else
            {
                return Ls;
            }
        }
        public ORespuesta ModificarDatos(OUsuario pmtPeticion, OUsuario pmtSesion)
        {
            ORespuesta Ls = new ORespuesta();
            try
            {
                if(pmtPeticion.UploadImagen != null)
                {
                    pmtPeticion.URLImagen = "../../Recursos/ImagenesPerfil/" + pmtSesion.ID.ToString() + Path.GetExtension(pmtPeticion.UploadImagen.FileName);
                }
                Hashtable Parametros = new Hashtable()
                {
                    {"@idUsuario", pmtSesion.ID},
                    {"@email", pmtSesion.CorreoElectronico},
                    {"@nuevoEmail", pmtPeticion.CorreoElectronico },
                    {"@nombre", pmtPeticion.Nombre},
                    {"@direccion", pmtPeticion.Direccion},
                    {"@idTipo", pmtPeticion.IDTipoMascota},
                    {"@idSexo", pmtPeticion.IDSexo },
                    {"@nombreMascota", pmtPeticion.NombreMascota},
                    {"@imagen", pmtPeticion.URLImagen},
                    {"@raza", pmtPeticion.Raza}
                };
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_update_datos_usuario", Parametros, cadenaConexionLocal);
                OUsuario usuario = new OUsuario();
                usuario.ID = pmtPeticion.ID;
                usuario.CorreoElectronico = pmtPeticion.CorreoElectronico;
                usuario.Nombre = pmtPeticion.Nombre;
                usuario.Direccion = pmtPeticion.Direccion;
                usuario.IDTipoMascota = pmtPeticion.IDTipoMascota;
                usuario.IDSexo = pmtPeticion.IDSexo;
                usuario.NombreMascota = pmtPeticion.NombreMascota;
                usuario.URLImagen = pmtPeticion.URLImagen;
                usuario.Raza = pmtPeticion.Raza;
                Ls.Respuesta.Add(usuario);
                Ls.Exitoso = true;
                return Ls;
            }
            catch (SqlException e)
            {
                Debug.Write(e.Message);
                return Ls;

            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return Ls;
            }
        }

        public ORespuesta SelectListaMascotas(OUsuario pmtPeticion)
        {
            ORespuesta Ls = new ORespuesta();
            try
            {
                Hashtable Parametros = new Hashtable()
                {
                    {"@email", pmtPeticion.CorreoElectronico},
                    {"@idTipo", pmtPeticion.IDTipoMascota},
                    {"@idSexo", pmtPeticion.IDSexo }
                };
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_select_lista_mascotas", Parametros, cadenaConexionLocal);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            OUsuario usuario = new OUsuario();
                            usuario.CorreoElectronico = row["email"].ToString();
                            usuario.NombreMascota = row["nombreMascota"].ToString();
                            usuario.Nombre = row["nombre"].ToString();
                            usuario.Raza = row["raza"].ToString();
                            usuario.Direccion = row["direccion"].ToString();
                            usuario.URLImagen = row["imagen"].ToString();
                            Ls.Respuesta.Add(usuario);
                        }
                    }
                }
                Ls.Exitoso = true;
                return Ls;
            }
            catch (SqlException e)
            {
                Ls.Mensaje = e.Message;
                return Ls;

            }
            catch (Exception e)
            {
                Ls.Mensaje = e.Message;
                return Ls;
            }
        }

        public ORespuesta InsertLike(OUsuario pmtUsuario1, OUsuario pmtUsuario2)
        {
            ORespuesta Ls = new ORespuesta();
            try
            {
                Hashtable Parametros = new Hashtable()
                {
                    {"@email1", pmtUsuario1.CorreoElectronico},
                    {"@email2", pmtUsuario2.CorreoElectronico}
                };
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_insert_like", Parametros, cadenaConexionLocal);
                Ls.Exitoso = true;
                return Ls;
            }
            catch (SqlException e)
            {
                Ls.Mensaje = e.Message;
                return Ls;

            }
            catch (Exception e)
            {
                Ls.Mensaje = e.Message;
                return Ls;
            }
        }
    }
}
