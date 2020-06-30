using System;
using System.IO;
using System.Security.Cryptography;
using EntityLayer.Negocio.Login;
using System.Web;

namespace EntityLayer.Negocio.Registro
{
    public class ORegistro : OLogin
    {
        public String Nombre { get; set; }
        public String Direccion { get; set; }
        public String NombreMascota { get; set; }
        public int IDTipoMascota { get; set; }
        public String Raza { get; set; }
        public int IDSexo { get; set; }
        public String URLImagen { get; set; }
        public HttpPostedFileBase UploadImagen { get; set; }
        public ORegistro()
        {
            this.Nombre = String.Empty;
            this.Direccion = String.Empty;
            this.NombreMascota = String.Empty;
            this.IDTipoMascota = 0;
            this.Raza = String.Empty;
            this.IDSexo = 0;
            this.CorreoElectronico = String.Empty;
            this.Password = String.Empty;
            this.Salt = new byte[0];
            this.PasswordCifrada = new byte[0];
            this.URLImagen = String.Empty;
            this.UploadImagen = null;
            this.IDEstatus = 1;
        }
    }
}
