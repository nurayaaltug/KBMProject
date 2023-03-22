using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KisiselBakimProje.Data
{
    public class KisiselBakimMagazaServices
    {
        public static SQLConfigs _bag;
        public SqlConnection baglanti;

        public KisiselBakimMagazaServices(SQLConfigs bag)
        {
            _bag = bag;
            baglanti = new SqlConnection(_bag.Value);

        }

        public List<KisiselBakimMagazaModel> MagazaListele()
        {
            List<KisiselBakimMagazaModel> liste = new List<KisiselBakimMagazaModel>();

            SqlCommand s = new SqlCommand("SELECT Id,MagazaAdi,Sehir,MagazaAciklama,MagazaKonum.STAsText( ) as Mkonum FROM magazaKonum", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(s);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    KisiselBakimMagazaModel item = new KisiselBakimMagazaModel();
                    item.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                    item.MagazaAdi = dt.Rows[i]["MagazaAdi"].ToString();
                    item.Sehir = dt.Rows[i]["Sehir"].ToString();
                    item.MagazaAciklama = dt.Rows[i]["MagazaAciklama"].ToString();
                    string[] sp = dt.Rows[i]["Mkonum"].ToString().Split('(');
                    string[] sp1 = sp[1].Split(' ');
                    item.lat = sp1[0];
                    string[] sp2 = sp1[1].Split(')');
                    item.lng = sp2[0];
                    liste.Add(item);
                }
            }
            return liste;
        }

        public void MagazaEkle(KisiselBakimMagazaModel model)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO magazaKonum(MagazaAdi,Sehir,MagazaAciklama,MagazaKonum) VALUES('"+model.MagazaAdi+"','"+model.Sehir+"','"+model.MagazaAciklama+"',geography::STGeomFromText('POINT("+model.lat+" "+model.lng+")',4326))",baglanti);
            if (baglanti.State.ToString() == "Closed")
            {
                baglanti.Open();
            }
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }

        public void SilMagazaKonum(KisiselBakimMagazaModel M)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM magazaKonum WHERE Id=" + M.Id + "", baglanti);
            if (baglanti.State.ToString() == "Closed")
            {
                baglanti.Open();
            }
            cmd.ExecuteNonQuery();
            baglanti.Close();

        }
    }
}
