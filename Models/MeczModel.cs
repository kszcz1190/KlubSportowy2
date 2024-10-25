using System.ComponentModel.DataAnnotations;

namespace KlubSportowy.Models
{
    public class MeczModel
    {
        public int Id { get; set; }

        [Required]
        public string Druzyna { get; set; }

        [Required]
        public string DruzynaPrzeciwna { get; set; }

        [Range(0, 100, ErrorMessage = "The number of goals should be between 0 and 100.")]
        public int GoleDruzyna { get; set; }

        [Range(0, 100, ErrorMessage = "The number of goals should be between 0 and 100.")]
        public int GoleDruzynaPrzeciwna { get; set; }

        [Range(0, 10, ErrorMessage = "The number of yellow cards should be between 0 and 10.")]
        public int IloscZoltychKartekDruzyna { get; set; }

        [Range(0, 10, ErrorMessage = "The number of yellow cards should be between 0 and 10.")]
        public int IloscZoltychKartekDruzynaPrzeciwna { get; set; }

        [Range(0, 5, ErrorMessage = "The number of red cards should be between 0 and 5.")]
        public int IloscCzerwonychKartekDruzyna { get; set; }

        [Range(0, 5, ErrorMessage = "The number of red cards should be between 0 and 5.")]
        public int IloscCzerwonychKartekDruzynaPrzeciwna { get; set; }
        //public ICollection<ZawodnikModel> Zawodnicy { get; set; }
        public ICollection<StatystykiZawodnikaMeczModel> StatystykiZawodnikow { get; set; } 

    }
}

