using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
{
    class Arco
    {
        int costo;
        Vertice v1, v2;

        public Arco(int costo, Vertice v1, Vertice v2)
        {
            this.costo = costo;
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}
