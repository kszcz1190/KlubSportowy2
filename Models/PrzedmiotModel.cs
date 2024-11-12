namespace KlubSportowy.Models
{
    public class PrzedmiotModel
    {
        public int Id { get; set; }
        public int CenaPrzedmiotu { get; set; }
        public int IloscPrzedmiotu { get; set; }
        public string NazwaPrzedmiotu { get; set; }
        public string OpisPrzedmiotu { get; set; }
        public bool CzyDostepny { get; set; }



    }
}