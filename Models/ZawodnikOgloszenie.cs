using KlubSportowy.Areas.Identity.Data;
using KlubSportowy.Models;
namespace KlubSportowy.Models;

public class ZawodnikOgloszenie
{
    public string ZawodnikId { get; set; }

    public bool IsRead { get; set; } = false;
    public ApplicationUser Zawodnik { get; set; }

    public int OgloszenieId { get; set; }
    public OgloszeniaModel Ogloszenie { get; set; }


}


