using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BornfightTask.Models
{
    public class Armies
    {
        public int Army1 { get; set; }
        public int Army2 { get; set; }

        public Armies()
        {

        }

        public Armies(int army1, int army2)
        {
            this.Army1 = army1;
            this.Army2 = army2;
        }

        public static (Armies, List<string>) Clash(Armies armies, List<string> log)
        {
            Random rnd = new Random();

            //earthquake has 10% of chance to happen
            int earthquakeChance = rnd.Next(1, 101);
            if(earthquakeChance  < 11)
            {
                //earthquake happened, eliminate random number of soldiers from each army
                log.Add("EARTHQUAKE!");
                int percentageOfEarthquakeCasualtiesArmy1 = rnd.Next(10, 16);
                int percentageOfEarthquakeCasualtiesArmy2 = rnd.Next(10, 16);
                armies = ChopPercentageOfSoldiersFromEachArmy(armies, percentageOfEarthquakeCasualtiesArmy1, percentageOfEarthquakeCasualtiesArmy2);
            }

            //major conflict is here below, each clash makes each amry lose between 10 and 15 percent of soldiers
            log.Add("FIGHT!");
            int percentageOfClashCasualtiesArmy1 = rnd.Next(10, 16);
            int percentageOfClashCasualtiesArmy2 = rnd.Next(10, 16);

            armies = ChopPercentageOfSoldiersFromEachArmy(armies, percentageOfClashCasualtiesArmy1, percentageOfClashCasualtiesArmy2);
            return (armies, log);
        }

        private static Armies ChopPercentageOfSoldiersFromEachArmy(Armies armies, int percentage1, int percentage2)
        {
            int casualtiesArmy1 = (percentage1 * armies.Army1) / 100;
            int casualtiesArmy2 = (percentage2 * armies.Army2) / 100;

            if (armies.Army1 < 10)
            {
                Random rnd = new Random();
                casualtiesArmy1 = rnd.Next(1, 11);
            }

            if (armies.Army2 < 10)
            {
                Random rnd = new Random();
                casualtiesArmy2 = rnd.Next(1, 11);
            }

            //we create 2 new armies with casualties applied
            return new Armies(armies.Army1-casualtiesArmy1, armies.Army2-casualtiesArmy2);
        }
    }
}
