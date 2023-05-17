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
using VPPHongHa.Model;

namespace VPPHongHa
{
    public partial class frmDangKi : Form
    {
        public frmDangKi()
        {
            InitializeComponent();
        }

        private void btnDangKi_Click(object sender, EventArgs e)
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
                    string TaiKhoan = txtTaiKhoan.Text;
                    string MatKhau = txtMatKhau.Text;
                    string Query = $"Insert into NguoiDung (TaiKhoan, MatKhau)VALUES(N'{TaiKhoan}', N'{MatKhau}')";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    int s1 = cmd.ExecuteNonQuery();
                    if (s1 == 1) //Kiểm tra xem có bao nhiêu Row được thêm vào database, nếu s1 = 1 (1 row được thêm) thì thông báo là thêm mới thành công
                    {
                        MessageBox.Show("Đăng kí thành công!");
                        frmDangNhap frm = new frmDangNhap();
                        frm.Show();
                        this.Hide();
                }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
           
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.Show();
            this.Hide();
        }
    }
}