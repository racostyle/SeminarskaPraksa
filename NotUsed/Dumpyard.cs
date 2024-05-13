using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeminarskaPraksa.NotUsed
{
    internal class Dumpyard
    {
    }

    public class RumenAvto : Vozilo{
        public RumenAvto() : base() {
            Console.WriteLine("RumenAvto narejen");
        }
        public override void Vozi() {
            Console.WriteLine("Rumen avto vozi");
        }
    }

    public abstract class Vozilo
    {
        public Vozilo() {
            Console.WriteLine("Vozilo se zaganja");
        }
        public abstract void Vozi(); // Metoda se implementira v podrazredu, kjer jo tudi definiramo
    }

}
