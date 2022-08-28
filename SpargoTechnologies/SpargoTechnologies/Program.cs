using System;
using System.Collections.Generic;

namespace SpargoTechnologies
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice = "";
            while (choice != "6") 
            {
                Logic.ListChoice(new List<string> { "Товары", "Аптеки", "Склады", "Партии", "Вывод товара по выбранной аптеке", "Выход" });
                choice = (Console.ReadKey()).KeyChar.ToString();
                Logic.MainLogic(choice);
                Console.Clear();
            }
        }
    }
}
