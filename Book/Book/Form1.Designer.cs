namespace Book
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ISBN_text = new TextBox();
            Search_btn = new Button();
            Book_detail = new TextBox();
            label1 = new Label();
            label2 = new Label();
            dgvBooks = new DataGridView();
            Title = new DataGridViewTextBoxColumn();
            Authors = new DataGridViewTextBoxColumn();
            Publisher = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            CreateBookshelfButton = new Button();
            AddToBookshelfButton = new Button();
            RemoveFromBookshelfButton = new Button();
            dgvBookshelf = new DataGridView();
            NameBook = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBookshelf).BeginInit();
            SuspendLayout();
            // 
            // ISBN_text
            // 
            ISBN_text.Location = new Point(267, 12);
            ISBN_text.Name = "ISBN_text";
            ISBN_text.Size = new Size(176, 27);
            ISBN_text.TabIndex = 0;
            ISBN_text.TextChanged += ISBN_text_TextChanged;
            // 
            // Search_btn
            // 
            Search_btn.Location = new Point(322, 45);
            Search_btn.Name = "Search_btn";
            Search_btn.Size = new Size(94, 29);
            Search_btn.TabIndex = 1;
            Search_btn.Text = "Search";
            Search_btn.UseVisualStyleBackColor = true;
            Search_btn.Click += Search_btn_Click;
            // 
            // Book_detail
            // 
            Book_detail.Location = new Point(12, 113);
            Book_detail.Multiline = true;
            Book_detail.Name = "Book_detail";
            Book_detail.Size = new Size(454, 91);
            Book_detail.TabIndex = 2;
            Book_detail.TextChanged += Book_detail_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(240, 20);
            label1.TabIndex = 3;
            label1.Text = "Nhập mã từ khóa để tìm kiếm sách";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 78);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 4;
            label2.Text = "Chi tiết sách";
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Columns.AddRange(new DataGridViewColumn[] { Title, Authors, Publisher, Description });
            dgvBooks.Location = new Point(12, 232);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.Size = new Size(556, 188);
            dgvBooks.TabIndex = 5;
            dgvBooks.CellContentClick += dgvBooks_CellContentClick;
            // 
            // Title
            // 
            Title.HeaderText = "Title";
            Title.MinimumWidth = 6;
            Title.Name = "Title";
            Title.ReadOnly = true;
            Title.Width = 125;
            // 
            // Authors
            // 
            Authors.HeaderText = "Authors";
            Authors.MinimumWidth = 6;
            Authors.Name = "Authors";
            Authors.ReadOnly = true;
            Authors.Width = 125;
            // 
            // Publisher
            // 
            Publisher.HeaderText = "Publisher";
            Publisher.MinimumWidth = 6;
            Publisher.Name = "Publisher";
            Publisher.ReadOnly = true;
            Publisher.Width = 125;
            // 
            // Description
            // 
            Description.HeaderText = "Description";
            Description.MinimumWidth = 6;
            Description.Name = "Description";
            Description.ReadOnly = true;
            Description.Width = 125;
            // 
            // CreateBookshelfButton
            // 
            CreateBookshelfButton.Location = new Point(929, 67);
            CreateBookshelfButton.Name = "CreateBookshelfButton";
            CreateBookshelfButton.Size = new Size(94, 43);
            CreateBookshelfButton.TabIndex = 3;
            CreateBookshelfButton.Text = "Create";
            CreateBookshelfButton.Click += CreateBookshelfButton_Click;
            // 
            // AddToBookshelfButton
            // 
            AddToBookshelfButton.Location = new Point(711, 172);
            AddToBookshelfButton.Name = "AddToBookshelfButton";
            AddToBookshelfButton.Size = new Size(98, 32);
            AddToBookshelfButton.TabIndex = 2;
            AddToBookshelfButton.Text = "Add";
            // 
            // RemoveFromBookshelfButton
            // 
            RemoveFromBookshelfButton.Location = new Point(909, 171);
            RemoveFromBookshelfButton.Name = "RemoveFromBookshelfButton";
            RemoveFromBookshelfButton.Size = new Size(81, 33);
            RemoveFromBookshelfButton.TabIndex = 1;
            RemoveFromBookshelfButton.Text = "Remove";
            // 
            // dgvBookshelf
            // 
            dgvBookshelf.ColumnHeadersHeight = 29;
            dgvBookshelf.Location = new Point(680, 232);
            dgvBookshelf.Name = "dgvBookshelf";
            dgvBookshelf.RowHeadersWidth = 51;
            dgvBookshelf.Size = new Size(355, 188);
            dgvBookshelf.TabIndex = 0;
            dgvBookshelf.CellContentClick += dgvBookshelf_CellContentClick;
            // 
            // NameBook
            // 
            NameBook.Location = new Point(711, 75);
            NameBook.Name = "NameBook";
            NameBook.Size = new Size(125, 27);
            NameBook.TabIndex = 6;
            NameBook.TextChanged += Name_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 450);
            Controls.Add(NameBook);
            Controls.Add(dgvBookshelf);
            Controls.Add(RemoveFromBookshelfButton);
            Controls.Add(AddToBookshelfButton);
            Controls.Add(CreateBookshelfButton);
            Controls.Add(dgvBooks);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Book_detail);
            Controls.Add(Search_btn);
            Controls.Add(ISBN_text);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBookshelf).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ISBN_text;
        private Button Search_btn;
        private TextBox Book_detail;
        private Label label1;
        private Label label2;
        private DataGridView dgvBooks;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn Authors;
        private DataGridViewTextBoxColumn Publisher;
        private DataGridViewTextBoxColumn Description;
        private Button CreateBookshelfButton;
        private Button AddToBookshelfButton;
        private Button RemoveFromBookshelfButton;
        private DataGridView dgvBookshelf;
        private TextBox NameBook;
    }
}
