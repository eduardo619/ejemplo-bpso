using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pso_binario
{
    class Enjambre
    {
        private const double w = 0.721;
        private const int c1 = 2, c2 = 2, n_dimensiones = 8;
        private int r1, r2, g, n_particulas;
        private Particula[] X, P; //Poblacion, memoria de BPSO
        private Random random;

        public Enjambre(int n_particulas)
        {
            this.n_particulas = n_particulas;
            random = new Random((int)DateTime.Now.Ticks);
            X = new Particula[this.n_particulas];

            for (int i = 0; i < X.Length; i++)
            {
                X[i] = new Particula();
            }

            r1 = random.Next(0, 2);
            r2 = random.Next(0, 2);
        }

        public void Algoritmo()
        {
            for (int i = 0; i < this.X.Length; i++)
                this.X[i].Inicializar();

            this.P = this.X;

            while (true) //Establecer condicion de paro
            {
                for (int i = 0; i < this.n_particulas; i++) //Paso2
                {
                    if (this.Maximizar(X[i].Valores) > this.Maximizar(P[i].Valores)) //Paso 3
                    {
                        for (int d = 0; d < n_dimensiones; d++)
                        {
                            P[i].Valores[d] = X[i].Valores[d];
                        }
                    }

                    g = i;

                    for (int j = 0; j < this.n_particulas; j++)
                    {
                        if (this.Maximizar(this.P[j].Valores) > this.Maximizar(this.P[g].Valores))
                            g = j;
                    }

                    for (int d = 0; d < n_dimensiones; d++)
                    {
                        ///this.X[i].Velocidades[d] = (int)(w * this.X[i].Velocidades[d] + c1 * c2 * (this.P[i].Valores[d] - this.X[i].Valores[d]) + c2 * r2 * (this.P[g].Valores[d] - this.X[i].Valores[d]));

                        ////////////
                    }
                }
            }
        }

        public double Maximizar(int[] valor) //De 0 a 255
        {
            string val = string.Empty;

            for (int i = 0; i < valor.Length; i++)
                val += valor[i];

            int numero = Convert.ToInt32(val, 2);

            double resultado = Math.Sin(Math.PI * numero / 256);
            return resultado;
        }

        public double Sigmoide(int velocidad)
        {
            return 1 / (1 + Math.Exp(-velocidad));
        }
    }
}
