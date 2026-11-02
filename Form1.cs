using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookl_Manager_System
{
    public partial class Form1 : Form
    {
        List<Books> bookList = new List<Books>();

        public Form1()
        {
            InitializeComponent();
            var book1 = new Books();
            book1.bookId = 1;
            book1.bookName = "C#图解教程";
            book1.price = 35.2;

            var book2 = new Books();
            book2.bookId = 2;
            book2.bookName = "Sql必知必会";
            book2.price = 37.1;

            var book3 = new Books();
            book3.bookId = 3;
            book3.bookName = "深入理解计算机系统";
            book3.price = 99.0;

            // 装进列表
            bookList.Add(book1);
            bookList.Add(book2);
            bookList.Add(book3);

            // 启动时直接显示在表格上
            dataGridView1.DataSource = bookList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var book1 = new Books();
            book1.bookId = 1;
            book1.bookName = "C#图解教程";
            book1.price = 35.2;
            var book2 = new Books();
            book2.bookId = 2;
            book2.bookName = "Sql必知必会";
            book2.price = 37.1;
            bookList.Add(book1);
            bookList.Add(book2);
            dataGridView1.DataSource = bookList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var book = new Books();
            book.bookId = bookList.Count + 1;
            if (textBox1.Text == "" || textBox2.Text == "") return;
            book.bookName = textBox1.Text;
            book.price = Convert.ToDouble(textBox2.Text);
            bookList.Add(book);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bookList;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (bookList.Count == 0) return;
            var index = dataGridView1.CurrentRow.Index;
            bookList.RemoveAt(index);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bookList;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var book = new Books();
            if (textBox1.Text == "" || textBox2.Text == "") return;
            var index = dataGridView1.CurrentRow.Index;
            book = bookList[index];
            book.bookName = textBox1.Text;
            book.price = Convert.ToDouble(textBox2.Text);
            bookList[index] = book;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bookList;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Books> seacrList = new List<Books>();
            int count = bookList.Count;
            var id = int.Parse(textBox3.Text);
            if(id > count)
            {
                MessageBox.Show("id不存在");
                return;
            }
            var book = bookList[id-1];
            seacrList.Add(book);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = seacrList;
        }
    }

    public class Books
    {
        public int bookId { get; set; }
        public string bookName { get; set; }
        public double price { get; set; }
    }


}
