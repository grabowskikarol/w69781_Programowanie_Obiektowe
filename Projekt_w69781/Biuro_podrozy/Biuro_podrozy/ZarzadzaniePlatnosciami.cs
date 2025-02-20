public class ZarzadzaniePlatnosciami
{
    private List<Platnosc> platnosci = new List<Platnosc>();

    public void DodajPlatnosc(Platnosc platnosc)
    {
        platnosc.Id = platnosci.Any() ? platnosci.Max(p => p.Id) + 1 : 1;
        platnosci.Add(platnosc);
    }

    public List<Platnosc> PobierzPlatnosci() => platnosci;

    public string GenerujFakture(int platnoscId)
    {
        var platnosc = platnosci.FirstOrDefault(p => p.Id == platnoscId);
        if (platnosc == null)
            return "Brak płatności o podanym ID.";
        return $"Faktura\n------\nID płatności: {platnosc.Id}\nID rezerwacji: {platnosc.RezerwacjaId}\nKwota: {platnosc.Kwota}\nData płatności: {platnosc.DataPlatnosci}\n------";
    }
}