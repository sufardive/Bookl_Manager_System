using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bookl_Manager_System.Models;
using Bookl_Manager_System.Services;


namespace Bookl_Manager_System
{
    public partial class Form1 : Form
    {
        private BookService bookService = new BookService();
        private TcpClientService tcpClientService = new();
        public string msg;
        public delegate string MsgHandler(string msg);
        public event MsgHandler OnmsgReceived;

        public Form1()
        {
            InitializeComponent();
            OnmsgReceived += ShowReceived;
            tcpClientService.BookReceived += TcpClientService_BookReceived;
            InitializeBooks();
            RefreshDataGridView();
        }
        public string ShowReceived(string msg)
        {
            MessageBox.Show(msg);
            return "";
        }

        public void InitializeBooks()
        {
            bookService.AddBook(new Books
            {
                bookName = "C#图解教程",
                price = 35.2
            });

            bookService.AddBook(new Books
            {
                bookName = "Sql必知必会",
                price = 37.1
            });

            bookService.AddBook(new Books
            {
                bookName = "深入理解计算机系统",
                price = 99.0
            });
        }

        public void RefreshDataGridView()
        {
            var bookList = bookService.GetBooks();
            // 启动时直接显示在表格上
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bookList;
        }


        private void BtnAddBook_Click(object sender, EventArgs e)
        {
            var book = new Books();
            if (boxTitle.Text == "" || boxPrice.Text == "") return;
            book.bookName = boxTitle.Text;
            book.price = Convert.ToDouble(boxPrice.Text);
            bookService.AddBook(book);
            RefreshDataGridView();
        }

        public void TcpClientService_BookReceived(string title, double price) {
            BeginInvoke(new Action(() =>
            { 
              var book = new Books{ bookName = title, price = price };
                bookService.AddBook(book);
                RefreshDataGridView() ;
            }));
        }

        private void BtnDeleteBook_Click(object sender, EventArgs e)
        {
            if (bookService.GetBooks().Count == 0) return;
            var book = dataGridView1.CurrentRow.DataBoundItem as Books;
            int id = book.bookId;
            bookService.RemoveBook(id);
            RefreshDataGridView();
        }

        private void BtnUpdateBook_Click(object sender, EventArgs e)
        {
            if (boxTitle.Text == "" || boxPrice.Text == "") return;
            if (!double.TryParse(boxPrice.Text, out double price))
            {
                MessageBox.Show("you input a eroor number");
                return;
            }
            var title = boxTitle.Text;
            var book = dataGridView1.CurrentRow.DataBoundItem as Books; ;
            bookService.UpdateBook(book.bookId, title, price);
            RefreshDataGridView();
        }

        private void BtnSearchBook_Click(object sender, EventArgs e)
        {
            if (boxSearch.Text == "")
            {
                RefreshDataGridView();
                return;
            }
            if (!int.TryParse(boxSearch.Text, out int id))
            {
                MessageBox.Show("please input number");
                return;
            }
            var book = bookService.SearcheBook(id);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new List<Books> { book }; ;
            //RefreshDataGridView();
        }
        private async void BtnTCPConect_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClientService.IsStarted)
                {
                    await tcpClientService.StopAsync();
                    BtnTCPConect.Text = "启动TCP连接";
                }
                else
                {
                    tcpClientService.Start();
                    BtnTCPConect.Text = "停止TCP连接";
                }
            }
            catch
            {

            }
        }
    }
}
