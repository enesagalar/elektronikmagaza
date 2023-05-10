using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class web_index : System.Web.UI.Page
{
    veritabani vtislemler = new veritabani();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        anamenu.InnerHtml = vtislemler.anamenu();

        sosyalsitebaglantilarialt.InnerHtml = vtislemler.sosyalsitebaglantilari();

        //Anketler tablosundaki Durum='Aktif' olan anketin AnketID verisini buldum.
        string aktifanketid = vtislemler.verigetir("select AnketID from Anketler where Durum='Aktif'", "AnketID");
        //Üst satırda bulduğum AnketID ye sahip soruyu AnketSoruları tablosundan buldum ve id, runat="server" özelliklerini verdiğim etikete gönderdim.
        anketsorusu.InnerHtml = vtislemler.verigetir("select Soru from AnketSorulari where AnketID='" + aktifanketid + "'", "Soru");
        //cevaplar için eklediğim rbtnlanketcevaplari id sine sahip radiobuttonlist içine bulduğum AnketID ye sahip cevapları doldurdum.
        vtislemler.rbtnllist_aktar("select Cevap from AnketCevaplari where AnketID='" + aktifanketid + "' order by Siralama", rbtnlanketcevaplari);
        rbtnlanketcevaplari.ClearSelection();
        }
    }

    protected void btncevapgonder_Click(object sender, EventArgs e)
    {
        //Anketler tablosundaki Durum='Aktif' olan anketin AnketID verisini buldum.
        string aktifanketid = vtislemler.verigetir("select AnketID from Anketler where Durum='Aktif'", "AnketID");
        //seçilen cevaba ait CevapID verisini buldum.
        string secilencevapid = vtislemler.verigetir("select CevapID from AnketCevaplari where AnketID='" + aktifanketid + "' and Cevap='" + rbtnlanketcevaplari.SelectedItem.Text + "'", "CevapID");
        //bulduğum CevapID ye sahip cevabın CevapAdet alanındaki değerini 1 artırarak update ettim.
        vtislemler.ekle_sil_guncelle("update AnketCevaplari set CevapAdet=CevapAdet+1 where CevapID='" + secilencevapid + "'");
        rbtnlanketcevaplari.ClearSelection();
        lblislemtamam.Visible = true;
        
        if (Timer1.Enabled == true)
        {
            Timer1.Enabled = false;
        }
        else
        {
            Timer1.Enabled = true;
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Timer1.Enabled = false;
        lblislemtamam.Visible = false;
    }
}