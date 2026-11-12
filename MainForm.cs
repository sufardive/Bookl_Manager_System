using Bookl_Manager_System.Data;
using Bookl_Manager_System.Models;
using Bookl_Manager_System.Services;
using Microsoft.EntityFrameworkCore;
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


namespace Bookl_Manager_System
{
    public partial class MainForm : Form
    {
        private readonly BookService bookService;
        private readonly TcpClientService tcpClientService = new();

        public MainForm(DbContextOptions<AppDbContext> contextOptions)
        {
            InitializeComponent();
            bookService = new BookService(contextOptions);
            tcpClientService.BookReceived += TcpClientService_BookReceived;
            this.Load += MainForm_Load;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await RefreshDataGridView();
        }

        public async Task RefreshDataGridView()
        {
            var bookList = await bookService.GetBooks();
            // 启动时直接显示在表格上
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bookList;
        }


        private async void BtnAddBook_Click(object sender, EventArgs e)
        {
            var book = new Books();
            if (boxTitle.Text == "" || boxPrice.Text == "") return;
            book.Title = boxTitle.Text;
            book.Price = Convert.ToDouble(boxPrice.Text);
            await bookService.AddBook(book);
            await RefreshDataGridView();
        }

        public void TcpClientService_BookReceived(string title, double price) {
            BeginInvoke(new Action(async() =>
            { 
              var book = new Books{ Title = title, Price = price };
                await bookService.AddBook(book);
                await RefreshDataGridView();
            }));
        }

        private async void BtnDeleteBook_Click(object sender, EventArgs e)
        {
            List<Books> Books =await bookService.GetBooks();
            if (Books.Count == 0) return;
            var book = dataGridView1.CurrentRow.DataBoundItem as Books;
            int id = book.Id;
            await bookService.RemoveBook(id);
            await RefreshDataGridView();
        }

        private async void BtnUpdateBook_Click(object sender, EventArgs e)
        {
            if (boxTitle.Text == "" || boxPrice.Text == "") return;
            if (!double.TryParse(boxPrice.Text, out double price))
            {
                MessageBox.Show("you input a eroor number");
                return;
            }
            var title = boxTitle.Text;
            var book = dataGridView1.CurrentRow.DataBoundItem as Books; ;
            await bookService.UpdateBook(book.Id, title, price);
            await RefreshDataGridView();
        }

        private async void BtnSearchBook_Click(object sender, EventArgs e)
        {
            if (boxSearch.Text == "")
            {
                await RefreshDataGridView();
                return;
            }
            if (!int.TryParse(boxSearch.Text, out int id))
            {
                MessageBox.Show("please input number");
                return;
            }
            var book = await bookService.SearcheBook(id);
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
