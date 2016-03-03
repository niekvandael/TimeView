using System.Collections.Generic;

namespace TimeView.data
{
    public class Fields
    {
        public string website { get; set; }
        public string latitude_breedtegraad { get; set; }
        public string adres { get; set; }
        public string naam { get; set; }
        public string numero_de_fax_fax_nummer { get; set; }
        public string longitude_lengtegraad { get; set; }
        public IList<double> geo_coordinates { get; set; }
        public int code_postal_postcode { get; set; }
        public string numero_de_telephone_telefoon_nummer { get; set; }
        public string gemeente { get; set; }
    }
}