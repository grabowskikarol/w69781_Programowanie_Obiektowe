public class ZarzadzanieRezerwacjami
{
    private List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

    public void DodajRezerwacje(Rezerwacja rezerwacja)
    {
        rezerwacja.Id = rezerwacje.Any() ? rezerwacje.Max(r => r.Id) + 1 : 1;
        rezerwacje.Add(rezerwacja);
    }

    public bool EdytujRezerwacje(int id, Rezerwacja nowaRezerwacja)
    {
        var rezerwacja = rezerwacje.FirstOrDefault(r => r.Id == id);
        if (rezerwacja == null) return false;
        rezerwacja.OfertaId = nowaRezerwacja.OfertaId;
        rezerwacja.Klient = nowaRezerwacja.Klient;
        rezerwacja.LiczbaMiejsc = nowaRezerwacja.LiczbaMiejsc;
        return true;
    }

    public bool AnulujRezerwacje(int id)
    {
        var rezerwacja = rezerwacje.FirstOrDefault(r => r.Id == id);
        if (rezerwacja == null) return false;
        rezerwacje.Remove(rezerwacja);
        return true;
    }

    public List<Rezerwacja> PobierzRezerwacje() => rezerwacje;
}