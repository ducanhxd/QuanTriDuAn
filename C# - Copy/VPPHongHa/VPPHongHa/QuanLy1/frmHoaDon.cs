using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using VPPHongHa.Model;


namespace VPPHongHa.QuanLy1
{
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            getData();
        }

        public void getData()
        {
            //Truy vấn lấy dữ liệu
            try
            {
                //Khởi tạo collection 
                List<Model.HoaDon> data = new List<Model.HoaDon>();

                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();
                string Query = "select * from HoaDon";
                SqlCommand cmd = new SqlCommand(Query, conn);
                //Câu lệnh trả về nhiều giá trị của bảng Hóa Đơn => chọn phương thức executeReader để đọc dữ liệu
                SqlDataReader rd = cmd.ExecuteReader();
                //Sử dụng cấu trúc lặp
                while (rd.Read())
                {
                    //MessageBox.Show($"{(string)rd["MaHoaDon"]}");
                    Model.HoaDon obj = new Model.HoaDon();
                    obj.MaHoaDon = (string)rd["MaHoaDon"];
                    obj.MaSanPham = (string)rd["MaSanPham"];
                    obj.MaNhanVien = (string)rd["MaNhanVien"];
                    obj.MaKhachHang = (string)rd["MaKhachHang"];
                    obj.GiaTien = (string)rd["GiaTien"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvHoaDon.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Xác định dòng người dùng đang click
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (row >= 0) //tránh trường hợp người dùng bấm vào tiêu đề của bảng
            {
                //Vô hiệu hóa textfield khóa chính
                txtMaHoaDon.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                //Đọc dữ liệu trên datagridview chuyển sâng phần thông tin
                txtMaHoaDon.Text = (string)dgvHoaDon.Rows[row].Cells["MaHoaDon"].Value;
                txtMaSanPham.Text = (string)dgvHoaDon.Rows[row].Cells["MaSanPham"].Value;
                txtMaNhanVien.Text = (string)dgvHoaDon.Rows[row].Cells["MaNhanVien"].Value;
                txtMaKhachHang.Text = (string)dgvHoaDon.Rows[row].Cells["MaKhachHang"].Value;
                txtGiaTien.Text = (string)dgvHoaDon.Rows[row].Cells["GiaTien"].Value;

            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
        }

        public void clearText()
        {
            txtMaHoaDon.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaHoaDon.Text = "";
            txtMaSanPham.Text = "";
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
                string MaHoaDon = txtMaHoaDon.Text;
                string MaSanPham = txtMaSanPham.Text;
                string MaNhanVien = txtMaNhanVien.Text;
                string MaKhachHang = txtMaKhachHang.Text;
                string GiaTien = txtGiaTien.Text;
                string Query = $"Insert into HoaDon (MaHoaDon, MaSanPham, MaNhanVien, MaKhachHang, GiaTien)VALUES(N'{MaHoaDon}', N'{MaSanPham}', N'{MaNhanVien}',N'{MaKhachHang}', N'{GiaTien}')";
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
                string MaHoaDon = txtMaHoaDon.Text;
                string MaSanPham = txtMaSanPham.Text;
                string MaNhanVien = txtMaNhanVien.Text;
                string MaKhachHang = txtMaKhachHang.Text;
                string GiaTien = txtGiaTien.Text;
                string Query = $"update hoadon set masanpham = '{MaSanPham}', manhanvien='{MaNhanVien}', makhachhang='{MaKhachHang}'  ,giatien = '{GiaTien}' where mahoadon='{MaHoaDon}'";
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
                string MaHoaDon = txtMaHoaDon.Text;
                string MaSanPham = txtMaSanPham.Text;
                string MaNhanVien = txtMaNhanVien.Text;
                string MaKhachHang = txtMaKhachHang.Text;
                string GiaTien = txtGiaTien.Text;
                string Query = $"Delete from HoaDon where MaHoaDon = {txtMaHoaDon.Text}";
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
                List<Model.HoaDon> data = new List<Model.HoaDon>();

                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();

                string Query = $"select * from HoaDon where mahoadon like '%{txtTimKiem.Text}%' ";
                SqlCommand cmd = new SqlCommand(Query, conn);
                //Câu lệnh trả về nhiều giá trị của bảng Hóa Đơn => chọn phương thức executeReader để đọc dữ liệu
                SqlDataReader rd = cmd.ExecuteReader();
                //Sử dụng cấu trúc lặp
                while (rd.Read())
                {
                    //MessageBox.Show($"{(string)rd["MaHoaDon"]}");
                    Model.HoaDon obj = new Model.HoaDon();
                    obj.MaHoaDon = (string)rd["MaHoaDon"];
                    obj.MaSanPham = (string)rd["MaSanPham"];
                    obj.MaNhanVien = (string)rd["MaNhanVien"];
                    obj.MaKhachHang = (string)rd["MaKhachHang"];
                    obj.GiaTien = (string)rd["GiaTien"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvHoaDon.DataSource = data;
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

        private void txtMaHoaDon_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

