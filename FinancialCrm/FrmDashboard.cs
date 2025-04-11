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
    public partial class FrmDashboard: Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities db = new FinancialCrmDbEntities();   // db nesne örneği çağırdık
        int count = 0;  // timer için atadık
        private void FrmDashboard_Load(object sender, EventArgs e)     // Form yüklendiği zaman çalışacak kısımları yazdık
        {
            // Toplam banka bakiyesi
            var totalBalance = db.Banks.Sum(x => x.BankBalance);   // veri tabanı(db) içinde bulunan Banks tablosundaki BankBalance leri topla
            lblTotalBalance.Text = totalBalance.ToString() + "₺";  // totalBalance dan gelen değeri string formatta ekrana yazdırdık

            // Gelen Son Havale
            var lastBankProcessAmount = db.BankProcesses.OrderByDescending(x => x.BankProcessId).Take(1).Select(y => y.Amount).FirstOrDefault();   // veri tabanında bulunan BankProcesses tablosuna eriştik, OrderByDescending ile BankProcessId yi büyükten küçüğe foğru sıraladık, Take ile 1. sırada olanı seçtik ve bunun amount unu seçtik
            lblLastBankProcessAmount.Text = lastBankProcessAmount.ToString() + "₺";    // seçilen miktarı ekrana yazdırdık

            // Chart 1 Kodları (Banka ve banka hesaplarındaki para miktarı)
            var bankData = db.Banks.Select(x => new      // new yazarak nesne örneği oluşturmak zorundayız.
            {
                x.BankTitle,
                x.BankBalance
            }).ToList();           // bankData içine banka ismini ve bakiyeleri ekledik.
            chart1.Series.Clear();   // Önce chart1 içindeki var olan Series i temizledim
            var series = chart1.Series.Add("Series1");       // Series1(bu bankaları tutan genel bir başlıktır) ismi eklediğimiz grafikte yazan isimdir. İstersen bu ismide değiştirebiliriz.
            foreach (var item in bankData)
            {
                series.Points.AddXY(item.BankTitle, item.BankBalance);    // series in noktalarına x kısmına banka ismi, y kısmına banka bakiyesi olacak şekilde atama yaptım
            }

            // Chart 2 Kodları (Faturalar grafiği)
            var billData = db.Bills.Select(x => new
            {
                x.BillTitle,
                x.BillAmount
            }).ToList();
            chart2.Series.Clear();
            var series2 = chart2.Series.Add("Faturalar");
            // series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Renko;    // burada renko başka bir grafik türüdür, renko yu silerek istediğin grafik türünü bu kod ile de ekleyebilirsin.
            foreach (var item in billData)
            {
                series2.Points.AddXY(item.BillTitle, item.BillAmount);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;  // sayaç 1 er artacak
            if (count % 4 == 1)   // yani her 4. saniyede bir fatura başlıkları değişecek
            {
                var elektrikFaturasi = db.Bills.Where(x => x.BillTitle == "Elektrik Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Elektrik Faturası";
                lblBillAmount.Text = elektrikFaturasi.ToString() + "₺";
            }
            if (count % 4 == 2)  // Her fatura kendi zaman ayarına göre ekranda belirecek 
            {
                var dogalgazFaturasi = db.Bills.Where(x => x.BillTitle == "Doğalgaz Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Doğalgaz Faturası";
                lblBillAmount.Text = dogalgazFaturasi.ToString() + "₺";
            }
            if (count % 4 == 3)  // Her fatura kendi zaman ayarına göre ekranda belirecek 
            {
                var suFaturasi = db.Bills.Where(x => x.BillTitle == "Su Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Su Faturası";
                lblBillAmount.Text = suFaturasi.ToString() + "₺";
            }
            if (count % 4 == 0)  // Her fatura kendi zaman ayarına göre ekranda belirecek 
            {
                var internetFaturasi = db.Bills.Where(x => x.BillTitle == "İnternet Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "İnternet Faturası";
                lblBillAmount.Text = internetFaturasi.ToString() + "₺";
            }
        }

        private void btnBanksForm_Click(object sender, EventArgs e)    // Bankalar Butonu
        {
            FrmBanks frm = new FrmBanks();   
            frm.Show();    
            this.Hide();   
        }

        private void btnBillForm_Click(object sender, EventArgs e)     // Giderler Butonu
        {
            FrmBilling frm = new FrmBilling();   
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
