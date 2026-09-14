using System.Text.Json;
using GildaCodice;
using LibreriaMissioni;
using LibreriaPersonaggi;

class MainClass
{
    static void Main(string[] args)
    {
        string fileDirectory = "SaveFile.json";
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

                            TipoDiPersonaggio tipoDiPersonaggio1 = Gilda.ScegliendoPersonaggio(1);
                            Console.WriteLine("Nome primo personaggio: ");
                            string name1 = Console.ReadLine() ?? "";

                            TipoDiPersonaggio tipoDiPersonaggio2 = Gilda.ScegliendoPersonaggio(2);
                            while(tipoDiPersonaggio2 == tipoDiPersonaggio1)
                            {
                                Console.WriteLine("Scelta invalida!\nNon puoi scegliere due volte lo stesso tipo di personaggio");
                                tipoDiPersonaggio2 = Gilda.ScegliendoPersonaggio(2);
                            }
                            Console.WriteLine("Nome secondo personaggio: ");
                            string name2 = Console.ReadLine() ?? "";

                            TipoDiPersonaggio tipoDiPersonaggio3 = Gilda.ScegliendoPersonaggio(3);
                            while(tipoDiPersonaggio3 == tipoDiPersonaggio2 || tipoDiPersonaggio3 == tipoDiPersonaggio1)
                            {
                                Console.WriteLine("Scelta invalida!\nNon puoi scegliere due volte lo stesso tipo di personaggio");
                                tipoDiPersonaggio2 = Gilda.ScegliendoPersonaggio(3);
                            }
                            Console.WriteLine("Nome terzo personaggio: ");
                            string name3 = Console.ReadLine() ?? "";

                            Personaggio personaggio1 = new Personaggio(tipoDiPersonaggio1, name1);
                            Personaggio personaggio2 = new Personaggio(tipoDiPersonaggio2, name2);
                            Personaggio personaggio3 = new Personaggio(tipoDiPersonaggio3, name3);

                            Gilda gilda = new Gilda(
                                gildaNome,
                                personaggio1,
                                personaggio2,
                                personaggio3
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