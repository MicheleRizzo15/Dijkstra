using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
{
    public class Vertice
    {
        string nome;
        int x, y;
        static float raggio = 10;

        public static float Raggio { get => raggio; set => raggio = value; }
        public int X { get => x; }
        public int Y { get => y; }
        public string Nome { get => nome;}

        public Vertice(string nome, int x, int y)
        {
            this.nome = nome.Trim();
            this.x = x;
            this.y = y;
        }

        private float GetDistanza(Vertice v1)
        {
            //return (float)(Math.Sqrt((double)(((double)Math.Pow((this.x - v1.x), 2)) + (double)(Math.Pow((this.y - v1.y), 2)))));
            return Logica.GetDistanza(this, v1);
        }

        public bool Sovrapponi(List<Vertice> listV)
        {
            foreach (Vertice item in listV)
            {
                if (this.GetDistanza(item) < (raggio * 4))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return this.X == (obj as Vertice).X && this.Y == (obj as Vertice).Y || this.Nome == (obj as Vertice).Nome;
        }
    }
}