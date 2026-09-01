using System;
using System.Collections.Generic;
using System.ComponentModel;

enum TipoDiPersonaggio
{
    Arciere,
    Mago,
    Curatore,
    Guerriero,
    Ladro
}

class Personaggio
{
    public TipoDiPersonaggio tipoPrincipale {get; private set;}
    public uint Livello = 0;
    public string Name;

    public Personaggio(TipoDiPersonaggio tipoPrincipale, string Name)
    {
        this.Name = Name;
        this.tipoPrincipale = tipoPrincipale;
    }
}

class Gilda
{
    public string Name = "";
    public Personaggio personaggio1 {get; private set;}
    public Personaggio personaggio2 {get; private set;}
    public Personaggio personaggio3 {get; private set;}

    public Gilda(
        string Name, 
        Personaggio personaggio1, 
        Personaggio personaggio2,
        Personaggio personaggio3
    )
    {
        this.Name = Name;
        this.personaggio1 = personaggio1;
        this.personaggio2 = personaggio2;
        this.personaggio3 = personaggio3;
    }

    public void StampaInfo()
    {
        Console.WriteLine($"Nome gilda: {Name}");
        Console.WriteLine($"Personaggio 1: {personaggio1.tipoPrincipale}");
        Console.WriteLine($"Personaggio 2: {personaggio2.tipoPrincipale}");
        Console.WriteLine($"Personaggio 3: {personaggio3.tipoPrincipale}");
    }

    public void Menu()
    {
        bool menuRunning = true;
        while (menuRunning)
        {
            Console.WriteLine("Cosa vuoi fare");
            Console.WriteLine("1. Avvia missione");
            Console.WriteLine("2. Incanta armi");
            Console.WriteLine("3. Esci");

            int scelta = Convert.ToInt16(Console.ReadLine());

            switch (scelta)
            {
                case 1:
                    break;
                    
                case 2:
                    break;

                case 3:
                    menuRunning = false;
                    break;
            }
        }
    }

    public static TipoDiPersonaggio ScegliendoPersonaggio(int n)
    {
        bool scegliendoPersonaggio = true;
        TipoDiPersonaggio tipoDiPersonaggio = TipoDiPersonaggio.Arciere;

        while(scegliendoPersonaggio)
        {
            Console.WriteLine($"Inserire il tipo del personaggio {n}");
            Console.WriteLine("1. Arciere");
            Console.WriteLine("2. Mago");
            Console.WriteLine("3. Curatore");
            Console.WriteLine("4. Guerriero");
            Console.WriteLine("5. Ladro");

            int scelta = Convert.ToInt16(Console.ReadLine());

            if(scelta < 1 || scelta > 5)
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            else 
            {
                tipoDiPersonaggio = (TipoDiPersonaggio)(scelta - 1);
                scegliendoPersonaggio = false;
            }
        }

        return tipoDiPersonaggio;
    }
}

class mainClass
{
    static void Main(string[] args)
    {
        bool mainMenu = true;
        List<Gilda> gilde = new List<Gilda>();

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
            Console.WriteLine("5. Esci (sconsigliato)\n");

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
                            string gildaNome = Console.ReadLine();

                            while (gilde.Contains(gilde.Find(x => x.Name == gildaNome)))
                            {
                                Console.WriteLine("Non puoi creare due gilde con lo stesso nome");
                                Console.WriteLine("Per favore inserire un nuovo nome");
                                gildaNome = Console.ReadLine();
                            }

                            TipoDiPersonaggio tipoDiPersonaggio1 = Gilda.ScegliendoPersonaggio(1);
                            Console.WriteLine("Nome primo personaggio: ");
                            string name1 = Console.ReadLine();

                            TipoDiPersonaggio tipoDiPersonaggio2 = Gilda.ScegliendoPersonaggio(2);
                            while(tipoDiPersonaggio2 == tipoDiPersonaggio1)
                            {
                                Console.WriteLine("Scelta invalida!\nNon puoi scegliere due volte lo stesso tipo di personaggio");
                                tipoDiPersonaggio2 = Gilda.ScegliendoPersonaggio(2);
                            }
                            Console.WriteLine("Nome secondo personaggio: ");
                            string name2 = Console.ReadLine();

                            TipoDiPersonaggio tipoDiPersonaggio3 = Gilda.ScegliendoPersonaggio(3);
                            while(tipoDiPersonaggio3 == tipoDiPersonaggio2 || tipoDiPersonaggio3 == tipoDiPersonaggio1)
                            {
                                Console.WriteLine("Scelta invalida!\nNon puoi scegliere due volte lo stesso tipo di personaggio");
                                tipoDiPersonaggio2 = Gilda.ScegliendoPersonaggio(3);
                            }
                            Console.WriteLine("Nome terzo personaggio: ");
                            string name3 = Console.ReadLine();

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
                    string NomeDaEliminare = Console.ReadLine();

                    Gilda gildaDaEliminare = gilde.Find(x => x.Name == NomeDaEliminare);

                    while (gildaDaEliminare == null)
                    {
                        Console.WriteLine("Gilda non trovata");
                        NomeDaEliminare = Console.ReadLine();
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
