using FinancialCrm.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class FrmBanks: Form
    {
        public FrmBanks()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities db = new FinancialCrmDbEntities();   // db nesne örneği yazdık

        private void FrmBanks_Load(object sender, EventArgs e)
        {
            // Banka Bakiyeleri
            var ziraatBankBalance = db.Banks.Where(x => x.BankTitle == "Ziraat Bankası").Select(y => y.BankBalance).FirstOrDefault();   // db içerisinden Bankalara git ve banka adı Ziraat Bankası olanın banka bakiyesini seç ve ziraatBankBalance içine ekle
            var vakifBankBalance = db.Banks.Where(x => x.BankTitle == "VakıfBank").Select(y => y.BankBalance).FirstOrDefault();  // yukarıdakini vakıfbank içinde yazıyoruz.
            var isBankBalance = db.Banks.Where(x => x.BankTitle == "İş Bankası").Select(y => y.BankBalance).FirstOrDefault();  // yukarıdakini İş bankası içinde yazıyoruz.

            lblisBankBalance.Text = isBankBalance.ToString() + " ₺";          // yukarıdaki bakiyeleri şimdi label içerisine atayarak ekrana yazdıralım.
            lblVakifBankBalance.Text = vakifBankBalance.ToString() + " ₺";
            lblZiraatBankBalance.Text = ziraatBankBalance.ToString() + " ₺";


            // Banka Hareketleri
            var bankProcess1 = db.BankProcesses.OrderByDescending(x => x.BankProcessId).Take(1).FirstOrDefault();   // veri tabanında bulunan BankProcess e erişim sağladık, OrderByDescending ile id yi azalana göre sıraladık, Take ile 1. sırada bulunanı seçtik ve sonucu döndürdük.
            lblBankProcess1.Text = bankProcess1.Description + " / Gelen Miktar: " + bankProcess1.Amount + " / İşlem Tarihi: " + bankProcess1.ProcessDate;

            var bankProcess2 = db.BankProcesses.OrderByDescending(x => x.BankProcessId).Take(2).Skip(1).FirstOrDefault();   // Yukarıdaki ile aynı işlem yapıldı tek fark skip kullandık. Skip(1) 1. olanı atlayacak.
            lblBankProcess2.Text = bankProcess2.Description + " / Gelen Miktar: " + bankProcess2.Amount + " / İşlem Tarihi: " + bankProcess2.ProcessDate;

            var bankProcess3 = db.BankProcesses.OrderByDescending(x => x.BankProcessId).Take(3).Skip(2).FirstOrDefault();   // Skip(2) bu sefer 2 tane atlayacak.
            lblBankProcess3.Text = bankProcess3.Description + " / Gelen Miktar: " + bankProcess3.Amount + " / İşlem Tarihi: " + bankProcess3.ProcessDate;

            var bankProcess4 = db.BankProcesses.OrderByDescending(x => x.BankProcessId).Take(4).Skip(3).FirstOrDefault();   // Skip(3) bu sefer 3 tane atlayacak.
            lblBankProcess4.Text = bankProcess4.Description + " / Gelen Miktar: " + bankProcess4.Amount + " / İşlem Tarihi: " + bankProcess4.ProcessDate;

            var bankProcess5 = db.BankProcesses.OrderByDescending(x => x.BankProcessId).Take(5).Skip(4).FirstOrDefault();   // Skip(4) bu sefer 4 tane atlayacak.
            lblBankProcess5.Text = bankProcess5.Description + " / Gelen Miktar: " + bankProcess5.Amount + " / İşlem Tarihi: " + bankProcess5.ProcessDate;

        }

        private void btnBillForm_Click(object sender, EventArgs e)    // Giderler Butonu için
        {
            FrmBilling frm = new FrmBilling();   // Banka formunda Giderlere tıklayınca FrmBilling formunu açacak
            frm.Show();
            this.Hide();
        }

        private void btnDashboardForm_Click(object sender, EventArgs e)    // Dashboard Butonu
        {
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
            this.Hide();
        }

        private void btnLoginOut_Click(object sender, EventArgs e)    // Çıkış Yap butonu
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
