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
    public partial class frmNhaCungCap : Form
    {
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            getData();
        }
        public void getData()
        {
            //Truy vấn lấy dữ liệu
            try
            {
                //Khởi tạo collection 
                List<Model.NhaCungCap> data = new List<Model.NhaCungCap>();

                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();
                string Query = "select * from NhaCungCap";
                SqlCommand cmd = new SqlCommand(Query, conn);
                //Câu lệnh trả về nhiều giá trị của bảng Hóa Đơn => chọn phương thức executeReader để đọc dữ liệu
                SqlDataReader rd = cmd.ExecuteReader();
                //Sử dụng cấu trúc lặp
                while (rd.Read())
                {
                    //MessageBox.Show($"{(string)rd["MaHoaDon"]}");
                    Model.NhaCungCap obj = new Model.NhaCungCap();
                    obj.MaNCC = (string)rd["MaNCC"];
                    obj.TenNCC = (string)rd["TenNCC"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvNhaCungCap.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Xác định dòng người dùng đang click
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (row >= 0) //tránh trường hợp người dùng bấm vào tiêu đề của bảng
            {
                //Vô hiệu hóa textfield khóa chính
                txtMaNCC.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                //Đọc dữ liệu trên datagridview chuyển sâng phần thông tin
                txtMaNCC.Text = (string)dgvNhaCungCap.Rows[row].Cells["MaNCC"].Value;
                txtTenNCC.Text = (string)dgvNhaCungCap.Rows[row].Cells["TenNCC"].Value;

            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
        }
        public void clearText()
        {
            txtMaNCC.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
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
                string MaNCC = txtMaNCC.Text;
                string TenNCC = txtTenNCC.Text;
                string Query = $"Insert into NhaCungCap (MaNCC, TenNCC)VALUES(N'{MaNCC}', N'{TenNCC}')";
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
                string MaNCC = txtMaNCC.Text;
                string TenNCC = txtTenNCC.Text;
                string Query = $"update NhaCungCap set tenncc = '{TenNCC}' where mancc='{MaNCC}'";
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
                string MaNCC = txtMaNCC.Text;
                string TenNCC = txtTenNCC.Text;
                string Query = $"Delete from NhaCungCap where MaNCC = {txtMaNCC.Text}";
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
                List<Model.NhaCungCap> data = new List<Model.NhaCungCap>();

                string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
                string dbName = "QLNV";

                string userID = "sa";
                string passWord = "123ducanh";

                string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
                SqlConnection conn = new SqlConnection(con_str);
                conn.Open();

                string Query = $"select * from NhaCungCap where MaNCC like '%{txtTimKiem.Text}%' ";
                SqlCommand cmd = new SqlCommand(Query, conn);
                //Câu lệnh trả về nhiều giá trị của bảng Hóa Đơn => chọn phương thức executeReader để đọc dữ liệu
                SqlDataReader rd = cmd.ExecuteReader();
                //Sử dụng cấu trúc lặp
                while (rd.Read())
                {
                    //MessageBox.Show($"{(string)rd["MaHoaDon"]}");
                    Model.NhaCungCap obj = new Model.NhaCungCap();
                    obj.MaNCC = (string)rd["MaNCC"];
                    obj.TenNCC = (string)rd["TenNCC"];
                    data.Add(obj); //lưu vào data

                }
                conn.Close();

                //hiển thị lên datagridview
                dgvNhaCungCap.DataSource = data;
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
    }
}
