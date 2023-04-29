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
            for (int i = 0; i < lv.Count; i++)
            {
                if (GetDistanza(lv[i], x, y) <= Vertice.Raggio * 2)
                {
                    return lv[i];
                }
            }

            return null;
        }

        public static List<Arco> TrovaArchi(List<Arco> la, int x, int y)
        {
            List<Arco> archiTrovati = new List<Arco>();
            for (int i = 0; i < la.Count; i++)
            {
                if (Convert.ToInt32(Math.Abs(((la[i].V2.Y - la[i].V1.Y) * x) + ((la[i].V1.X - la[i].V2.X)*y) - ((la[i].V1.X)*(la[i].V2.Y)) + (la[i].V1.Y)*(la[i].V2.X))/Math.Sqrt(Math.Pow((la[i].V2.Y - la[i].V1.Y),2)+ Math.Pow((la[i].V1.X - la[i].V2.X), 2)))<=Vertice.Raggio)
                {
                    archiTrovati.Add(la[i]);
                }
            }
            if (archiTrovati.Count>0)
            {
                return archiTrovati;
            }
            else
            {
                return null;
            }
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
