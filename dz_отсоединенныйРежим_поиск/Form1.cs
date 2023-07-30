using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dz_отсоединенныйРежим_поиск
{
    public partial class Form1 : Form
    {
        DbLibrary db = null;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "Выберите, по какой категории будет осуществляться поиск:";
            label2.Text = "Введите параметр поиска:";
            button1.Text = "Найти";
        }

        // обработчик события - клака по кнопке Найти
        private void button1_Click(object sender, EventArgs e)
        {
            // в зависимости от выбранного пункта в КомбоБокс
            switch (comboBox1.SelectedIndex)
            {
                case 0:  // если выбран Поиск по автору
                    using (db = new DbLibrary())
                    {
                        dataGridView1.DataSource = null;   // очищаем таблицу от предыдущих данных
                        db.SearchByAuthor(textBox1.Text);  // вызов метода Поиск по автору
                        DataTable myTable = db.MyTable();  // создаем новый объект myTable и заполняем данными
                        dataGridView1.DataSource           // создаем привязку для dataGridView1 - источник 
                            = myTable.DefaultView;         // данных это объект myTable
                    }                 
                    break;
                case 1:  // если выбран поиск по Названию, аналогично case 0
                    using (db = new DbLibrary())
                    {                      
                        dataGridView1.DataSource = null;
                        db.SearchByTitle(textBox1.Text);
                        DataTable myTable1 = db.MyTable();
                        dataGridView1.DataSource = myTable1.DefaultView;
                    }                       
                    break;
                case 2:  // если выбран поиск по Категории, аналогично case 0
                    using (db = new DbLibrary())
                    {
                        dataGridView1.DataSource = null;
                        db.SearchByCategory(textBox1.Text);
                        DataTable myTable1 = db.MyTable();
                        dataGridView1.DataSource = myTable1.DefaultView;
                    }
                    break;
            }
        }
    }
}
