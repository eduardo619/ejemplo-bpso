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
        private double[,] velocidades;

        public Enjambre(int n_particulas)
        {
            this.n_particulas = n_particulas;
            random = new Random((int)DateTime.Now.Ticks);
            velocidades = new double[n_particulas, n_dimensiones];

            X = new Particula[this.n_particulas];

            for (int i = 0; i < X.Length; i++)
                X[i] = new Particula();

            r1 = random.Next(0, 2);
            r2 = random.Next(0, 2);
        }

        public void Algoritmo(int n_iteraciones)
        {
            // Inicializar la matriz de velocidades
            for (int i = 0; i < n_particulas; i++)
                for (int j = 0; j < n_dimensiones; j++)
                    velocidades[i, j] = random.Next(-6, 6);

            for (int i = 0; i < this.X.Length; i++) //Inicializar partículas
                this.X[i].Inicializar();

            this.P = this.X;  //Copiar X a P

            while (true) //Establecer condicion de paro
            {
                for (int i = 0; i < this.n_particulas; i++) //Paso2
                {
                    if (this.Maximizar(X[i].Valores) > this.Maximizar(P[i].Valores)) //Paso 3
                        for (int d = 0; d < n_dimensiones; d++)
                            P[i].Valores[d] = X[i].Valores[d];

                    g = i;

                    for (int j = 0; j < this.n_particulas; j++)
                        if (this.Maximizar(this.P[j].Valores) > this.Maximizar(this.P[g].Valores))
                            g = j;

                    for (int d = 0; d < n_dimensiones; d++)
                    {
                        this.velocidades[i, d] = w * this.velocidades[i, d] + c1 * r1 * (this.P[i].Valores[d] - this.X[i].Valores[d]) + c2 * r2 * (this.P[g].Valores[d] - this.X[i].Valores[d]);

                        if (random.NextDouble() < this.Sigmoide(this.velocidades[i, d]))
                            this.X[i].Valores[d] = 1;
                        else
                            this.X[i].Valores[d] = 1;
                    }
                }
            }
        }

        public double Maximizar(int[] valor) //De 0 a 255
        {
            string val = valor[0].ToString() + valor[1].ToString() + valor[2].ToString() + valor[3].ToString() + valor[4].ToString() + valor[5].ToString() + valor[6].ToString() + valor[7].ToString();

            int numero = Convert.ToInt32(val, 2);

            double resultado = Math.Sin(Math.PI * numero / 256);
            return resultado;
        }

        public double Sigmoide(double velocidad)
        {
            return 1 / (1 + Math.Exp(-velocidad));
        }
    }
}
