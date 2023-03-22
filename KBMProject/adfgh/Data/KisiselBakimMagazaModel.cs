using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace KisiselBakimProje.Data
{
    public class KisiselBakimMagazaModel
    {
        public int Id { get; set; }

        public string MagazaAdi { get; set; }

        public string Sehir { get; set; }

        public string MagazaAciklama { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }
    }
}
