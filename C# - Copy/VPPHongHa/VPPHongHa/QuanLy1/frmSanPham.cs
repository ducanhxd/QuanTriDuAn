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
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            getData();
        }

        public void getData()
        {
            //Truy vấn lấy dữ liệu
            try
            {
                //Khởi tạo collection 
                List<Model.SanPham> data = new List<Model.SanPham>();

                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();
                string Query = "select * from SanPham";
                SqlCommand cmd = new SqlCommand(Query, conn);
                //Câu lệnh trả về nhiều giá trị của bảng Hóa Đơn => chọn phương thức executeReader để đọc dữ liệu
                SqlDataReader rd = cmd.ExecuteReader();
                //Sử dụng cấu trúc lặp
                while (rd.Read())
                {
                    //MessageBox.Show($"{(string)rd["MaHoaDon"]}");
                    Model.SanPham obj = new Model.SanPham();
                    obj.MaSanPham = (string)rd["MaSanPham"];
                    obj.TenSanPham = (string)rd["TenSanPham"];
                    obj.MaNCC = (string)rd["MaNCC"];
                    obj.GiaNhap = (string)rd["GiaNhap"];
                    obj.GiaBan = (string)rd["GiaBan"];
                    obj.SoLuong = (string)rd["SoLuong"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvSanPham.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Xác định dòng người dùng đang click
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (row >= 0) //tránh trường hợp người dùng bấm vào tiêu đề của bảng
            {
                //Vô hiệu hóa textfield khóa chính
                txtMaSanPham.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                //Đọc dữ liệu trên datagridview chuyển sâng phần thông tin
                txtMaSanPham.Text = (string)dgvSanPham.Rows[row].Cells["MaSanPham"].Value;
                txtTenSanPham.Text = (string)dgvSanPham.Rows[row].Cells["TenSanPham"].Value;
                txtMaNCC.Text = (string)dgvSanPham.Rows[row].Cells["MaNCC"].Value;
                txtGiaNhap.Text = (string)dgvSanPham.Rows[row].Cells["GiaNhap"].Value;
                txtGiaBan.Text = (string)dgvSanPham.Rows[row].Cells["GiaBan"].Value;
                txtSoLuong.Text = (string)dgvSanPham.Rows[row].Cells["SoLuong"].Value;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
        }

        public void clearText()
        {
            txtMaSanPham.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaSanPham.Text = "";
            txtMaNCC.Text = "";
            txtGiaNhap.Text = "";
            txtGiaBan.Text = "";
            txtSoLuong.Text =  "";
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
                    string MaSanPham = txtMaSanPham.Text;
                    string TenSanPham = txtTenSanPham.Text;
                    string MaNCC = txtMaNCC.Text;
                    string GiaNhap = txtGiaNhap.Text;
                    string GiaBan = txtGiaBan.Text;
                    string SoLuong = txtSoLuong.Text;
                    string Query = $"Insert into SanPham (MaSanPham, TenSanPham, MaNCC, GiaNhap, GiaBan, SoLuong)VALUES(N'{MaSanPham}', N'{TenSanPham}', N'{MaNCC}', N'{GiaNhap}', N'{GiaBan}', N'{SoLuong}')";
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
                string MaSanPham = txtMaSanPham.Text;
                string TenSanPham = txtTenSanPham.Text;
                string MaNCC = txtMaNCC.Text;
                string GiaNhap = txtGiaNhap.Text;
                string GiaBan = txtGiaBan.Text;
                string SoLuong = txtSoLuong.Text;
                string Query = $"update sanpham set tensanpham = '{TenSanPham}', mancc ='{MaNCC}', giaban='{GiaBan}', gianhap ='{GiaNhap}', soluong='{SoLuong}' where masanpham='{MaSanPham}'";
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
                string MaSanPham = txtMaSanPham.Text;
                string TenSanPham = txtTenSanPham.Text;
                string MaNCC = txtMaNCC.Text;
                string GiaNhap = txtGiaNhap.Text;
                string GiaBan = txtGiaBan.Text;
                string SoLuong = txtSoLuong.Text;
                string Query = $"Delete from SanPham where MaSanPham = {txtMaSanPham.Text}";
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
                List<Model.SanPham> data = new List<Model.SanPham>();

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
                    Model.SanPham obj = new Model.SanPham();
                    obj.MaSanPham = (string)rd["MaSanPham"];
                    obj.TenSanPham = (string)rd["TenSanPham"];
                    obj.MaNCC = (string)rd["MaNCC"];
                    obj.GiaNhap = (string)rd["GiaNhap"];
                    obj.GiaBan = (string)rd["GiaBan"];
                    obj.SoLuong = (string)rd["SoLuong"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvSanPham.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}

