using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

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
        private StringBuilder csv_content;
        private string csv_path;

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
            csv_content = new StringBuilder();
        }

        public string Algoritmo(int n_iteraciones)
        {
            csv_content.AppendLine("INDICE,INDICE DE G,FORMA,VALOR");

            // Inicializar la matriz de velocidades
            for (int i = 0; i < n_particulas; i++)
                for (int j = 0; j < n_dimensiones; j++)
                    velocidades[i, j] = random.Next(-6, 6);

            for (int i = 0; i < this.X.Length; i++) //Inicializar partículas
                this.X[i].Inicializar();

            this.P = this.X;  //Copiar X a P

            int contador = 0;
            while (n_iteraciones > 0) //Establecer condicion de paro
            {
                for (int i = 0; i < this.n_particulas; i++) //Paso2
                {
                    if (this.Maximizar(X[i].Valores) > this.Maximizar(P[i].Valores)) //Paso 3
                        for (int d = 0; d < n_dimensiones; d++) 
                            P[i].Valores[d] = X[i].Valores[d];

                    g = i;
                    //Ver y guardar qué sucede con g en cada iteración
                    //csv_content.AppendLine(contador + "," + g + "," + this.toString(this.P[g].Valores) + "," + this.Maximizar(this.P[g].Valores));

                    for (int j = 0; j < this.n_particulas; j++)
                        if (this.Maximizar(this.P[j].Valores) > this.Maximizar(this.P[g].Valores))
                            g = this.SeleccionarVecino(j);
                            //g = j;

                    for (int d = 0; d < n_dimensiones; d++)
                    {
                        this.velocidades[i, d] = w * this.velocidades[i, d] + c1 * r1 * (this.P[i].Valores[d] - this.X[i].Valores[d]) + c2 * r2 * (this.P[g].Valores[d] - this.X[i].Valores[d]);

                        if (random.NextDouble() < this.Sigmoide(this.velocidades[i, d]))
                            this.X[i].Valores[d] = 1;
                        else
                            this.X[i].Valores[d] = 0;
                    }
                }
                csv_content.AppendLine(contador + "," + g + "," + this.toString(this.P[g].Valores) + "," + this.Maximizar(this.P[g].Valores));
                n_iteraciones--;
                contador++;
            }
            DateTime dt = DateTime.Now;
            csv_path = "D:\\CSV\\bpso" + dt.Minute + dt.Second + dt.Millisecond + ".csv";
            File.AppendAllText(csv_path, csv_content.ToString());

            return "La mejor partícula es: " + this.toString(this.P[g].Valores) + "; Con un fitness de: " + this.Maximizar(this.P[g].Valores);
        }

        private int SeleccionarVecino(int pos_mejor)
        {
            int vecino_inf = pos_mejor - 1, vecino_sup = pos_mejor + 1;
            double fit_v_inf = 0, fit_v_sup = 0;

            if (vecino_inf > -1)
                fit_v_inf = this.Maximizar(this.P[vecino_inf].Valores);

            if (!(vecino_sup > (this.n_particulas - 1)))
                fit_v_sup = this.Maximizar(this.P[vecino_sup].Valores);

            if (fit_v_inf > fit_v_sup)
                return vecino_inf;
            else
                return vecino_sup;
        }

        private string toString(int[] valor)
        {
            string val = "" + valor[0] + valor[1] + valor[2] + valor[3] + valor[4] + valor[5] + valor[6] + valor[7];
            return val;
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
