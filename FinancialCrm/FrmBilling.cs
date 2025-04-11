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
    public partial class FrmBilling: Form
    {
        public FrmBilling()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities db = new FinancialCrmDbEntities();    // db nesne örneği yazdık

        private void FrmBilling_Load(object sender, EventArgs e)
        {
            var values = db.Bills.ToList();   // veri tabanında bulunan Bill i bana listele
            dataGridView1.DataSource = values;
        }

        private void btnBillList_Click(object sender, EventArgs e)   // Ödeme Listesi Butonu yukarıdaki metot ile aynı
        {
            var values = db.Bills.ToList();  
            dataGridView1.DataSource = values;
        }

        private void btnCreateBill_Click(object sender, EventArgs e)   // Yeni Ödeme Butonu
        {
            string title = txtBillTitle.Text;
            decimal amount = decimal.Parse(txtBillAmount.Text);
            string period = txtBillPeriod.Text;

            Bills bills = new Bills();  // Bills den bir nesne örneği türettim.
            bills.BillTitle = title;
            bills.BillAmount = amount;
            bills.BillPeriod = period;
            db.Bills.Add(bills); // Bills içerisine bills den gelen parametreleri ekledim
            db.SaveChanges();    // değişiklikleri kaydettik
            MessageBox.Show("Ödeme Başarılı Bir Şekilde Sisteme Eklendi", "Ödeme & Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Ekleme işlemi sonrası listeyi bize tekrar döndürmesi için yazdık
            var values = db.Bills.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnRemoveBill_Click(object sender, EventArgs e)    // Ödeme Sil Butonu
        {
            int id = int.Parse(txtBillId.Text);     
            var removeValue = db.Bills.Find(id);    // Bills içinden id ye göre bul
            db.Bills.Remove(removeValue);    // Bills içinden removeValue den gelen değerleri sil
            db.SaveChanges();
            MessageBox.Show("Ödeme Başarılı Bir Şekilde Sistemden Silindi", "Ödeme & Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Silme işlemi sonrası listeyi bize tekrar döndürmesi için yazdık
            var values = db.Bills.ToList();
            dataGridView1.DataSource = values;

        }

        private void btnUpdateBill_Click(object sender, EventArgs e)    // Ödeme Güncelle Butonu
        {
            string title = txtBillTitle.Text;
            decimal amount = decimal.Parse(txtBillAmount.Text);
            string period = txtBillPeriod.Text;
            int id = int.Parse(txtBillId.Text);

            var values = db.Bills.Find(id);

            values.BillTitle = title;
            values.BillAmount = amount;
            values.BillPeriod = period;
            db.SaveChanges();    // değişiklikleri kaydettik
            MessageBox.Show("Ödeme Başarılı Bir Şekilde Sistemde Güncellendi", "Ödeme & Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Güncelleme işlemi sonrası listeyi bize tekrar döndürmesi için yazdık
            var values2 = db.Bills.ToList();
            dataGridView1.DataSource = values2;
        }

        private void btnBanksForm_Click(object sender, EventArgs e)   // Bankalar Butonu için
        {
            FrmBanks frm = new FrmBanks();   // FrmBanks dan bir tane nesne örneği türettik
            frm.Show();    // bunu bize gösterecek
            this.Hide();   // üstünde çalıştığımız formu gizleyecek
        }

        private void btnDashboardForm_Click(object sender, EventArgs e)   // Dashboard Butonu
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

        private void btnCategory_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            frm.Show();
            this.Close();
        }
    }
}
