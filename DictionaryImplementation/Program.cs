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
            Dictionary<string, string> dictioary = new Dictionary<string, string>();

            dictioary.Add("monday", "понедельник");
            dictioary.Add("tuesday", "вторник");
            dictioary.Add("wednesday", "среда");
            dictioary.Add("thursday", "четверг");
            dictioary.Add("fridaay", "Пятница");
            dictioary.Add("saturday", "Суббота");
            dictioary.Add("sunday", "Воскресенье");
            foreach (var item in dictioary)
            {
                Console.WriteLine($"{item.Key} is {item.Value}");
            }

            _Dictionary<string, List<string>> dictionary1 = new _Dictionary<string, List<string>>();
            dictionary1.Add("BMW", new List<string> {"M5","M3","X5","X6"});

            for (int i = 0; i < dictionary1["BMW"].Count; i++)
            {
                Console.WriteLine(dictionary1["BMW"][i]);
            }


            _Dictionary<string, string> engToRus = new _Dictionary<string, string>();
            engToRus.Add("monday", "понедельник");
            engToRus.Add("tuesday", "вторник");
            engToRus.Add("wednesday", "среда");
            engToRus.Add("thursday", "четверг");
            engToRus.Add("fridaay", "Пятница");
            engToRus.Add("saturday", "Суббота");
            engToRus.Add("sunday", "Воскресенье");
            

            Console.Write("Please enter day of week to translate: ");
            string input = Console.ReadLine();
            Console.WriteLine($"{input} in russian is {engToRus[input]}");
            


        }
    }

}
