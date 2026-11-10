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
        public string msg;
        public delegate string MsgHandler(string msg);
        public event MsgHandler OnmsgReceived;

        public Form1()
        {
            InitializeComponent();
            OnmsgReceived += ShowReceived;
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
            var index = dataGridView1.CurrentRow.Index;
            bookService.UpdateBook(index,title, price);
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
        private Task _tcpTask;

        private CancellationTokenSource _cts;
        private void BtnTCPConect_Click(object sender, EventArgs e)
        {
            // 1. 检查有没有在跑，如果在跑，就发停止信号并退出。
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
                BtnTCPConect.Text = "启动TCP连接";
                return;
            }
            // 2. 如果没在跑，创建一个取消令牌，修改按钮文字。
            _cts = new();
            var token = _cts.Token;
            BtnTCPConect.Text = "停止TCP连接";
            // 3. 开启后台 Task.Run：
            // 【这部分是脚手架，永远只管复制，绝不手敲】
            var ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            _tcpTask = Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    try
                    {
                        using (TcpClient tcpClient = new TcpClient())
                        {
                            await tcpClient.ConnectAsync(ipEndPoint);
                            await using NetworkStream stream = tcpClient.GetStream();
                            var buffer = new byte[1024];
                            while (true)
                            {
                                var received = await stream.ReadAsync(buffer, token);
                                if (received == 0) break;

                                var msg = Encoding.UTF8.GetString(buffer, 0, received);
                                string[] parts = msg.Split(",");
                                if (parts.Length == 2)
                                {
                                    var title = parts[0];
                                    var price = double.Parse(parts[1]);
                                    var bookId = bookService.GetBooks().Max(b => b.bookId) + 1;
                                    var book = new Books { bookId = bookId, bookName = title, price = price };
                                    this.Invoke(new Action(() =>
                                    {
                                        bookService.AddBook(book);
                                        dataGridView1.DataSource = null;
                                        dataGridView1.DataSource = bookService.GetBooks();
                                    }));
                                }
                            }
                        }
                    }
                    catch { }
                    await Task.Delay(3000);
                }
            });

        }
    }
}
