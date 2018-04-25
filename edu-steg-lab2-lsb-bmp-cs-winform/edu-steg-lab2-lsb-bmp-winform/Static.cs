using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace edu_steg_lab2_lsb_bmp_winform
{
    class Static
    {
        public static byte[] GetEncryptionKey(string enc_key)
        {
            var dataKey = Encoding.UTF8.GetBytes(enc_key);
            SHA512 shaM = new SHA512Managed();
            byte[] key_hash = shaM.ComputeHash(dataKey);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in key_hash)
                stringBuilder.AppendFormat("{0:X2}", b);

            string key_hash_string = stringBuilder.ToString();

            /* Initialize AES crypto provider */
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

            /* Creating key bytes array for AES */
            byte[] key_bytes = Encoding.UTF8.GetBytes(key_hash_string.Substring(0, aes.Key.Length));

            VarStore.Global.keyBase64 = Convert.ToBase64String(key_bytes);

            return key_bytes;
        }

        public static int GetSteganographyKey(string ste_key)
        {
            var stegKey = Encoding.UTF8.GetBytes(ste_key);
            SHA512 shaStegKey = new SHA512Managed();
            byte[] stegKeyBytes = shaStegKey.ComputeHash(stegKey);
            int outputKey = BitConverter.ToInt32(stegKeyBytes, 0);

            VarStore.Global.stegKey = outputKey;

            return outputKey;
        }
    }
}
