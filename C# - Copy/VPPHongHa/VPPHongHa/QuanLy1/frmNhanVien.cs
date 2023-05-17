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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            getData();
        }

        public void getData()
        {
            //Truy vấn lấy dữ liệu
            try
            {
                //Khởi tạo collection 
                List<Model.NhanVien> data = new List<Model.NhanVien>();

                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();
                string Query = "select * from NhanVien";
                SqlCommand cmd = new SqlCommand(Query, conn);
                //Câu lệnh trả về nhiều giá trị của bảng Hóa Đơn => chọn phương thức executeReader để đọc dữ liệu
                SqlDataReader rd = cmd.ExecuteReader();
                //Sử dụng cấu trúc lặp
                while (rd.Read())
                {
                    //MessageBox.Show($"{(string)rd["MaHoaDon"]}");
                    Model.NhanVien obj = new Model.NhanVien();
                    obj.MaNhanVien = (string)rd["MaNhanVien"];
                    obj.TenNhanVien = (string)rd["TenNhanVien"];
                    obj.Tuoi = (string)rd["Tuoi"];
                    obj.QueQuan = (string)rd["QueQuan"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvNhanVien.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Xác định dòng người dùng đang click
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (row >= 0) //tránh trường hợp người dùng bấm vào tiêu đề của bảng
            {
                //Vô hiệu hóa textfield khóa chính
                txtMaNhanVien.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                //Đọc dữ liệu trên datagridview chuyển sâng phần thông tin
                txtMaNhanVien.Text = (string)dgvNhanVien.Rows[row].Cells["MaNhanVien"].Value;
                txtTenNhanVien.Text = (string)dgvNhanVien.Rows[row].Cells["TenNhanVien"].Value;
                txtTuoi.Text = (string)dgvNhanVien.Rows[row].Cells["Tuoi"].Value;
                txtQueQuan.Text = (string)dgvNhanVien.Rows[row].Cells["QueQuan"].Value;

            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
        }
        public void clearText()
        {
            txtMaNhanVien.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtTuoi.Text = "";
            txtQueQuan.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
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
                    string MaNhanVien = txtMaNhanVien.Text;
                    string TenNhanVien = txtTenNhanVien.Text;
                    string Tuoi = txtTuoi.Text; 
                    string QueQuan = txtQueQuan.Text;
                    string Query = $"Insert into NhanVien (MaNhanVien, TenNhanVien, Tuoi, QueQuan)VALUES(N'{MaNhanVien}', N'{TenNhanVien}', N'{Tuoi}', N'{QueQuan}')";
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
                string MaNhanVien = txtMaNhanVien.Text;
                string TenNhanVien = txtTenNhanVien.Text;
                string Tuoi = txtTuoi.Text;
                string QueQuan = txtQueQuan.Text;
                string Query = $"update nhanvien set tennhanvien = '{TenNhanVien}', tuoi ='{Tuoi}', quequan='{QueQuan}' where manhanvien='{MaNhanVien}'";
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
                string MaNhanVien = txtMaNhanVien.Text;
                string TenNhanVien = txtTenNhanVien.Text;
                string Tuoi = txtTuoi.Text;
                string QueQuan = txtQueQuan.Text;
                string Query = $"Delete from NhanVien where MaNhanVien = {txtMaNhanVien.Text}";
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

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            frmHeThong frm = new frmHeThong();
            frm.Show();
            this.Hide();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                //Khởi tạo collection 
                List<Model.NhanVien> data = new List<Model.NhanVien>();

                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();

                string Query = $"select * from NhanVien where MaNhanVien like '%{txtTimKiem.Text}%' ";
                SqlCommand cmd = new SqlCommand(Query, conn);
                //Câu lệnh trả về nhiều giá trị của bảng Hóa Đơn => chọn phương thức executeReader để đọc dữ liệu
                SqlDataReader rd = cmd.ExecuteReader();
                //Sử dụng cấu trúc lặp
                while (rd.Read())
                {
                    //MessageBox.Show($"{(string)rd["MaHoaDon"]}");
                    Model.NhanVien obj = new Model.NhanVien();
                    obj.MaNhanVien = (string)rd["MaNhanVien"];
                    obj.TenNhanVien = (string)rd["TenNhanVien"];
                    obj.QueQuan = (string)rd["QueQuan"];
                    obj.Tuoi = (string)rd["Tuoi"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvNhanVien.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
