using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("anun", "aram");
            Console.WriteLine(dict["anun"]);
            _Dictionary<int, string> dictionary1 = new _Dictionary<int, string>();
            dictionary1.Add(15, "Menua");
            Console.WriteLine(dictionary1[15]);

        }
    }

}
