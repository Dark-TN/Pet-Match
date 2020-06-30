using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace EntityLayer.Negocio.Login
{
    public class OLogin
    {
        public String CorreoElectronico { get; set; }
        public String Password { get; set; }
        public byte[] Salt { get; set; }
        public byte[] PasswordCifrada { get; set; }
        public bool Correcto { get; set; }
        public int IDEstatus { get; set; }
        public OLogin()
        {
            this.CorreoElectronico = "";
            this.Password = "";
            this.Salt = new byte[0];
            this.PasswordCifrada = new byte[0];
            this.Correcto = false;
            this.IDEstatus = 1;
        }
        public byte[] GenerarSalt()
        {
            const int saltLength = 32;

            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }
        private byte[] Combinar(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }
        public byte[] HashPasswordConSalt(byte[] toBeHashed, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedHash = Combinar(toBeHashed, salt);

                return sha256.ComputeHash(combinedHash);
            }
        }
        public bool PasswordValida(string password)
        {
            bool valido = false;
            byte[] hashedPassword = HashPasswordConSalt(Encoding.UTF8.GetBytes(password), Salt);
            if (hashedPassword.SequenceEqual(PasswordCifrada))
            {
                valido = true;
            }                   
            return valido;
        }
    }
}
