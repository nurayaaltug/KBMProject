using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KisiselBakimProje.Data
{
    public class SeciliKonumServices
    {
        public static SQLConfigs _bag;
        public SqlConnection baglanti;

        public SeciliKonumServices(SQLConfigs bag)
        {
            _bag = bag;
            baglanti = new SqlConnection(_bag.Value);

        }
        public void SeciliKonumTanimlama(SeciliKonumModel model)
        {
            SqlCommand s = new SqlCommand("INSERT INTO seciliKonum(SeciliKonum) VALUES(geography::STGeomFromText('POINT(" + model.lat + " " + model.lng + ")',4326))", baglanti);
            if (baglanti.State.ToString() == "Closed")
            {
                baglanti.Open();
            }
            s.ExecuteNonQuery();
            baglanti.Close();
        }
        public void SeciliKonumSil()
        {
            SqlCommand s = new SqlCommand("DELETE FROM seciliKonum", baglanti);
            if (baglanti.State.ToString() == "Closed")
            {
                baglanti.Open();
            }
            s.ExecuteNonQuery();
            baglanti.Close();

        }
    }
}
