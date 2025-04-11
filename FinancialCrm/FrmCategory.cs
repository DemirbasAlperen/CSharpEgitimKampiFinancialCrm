using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialCrm.Models;     // model i buraya çağırdık

namespace FinancialCrm
{
    public partial class FrmCategory: Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities db = new FinancialCrmDbEntities();    // db nesne örneği yazdık
        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            var values = db.Categories.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text;

            Categories category = new Categories();
            category.CategoryName = categoryName;
            db.Categories.Add(category);
            db.SaveChanges();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Sisteme Eklendi", "Kategoriler", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Ekleme işlemi sonrası listeyi bize tekrar döndürmesi için yazdık
            var values = db.Categories.ToList();
            dataGridView1.DataSource = values;

        }

        private void btnRemoveCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCategoryId.Text);
            var removeValue = db.Categories.Find(id);
            db.Categories.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Sistemden Silindi", "Kategoriler", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Silme işlemi sonrası listeyi bize tekrar döndürmesi için yazdık
            var values = db.Categories.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text;
            int id = int.Parse(txtCategoryId.Text);

            var values = db.Categories.Find(id);

            values.CategoryName = categoryName;
            db.SaveChanges();    // değişiklikleri kaydettik
            MessageBox.Show("Kategori Başarılı Bir Şekilde Sistemde Güncellendi", "Kategoriler", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Güncelleme işlemi sonrası listeyi bize tekrar döndürmesi için yazdık
            var values2 = db.Categories.ToList();
            dataGridView1.DataSource = values2;

        }

        private void btnBanksForm_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();   // FrmBanks dan bir tane nesne örneği türettik
            frm.Show();    // bunu bize gösterecek
            this.Hide();   // üstünde çalıştığımız formu gizleyecek
        }

        private void btnBillForm_Click(object sender, EventArgs e)
        {
            FrmBilling frm = new FrmBilling();   // Banka formunda Giderlere tıklayınca FrmBilling formunu açacak
            frm.Show();
            this.Hide();
        }

        private void btnDashboardForm_Click(object sender, EventArgs e)
        {
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
            this.Hide();
        }

        private void btnLoginOut_Click(object sender, EventArgs e)
        {
            // Login formunu tekrar aç
            FrmLogin loginForm = new FrmLogin();
            loginForm.Show();

            // Bu (banka) formu kapat
            this.Close();
        }
    }
}
