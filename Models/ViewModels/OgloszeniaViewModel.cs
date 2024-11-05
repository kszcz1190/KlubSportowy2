using System.ComponentModel.DataAnnotations;
using System.Data;
using KlubSportowy.Models;

namespace KlubSportowy.Models.ViewModels
{
    public class OgloszeniaViewModel
    {
        [Required]
        public string Tresc { get; set; } = default!;

        [Required]
        public DateTime DataUsunieciaOgloszenia { get; set; }
        public List<ZawodnikOgloszenie> ZawodnikOgloszenie { get; set; } = new List<ZawodnikOgloszenie>();

    }
}
