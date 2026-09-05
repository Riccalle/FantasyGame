using GildaCodice;

namespace LibreriaMissioni
{
    abstract class Missione
    {
        abstract public void MainMenu();
        public Random random = new Random();
    }

    class MissioneForesta1 : Missione
    {
        public Gilda gilda;

        public MissioneForesta1(Gilda gilda)
        {
            this.gilda = gilda;
        }

        public override void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Benvenuto nella foresta");
            Console.WriteLine("Mentre la gilda si trova in una foresta fitta un albero cade bloccando la strada dietro di essi");
            Console.WriteLine("Sono costretti ad avventurarsi nella foresta sconosciuta a ognuno di loro");
            Console.WriteLine("Mentre passeggiano incontrano una gang di goblin affamata intenta a usare ogni mezzo necessario per mettere tra i denti un po' di carne");
            Console.WriteLine("Decidono di attaccarli");
        }

        public void MenuCombattimento1()
        {
            int vitaGoblin = random.Next(300, 400);
            int vitaGoblinLanciere = random.Next(250, 300);

            while (true)
            {
                Console.WriteLine("Inizio del combattimento");
                Console.WriteLine("Goblin 1:");
                Console.WriteLine($"Vita: {vitaGoblin}");

                Console.WriteLine("Goblin 2:");
                Console.WriteLine($"Vita: {vitaGoblin}");

                Console.WriteLine("Goblin lanciere:");
                Console.WriteLine($"Vita: {vitaGoblinLanciere}\n");

                Console.WriteLine("Che azione si vuole fare?");
                Console.WriteLine("1. Attacco");
                Console.WriteLine("2. Inventario");
                Console.WriteLine("3. salta turno");
                Console.WriteLine("4. Info");
                Console.WriteLine("5. Esci");
                
                int scelta = Convert.ToInt16(Console.ReadLine() ?? "0");
                switch (scelta)
                {
                    case 1:
                        gilda.Attacca();
                        break;

                    case 2:
                        gilda.Inventario();
                        break;

                    case 3:
                        Console.WriteLine("Hai saltato il turno");
                        break;

                    case 4:
                        Console.WriteLine("----------------------");
                        Console.WriteLine("Gang di goblin");
                        Console.WriteLine("----------------------");
                        Console.WriteLine("Membri: 3");

                        Console.WriteLine("Membro 1: Goblin");
                        Console.WriteLine("Un semplice goblin affamato");
                        Console.WriteLine("Per quanto si creda forte, è un sempliciotto");
                        Console.WriteLine($"Vita: {vitaGoblin}");

                        Console.WriteLine("Membro 2: Goblin");
                        Console.WriteLine("Un semplice goblin affamato");
                        Console.WriteLine("Per quanto si creda forte, è un sempliciotto");
                        Console.WriteLine($"Vita: {vitaGoblin}");

                        Console.WriteLine("Membro 3: Goblin lanciere");
                        Console.WriteLine("Questo goblin ha passato la sua vita ad allenarsi con la lancia");
                        Console.WriteLine("Non ha portato a molto però");
                        Console.WriteLine($"Vita: {vitaGoblinLanciere}");
                        break;

                    case 5:
                        Console.WriteLine("Missione terminata");
                        Console.WriteLine("La gilda verrà inviata al campo di addestramento");
                        return;
                        
                    default:
                        Console.WriteLine("Non è una scelta valida!");
                        break;
                }
            }
        }
    }

    class MissioneCampo1 : Missione
    {
        public override void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Benvenuto nel campo");
        }
    }   
}