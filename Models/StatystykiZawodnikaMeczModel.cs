namespace KlubSportowy.Models
{
    public class StatystykiZawodnikaMeczModel
    {
        public int Id { get; set; }
        public int MeczModelId { get; set; }
        public MeczModel MeczModel { get; set; }
        public int ZawodnikModelId { get; set; }
        public ZawodnikModel ZawodnikModel { get; set; }
        public int IloscGoli { get; set; }
        public int IloscZoltychKartek { get; set; }
        public int IloscCzerwonychKartek { get; set; }
        public int IloscMinutRozegranych { get; set; }
        public bool CzyZawodnikZagralWMeczu { get; set; }
        public bool CzyKapitan { get; set; }
        public string Pozycja { get; set; }

    }
}
