using System;

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
        }

        public void Inicializar()
        {
            rand = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < valores.Length; i++)
            {
                valores[i] = rand.Next(0, 2);
            }
        }
    }
}
