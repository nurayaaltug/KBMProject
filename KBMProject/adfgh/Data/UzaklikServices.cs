using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KisiselBakimProje.Data
{
    public class UzaklikServices
    {
        public static SQLConfigs _bag;
        public SqlConnection baglanti;

        public UzaklikServices(SQLConfigs bag)
        {
            _bag = bag;
            baglanti = new SqlConnection(_bag.Value);

        }
        public List<UzaklikModel> MagazaKonumUzaklik()
        {

            List<UzaklikModel> liste = new List<UzaklikModel>();
            if (baglanti.State.ToString() == "Closed")
            {
                baglanti.Open();
            }
            SqlCommand s = new SqlCommand("SELECT TOP 5 mk.MagazaAdi,mk.Sehir,((mk.MagazaKonum.STDistance(sk.SeciliKonum))/1000) AS uzaklik,mk.MagazaKonum.STAsText( ) AS Mkonum FROM magazaKonum mk, seciliKonum sk ORDER BY uzaklik", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(s);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UzaklikModel item = new UzaklikModel();
                    item.MagazaAdi = dt.Rows[i]["MagazaAdi"].ToString();
                    item.Sehir = dt.Rows[i]["Sehir"].ToString();
                    item.Uzaklik = dt.Rows[i]["Uzaklik"].ToString();
                    string[] sp = dt.Rows[i]["Mkonum"].ToString().Split('(');
                    string[] sp1 = sp[1].Split(' ');
                    item.lat = sp1[0];
                    string[] sp2 = sp1[1].Split(')');
                    item.lng = sp2[0];
                    liste.Add(item);
                }
            }
            baglanti.Close();
            return liste;
        }
    }
}
