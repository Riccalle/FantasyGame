using System.Globalization;

namespace LibreriaPersonaggi 
{
    enum TipoDiPersonaggio
    {
        Arciere,
        Mago,
        Curatore,
        Guerriero,
        Ladro
    }

    abstract class Personaggio // Siccome oramai la classe personaggio è astratta, bisogna andare ad aggiustare il resto del codice
    {                          // Togliendo i personaggi dichiarati con Personaggio personaggio = new... e mettendo una lista polimorfica
        public TipoDiPersonaggio tipoPrincipale {get; set;}
        public uint Livello {get; set;} = 0;
        public string Name {get; set;} = "";
    }

    class Arciere : Personaggio
    {
        public Arciere(string name)
        {
            tipoPrincipale = TipoDiPersonaggio.Arciere;
            Livello = 0;
            Name = name;
        }

        static int Attacco(int dannoMinimo)
        {
            int danno = dannoMinimo;
            int moltiplicatore;
            int numeroDiFrame = 9;
            string[] frames = new string[numeroDiFrame];

            // Questa parte serve solo per mettere i frame dentro frames;
            // Volendo lo si può fare anche manualmente
            // ES: frames = {"", "|", "||", ...};
            for (int i = 0; i < numeroDiFrame; i++) 
            {
                string frame = "";
                int length = i - i % numeroDiFrame / 2; // C# approssima automaticamente la divisione fra interi

                for (int j = 0; j < length; j++) 
                    frame += "|";

                frames[i] = frame;
            }

            int counter = 0;
            while (true)
            {
                Console.Write($"\r{frames[counter]}");
                if(Console.KeyAvailable) // Controlla se la macchina ha un sistema di input (Es. tastiera)
                {
                    ConsoleKeyInfo key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        // SpaceBar premuta!
                        moltiplicatore = frames[counter].Length;
                        break;
                    }
                }
                Thread.Sleep(20);
                counter++;
            }

            danno += moltiplicatore * 2; // Numero a caso da aggiustare
            // Range di danno:
            // Il minimo danno che si può fare è quello passato come input della funzione
            // Il massimo danno che si può fare è il numero di frame diviso due (approssimato) moltiplicato per il
            // "Numero a caso da aggiustare", sommato al danno base passato in input 

            return danno;
        }
    }
}