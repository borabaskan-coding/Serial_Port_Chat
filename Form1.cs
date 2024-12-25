using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortChatPrg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var seriPort in SerialPort.GetPortNames())//var tipinde bir seriPort degiskeni olusturduk ve                                           
            {
                comboBoxPorts.Items.Add(seriPort);//Dongude gelen her bir ismi combobox icine attik
            }
            comboBoxPorts.SelectedIndex = 0;//Combobox icinde bir port adi gorunmesini istedik
            buttonDisconnect.Enabled = false;
            buttonSender.Enabled = false;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBoxPorts.Text;//forma serialport1 adinda bir nesne ekledik bu sayede 
            //combobox ta secili olan text (port adi) artik serialport1 in hangi port oldugunu gosterecek
            serialPort1.BaudRate = 9600;//serialPort1 asenkron haberlesme (rs232 gibi) bu yuzden baund rate e 
            //ihtiyac var ve belirli bir sayi olmali rastgele degil (orn: 9600)
            serialPort1.Parity = Parity.Even;//Hata tespit biti (cift)
            serialPort1.StopBits = StopBits.One;//Stop bit i belirledik veri iletimi buna gore sonlanacak
            serialPort1.DataBits = 8;//Veri paketleri 8 er bit olarak gonderilecek
            
            try
            {
                serialPort1.Open();//Seri port u actik
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Seri Port Baglantisi Yapilamadi \n Hata: {ex.Message}","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (serialPort1.IsOpen)//seri port acik ise baglanti butonu calismasin kosulu
            {
                buttonConnect.Enabled = false;//connect butonu false
                buttonDisconnect.Enabled = true;//disconnect butonu acik
                buttonSender.Enabled = true;//sender butonu acik
            }

        }


        private void buttonDisconnect_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)//seri port 1 acik ise
            {
                serialPort1.Close();//seri port u kapat
                buttonConnect.Enabled = true;//connect butonu acik(tekrar baglanti icin)
                buttonDisconnect.Enabled = false;//zaten baglanti olmadigi icin disconnect butonu kapali
                buttonSender.Enabled = false;//gonder butonu baglanti olmadigi icin kapali
            }
            

        }

        private void buttonSender_Click(object sender, EventArgs e)//Sender butonu ile veri gonderimi icin
        {
            if (serialPort1.IsOpen) //port un acik olup olmadigini kontrol et
            {
                serialPort1.Write(textBoxSend.Text);//port acik olmasi durumunda textbox ta yazan veriyi gonder
                textBoxSend.Clear();//veri gonderiminden sonra metin yazan textbox u kapat
            }
        }

        public delegate void veriGoster(String s);//delegate ler metotlari karsilayan yapilar capraz calisan 
        //uygulamalarda hatalarin onune gecmek icin kullanilabilir

        public void textBoxYaz(String s)//Yeni bir metot olusturduk ve icine String bir degisken almasini soyledik 
            //aldigi string degiskeni textBoxReceived teki text e atadik
        {
            textBoxReceived.Text += s;
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //serialPort1 nesnesinin data received metodunu olusturduk cunku giden veri ve gelen veri oldugu icin 
            //2 thread var bu thread sayisinin 2 olmasi UI (User Interface) tarafinda bir sorun olusturuyor
            //bu yuzden bir gelenVeri adinda string olusturduk ve serialPort1 in ReadExisting ozelligi ile okudugu
            //veriyi gelenVeri nesnesine atadik bu sayede gonderilen mesaj ayni zamanda received textbox unda 
            //gozukecek
            String gelenVeri = serialPort1.ReadExisting();
            //textBoxReceived.Text += gelenVeri;
            textBoxReceived.Invoke(new veriGoster(textBoxYaz),gelenVeri);//Invoke komutu temsilci(delegate) i cagirmak
            //icin yazildi ayrica UI da istenilen thread i de cagirabilir
        }
    }
}
