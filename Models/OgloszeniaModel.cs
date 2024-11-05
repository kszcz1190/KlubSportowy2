using System;
using KlubSportowy.Areas.Identity.Data;
namespace KlubSportowy.Models
{
    public class OgloszeniaModel
    {
        public int Id { get; set; }
        public string Tresc { get; set; }
        public DateTime DataDodania { get; set; } = DateTime.Now;
        public DateTime DataUsunieciaOgloszenia { get; set; }

        public virtual ICollection<ZawodnikOgloszenie> ZawodnikOgloszenie { get; set; }
    }


}
