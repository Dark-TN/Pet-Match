using EntityLayer.Negocio.Registro;
using System;
using System.IO;
using System.Security.Cryptography;

namespace EntityLayer.Negocio.Usuario
{
    public class OUsuario : ORegistro
    {
        public int ID { get; set; }
        public String NuevaPassword { get; set; }
        public OUsuario()
        {
            this.Nombre = String.Empty;
            this.IDSexo = 0;
            this.CorreoElectronico = String.Empty;
            this.Password = String.Empty;
            this.Salt = new byte[0];
            this.PasswordCifrada = new byte[0];
            this.IDEstatus = 1;
            this.ID = 0;
            this.NuevaPassword = String.Empty;
        }
    }
}
