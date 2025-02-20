public class ZarzadzanieUzytkownikami
{
    private List<Uzytkownik> uzytkownicy = new List<Uzytkownik>();

    public void DodajUzytkownika(Uzytkownik uzytkownik)
    {
        uzytkownik.Id = uzytkownicy.Any() ? uzytkownicy.Max(u => u.Id) + 1 : 1;
        uzytkownicy.Add(uzytkownik);
    }

    public bool EdytujUzytkownika(int id, Uzytkownik nowyUzytkownik)
    {
        var uzytkownik = uzytkownicy.FirstOrDefault(u => u.Id == id);
        if (uzytkownik == null) return false;
        uzytkownik.Nazwa = nowyUzytkownik.Nazwa;
        uzytkownik.Rola = nowyUzytkownik.Rola;
        return true;
    }

    public bool UsunUzytkownika(int id)
    {
        var uzytkownik = uzytkownicy.FirstOrDefault(u => u.Id == id);
        if (uzytkownik == null) return false;
        uzytkownicy.Remove(uzytkownik);
        return true;
    }

    public List<Uzytkownik> PobierzUzytkownikow() => uzytkownicy;
}