using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialCrm.Models;    // Model imizi buraya çağırdık

namespace FinancialCrm
{
    public partial class FrmLogin: Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities db = new FinancialCrmDbEntities();    // db nesne örneği çağırdık
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            var user = db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);   

            if (user != null)
            {
                // Giriş başarılıysa banka formunu aç
                FrmBanks frm = new FrmBanks();
                frm.Show();

                // Giriş başarılıysa banka formunu aç
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya parola", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txtUsername.Text = "";
            txtPassword.Text = ""; 
        }
    }
}
