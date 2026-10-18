using LibreriaPersonaggi;
using LibreriaMissioni;

namespace GildaCodice
{
    class Gilda
    {
        public string Name {get; set;} = "";
        public Personaggio[] Personaggi = new Personaggio[3];

        static int numeroDiMissioni = 5;
        private int missioneAttuale = 0; // La prima missione è quella nuemero 0
        static Missione[,] missioniPossibili =
        {
            // Ci devono essere tanti elementi nella prima dimensione quanto il numero di missioni!
            {   // Missione di livello 1
                new MissioneForesta1(),
                new MissioneCampo1(),
                new MissioneLago1()
            },
            {
                new Missione1_2(),
                new Missione2_2(),
                new Missione3_2()
            },
            {
                new Missione1_3(),
                new Missione2_3(),
                new Missione3_3()
            },
            {
                new Missione1_4(),
                new Missione2_4(),
                new Missione3_4()
            },
            {
                new Missione1_5(),
                new Missione2_5(),
                new Missione3_5()
            }
        };

        Missione[] missioni = new Missione[numeroDiMissioni];
        private void GetMissioni() // Sceglie le missioni casualmente per ogni livello
        {
            Random random = new Random();
            for (int i = 0; i < numeroDiMissioni; i++)
            {
                missioni[i] = missioniPossibili[i, random.Next(0, 2)]; // Ci sono tre missioni per livello (per ora)
                missioni[i].gilda = this; // Così si può fare riferimenti alla gilda
            }
        }

        public Gilda(
            string Name, 
            Personaggio personaggio1, 
            Personaggio personaggio2,
            Personaggio personaggio3
        )
        {
            this.Name = Name;
            Personaggi[0] = personaggio1;
            Personaggi[1] = personaggio2;
            Personaggi[2] = personaggio3;
        }

        public void StampaInfo()
        {
            Console.WriteLine($"Nome gilda: {Name}");
            Console.WriteLine($"Personaggio 1: {Personaggi[0].tipoPrincipale}");
            Console.WriteLine($"Personaggio 2: {Personaggi[1].tipoPrincipale}");
            Console.WriteLine($"Personaggio 3: {Personaggi[2].tipoPrincipale}");
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
                        GetMissioni();
                        missioni[missioneAttuale].MainMenu();
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

        public void Attacca()
        {
            Console.WriteLine("Attacco! WIP");
        }
    }
}
