using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using BussinessLayer.Interfaz.Registro;
using DataLayer.Aplicacion;
using EntityLayer.Negocio.Registro;
using EntityLayer.Peticion;

namespace BussinessLayer.Data.Registro
{
    public class DRegistro : IRegistro
    {
        private readonly String cadenaConexionLocal;
        public DRegistro()
        {
            cadenaConexionLocal = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        }
        
        public ORespuesta ValidarCorreoElectronicoDisponible(ORegistro pmtPeticion)
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
                            if (Int32.Parse(row["resultado"].ToString()) == 0)
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
        public ORespuesta Registrar(ORegistro pmtPeticion)
        {
            ORespuesta Ls = new ORespuesta();
            Ls.Respuesta.Add(0);
            try
            {
                byte[] salt = pmtPeticion.GenerarSalt();
                Hashtable Parametros = new Hashtable()
                {
                    {"@email", pmtPeticion.CorreoElectronico},
                    {"@password", pmtPeticion.HashPasswordConSalt(Encoding.UTF8.GetBytes(pmtPeticion.Password), salt)},
                    {"@salt", salt},
                    {"@idEstatus", 1},
                    {"@nombre", pmtPeticion.Nombre},
                    {"@direccion", pmtPeticion.Direccion},
                    {"@idTipo", pmtPeticion.IDTipoMascota},
                    {"@raza", pmtPeticion.Raza},
                    {"@idSexo", pmtPeticion.IDSexo},
                    {"@nombreMascota", pmtPeticion.NombreMascota}
                };
                DataSet ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_insert_registro", Parametros, cadenaConexionLocal);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            Ls.Respuesta[0] = Int32.Parse(row["idUsuario"].ToString());
                        }
                    }
                }
                if ((int)Ls.Respuesta[0] > 0)
                {
                    Parametros = new Hashtable()
                    {
                        {"@idUsuario", Ls.Respuesta[0]},
                        {"@imagen", "../../Recursos/ImagenesPerfil/" + Ls.Respuesta[0].ToString() + Path.GetExtension(pmtPeticion.UploadImagen.FileName)}
                    };
                    ds = BaseDeDatos.EjecutaProcedimientoAlmacenado("sp_update_imagen_usuario", Parametros, cadenaConexionLocal);
                    Ls.Exitoso = true;
                    return Ls;
                } 
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
