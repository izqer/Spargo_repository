using System;
using System.Collections.Generic;

namespace SpargoTechnologies
{
    class Logic
    {
        /// <summary>
        /// Главное меню
        /// </summary>
        /// <param name="choice">Выбор</param>
        public static void MainLogic(string choice)
        {
            switch (choice)
            {
                case "1":
                    {
                        Product();
                    }
                    break;
                case "2":
                    {
                        Pharmacie();
                    }
                    break;
                case "3":
                    {
                        Warehouse();
                    }
                    break;
                case "4":
                    {
                        Party();
                    }
                    break;
                case "5":
                    {
                        SelectProductByPharmacy();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Генерация списков
        /// </summary>
        /// <param name="choice">Текст выбора</param>
        public static void ListChoice(List<string> choice)
        {
            for (int i = 0; i < choice.Count; i++)
            {
                Console.WriteLine(String.Format("{0}.{1}", i+1, choice[i]));
            }
        }

        /// <summary>
        /// Товар
        /// </summary>
        private static void Product()
        {
            string choice = "";
            while (choice != "4")
            {
                Console.Clear();
                Logic.ListChoice(new List<string> { "Создать товар", "Удалить товар", "Просмотр товаров","Назад" });
                string choice_ = "";
                choice = (Console.ReadKey()).KeyChar.ToString();
                switch (choice)
                {
                    case "1":
                        {
                            SqlHelper.Param param = new SqlHelper.Param();
                            SqlHelper.AddParam("Напишите название товара", param, "@Name");
                            Console.WriteLine("Вы уверены что хотите выполнить действие? (1 - да, 2(или др.) - нет)?");
                            choice_ = (Console.ReadKey()).KeyChar.ToString();
                            if (choice_ == "1")
                                SqlHelper.PerfomProcedureNonResultOneParam("AddProduct", param);
                        }
                        break;
                    case "2":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Название" };
                            SqlHelper.PerfomProcedureResult("SELECT Id, Name FROM Products", nameColumn, 45);
                            Console.WriteLine();
                            SqlHelper.Param param = new SqlHelper.Param();
                            SqlHelper.AddParam("Напишите идентификатор товара", param, "@id", false,true);
                            param.paramName = "@id";
                            string text = Console.ReadLine();
                            if (int.TryParse(text, out int number))
                            {
                                if (SqlHelper.PerfomProcedureResult(String.Format("SELECT COUNT(*) FROM Products WHERE Id = {0}", text)) > 0)
                                {
                                    param.paramValue = text;
                                }
                                else
                                {
                                    Console.WriteLine("Такого товара не существует, попробуйте ещё раз.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("Вы уверены что хотите выполнить действие? (1 - да, 2(или др.) - нет)?");
                            choice_ = (Console.ReadKey()).KeyChar.ToString();
                            if (choice_ == "1")
                                SqlHelper.PerfomProcedureNonResultOneParam("DeleteProduct", param);
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Название" };
                            SqlHelper.PerfomProcedureResult("SELECT * FROM Products", nameColumn, 36);
                            Console.ReadKey();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Аптека
        /// </summary>
        private static void Pharmacie()
        {
            string choice = "";
            while (choice != "4")
            {
                Console.Clear();
                Logic.ListChoice(new List<string> { "Создать аптеку", "Удалить аптеку", "Просмотр аптек", "Назад" });
                string choice_ = "";
                choice = (Console.ReadKey()).KeyChar.ToString();
                switch (choice)
                {
                    case "1":
                        {
                            List<SqlHelper.Param> params_ = new List<SqlHelper.Param>{};
                            SqlHelper.AddListParam("Напишите имя", params_, "@Name"); 
                            SqlHelper.AddListParam("Напишите адрес", params_, "@Address");
                            SqlHelper.AddListParam("Напишите номер телефона", params_, "@Phone");
                            Console.WriteLine("Вы уверены что хотите выполнить действие? (1 - да, 2(или др.) - нет)?");
                            choice_ = (Console.ReadKey()).KeyChar.ToString();
                            if (choice_ == "1")
                                SqlHelper.PerfomProcedureNonResultSeveralParam("AddPharmacy", params_);
                        }
                        break;
                    case "2":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Название" };
                            SqlHelper.PerfomProcedureResult("SELECT Id, Name FROM Pharmacies", nameColumn, 45);
                            Console.WriteLine();
                            SqlHelper.Param param = new SqlHelper.Param();
                            SqlHelper.AddParam("Напишите идентификатор аптеки", param, "@id", false, true);
                            param.paramName = "@id";
                            string text = Console.ReadLine();
                            if (int.TryParse(text, out int number))
                            {
                                if (SqlHelper.PerfomProcedureResult(String.Format("SELECT COUNT(*) FROM Pharmacies WHERE Id = {0}", text)) > 0)
                                {
                                    param.paramValue = text;
                                }
                                else
                                {
                                    Console.WriteLine("Такой аптеки не существует, попробуйте ещё раз.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("Вы уверены что хотите выполнить действие? (1 - да, 2(или др.) - нет)?");
                            choice_ = (Console.ReadKey()).KeyChar.ToString();
                            if (choice_ == "1")
                                SqlHelper.PerfomProcedureNonResultOneParam("DeletePharmacy", param);
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Название", "Адрес", "Телефон" };
                            SqlHelper.PerfomProcedureResult("SELECT * FROM Pharmacies", nameColumn, 70);
                            Console.ReadKey();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Склад
        /// </summary>
        private static void Warehouse()
        {
            string choice = "";
            while (choice != "4")
            {
                Console.Clear();
                Logic.ListChoice(new List<string> { "Создать склад", "Удалить склад", "Просмотр складов", "Назад" });
                string choice_ = "";
                choice = (Console.ReadKey()).KeyChar.ToString();
                switch (choice)
                {
                    case "1":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Название аптеки" };
                            SqlHelper.PerfomProcedureResult("SELECT Id, Name FROM Pharmacies", nameColumn, 45);
                            Console.WriteLine();
                            List<SqlHelper.Param> params_ = new List<SqlHelper.Param> { };
                            SqlHelper.Param param = new SqlHelper.Param();
                            SqlHelper.AddParam("Напишите идентификатор аптеки", param, "@idPharmacie", false, true);
                            param.paramName = "@idPharmacie";
                            string text = Console.ReadLine();
                            if (int.TryParse(text, out int number))
                            {
                                if (SqlHelper.PerfomProcedureResult(String.Format("SELECT COUNT(*) FROM Pharmacies WHERE Id = {0}", text)) > 0)
                                {
                                    param.paramValue = text;
                                    params_.Add(param);
                                }
                                else
                                {
                                    Console.WriteLine("Такой аптеки не существует, попробуйте ещё раз.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                                Console.ReadKey();
                                break;
                            }

                            SqlHelper.AddListParam("Напишите название склада", params_, "@Name");
                            Console.WriteLine("Вы уверены что хотите выполнить действие? (1 - да, 2(или др.) - нет)?");
                            choice_ = (Console.ReadKey()).KeyChar.ToString();
                            if (choice_ == "1")
                                SqlHelper.PerfomProcedureNonResultSeveralParam("AddWarehouse", params_);
                        }
                        break;
                    case "2":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Название склада", "Название аптеки" };
                            SqlHelper.PerfomProcedureResult("SELECT wh.[id] as idWarehouse, wh.[Name] as WarehouseName, ph.[Name] as PharmacieName FROM Warehouses wh LEFT JOIN Pharmacies ph ON wh.idPharmacie = ph.Id", nameColumn, 45);
                            Console.WriteLine();
                            SqlHelper.Param param = new SqlHelper.Param();
                            SqlHelper.AddParam("Напишите идентификатор склада", param, "@id", false,true);
                            string text = Console.ReadLine();
                            if (int.TryParse(text, out int number))
                            {
                                if (SqlHelper.PerfomProcedureResult(String.Format("SELECT COUNT(*) FROM Warehouses WHERE Id = {0}", text)) > 0)
                                {
                                    param.paramName = "@id";
                                    param.paramValue = text;
                                }
                                else
                                {
                                    Console.WriteLine("Такого слада не существует, попробуйте ещё раз.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("Вы уверены что хотите выполнить действие? (1 - да, 2(или др.) - нет)?");
                            choice_ = (Console.ReadKey()).KeyChar.ToString();
                            if (choice_ == "1")
                                SqlHelper.PerfomProcedureNonResultOneParam("DeleteWarehouse", param);
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Название склада", "Название аптеки" };
                            SqlHelper.PerfomProcedureResult("SELECT wh.[id] as idWarehouse, wh.[Name] as WarehouseName, ph.[Name] as PharmacieName FROM Warehouses wh LEFT JOIN Pharmacies ph ON wh.idPharmacie = ph.Id", nameColumn, 76);
                            Console.ReadKey();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Партия
        /// </summary>
        private static void Party()
        {
            string choice = "";
            while (choice != "4")
            {
                Console.Clear();
                Logic.ListChoice(new List<string> { "Создать партию", "Удалить партию", "Просмотр партий", "Назад" });
                string choice_ = "";
                choice = (Console.ReadKey()).KeyChar.ToString();
                switch (choice)
                {
                    case "1":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Название товара" };
                            SqlHelper.PerfomProcedureResult("SELECT Id, Name FROM Products", nameColumn, 45);
                            Console.WriteLine();
                            List<SqlHelper.Param> params_ = new List<SqlHelper.Param> { };
                            SqlHelper.Param param = new SqlHelper.Param();
                            SqlHelper.AddParam("Напишите идентификатор товара", param, "@idProduct", false, true);
                            param.paramName = "@idProduct";
                            string text = Console.ReadLine();
                            if (int.TryParse(text, out int number))
                            {
                                if (SqlHelper.PerfomProcedureResult(String.Format("SELECT COUNT(*) FROM Products WHERE Id = {0}", text)) > 0)
                                {
                                    param.paramValue = text;
                                    params_.Add(param);
                                }
                                else
                                {
                                    Console.WriteLine("Такого товара не существует, попробуйте ещё раз.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                                Console.ReadKey();
                                break;
                            }

                            Console.Clear();
                            nameColumn = new List<string> { "Id", "Название склада" };
                            SqlHelper.PerfomProcedureResult("SELECT Id, Name FROM Warehouses", nameColumn, 45);
                            Console.WriteLine();
                            param = new SqlHelper.Param();
                            SqlHelper.AddParam("Напишите идентификатор склада", param, "@idWarehouse", false, true);
                            param.paramName = "@idWarehouse";
                            text = Console.ReadLine();
                            if (int.TryParse(text, out int number_))
                            {
                                if (SqlHelper.PerfomProcedureResult(String.Format("SELECT COUNT(*) FROM Warehouses WHERE Id = {0}", text)) > 0)
                                {
                                    param.paramValue = text;
                                    params_.Add(param);
                                }
                                else
                                {
                                    Console.WriteLine("Такого склада не существует, попробуйте ещё раз.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                                Console.ReadKey();
                                break;
                            }

                            Console.Clear();
                            SqlHelper.AddParam("Напишите кол-во товара в партии", param, "@Quantity", false, true);
                            param = new SqlHelper.Param();
                            param.paramName = "@Quantity";
                            text = Console.ReadLine();
                            if (int.TryParse(text, out int number__))
                            {
                                param.paramValue = text;
                                params_.Add(param);
                            }
                            else
                            {
                                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("Вы уверены что хотите выполнить действие? (1 - да, 2(или др.) - нет)?");
                            choice_ = (Console.ReadKey()).KeyChar.ToString();
                            if (choice_ == "1")
                                SqlHelper.PerfomProcedureNonResultSeveralParam("AddParty", params_);
                        }
                        break;
                    case "2":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Id склада", "Название склада", "Id товара", "Название продукта", "Количество" };
                            SqlHelper.PerfomProcedureResult("SELECT prt.id AS idParty, idWarehouse, wr.[Name] AS NameWarehouse, idProduct, pr.[Name] AS NameProduct, Quantity FROM Parties prt LEFT JOIN Products pr ON prt.idProduct = pr.Id LEFT JOIN Warehouses wr ON prt.idWarehouse = wr.Id", nameColumn, 76);
                            Console.WriteLine();
                            SqlHelper.Param param = new SqlHelper.Param();
                            SqlHelper.AddParam("Напишите идентификатор партии", param, "@id", false, true);
                            string text = Console.ReadLine();
                            if (int.TryParse(text, out int number_))
                            {
                                if (SqlHelper.PerfomProcedureResult(String.Format("SELECT COUNT(*) FROM Parties WHERE Id = {0}", text)) > 0)
                                {
                                    param.paramName = "@id";
                                    param.paramValue = text;
                                }
                                else
                                {
                                    Console.WriteLine("Такой партии не существует, попробуйте ещё раз.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("Вы уверены что хотите выполнить действие? (1 - да, 2(или др.) - нет)?");
                            choice_ = (Console.ReadKey()).KeyChar.ToString();
                            if (choice_ == "1")
                                SqlHelper.PerfomProcedureNonResultOneParam("DeleteParty", param);
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                            List<string> nameColumn = new List<string> { "Id", "Id склада", "Название склада", "Id товара", "Название продукта", "Количество" };
                            SqlHelper.PerfomProcedureResult("SELECT prt.id AS idParty, idWarehouse, wr.[Name] AS NameWarehouse, idProduct, pr.[Name] AS NameProduct, Quantity FROM Parties prt LEFT JOIN Products pr ON prt.idProduct = pr.Id LEFT JOIN Warehouses wr ON prt.idWarehouse = wr.Id", nameColumn, 110);
                            Console.ReadKey();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Вывести на экран весь список товаров и его количество в выбранной аптеке (количество товара во всех складах аптеки)
        /// </summary>
        private static void SelectProductByPharmacy()
        {
            Console.Clear();
            List<string> nameColumn = new List<string> { "Id", "Название аптеки" };
            SqlHelper.PerfomProcedureResult("SELECT Id, Name FROM Pharmacies", nameColumn, 45);
            Console.WriteLine();
            Console.WriteLine("Выберите аптеку: ");
            string text = Console.ReadLine();
            if (int.TryParse(text, out int number))
            {
                if (SqlHelper.PerfomProcedureResult(String.Format("SELECT COUNT(*) FROM Pharmacies WHERE Id = {0}", text)) > 0)
                {
                    Console.Clear();
                    nameColumn = new List<string> { "Название аптеки", "Название товара", "Количество товаров" };
                    SqlHelper.PerfomProcedureResult(String.Format("SELECT Pharmacie, Product, Quantity FROM MainView WHERE idPharmacie = {0}", text), nameColumn, 76);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Такой аптеки не существует, попробуйте ещё раз.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                Console.ReadKey();
            }
        }
    }
}
