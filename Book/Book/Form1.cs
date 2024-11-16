using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Book
{
    public partial class Form1 : Form
    {

        public static BooksService service = new BooksService(
            new BaseClientService.Initializer
            {
                ApplicationName = "GoogleBooksSearch",
                ApiKey = "AIzaSyDuZb_d5viCjglZHXKkqf_LG4O0h2CDsd0" // Thay bằng API Key của riêng mình
            });
        public static async Task<List<Volume>> SearchBooksByKeyword(string keyword)
        {
            try
            {
                var result = await service.Volumes.List(keyword).ExecuteAsync();
                if (result != null && result.Items != null)
                {
                    return result.Items.ToList();  // Trả về danh sách các cuốn sách
                }
                else
                {
                    return new List<Volume>();  // Trả về danh sách rỗng nếu không có kết quả
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Volume>();  // Nếu có lỗi, trả về danh sách rỗng
            }
        }

        public static async Task<string> SearchBookByISBN(string isbn)
        {
            try
            {
                var result = await service.Volumes.List($"isbn:{isbn}").ExecuteAsync();
                if (result != null && result.Items != null)
                {
                    var book = result.Items.FirstOrDefault();
                    return $"Tên sách: {book.VolumeInfo.Title}\n" + Environment.NewLine +
                           $"Tác giả: {string.Join(", ", book.VolumeInfo.Authors ?? new string[] { "Không rõ" })}\n" + Environment.NewLine +
                           $"Nhà xuất bản: {book.VolumeInfo.Publisher}\n" + Environment.NewLine +
                           $"Mô tả: {book.VolumeInfo.Description}";
                }
                else
                {
                    return "Không tìm thấy sách.";
                }
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void ISBN_text_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Search_btn_Click(object sender, EventArgs e)
        {
            string keyword = ISBN_text.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Book_detail.Text = "Đang tìm kiếm...";
            var books = await SearchBooksByKeyword(keyword);

            // Kiểm tra nếu có sách, hiển thị trong DataGridView
            if (books.Any())
            {
                //--- Dữ liệu hiển thị trong DataGridView
                dgvBooks.DataSource = books.Select(book => new
                {
                    Title = book.VolumeInfo.Title,
                    ISBN = book.VolumeInfo?.IndustryIdentifiers?.FirstOrDefault()?.Identifier ?? "Không có ISBN",
                    Book = book //--- Lưu toàn bộ đối tượng sách vào DataGridView để dễ dàng lấy chi tiết sau
                }).ToList();
            }
            else
            {
                MessageBox.Show("Không tìm thấy sách nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Book_detail_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng đã click vào một dòng
            if (e.RowIndex >= 0)
            {
                var selectedBook = dgvBooks.Rows[e.RowIndex].Cells["Book"].Value as Volume; // Truy cập đối tượng Volume từ cột ẩn
                if (selectedBook != null)
                {
                    // Hiển thị thông tin chi tiết cuốn sách trong ô Book_detail
                    Book_detail.Text = GetBookDetails(selectedBook).Result;
                }
            }
        }
        public static async Task<string> GetBookDetails(Volume book)
        {
            try
            {
                //--- Đây là nơi bạn hiển thị chi tiết sách. Sửa lại nếu muốn thay đổi format thông tin sách
                return $"Tên sách: {book.VolumeInfo.Title}\n" + Environment.NewLine +
                       $"Tác giả: {string.Join(", ", book.VolumeInfo.Authors ?? new string[] { "Không rõ" })}\n" + Environment.NewLine +
                       $"Nhà xuất bản: {book.VolumeInfo.Publisher}\n" + Environment.NewLine +
                       $"Mô tả: {book.VolumeInfo.Description ?? "Không có mô tả"}";
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }
        private void SetupDataGridView()
        {
            // Xóa tất cả các cột cũ (nếu có)
            dgvBooks.Columns.Clear();

            // Thêm các cột cần thiết
            dgvBooks.Columns.Add("Title", "Tên sách");
            dgvBooks.Columns.Add("ISBN", "Mã ISBN");

            // Thêm cột ẩn để lưu đối tượng sách (Volume)
            DataGridViewTextBoxColumn bookColumn = new DataGridViewTextBoxColumn();
            bookColumn.Name = "Book";  // Tên cột là "Book"
            bookColumn.Visible = false;  // Ẩn cột này
            dgvBooks.Columns.Add(bookColumn);

            // Tắt tính năng tự động tạo cột
            dgvBooks.AutoGenerateColumns = false;

            // Thiết lập các cột
            dgvBooks.Columns["Title"].DataPropertyName = "Title"; // Liên kết cột Title với thuộc tính Title trong đối tượng nguồn
            dgvBooks.Columns["ISBN"].DataPropertyName = "ISBN"; // Liên kết cột ISBN với thuộc tính ISBN trong đối tượng nguồn
            dgvBooks.Columns["Book"].DataPropertyName = "Book"; // Liên kết cột Book với đối tượng sách (Volume)

            // Các thiết lập khác cho DataGridView
            dgvBooks.ReadOnly = true; // Chỉ xem, không chỉnh sửa
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn toàn bộ hàng
            dgvBooks.AllowUserToAddRows = false; // Không cho phép thêm dòng mới
            dgvBooks.AllowUserToDeleteRows = false; // Không cho phép xóa dòng
        }
        private void SetupBookDetail()
        {
            // Đảm bảo rằng TextBox Book_detail có thể hiển thị nhiều dòng và có thanh cuộn dọc
            Book_detail.Multiline = true; // Cho phép nhiều dòng
            Book_detail.ScrollBars = ScrollBars.Vertical; // Thanh cuộn dọc khi nội dung quá dài
            Book_detail.WordWrap = true; // Cho phép ngắt dòng tự động
            Book_detail.ReadOnly = true; // Đảm bảo không chỉnh sửa được
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupDataGridView();

        }

        private void dgvBookshelf_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CreateBookshelfButton_Click(object sender, EventArgs e)
        {

        }

        private void Name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


