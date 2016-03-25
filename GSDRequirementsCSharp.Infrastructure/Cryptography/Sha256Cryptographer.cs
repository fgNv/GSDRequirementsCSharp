using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Cryptography
{
    public class Sha256Cryptographer : ICryptographer
    {
        public string Encrypt(string input)
        {
            if (input == null)
                throw new Exception("\"input\" param is null");

            var data = new byte[input.Length];
            var shaM = new SHA256Managed();
            var result = shaM.ComputeHash(data);
            var strResult = Encoding.Default.GetString(result);
            return strResult;
        }
    }
}
