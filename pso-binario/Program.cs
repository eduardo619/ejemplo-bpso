using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pso_binario
{
    class Program
    {
        static void Main(string[] args)
        {
            Enjambre pry = new Enjambre(10);
            pry.Algoritmo();
        }
    }
}
