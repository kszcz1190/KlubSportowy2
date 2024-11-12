using KlubSportowy.Areas.Identity.Data;

namespace KlubSportowy.Models
{
    public class RezerwacjaModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public int PrzedmiotModelId { get; set; }
        public PrzedmiotModel przedmiotModel { get; set; }

    }
}
