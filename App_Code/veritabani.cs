using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


/// <summary>
/// veritabani için özet açıklama
/// </summary>
public class veritabani
{
    string strveritabanibaglantisi = ConfigurationManager.ConnectionStrings["elektromagazadb"].ToString();


    public string anamenu()
    {

        DataTable dt = DataTableGetir("select * from Anamenu order by Siralama");
        string strdata = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strdata += "<li class=\"nav-item mr-lg-2 mb-lg-0 mb-2\"><a class=\"nav-link\" href=\"" + dt.Rows[i]["AcacagiSayfa"] + "\" target=\"" + dt.Rows[i]["AcilisSekli"] + "\">" + dt.Rows[i]["MenuAdi"] + "</a></li>";
        }
        return strdata;


    }

    public string sosyalsitebaglantilari()
    {
        DataTable dt = DataTableGetir("select * from SosyalSiteBaglantisi where Durum='Açık' order by Siralama");
        string strdata = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //strdata += "<li><a class= \"icon tw \" href= \"" + dt.Rows[i]["AcacagiSayfa"] + "\"target=\"blank\"><i class= \"fa-" + dt.Rows[i]["SiteAdi"] + " \"></i></a></li>";
            strdata += "<li><a href=\"" + dt.Rows[i]["AcacagiSayfa"] + "\" target=\"_blank\"><i class=\"fab fa-" + dt.Rows[i]["SiteAdi"] + "\"></i></a></li>";
        }
        //strdata += "<li></li>";
        return strdata;

    }
    public void ekle_sil_guncelle(string sorgu)
    {
        using (SqlConnection baglanti = new SqlConnection(strveritabanibaglantisi))
        {
            using (SqlCommand cmd = new SqlCommand(sorgu, baglanti))
            {
                cmd.CommandType = CommandType.Text;
                baglanti.Open();
                cmd.ExecuteNonQuery();
                baglanti.Close();
            }
        }
    }
    public DataTable DataTableGetir(string sorgu)
    {
        DataTable dt = new DataTable();
        using (SqlConnection baglanti = new SqlConnection(strveritabanibaglantisi))
        {
            using (SqlCommand cmd = new SqlCommand(sorgu, baglanti))
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sorgu, baglanti);
                try
                {
                    adapter.Fill(dt);
                }
                catch
                {

                }
                baglanti.Close();
            }
        }
        return dt;
    }

    //bu fonksiyon kendisine gönderilen sorgu sonucu dönen kayıtları radiobuttonlist e doldurur.
    public void rbtnllist_aktar(string sql, System.Web.UI.WebControls.RadioButtonList rbtnl)
    {
        rbtnl.Items.Clear();
        using (SqlConnection baglanti = new SqlConnection(strveritabanibaglantisi))
        {
            using (SqlCommand cmd = new SqlCommand(sql, baglanti))
            {
                baglanti.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        rbtnl.Items.Add(reader[0].ToString());
                    }
                }
                finally
                {
                    if (reader != null)
                        ((IDisposable)reader).Dispose();
                }
                baglanti.Close();
            }

        }
    }
    public string verigetir(string sorgu, string alan)
    {
        string strdata = "";

        using (SqlConnection baglanti = new SqlConnection(strveritabanibaglantisi))
        {
            using (SqlCommand cmd = new SqlCommand(sorgu, baglanti))
            {
                baglanti.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        strdata = reader["" + alan + ""].ToString();
                    }

                }
                finally
                {
                    if (reader != null)
                        ((IDisposable)reader).Dispose();
                }

                baglanti.Close();

            }

        }
        return strdata;


    }
}