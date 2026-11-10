namespace Bookl_Manager_System
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new System.Windows.Forms.DataGridView();
            BtnAddBook = new System.Windows.Forms.Button();
            boxTitle = new System.Windows.Forms.TextBox();
            boxPrice = new System.Windows.Forms.TextBox();
            BtnRemoveBook = new System.Windows.Forms.Button();
            BtnUpdateBook = new System.Windows.Forms.Button();
            boxSearch = new System.Windows.Forms.TextBox();
            BtnSearchBook = new System.Windows.Forms.Button();
            BtnTCPConect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(388, 176);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 23;
            dataGridView1.Size = new System.Drawing.Size(280, 212);
            dataGridView1.TabIndex = 0;
            // 
            // BtnAddBook
            // 
            BtnAddBook.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            BtnAddBook.Location = new System.Drawing.Point(264, 238);
            BtnAddBook.Margin = new System.Windows.Forms.Padding(4);
            BtnAddBook.Name = "BtnAddBook";
            BtnAddBook.Size = new System.Drawing.Size(88, 33);
            BtnAddBook.TabIndex = 2;
            BtnAddBook.Text = "AddBook";
            BtnAddBook.UseVisualStyleBackColor = true;
            BtnAddBook.Click += BtnAddBook_Click;
            // 
            // boxTitle
            // 
            boxTitle.Location = new System.Drawing.Point(264, 176);
            boxTitle.Margin = new System.Windows.Forms.Padding(4);
            boxTitle.Name = "boxTitle";
            boxTitle.Size = new System.Drawing.Size(116, 23);
            boxTitle.TabIndex = 3;
            boxTitle.Text = "Title";
            // 
            // boxPrice
            // 
            boxPrice.Location = new System.Drawing.Point(264, 207);
            boxPrice.Margin = new System.Windows.Forms.Padding(4);
            boxPrice.Name = "boxPrice";
            boxPrice.Size = new System.Drawing.Size(116, 23);
            boxPrice.TabIndex = 4;
            boxPrice.Text = "Price";
            // 
            // BtnRemoveBook
            // 
            BtnRemoveBook.Location = new System.Drawing.Point(388, 396);
            BtnRemoveBook.Margin = new System.Windows.Forms.Padding(4);
            BtnRemoveBook.Name = "BtnRemoveBook";
            BtnRemoveBook.Size = new System.Drawing.Size(88, 27);
            BtnRemoveBook.TabIndex = 5;
            BtnRemoveBook.Text = "RemoveBook";
            BtnRemoveBook.UseVisualStyleBackColor = true;
            BtnRemoveBook.Click += BtnDeleteBook_Click;
            // 
            // BtnUpdateBook
            // 
            BtnUpdateBook.Location = new System.Drawing.Point(264, 279);
            BtnUpdateBook.Margin = new System.Windows.Forms.Padding(4);
            BtnUpdateBook.Name = "BtnUpdateBook";
            BtnUpdateBook.Size = new System.Drawing.Size(88, 33);
            BtnUpdateBook.TabIndex = 6;
            BtnUpdateBook.Text = "UpdateBook";
            BtnUpdateBook.UseVisualStyleBackColor = true;
            BtnUpdateBook.Click += BtnUpdateBook_Click;
            // 
            // boxSearch
            // 
            boxSearch.Location = new System.Drawing.Point(264, 324);
            boxSearch.Margin = new System.Windows.Forms.Padding(4);
            boxSearch.Name = "boxSearch";
            boxSearch.Size = new System.Drawing.Size(116, 23);
            boxSearch.TabIndex = 7;
            boxSearch.Text = "Search";
            // 
            // BtnSearchBook
            // 
            BtnSearchBook.Location = new System.Drawing.Point(264, 355);
            BtnSearchBook.Margin = new System.Windows.Forms.Padding(4);
            BtnSearchBook.Name = "BtnSearchBook";
            BtnSearchBook.Size = new System.Drawing.Size(88, 33);
            BtnSearchBook.TabIndex = 8;
            BtnSearchBook.Text = "SearchBook";
            BtnSearchBook.UseVisualStyleBackColor = true;
            BtnSearchBook.Click += BtnSearchBook_Click;
            // 
            // BtnTCPConect
            // 
            BtnTCPConect.Location = new System.Drawing.Point(676, 176);
            BtnTCPConect.Margin = new System.Windows.Forms.Padding(4);
            BtnTCPConect.Name = "BtnTCPConect";
            BtnTCPConect.Size = new System.Drawing.Size(117, 33);
            BtnTCPConect.TabIndex = 9;
            BtnTCPConect.Text = "TCPConect";
            BtnTCPConect.UseVisualStyleBackColor = true;
            BtnTCPConect.Click += BtnTCPConect_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1018, 700);
            Controls.Add(BtnTCPConect);
            Controls.Add(BtnSearchBook);
            Controls.Add(boxSearch);
            Controls.Add(BtnUpdateBook);
            Controls.Add(BtnRemoveBook);
            Controls.Add(boxPrice);
            Controls.Add(boxTitle);
            Controls.Add(BtnAddBook);
            Controls.Add(dataGridView1);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "Form1";
            Text = "X";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BtnAddBook;
        private System.Windows.Forms.TextBox boxTitle;
        private System.Windows.Forms.TextBox boxPrice;
        private System.Windows.Forms.Button BtnRemoveBook;
        private System.Windows.Forms.Button BtnUpdateBook;
        private System.Windows.Forms.TextBox boxSearch;
        private System.Windows.Forms.Button BtnSearchBook;
        private System.Windows.Forms.Button BtnTCPConect;
    }
}

