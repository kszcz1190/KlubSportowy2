namespace KlubSportowy.Models
{
    public class ZawodnikModel
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public string Kraj { get; set; }

        public string Pozycja { get; set; }
        public int LacznaIloscGoli { get; set; }
        public int LacznaIloscZoltychKartek { get; set; }
        public int LacznaIloscCzerwonychKartek { get; set; }
        public int LacznaIloscMeczyRozegranych { get; set; }
        public int LacznaIloscMinutRozegranych { get; set; }
        public int NumerZawodnika { get; set; }
        public ICollection<StatystykiZawodnikaMeczModel> StatystykiZawodnikaZMeczu { get; set; }
        // public ICollection<MeczModel> MeczeZawodnika { get; set; }


    }
}
