using System.Text.Json;
using GildaCodice;
using LibreriaMissioni;
using LibreriaPersonaggi;

class MainClass
{
    static void Main(string[] args)
    {
        string fileDirectory = "SaveFiles/SaveFile.json";
        if (!File.Exists(fileDirectory))
        {
            Console.WriteLine("Errore! Non è presente il file di salvataggio!");
            File.WriteAllText(fileDirectory, "[]");
        }

        string saveFileContents = File.ReadAllText(fileDirectory);

        bool mainMenu = true;
        List<Gilda> gilde = JsonSerializer.Deserialize<List<Gilda>>(saveFileContents) ?? new List<Gilda>();

        while(mainMenu)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Benvenuto in NOMEGIOCO");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Cosa vuoi fare?");
            Console.WriteLine("1. Scegliere gilda");
            Console.WriteLine("2. Creare gilda");
            Console.WriteLine("3. Eliminare gilda");
            Console.WriteLine("4. Mostra tutte le gilde");
            Console.WriteLine("5. Salva ed esci (sconsigliato)");

            int sceltaPrincipale = Convert.ToInt16(Console.ReadLine());
            switch(sceltaPrincipale)
            {
                case 1:
                    Console.Clear();
                    if(gilde.Count == 0)
                    {
                        Console.WriteLine("Non hai ancora creato una gilda");
                        Console.WriteLine("Creala dal menù principale!");
                        break;
                    }
                    
                    foreach(Gilda gilda in gilde)
                        gilda.StampaInfo();
                    
                    Console.WriteLine("Inserire nome della gilda a cui si vuole accedere");

                    var newName = Console.ReadLine();
                    var newGilda = gilde.Find(x => x.Name == newName);

                    while (newGilda == null)
                    {
                        Console.WriteLine($"Non è stata trovata una gilda di nome {newName}");
                        Console.WriteLine("Inserire un nuovo nome");
                        newName = Console.ReadLine();
                        newGilda = gilde.Find(x => x.Name == newName);
                    }

                    newGilda.Menu();
                    break;

                case 2:
                    Console.Clear();
                    if(gilde.Count >= 3)
                    {
                        Console.WriteLine("Puoi avere un massimo di tre gilde");
                    }

                    else
                    {
                        bool scegliendoGilda = true;
                        while(scegliendoGilda) 
                        {
                            Console.WriteLine("Inserire il nome della gilda");
                            string gildaNome = Console.ReadLine() ?? "";
                            Gilda? tmp = gilde.Find(x => x.Name == gildaNome); // Controllo se non è null per evitare problemi con l'interpreter

                            if (tmp != null) 
                            {
                                while (gilde.Contains(tmp))
                                {
                                    Console.WriteLine("Non puoi creare due gilde con lo stesso nome");
                                    Console.WriteLine("Per favore inserire un nuovo nome");
                                    gildaNome = Console.ReadLine() ?? "";
                                }
                            }

                            TipoDiPersonaggio[] tipiDiPersonaggio = new TipoDiPersonaggio[3];
                            string[] names = new string[3];

                            tipiDiPersonaggio[0] = Gilda.ScegliendoPersonaggio(1);
                            Console.WriteLine("Nome primo personaggio: ");
                            names[0] = Console.ReadLine() ?? "";

                            tipiDiPersonaggio[1] = Gilda.ScegliendoPersonaggio(2);
                            while(tipiDiPersonaggio[1] == tipiDiPersonaggio[0])
                            {
                                Console.WriteLine("Scelta invalida!\nNon puoi scegliere due volte lo stesso tipo di personaggio");
                                tipiDiPersonaggio[1] = Gilda.ScegliendoPersonaggio(2);
                            }
                            Console.WriteLine("Nome secondo personaggio: ");
                            names[1] = Console.ReadLine() ?? "";

                            tipiDiPersonaggio[2] = Gilda.ScegliendoPersonaggio(3);
                            while(tipiDiPersonaggio[2] == tipiDiPersonaggio[1] || tipiDiPersonaggio[2]== tipiDiPersonaggio[0])
                            {
                                Console.WriteLine("Scelta invalida!\nNon puoi scegliere due volte lo stesso tipo di personaggio");
                                tipiDiPersonaggio[2] = Gilda.ScegliendoPersonaggio(3);
                            }
                            Console.WriteLine("Nome terzo personaggio: ");
                            names[2] = Console.ReadLine() ?? "";

                            Personaggio[] personaggi = new Personaggio[3];

                            for (int i = 0; i < 3; i++)
                            {
                                switch(tipiDiPersonaggio[i])
                                {
                                    case TipoDiPersonaggio.Arciere:
                                        personaggi[i] = new Arciere(names[i]);
                                        break;

                                    case TipoDiPersonaggio.Mago:
                                        personaggi[i] = new Mago(names[i]);
                                        break;

                                    case TipoDiPersonaggio.Curatore:
                                        personaggi[i] = new Curatore(names[i]);
                                        break;

                                    case TipoDiPersonaggio.Guerriero:
                                        personaggi[i] = new Guerriero(names[i]);
                                        break;

                                    case TipoDiPersonaggio.Ladro:
                                        personaggi[i] = new Ladro(names[i]);
                                        break;
                                }
                            }

                            Gilda gilda = new Gilda(
                                gildaNome,
                                personaggi[0],
                                personaggi[1],
                                personaggi[2]
                            );

                            gilde.Add(gilda);
                            scegliendoGilda = false;
                        }
                    }

                    break;

                case 3:
                    Console.Clear();
                    if (gilde.Count == 0)
                    {
                        Console.WriteLine("Non ci sono gilde");
                        Console.WriteLine("Creane una dal menù");
                        break;
                    }

                    foreach (Gilda x in gilde)
                    {
                        Console.WriteLine("Le gilde attuali sono: " + x.Name + "\n");
                    }

                    Console.WriteLine("Inserire il nome della gilda che si vuole eliminare");
                    string NomeDaEliminare = Console.ReadLine() ?? "";

                    Gilda? gildaDaEliminare = gilde.Find(x => x.Name == NomeDaEliminare);

                    while (gildaDaEliminare == null)
                    {
                        Console.WriteLine("Gilda non trovata");
                        NomeDaEliminare = Console.ReadLine() ?? "";
                        gildaDaEliminare = gilde.Find(x => x.Name == NomeDaEliminare);
                    }

                    gilde.Remove(gildaDaEliminare);
                    Console.WriteLine("Gilda eliminata");
                    break;

                case 4:
                    Console.Clear();
                    if(gilde.Count == 0)
                    {
                        Console.WriteLine("Non ci sono gilde");
                        Console.WriteLine("Creane una dal menù");
                        break;
                    }

                    foreach (Gilda x in gilde)
                    {
                        x.StampaInfo();
                    }
                    break;

                case 5:
                    Console.Clear();
                    Console.WriteLine("Grazie per aver giocato!");
                    Console.WriteLine("Ci vediamo la prossima volta");

                    string contentsToSave = JsonSerializer.Serialize(gilde, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(fileDirectory, contentsToSave);

                    mainMenu = false;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Scelta non disponibile!\n\n");
                    break;
            }
        }
    }
}
