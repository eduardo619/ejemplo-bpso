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

        public int[] Valores
        {
            get { return valores; }
            set { valores = value; }
        }

        public Particula()
        {
            valores = new int[8];
            rand = new Random((int)DateTime.Now.Ticks);
        }
    }
}
