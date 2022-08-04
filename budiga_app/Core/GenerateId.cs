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

        public static string GenerateBranch(string name)
        {
            string code = name.Substring(0, 3).ToUpper();
            string id = code + "-" + GenerateNum();

            return id;
        }

        public static string GenerateItemHistory(DateTime date)
        {
            string id = string.Format("{0:00}{1:00}{2}{3}-{4}", date.Month, date.Day, date.Year, (date.Hour * date.Minute * date.Second).ToString().Substring(0,3), GenerateString());

            return id;
        }

        public static string GenerateInvoice(DateTime date)
        {
            string id = string.Format("{0:00}{1:00}{2}{3}", date.Month, date.Day, date.Year, (date.Hour * date.Minute * date.Second).ToString().Substring(0,3));

            return id;
        }

        public static string GenerateOrder(DateTime date)
        {
            string id = string.Format("{0:00}{1:00}{2}{3}-{4}", date.Month, date.Day, date.Year, (date.Hour * date.Minute * date.Second).ToString().Substring(0, 3), GenerateString().Substring(0, 8));

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
