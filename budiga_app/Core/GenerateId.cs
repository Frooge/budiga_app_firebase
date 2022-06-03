using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.Core
{
    public class GenerateId
    {
        private static Random random = new Random();
        public static string[] ids = { };
        
        public static string GenerateCommon()
        {
            string id = GenerateString();

            return id;
        }

        public static string GenerateStore(string name)
        {
            string code = name.Substring(0, 3).ToUpper();
            string id = code + "-" + GenerateNum();

            return id;
        }

        public static string GenerateString(int length = 20)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(characters, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateNum(int length = 4)
        {
            const string characters = "0123456789";
            return new string(Enumerable.Repeat(characters, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
