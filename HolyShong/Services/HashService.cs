using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HolyShong.Services
{
    public class HashService
    {
        public static string MD5Hash(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;
            using (MD5 mD5 = MD5.Create())
            {
                //將字串轉乘Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);

                //進行MD5雜湊加密
                byte[] encryption = mD5.ComputeHash(byteArray);

                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    //2, 8, 10, 16進位
                    //hexadecmimal- 十六進位
                    sb.Append(encryption[i].ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}