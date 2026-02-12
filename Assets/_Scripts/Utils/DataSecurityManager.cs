using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MyBox;

namespace _Scripts.Utils
{
    public static class DataSecurityManager
    {
        public static string ApplyChecksum(this string content)
        {
            return content.HMACSHA1_Hash() + "\n" + content;
        }
        
        public static string EnsureChecksum(this string content)
        {
            var index = content.FirstIndex(x=> x.Equals('\n'));
            if (index == -1)
            {
                return null;
            }
            var a = content.Substring(0, index);
            content = content.Substring(index + 1);
            var b = content.HMACSHA1_Hash();
            return a == b ? content : null;
        }
        
        private static string HMACSHA1_Hash(this string zSource, ESigningType type = ESigningType.ClientEverything)
        {
            var key = Encoding.ASCII.GetBytes(type.ToString());
            HMACSHA1 myhmacsha1 = new HMACSHA1(key);
            var byteArray = Encoding.ASCII.GetBytes(zSource);
            MemoryStream stream = new MemoryStream(byteArray);
            return myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }
    }
    
    public enum ESigningType
    {
        ClientEverything,
        ServerAccounts,
        ServerRtw,
        ServerRyf
    }
}