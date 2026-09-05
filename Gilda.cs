namespace GildaCodice
{
    class Gilda
    {
        public string Name {get; set;} = "";
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

        public void Attacco()
        {
            Console.WriteLine("");
        }
    }
}