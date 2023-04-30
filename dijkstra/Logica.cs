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

		public static List<Arco> Disjkstra(List<Vertice> nodi, List<Arco> archi)
		{
			List<Arco> archiDisjkstra = new List<Arco>();
			int[] costoPerRaggungereIlNodoDallaPartenza = new int[nodi.Count];
			List<int> nodiSelezionati = new List<int>();
			for (int i = 0; i < nodi.Count; i++)
			{
				costoPerRaggungereIlNodoDallaPartenza[i] = -1;
			}
			nodiSelezionati.Add(0);
			costoPerRaggungereIlNodoDallaPartenza[nodiSelezionati[nodiSelezionati.Count - 1]] = 0;
			while (nodiSelezionati.Count + 1 < nodi.Count)
			{
				int min = -1;
				int indexMin = -1;
				for (int i = 0; i < archi.Count; i++)
				{
					if (archi[i].V1 == nodi[nodiSelezionati[nodiSelezionati.Count - 1]])
					{
						//sono sull'arco del nodo
						if (costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V2)] == -1 || costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V2)] < archi[i].Costo)
						{
							costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V2)] = archi[i].Costo;
							if (min == -1 || min > archi[i].Costo)
							{
								if (min == -1)
								{
									archiDisjkstra.Add(archi[i]);
								}
								else
								{
									archiDisjkstra.RemoveAll(x => x.V1 == nodi[nodiSelezionati[nodiSelezionati.Count - 1]] && x.Costo == costoPerRaggungereIlNodoDallaPartenza[nodiSelezionati[nodiSelezionati.Count - 1]]);
									archiDisjkstra.Add(archi[i]);
								}
								min = archi[i].Costo;
								indexMin = i;
							}
						}
					}
					else if (archi[i].V2 == nodi[nodiSelezionati[nodiSelezionati.Count - 1]])
					{
						//sono sull'arco del nodo
						if (costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V1)] == -1 || costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V1)] < archi[i].Costo)
						{
							costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V1)] = archi[i].Costo;
							if (min == -1 || min > archi[i].Costo)
							{
								if (min == -1)
								{
									archiDisjkstra.Add(archi[i]);
								}
								else
								{
									archiDisjkstra.RemoveAll(x => x.V2 == nodi[nodiSelezionati[nodiSelezionati.Count - 1]] && x.Costo == costoPerRaggungereIlNodoDallaPartenza[nodiSelezionati[nodiSelezionati.Count - 1]]);
									archiDisjkstra.Add(archi[i]);
								}
								min = archi[i].Costo;
								indexMin = i;
							}
						}
					}
				}
				if (indexMin != -1)
				{
					nodiSelezionati.Add(indexMin);
				}
			}
			for (int i = 0; i < archi.Count; i++)
			{
				if (archi[i].V1 == nodi[nodiSelezionati[nodiSelezionati.Count - 1]])
				{
					if (costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V2)] == -1 || costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V2)] < archi[i].Costo)
					{
						costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V2)] = archi[i].Costo;
					}
				}
				else if (archi[i].V2 == nodi[nodiSelezionati[nodiSelezionati.Count - 1]])
				{
					//sono sull'arco del nodo
					if (costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V1)] == -1 || costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V1)] < archi[i].Costo)
					{
						costoPerRaggungereIlNodoDallaPartenza[nodi.IndexOf(archi[i].V1)] = archi[i].Costo;
					}
				}
			}
			return archiDisjkstra;
		}
	}
}
