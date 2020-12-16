using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Unidad4SamaniegoActividad2.Helpers
{
    public class HashingHelper
    {
        public static string GetHash(string cadena)
        {
            var alg = SHA256.Create();
            byte[] codificar = Encoding.UTF8.GetBytes(cadena);
            byte[] hash = alg.ComputeHash(codificar);
            string a = "";
            foreach (var objeto in hash)
            {
                a += objeto.ToString("x2");
            }
            return a;
        }
    }
}
