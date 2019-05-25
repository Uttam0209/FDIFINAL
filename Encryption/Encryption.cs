using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    public class Cryptography
    {
        public string strKey = "W@!dghDW";
        public string EncryptData(string strData)
        {
            byte[] key = { }; //Encryption Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray;

            try
            {
                key = Encoding.UTF8.GetBytes(strKey.Trim());
                // DESCryptoServiceProvider is a cryptography class defind in c#.  
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(strData.Trim());
                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();
                return Convert.ToBase64String(Objmst.ToArray());//encrypted string  
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public string DecryptData(string strData)
        {
            //strData = strData.Replace(" ", "+");
            // strData = strData.Replace(" ", "");
            // strData = strData + new string('=', (4 - strData.Length % 4) % 4);
            byte[] key = { };// Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray = new byte[strData.Length];

            try
            {
                key = Encoding.UTF8.GetBytes(strKey.Trim());
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strData.Trim());

                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(Objmst.ToArray());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
