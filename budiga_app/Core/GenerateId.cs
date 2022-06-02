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
        
        public static string Generate()
        {
            string id = GenerateString();

            return id;
        }

        public static string GenerateString()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(characters, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
