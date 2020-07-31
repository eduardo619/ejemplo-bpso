using System;

namespace pso_binario
{
    class PSOBinarioModificado
    {
        private double[,] V, G; // V = Matriz de velocidades, Genotype
        private int[,] X, P; // X = Matriz de población Phenotype, P = Matriz de memoria de PSO
        private const double w = 0.721;
        private const int c1 = 2, c2 = 2;
        private double r1, r2, prc_mutacion;
        private int g, n_particulas, n_dimensiones, v_min, v_max;
        private Random random;

        public PSOBinarioModificado(int nParticulas, int nDimensiones, double prcMutacion, int vMin, int vMax)
        {
            this.n_particulas = nParticulas;
            this.n_dimensiones = nDimensiones;
            this.prc_mutacion = prcMutacion;
            this.v_min = vMin;
            this.v_max = vMax;


            this.random = new Random((int)DateTime.Now.Ticks);

            this.V = new double[this.n_particulas, this.n_dimensiones];
            this.X = new int[this.n_particulas, this.n_dimensiones];
            this.r1 = random.NextDouble();
            this.r2 = random.NextDouble();
        }

        public void Algoritmo(int nIteraciones)
        {
            // Inicializar la matriz de velocidades
            for (int i = 0; i < this.n_particulas; i++)
                for (int j = 0; j < this.n_dimensiones; j++)
                    this.V[i, j] = random.Next(this.v_min, this.v_max);
            this.G = this.V;

            //Inicializar partículas
            for (int i = 0; i < this.n_particulas; i++)
                for (int j = 0; j < this.n_dimensiones; j++)
                    this.X[i, j] = random.Next(0, 2);

            //Copiar la Matriz de Particulas(X) a Memoria de PSO(P)
            this.P = this.X;

            while (nIteraciones != 0)
            {
                for (int i = 0; i < this.n_particulas; i++)
                {
                    if (this.Maximizar(this.getValores(i, this.X)) > this.Maximizar(this.getValores(i, this.P))) //Paso 3
                        for (int d = 0; d < n_dimensiones; d++)
                            P[i, d] = X[i, d];

                    g = i;

                    for (int j = 0; j < this.n_particulas; j++)
                        if (this.Maximizar(this.getValores(j, this.P)) > this.Maximizar(this.getValores(g, this.P)))
                            g = j;

                    for (int d = 0; d < this.n_dimensiones; d++)
                    {
                        this.V[i, d] = w * this.V[i, d] + c1 * this.r1 * (this.P[i, d] - this.X[i, d]) + c2 * this.r2 * (this.P[g, d] - this.X[i, d]);
                        this.G[i, d] = this.G[i, d] + this.V[i, d];

                        if (random.NextDouble() < this.prc_mutacion)
                            this.G[i, d] = -this.G[i, d];

                        if (random.NextDouble() < this.Sigmoide(this.G[i, d]))
                            this.X[i, d] = 1;
                        else
                            this.X[i, d] = 0;
                    }
                }
                nIteraciones--;
            }
        }

        private int[] getValores(int indice, int[,] matriz)
        {
            int[] res = new int[this.n_dimensiones];

            for (int i = 0; i < this.n_dimensiones; i++)
                res[i] = matriz[indice, i];

            return res;
        }

        public double Maximizar(int[] valor) //De 0 a 255
        {
            string val = "" + valor[0] + valor[1] + valor[2] + valor[3] + valor[4] + valor[5] + valor[6] + valor[7];

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
