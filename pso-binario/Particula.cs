using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pso_binario
{
    class Particula
    {
        private int[] valores;
        private Random rand;
        private double fitness;
        public int[] Valores
        {
            get { return valores; }
            set { valores = value; }
        }

        public double Fitness
        {
            get { return fitness; }
            set { fitness = value; }
        }

        public Particula()
        {
            valores = new int[8];
            rand = new Random((int)DateTime.Now.Ticks);
        }

        public void Inicializar()
        {
            for (int i = 0; i < valores.Length; i++)
                valores[i] = rand.Next(0, 2);
        }
    }
}
