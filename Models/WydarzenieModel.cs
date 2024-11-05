using System.ComponentModel.DataAnnotations.Schema;
namespace KlubSportowy.Models
{
    public class WydarzenieModel
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DataWydarzenia { get; set; }
        public string TypWydarzenia { get; set; }
        public string OpisWydarzenia { get; set; }
        public string MiejsceWydarzenia { get; set; }
        public string NazwaWydarzenia { get; set; }
        public int? MeczModelId { get; set; }
        public MeczModel MeczModel { get; set; }

    }
}
