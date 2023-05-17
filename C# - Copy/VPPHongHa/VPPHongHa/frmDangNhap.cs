using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //Thư viện sql

namespace VPPHongHa
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            { 
            //B1: tạo chuỗi kết nối
            string svName = "DESKTOP-T6LA3R6\\SQLEXPRESS";
            string dbName = "QLNV";
            /*string Sec = "SSPI";*/

            string userID = "sa";
            string passWord = "123ducanh";

            string con_str = $"Data Source={svName}; Initial Catalog={dbName}; User ID={userID}; Password ={passWord}";
            //B2: Tạo kết nối CDSL
            SqlConnection conn = new SqlConnection(con_str);
            //B3: Mở kết nối
            conn.Open();
            //B?.1: Tạo truy vấn
            string TaiKhoan = txtTaiKhoan.Text;
            string MatKhau = txtMatKhau.Text;
            string Query = $"select count(*) from NguoiDung where TaiKhoan = '{TaiKhoan}' and MatKhau = '{MatKhau}'";
            //B?.2: Tạo đối tượng để thực thi truy vấn
            SqlCommand cmd = new SqlCommand(Query, conn);
            //B?.3: Chọn phương thức thự thi và nhận kết quả trả về
            //Có 1 dữ liệu trả về duy nhất => executeScalar()
            int SoLuong = (int)cmd.ExecuteScalar();
            //B4: Đóng kết nối
            conn.Close();

            //Xác định trạng thái đăng nhập của người dùng đựa trên SoLuong row trả về
            if(SoLuong == 1)
            {
                MessageBox.Show("Đăng nhập thành công");
                //Mở giao diện của hệ thống lên
                frmHeThong frm = new frmHeThong();
                frm.Show();
                this.Hide();
            }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            frmDangKi frm = new frmDangKi();
            frm.Show();
            this.Hide();
        }
    }
}
//Thực thi truy vấn => Truy vấn thực thi là insert thì chọn ExecuteNonQuery()