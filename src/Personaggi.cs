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
        public int Livello {get; set;} = 0;
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

    class Mago : Personaggio
    {
        public Mago(string name)
        {
            tipoPrincipale = TipoDiPersonaggio.Mago;
            Livello = 0;
            Name = name;
        }

        static int Attacco(int dannoMinimo)
        {
            int danno = dannoMinimo;
            double TempoRimanente = 5; // Tempo rimanente per spammare l'attacco (spacebar)
            string PotenzaAttacco = "";

            // Logica attacco

            while(TempoRimanente > 0)
            {
                Console.Write(TempoRimanente);
                if(Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();

                    if(key.Key == ConsoleKey.Spacebar)
                    {
                        PotenzaAttacco += "|";
                        Console.Clear();
                        Console.Write($"\r{PotenzaAttacco}");
                    }
                }
                TempoRimanente -= 0.01;
                Thread.Sleep(10);
            }

            int Attacco = PotenzaAttacco.Length;
            danno += Attacco * 2; // Moltiplicatore ignoto

            return danno;
        }
    }

    class Curatore : Personaggio
    {
        public Curatore(string name)
        {
            tipoPrincipale = TipoDiPersonaggio.Curatore;
            Livello = 0;
            Name = name;
        }

        int CuraMinima()
        {
            return 50 + Livello * 8;
        }

        private void StampaSchermo(char[,] schermo)
        {
            Console.Clear();
            int row = schermo.GetLength(0);
            int col = schermo.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    Console.Write(schermo[i, j]);
                Console.WriteLine(); // Per cambiare riga
            }
        }

        int Cura(int incrementoCura)
        {
            int cura = CuraMinima();

            // Schermo
            int row = 15;
            int col = 27;

            char[,] schermo = new char[row, col];

            // Timer

            double timer = 0;
            double maxTime = 10;

            double refreshTimer = 0;
            double refreshTimerBound = 1;

            Random rand = new Random();
            List<int> numeriSulloSchermo = new List<int>();
            List<KeyValuePair<int, int>> indiciSulloSchermo = new List<KeyValuePair<int, int>>();

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;

            // Loop principale

            while (timer < maxTime)
            {
                timer += 0.01;
                refreshTimer += 0.01;

                // Ogni tot genera numeri casuali
                if (refreshTimer > refreshTimerBound)
                {
                    // Pulisce lo schermo
                    for (int i = 0; i < row; i++)
                        for (int j = 0; j < col; j++)
                            schermo[i, j] = ' ';
                    indiciSulloSchermo.Clear();
                    numeriSulloSchermo.Clear();
                    
                    refreshTimer = 0.0;

                    for (int i = 0; i < 5; i++)
                    {
                        int num = rand.Next(1, 9);
                        while (numeriSulloSchermo.Contains(num)) // Mescola se il numero c'è già
                            num = rand.Next(1, 9);
                        numeriSulloSchermo.Add(num);

                        int r = rand.Next(0, row);
                        int c = rand.Next(0, col);
                        while (indiciSulloSchermo.Contains(new KeyValuePair<int, int>(r, c)))
                        {
                            r = rand.Next(0, row);
                            c = rand.Next(0, col);
                        }
                        indiciSulloSchermo.Add(new KeyValuePair<int, int>(r, c));

                        schermo[r, c] = (char)('0' + num);
                    }

                    StampaSchermo(schermo);
                }

                // Input player 
                if(Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    int numLetto = key.KeyChar - '0';

                    for (int i = 0; i < numeriSulloSchermo.Count(); i++)
                    {
                        if (numLetto == numeriSulloSchermo[i])
                        {
                            cura += incrementoCura;

                            var index = indiciSulloSchermo[i];
                            schermo[index.Key, index.Value] = ' ';
                            StampaSchermo(schermo);

                            numeriSulloSchermo.RemoveAt(i);
                            indiciSulloSchermo.RemoveAt(i);
                        }
                        else
                            cura -= incrementoCura / 2;
                    }
                }
                Thread.Sleep(10);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = true;
            
            if (cura < 0) 
                cura = 0;
            return cura;
        }
    }

    class Guerriero : Personaggio
    {
        public Guerriero(string name)
        {
            tipoPrincipale = TipoDiPersonaggio.Guerriero;
            Livello = 0;
            Name = name;
        }
    }

    class Ladro : Personaggio
    {
        public Ladro(string name)
        {
            tipoPrincipale = TipoDiPersonaggio.Ladro;
            Livello = 0;
            Name = name;
        }
    }
}
