using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace dz_отсоединенныйРежим_поиск
{
    internal class DbLibrary : DataContext
    {
        // создаем объект Соединение
        SqlConnection connection = new SqlConnection();
        // создаем объект класса DataSet - контейнер для хранения
        // данных из БД 
        DataSet dataSet = new DataSet();
        // создаем объект класса SqlDataAdapter - для работы
        // отсоединенного режима
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
       
        public DbLibrary() : base(ConfigurationManager.ConnectionStrings["library"].ConnectionString)
        {
            // синициализация свойства ConnectionString подключения
            // строкой подключения из конфигурационного файла
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
        }

        // метод, который возвращает таблицу с данными DataTable
        public DataTable MyTable()
        {
            dataAdapter.Fill(dataSet);     // получение данных из БД
                                           // Fill(dataSet) выполняет запрос, занесенный 
                                           // свойство SelectCommand и заносит данные в dataSet
            if (dataSet.Tables.Count== 0)  // если таблице не загружены (нет данных в БД)
                return null;               // возвращаем пустую ссылку
            return dataSet.Tables[0];      // иначе возвращаем таблицу с данными
        }

        // метод для поиска книг по автору
        public void SearchByAuthor(string surname)
        {
            // создание строки-запроса в БД
            string cmdText = $"select * from Authors where LastName like'%{surname}%'";
            // создание объекта-команды, инициализация его подключением и запросом
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            // инициализируем свойство SelectCommand класса dataAdapter
            dataAdapter.SelectCommand = cmd;
        }

        // метод для поиска книг по названию
        public void SearchByTitle(string title)
        {
            // создание строки-запроса в БД
            string cmdText = $"select * from Books where Name like'%{title}%'";
            // создание объекта-команды, инициализация его подключением и запросом
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            // инициализируем свойство SelectCommand класса dataAdapter
            dataAdapter.SelectCommand = cmd;
            
        }

        // метод для поиска книг по категории
        public void SearchByCategory(string category)
        {
            // создание строки-запроса в БД
            string cmdText = $"select * from Books, Categories " +
                $"where Id_Category = Categories.Id and Categories.Name like '%{category}%'";
            // создание объекта-команды, инициализация его подключением и запросом
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            // инициализируем свойство SelectCommand класса dataAdapter
            dataAdapter.SelectCommand = cmd;
        }
    }
}
