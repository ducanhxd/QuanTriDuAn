using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPPHongHa.QuanLy1
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            getData();
        }

        public void getData()
        {
            //Truy vấn lấy dữ liệu
            try
            {
                //Khởi tạo collection 
                List<Model.KhachHang> data = new List<Model.KhachHang>();

                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();
                string Query = "select * from KhachHang";
                SqlCommand cmd = new SqlCommand(Query, conn);
                //Câu lệnh trả về nhiều giá trị của bảng Hóa Đơn => chọn phương thức executeReader để đọc dữ liệu
                SqlDataReader rd = cmd.ExecuteReader();
                //Sử dụng cấu trúc lặp
                while (rd.Read())
                {
                    //MessageBox.Show($"{(string)rd["MaHoaDon"]}");
                    Model.KhachHang obj = new Model.KhachHang();
                    obj.MaKhachHang = (string)rd["MaKhachHang"];
                    obj.TenKhachHang = (string)rd["TenKhachHang"];
                    obj.SDT = (string)rd["SDT"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvKhachHang.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Xác định dòng người dùng đang click
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (row >= 0) //tránh trường hợp người dùng bấm vào tiêu đề của bảng
            {
                //Vô hiệu hóa textfield khóa chính
                txtMaKH.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                //Đọc dữ liệu trên datagridview chuyển sâng phần thông tin
                txtMaKH.Text = (string)dgvKhachHang.Rows[row].Cells["MaKhachHang"].Value;
                txtTenKH.Text = (string)dgvKhachHang.Rows[row].Cells["TenKhachHang"].Value;
                txtSDT.Text = (string)dgvKhachHang.Rows[row].Cells["SDT"].Value;

            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
        }

        public void clearText()
        {
            txtMaKH.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtSDT.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();
                string MaKhachHang = txtMaKH.Text;
                string TenKhachHang = txtTenKH.Text;
                string SDT = txtSDT.Text;
                string Query = $"Insert into KhachHang (MaKhachHang, TenKhachHang, SDT)VALUES(N'{MaKhachHang}', N'{TenKhachHang}', N'{SDT}')";
                SqlCommand cmd = new SqlCommand(Query, conn);
                int s1 = cmd.ExecuteNonQuery();
                if (s1 == 1) //Kiểm tra xem có bao nhiêu Row được thêm vào database, nếu s1 = 1 (1 row được thêm) thì thông báo là thêm mới thành công
                {
                    MessageBox.Show("Thêm mới thành công!");

                    //Load lại dữ liệu trong datagridview
                    getData();
                    clearText();
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();
                string MaKhachHang = txtMaKH.Text;
                string TenKhachHang = txtTenKH.Text;
                string SDT = txtSDT.Text;
                string Query = $"update KhachHang set TenKhachHang = '{TenKhachHang}', SDT='{SDT}' where MaKhachHang='{MaKhachHang}'";
                SqlCommand cmd = new SqlCommand(Query, conn);
                int s1 = cmd.ExecuteNonQuery();
                if (s1 == 1) //Kiểm tra xem có bao nhiêu Row được thêm vào database, nếu s1 = 1 (1 row được thêm) thì thông báo là thêm mới thành công
                {
                    MessageBox.Show("Sửa thành công!");

                    //Load lại dữ liệu trong datagridview
                    getData();
                    clearText();
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();
                string MaKhachHang = txtMaKH.Text;
                string TenKhachHang = txtTenKH.Text;
                string SDT = txtSDT.Text;
                string Query = $"Delete from KhachHang where MaKhachHang = {txtMaKH.Text}";
                SqlCommand cmd = new SqlCommand(Query, conn);
                int s1 = cmd.ExecuteNonQuery();
                if (s1 == 1) //Kiểm tra xem có bao nhiêu Row được thêm vào database, nếu s1 = 1 (1 row được thêm) thì thông báo là thêm mới thành công
                {
                    MessageBox.Show("Xóa thành công!");

                    //Load lại dữ liệu trong datagridview
                    getData();
                    clearText();
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                //Khởi tạo collection 
                List<Model.KhachHang> data = new List<Model.KhachHang>();

                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();

                string Query = $"select * from KhachHang where MaKhachHang like '%{txtTimKiem.Text}%' ";
                SqlCommand cmd = new SqlCommand(Query, conn);
                //Câu lệnh trả về nhiều giá trị của bảng Hóa Đơn => chọn phương thức executeReader để đọc dữ liệu
                SqlDataReader rd = cmd.ExecuteReader();
                //Sử dụng cấu trúc lặp
                while (rd.Read())
                {
                    Model.KhachHang obj = new Model.KhachHang();
                    obj.MaKhachHang = (string)rd["MaKhachHang"];
                    obj.TenKhachHang = (string)rd["TenKhachHang"];
                    obj.SDT = (string)rd["SDT"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvKhachHang.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            frmHeThong frm = new frmHeThong();
            frm.Show();
            this.Hide();
        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
