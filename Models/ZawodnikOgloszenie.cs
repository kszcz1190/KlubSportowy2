using KlubSportowy.Areas.Identity.Data;
using KlubSportowy.Models;
namespace KlubSportowy.Models;

public class ZawodnikOgloszenie
{
    public string ZawodnikId { get; set; }

    public int OgloszenieId { get; set; }

    public bool IsRead { get; set; }

    public virtual ApplicationUser Zawodnik { get; set; } // Powiązanie z użytkownikiem
    public virtual OgloszeniaModel Ogloszenie { get; set; } // Powiązanie z ogłoszeniem
}

