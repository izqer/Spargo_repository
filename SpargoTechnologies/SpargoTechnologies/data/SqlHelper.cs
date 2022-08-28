using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SpargoTechnologies
{
    class SqlHelper
    {
        /// <summary>
        /// Класс параметров для Sql
        /// </summary>
        public class Param
        {
            public string paramName { get; set; }
            public object paramValue { get; set; }
        }

        /// <summary>
        /// Строка подключения
        /// </summary>
        private static System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection();

        /// <summary>
        /// Открытие подключения
        /// </summary>
        private static void OpenSqlConnect()
        {
            try
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Ошибка с открытием подключения: {0}", ex.Message));
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Закрытие подключения
        /// </summary>
        private static void CloseSqlConnect()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Ошибка с закрытием подключения: {0}", ex.Message));
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Выполнение процедуры без вывода результата с несколькими параметрами
        /// </summary>
        /// <param name="command">Команда</param>
        public static void PerfomProcedureNonResultSeveralParam(string procedure, List<Param> param)
        {
            try
            {
                OpenSqlConnect();
                SqlCommand command = new SqlCommand(procedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < param.Count; i++)
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = param[i].paramName, Value = param[i].paramValue});
                }
                command.ExecuteNonQuery();
                CloseSqlConnect();
            }
            catch (Exception ex)
            {
                CloseSqlConnect();
                Console.WriteLine(String.Format("Ошибка с выполнение команды без результата: {0}", ex.Message));
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Выполнение процедуры без вывода результата с одним параметром
        /// </summary>
        /// <param name="command">Команда</param>
        public static void PerfomProcedureNonResultOneParam(string procedure, Param param)
        {
            try
            {
                OpenSqlConnect();
                SqlCommand command = new SqlCommand(procedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter { ParameterName = param.paramName, Value = param.paramValue, SqlDbType = SqlDbType.NVarChar });
                command.ExecuteNonQuery();
                CloseSqlConnect();
            }
            catch (Exception ex)
            {
                CloseSqlConnect();
                Console.WriteLine(String.Format("Ошибка с выполнение команды без результата: {0}", ex.Message));
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Выполнение команды с выводом результата с таблицей
        /// </summary>
        /// <param name="command">Команда</param>
        public static void PerfomProcedureResult(string command_, List<string> columns, int tableWidth)
        {
            try
            {
                OpenSqlConnect();
                SqlCommand command = new SqlCommand(command_, connection);
                List<string> list = new List<string> { };
                command.CommandType = CommandType.Text;
                var reader = command.ExecuteReader();
                PrintLine(tableWidth);
                PrintRow(tableWidth, columns);
                PrintLine(tableWidth);
                while (reader.Read())
                {
                    list = new List<string> { };
                    for (int i = 0; i < columns.Count; i++)
                    {
                        list.Add(reader[i].ToString());
                    }
                    PrintRow(tableWidth, list);
                }
                PrintLine(tableWidth);
                CloseSqlConnect();
            }
            catch (Exception ex)
            {
                CloseSqlConnect();
                Console.WriteLine(String.Format("Ошибка с выполнение команды c выводом результата с таблицей: {0}", ex.Message));
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Узнать количество элементов в таблице
        /// </summary>
        /// <param name="command">Команда</param>
        public static int PerfomProcedureResult(string command_)
        {
            try
            {
                OpenSqlConnect();
                SqlCommand command = new SqlCommand(command_, connection);
                List<string> list = new List<string> { };
                command.CommandType = CommandType.Text;
                int count = (int)command.ExecuteScalar();
                CloseSqlConnect();
                return count;
            }
            catch (Exception ex)
            {
                CloseSqlConnect();
                Console.WriteLine(String.Format("Ошибка с выполнение команды c выводом результата без таблицы: {0}", ex.Message));
                Console.ReadKey();
                return -1;
            }
        }

        /// <summary>
        /// Линия
        /// </summary>
        /// <param name="tableWidth">Размер таблицы</param>
        static void PrintLine(int tableWidth)
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        /// <summary>
        /// Строка
        /// </summary>
        /// <param name="tableWidth">Размер таблицы</param>
        /// <param name="columns">Колонки</param>
        static void PrintRow(int tableWidth, List<string> list)
        {
            string[] columns = list.ToArray();
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        /// <summary>
        /// Центировать текст
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="width">Ширина</param>
        /// <returns></returns>
        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        /// <summary>
        /// Добавить параметр в лист параметров
        /// </summary>
        /// <param name="helpText">Текст для вывода</param>
        /// <param name="param">Список параметров</param>
        /// <param name="name">Имя параметра</param>
        public static void AddListParam(string helpText, List<SqlHelper.Param> param, string name, bool isClear = true)
        {
            if(isClear)
                Console.Clear();
            Console.WriteLine(String.Format("{0}:", helpText));
            param.Add(new SqlHelper.Param { paramName = name, paramValue = Console.ReadLine() });
        }

        /// <summary>
        /// Добавить параметр
        /// </summary>
        /// <param name="helpText">Текст для вывода</param>
        /// <param name="param">Список параметров</param>
        /// <param name="name">Имя параметра</param>
        public static void AddParam(string helpText, SqlHelper.Param param, string name, bool isClear = true, bool isFk = false)
        {
            if(isClear)
                Console.Clear();
            Console.WriteLine(String.Format("{0}:", helpText));
            if (!isFk)
            {
                param.paramName = name;
                param.paramValue = Console.ReadLine();
            }
        }
    }
}
