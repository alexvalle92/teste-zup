using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientesWebAPI.Helpers
{
    internal static class UtilsHelper
    {

        /// <summary>
        /// Critografar senha para BD
        /// </summary>
        /// <param name="passowrd">Senha do cliente</param>
        internal static string CriptografaSenha(string passowrd)
        {
            HashAlgorithm sha1 = SHA1.Create();
            byte[] aaa = sha1.ComputeHash(Encoding.ASCII.GetBytes(passowrd));
            return Convert.ToBase64String(aaa);
        }
    }
}
