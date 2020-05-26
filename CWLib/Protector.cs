using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace CWLib
{
    public class Protector
    {
        const int Length = 20;
        const int Iterations = 10;

        byte[] Salt { get; set; }        

        public Protector(byte[] salt)
        {
            if (salt == null)
                Salt = CreateSlt(Length);
        }

        public Protector(byte[] salt, out byte[] res)
        {
            if (salt == null)
                Salt = CreateSlt(Length);

            res = Salt.ToArray();
        }

        byte[] CreateSlt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        public byte[] CreateHash(byte[] password, byte[] salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return deriveBytes.GetBytes(Length);
            }
        }
    }
}
