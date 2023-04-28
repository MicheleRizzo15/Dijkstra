using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
{
    public class Arco
    {
        int costo;
        Vertice v1, v2;

        public Arco(int costo, Vertice v1, Vertice v2)
        {
            this.costo = costo;
            this.v1 = v1;
            this.v2 = v2;
        }

        public int Costo { get => costo; }
        public Vertice V1 { get => v1; }
        public Vertice V2 { get => v2; }

        public override bool Equals(object obj)
        {
            return (this.v1 == (obj as Arco).v1 || this.v1 == (obj as Arco).v2) && (this.v2 == (obj as Arco).v1 || this.v2 == (obj as Arco).v2);
        }
    }
}
