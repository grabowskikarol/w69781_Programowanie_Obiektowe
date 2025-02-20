using System.Text.Json;

public class ZarzadzanieOfertami
{
    private const string FolderDanych = "Dane";
    private const string SciezkaPliku = FolderDanych + "/oferty.json";
    private List<Oferta> oferty;

    public ZarzadzanieOfertami()
    {
        UpewnijSieZeFolderIstnieje();
        if (!File.Exists(SciezkaPliku))
        {
            File.WriteAllText(SciezkaPliku, "[]");
        }
        oferty = WczytajOferty();
    }

    private void UpewnijSieZeFolderIstnieje()
    {
        if (!Directory.Exists(FolderDanych))
        {
            Directory.CreateDirectory(FolderDanych);
        }
    }

    private List<Oferta> WczytajOferty()
    {
        string json = File.ReadAllText(SciezkaPliku);
        var listaOfert = JsonSerializer.Deserialize<List<Oferta>>(json);
        return listaOfert ?? new List<Oferta>();
    }

    private void ZapiszOferty()
    {
        UpewnijSieZeFolderIstnieje();
        string json = JsonSerializer.Serialize(oferty, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SciezkaPliku, json);
    }

    public List<Oferta> PobierzWszystkieOferty() => oferty;

    public void DodajOferte(Oferta oferta)
    {
        oferta.Id = oferty.Any() ? oferty.Max(o => o.Id) + 1 : 1;
        oferty.Add(oferta);
        ZapiszOferty();
    }

    public bool EdytujOferte(int id, Oferta nowaOferta)
    {
        var oferta = oferty.FirstOrDefault(o => o.Id == id);
        if (oferta == null) return false;
        oferta.Nazwa = nowaOferta.Nazwa;
        oferta.MiejsceDocelowe = nowaOferta.MiejsceDocelowe;
        oferta.Cena = nowaOferta.Cena;
        oferta.DostepneMiejsca = nowaOferta.DostepneMiejsca;
        ZapiszOferty();
        return true;
    }

    public bool UsunOferte(int id)
    {
        var oferta = oferty.FirstOrDefault(o => o.Id == id);
        if (oferta == null) return false;
        oferty.Remove(oferta);
        ZapiszOferty();
        return true;
    }

    // Metoda tworzenia kopii zapasowej pliku z ofertami.
    public void BackupOfert()
    {
        string backupPath = FolderDanych + "/oferty_backup.json";
        File.Copy(SciezkaPliku, backupPath, true);
    }
}