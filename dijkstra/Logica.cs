using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
{
    public static class Logica
    {
        /// <summary>
        /// Ritorna il riferimento al nodo più vicino
        /// </summary>
        /// <param name="x">ascissa richiesta</param>
        /// <param name="y">ordinata richiesta</param>
        /// <param name="lv">lista di vertici dove cercare</param>
        /// <returns>riferimento al vertice, null se non disponibile</returns>
        public static Vertice TrovaVertice(int x, int y, List<Vertice> lv)
        {
            for(int i=0; i < lv.Count; i++)
            {
                if (GetDistanza(lv[i], x, y)<=Vertice.Raggio*2)
                {
                    return lv[i];
                }
            }

            return null;
        }

        public static float GetDistanza(Vertice v1, Vertice v2)
        {
            return (float)(Math.Sqrt((double)(((double)Math.Pow((v2.X - v1.X), 2)) + (double)(Math.Pow((v2.Y - v1.Y), 2)))));
        }

        public static float GetDistanza(Vertice v1, int x, int y)
        {
            return (float)(Math.Sqrt((double)(((double)Math.Pow((x - v1.X), 2)) + (double)(Math.Pow((y - v1.Y), 2)))));
        }
    }
}
