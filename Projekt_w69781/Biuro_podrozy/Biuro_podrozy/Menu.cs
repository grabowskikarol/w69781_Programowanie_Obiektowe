public class Menu
{
    private ZarzadzanieOfertami zarzadzanieOfertami = new ZarzadzanieOfertami();
    private ZarzadzanieRezerwacjami zarzadzanieRezerwacjami = new ZarzadzanieRezerwacjami();
    private ZarzadzaniePlatnosciami zarzadzaniePlatnosciami = new ZarzadzaniePlatnosciami();
    private ZarzadzanieUzytkownikami zarzadzanieUzytkownikami = new ZarzadzanieUzytkownikami();

    public void PokazMenuGlowne()
    {
        string wybor = "";
        while (wybor != "0")
        {
            Console.WriteLine("=== Biuro Podróży ===");
            Console.WriteLine("1. Zarządzanie ofertami");
            Console.WriteLine("2. Zarządzanie rezerwacjami");
            Console.WriteLine("3. Zarządzanie płatnościami");
            Console.WriteLine("4. Zarządzanie użytkownikami");
            Console.WriteLine("0. Wyjście");
            Console.Write("Wybierz opcję: ");
            wybor = Console.ReadLine();
            Console.WriteLine();

            switch (wybor)
            {
                case "1":
                    MenuOfert();
                    break;
                case "2":
                    MenuRezerwacji();
                    break;
                case "3":
                    MenuPlatnosci();
                    break;
                case "4":
                    MenuUzytkownikow();
                    break;
                case "0":
                    Console.WriteLine("Koniec programu.");
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }
            Console.WriteLine();
        }
    }

    #region Menu Ofert

    private void MenuOfert()
    {
        string wybor = "";
        while (wybor != "0")
        {
            Console.WriteLine("=== Zarządzanie ofertami ===");
            Console.WriteLine("1. Pokaż wszystkie oferty");
            Console.WriteLine("2. Pokaż szczegóły oferty");
            Console.WriteLine("3. Dodaj ofertę");
            Console.WriteLine("4. Edytuj ofertę");
            Console.WriteLine("5. Usuń ofertę");
            Console.WriteLine("6. Backup ofert");
            Console.WriteLine("0. Powrót do głównego menu");
            Console.Write("Wybierz opcję: ");
            wybor = Console.ReadLine();
            Console.WriteLine();

            switch (wybor)
            {
                case "1":
                    PokazOferty();
                    break;
                case "2":
                    PokazSzczegolyOferty();
                    break;
                case "3":
                    DodajOferte();
                    break;
                case "4":
                    EdytujOferte();
                    break;
                case "5":
                    UsunOferte();
                    break;
                case "6":
                    zarzadzanieOfertami.BackupOfert();
                    Console.WriteLine("Backup został wykonany.");
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }
            Console.WriteLine();
        }
    }

    private void PokazOferty()
    {
        var oferty = zarzadzanieOfertami.PobierzWszystkieOferty();
        Console.WriteLine("=== Lista Ofert ===");
        foreach (var oferta in oferty)
        {
            Console.WriteLine($"ID: {oferta.Id}, Nazwa: {oferta.Nazwa}, Miejsce: {oferta.MiejsceDocelowe}, Cena: {oferta.Cena}, Miejsca: {oferta.DostepneMiejsca}");
        }
    }

    private void PokazSzczegolyOferty()
    {
        Console.Write("Podaj ID oferty: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var oferta = zarzadzanieOfertami.PobierzWszystkieOferty().FirstOrDefault(o => o.Id == id);
            if (oferta != null)
            {
                Console.WriteLine("=== Szczegóły Oferty ===");
                Console.WriteLine($"ID: {oferta.Id}");
                Console.WriteLine($"Nazwa: {oferta.Nazwa}");
                Console.WriteLine($"Miejsce docelowe: {oferta.MiejsceDocelowe}");
                Console.WriteLine($"Cena: {oferta.Cena}");
                Console.WriteLine($"Dostępne miejsca: {oferta.DostepneMiejsca}");
            }
            else
            {
                Console.WriteLine("Oferta o podanym ID nie istnieje.");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidłowy identyfikator.");
        }
    }

    private void DodajOferte()
    {
        Console.Write("Podaj nazwę oferty: ");
        string nazwa = Console.ReadLine();

        Console.Write("Podaj miejsce docelowe: ");
        string miejsce = Console.ReadLine();

        Console.Write("Podaj cenę: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal cena))
        {
            Console.WriteLine("Nieprawidłowa cena.");
            return;
        }

        Console.Write("Podaj liczbę dostępnych miejsc: ");
        if (!int.TryParse(Console.ReadLine(), out int miejsca))
        {
            Console.WriteLine("Nieprawidłowa liczba miejsc.");
            return;
        }

        Oferta nowaOferta = new Oferta
        {
            Nazwa = nazwa,
            MiejsceDocelowe = miejsce,
            Cena = cena,
            DostepneMiejsca = miejsca
        };

        zarzadzanieOfertami.DodajOferte(nowaOferta);
        Console.WriteLine("Oferta została dodana!");
    }

    private void EdytujOferte()
    {
        Console.Write("Podaj ID oferty do edycji: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Nieprawidłowy identyfikator.");
            return;
        }

        Console.Write("Podaj nową nazwę oferty: ");
        string nazwa = Console.ReadLine();

        Console.Write("Podaj nowe miejsce docelowe: ");
        string miejsce = Console.ReadLine();

        Console.Write("Podaj nową cenę: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal cena))
        {
            Console.WriteLine("Nieprawidłowa cena.");
            return;
        }

        Console.Write("Podaj nową liczbę dostępnych miejsc: ");
        if (!int.TryParse(Console.ReadLine(), out int miejsca))
        {
            Console.WriteLine("Nieprawidłowa liczba miejsc.");
            return;
        }

        Oferta nowaOferta = new Oferta
        {
            Nazwa = nazwa,
            MiejsceDocelowe = miejsce,
            Cena = cena,
            DostepneMiejsca = miejsca
        };

        if (zarzadzanieOfertami.EdytujOferte(id, nowaOferta))
            Console.WriteLine("Oferta została zaktualizowana!");
        else
            Console.WriteLine("Oferta o podanym ID nie istnieje.");
    }

    private void UsunOferte()
    {
        Console.Write("Podaj ID oferty do usunięcia: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Nieprawidłowy identyfikator.");
            return;
        }

        if (zarzadzanieOfertami.UsunOferte(id))
            Console.WriteLine("Oferta została usunięta!");
        else
            Console.WriteLine("Oferta o podanym ID nie istnieje.");
    }

    #endregion

    #region Menu Rezerwacji

    private void MenuRezerwacji()
    {
        string wybor = "";
        while (wybor != "0")
        {
            Console.WriteLine("=== Zarządzanie rezerwacjami ===");
            Console.WriteLine("1. Dodaj rezerwację");
            Console.WriteLine("2. Edytuj rezerwację");
            Console.WriteLine("3. Anuluj rezerwację");
            Console.WriteLine("4. Pokaż wszystkie rezerwacje");
            Console.WriteLine("0. Powrót do głównego menu");
            Console.Write("Wybierz opcję: ");
            wybor = Console.ReadLine();
            Console.WriteLine();

            switch (wybor)
            {
                case "1":
                    DodajRezerwacje();
                    break;
                case "2":
                    EdytujRezerwacje();
                    break;
                case "3":
                    AnulujRezerwacje();
                    break;
                case "4":
                    PokazRezerwacje();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }
            Console.WriteLine();
        }
    }

    private void DodajRezerwacje()
    {
        Console.Write("Podaj ID oferty: ");
        if (!int.TryParse(Console.ReadLine(), out int ofertaId))
        {
            Console.WriteLine("Nieprawidłowy identyfikator oferty.");
            return;
        }

        Console.Write("Podaj imię i nazwisko klienta: ");
        string klient = Console.ReadLine();

        Console.Write("Podaj liczbę miejsc: ");
        if (!int.TryParse(Console.ReadLine(), out int liczbaMiejsc))
        {
            Console.WriteLine("Nieprawidłowa liczba miejsc.");
            return;
        }

        Rezerwacja rezerwacja = new Rezerwacja
        {
            OfertaId = ofertaId,
            Klient = klient,
            LiczbaMiejsc = liczbaMiejsc
        };

        zarzadzanieRezerwacjami.DodajRezerwacje(rezerwacja);
        Console.WriteLine("Rezerwacja została dodana!");
    }

    private void EdytujRezerwacje()
    {
        Console.Write("Podaj ID rezerwacji do edycji: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Nieprawidłowy identyfikator.");
            return;
        }

        Console.Write("Podaj nowe ID oferty: ");
        if (!int.TryParse(Console.ReadLine(), out int ofertaId))
        {
            Console.WriteLine("Nieprawidłowy identyfikator oferty.");
            return;
        }

        Console.Write("Podaj nowe imię i nazwisko klienta: ");
        string klient = Console.ReadLine();

        Console.Write("Podaj nową liczbę miejsc: ");
        if (!int.TryParse(Console.ReadLine(), out int liczbaMiejsc))
        {
            Console.WriteLine("Nieprawidłowa liczba miejsc.");
            return;
        }

        Rezerwacja nowaRezerwacja = new Rezerwacja
        {
            OfertaId = ofertaId,
            Klient = klient,
            LiczbaMiejsc = liczbaMiejsc
        };

        if (zarzadzanieRezerwacjami.EdytujRezerwacje(id, nowaRezerwacja))
            Console.WriteLine("Rezerwacja została zaktualizowana!");
        else
            Console.WriteLine("Rezerwacja o podanym ID nie istnieje.");
    }

    private void AnulujRezerwacje()
    {
        Console.Write("Podaj ID rezerwacji do anulowania: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Nieprawidłowy identyfikator.");
            return;
        }

        if (zarzadzanieRezerwacjami.AnulujRezerwacje(id))
            Console.WriteLine("Rezerwacja została anulowana!");
        else
            Console.WriteLine("Rezerwacja o podanym ID nie istnieje.");
    }

    private void PokazRezerwacje()
    {
        var rezerwacje = zarzadzanieRezerwacjami.PobierzRezerwacje();
        Console.WriteLine("=== Lista Rezerwacji ===");
        foreach (var r in rezerwacje)
        {
            Console.WriteLine($"ID: {r.Id}, OfertaID: {r.OfertaId}, Klient: {r.Klient}, Liczba miejsc: {r.LiczbaMiejsc}");
        }
    }

    #endregion

    #region Menu Płatności

    private void MenuPlatnosci()
    {
        string wybor = "";
        while (wybor != "0")
        {
            Console.WriteLine("=== Zarządzanie płatnościami ===");
            Console.WriteLine("1. Dodaj płatność");
            Console.WriteLine("2. Pokaż wszystkie płatności");
            Console.WriteLine("3. Generuj fakturę");
            Console.WriteLine("0. Powrót do głównego menu");
            Console.Write("Wybierz opcję: ");
            wybor = Console.ReadLine();
            Console.WriteLine();

            switch (wybor)
            {
                case "1":
                    DodajPlatnosc();
                    break;
                case "2":
                    PokazPlatnosci();
                    break;
                case "3":
                    GenerujFakture();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }
            Console.WriteLine();
        }
    }

    private void DodajPlatnosc()
    {
        Console.Write("Podaj ID rezerwacji: ");
        if (!int.TryParse(Console.ReadLine(), out int rezerwacjaId))
        {
            Console.WriteLine("Nieprawidłowy identyfikator rezerwacji.");
            return;
        }

        Console.Write("Podaj kwotę płatności: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal kwota))
        {
            Console.WriteLine("Nieprawidłowa kwota.");
            return;
        }

        Console.Write("Podaj datę płatności (np. 2025-02-19): ");
        string dataPlatnosci = Console.ReadLine();

        Platnosc platnosc = new Platnosc
        {
            RezerwacjaId = rezerwacjaId,
            Kwota = kwota,
            DataPlatnosci = dataPlatnosci
        };

        zarzadzaniePlatnosciami.DodajPlatnosc(platnosc);
        Console.WriteLine("Płatność została dodana!");
    }

    private void PokazPlatnosci()
    {
        var platnosci = zarzadzaniePlatnosciami.PobierzPlatnosci();
        Console.WriteLine("=== Lista Płatności ===");
        foreach (var p in platnosci)
        {
            Console.WriteLine($"ID: {p.Id}, RezerwacjaID: {p.RezerwacjaId}, Kwota: {p.Kwota}, Data: {p.DataPlatnosci}");
        }
    }

    private void GenerujFakture()
    {
        Console.Write("Podaj ID płatności: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Nieprawidłowy identyfikator płatności.");
            return;
        }

        string faktura = zarzadzaniePlatnosciami.GenerujFakture(id);
        Console.WriteLine(faktura);
    }

    #endregion

    #region Menu Użytkowników

    private void MenuUzytkownikow()
    {
        string wybor = "";
        while (wybor != "0")
        {
            Console.WriteLine("=== Zarządzanie użytkownikami ===");
            Console.WriteLine("1. Dodaj użytkownika");
            Console.WriteLine("2. Edytuj użytkownika");
            Console.WriteLine("3. Usuń użytkownika");
            Console.WriteLine("4. Pokaż wszystkich użytkowników");
            Console.WriteLine("0. Powrót do głównego menu");
            Console.Write("Wybierz opcję: ");
            wybor = Console.ReadLine();
            Console.WriteLine();

            switch (wybor)
            {
                case "1":
                    DodajUzytkownika();
                    break;
                case "2":
                    EdytujUzytkownika();
                    break;
                case "3":
                    UsunUzytkownika();
                    break;
                case "4":
                    PokazUzytkownikow();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }
            Console.WriteLine();
        }
    }

    private void DodajUzytkownika()
    {
        Console.Write("Podaj nazwę użytkownika: ");
        string nazwa = Console.ReadLine();

        Console.Write("Podaj rolę użytkownika (np. admin, pracownik, klient): ");
        string rola = Console.ReadLine();

        Uzytkownik uzytkownik = new Uzytkownik
        {
            Nazwa = nazwa,
            Rola = rola
        };

        zarzadzanieUzytkownikami.DodajUzytkownika(uzytkownik);
        Console.WriteLine("Użytkownik został dodany!");
    }

    private void EdytujUzytkownika()
    {
        Console.Write("Podaj ID użytkownika do edycji: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Nieprawidłowy identyfikator.");
            return;
        }

        Console.Write("Podaj nową nazwę użytkownika: ");
        string nazwa = Console.ReadLine();

        Console.Write("Podaj nową rolę użytkownika: ");
        string rola = Console.ReadLine();

        Uzytkownik nowyUzytkownik = new Uzytkownik
        {
            Nazwa = nazwa,
            Rola = rola
        };

        if (zarzadzanieUzytkownikami.EdytujUzytkownika(id, nowyUzytkownik))
            Console.WriteLine("Użytkownik został zaktualizowany!");
        else
            Console.WriteLine("Użytkownik o podanym ID nie istnieje.");
    }

    private void UsunUzytkownika()
    {
        Console.Write("Podaj ID użytkownika do usunięcia: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Nieprawidłowy identyfikator.");
            return;
        }

        if (zarzadzanieUzytkownikami.UsunUzytkownika(id))
            Console.WriteLine("Użytkownik został usunięty!");
        else
            Console.WriteLine("Użytkownik o podanym ID nie istnieje.");
    }

    private void PokazUzytkownikow()
    {
        var uzytkownicy = zarzadzanieUzytkownikami.PobierzUzytkownikow();
        Console.WriteLine("=== Lista Użytkowników ===");
        foreach (var u in uzytkownicy)
        {
            Console.WriteLine($"ID: {u.Id}, Nazwa: {u.Nazwa}, Rola: {u.Rola}");
        }
    }

    #endregion
}
