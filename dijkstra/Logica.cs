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





        private class VerticeConCosto
        {
            Vertice v;
            int costo;
            bool scelto;
            public Vertice Nodo { get => v; set => v = value; }
            public int Costo { get => costo; set => costo = value; }
            public bool Scelto { get => scelto; set => scelto = value; }
        }
        public static List<Arco> Disjkstra(List<Vertice> nodi, List<Arco> archi, Vertice partenza)
        {
            List<Arco> archiDisjkstra = new List<Arco>();
            List<VerticeConCosto> vcc = new List<VerticeConCosto>();
            VerticeConCosto vccTmp;
            int[] distanze = new int[nodi.Count];
            //---->INIZIALIZZAZIONE
            for (int i = 0; i < nodi.Count; i++)
            {
                vccTmp = new VerticeConCosto();
                vccTmp.Nodo = nodi[i];
                vccTmp.Scelto = false;
                vccTmp.Costo = -1;
                distanze[i] = -1;
                vcc.Add(vccTmp);
            }
            //<----INIZIALIZZAIZONE

            int indiceNodoDiPartenza = nodi.IndexOf(partenza);
            int indiceNodo;
            distanze[indiceNodoDiPartenza] = 0;
            for (int i = 0; i < archi.Count; i++)
            {
                if (archi[i].V1 == nodi[indiceNodoDiPartenza])
                {
                    indiceNodo = nodi.IndexOf(archi[i].V2);
                    vcc[indiceNodo].Costo = archi[i].Costo;
                    distanze[indiceNodo] = archi[i].Costo;
                }
                else if (archi[i].V2 == nodi[indiceNodoDiPartenza])
                {
                    indiceNodo = nodi.IndexOf(archi[i].V1);
                    vcc[indiceNodo].Costo = archi[i].Costo;
                    distanze[indiceNodo] = archi[i].Costo;
                }
            }
            vcc[indiceNodoDiPartenza].Costo = 0;
            vcc[indiceNodoDiPartenza].Scelto = true;
            int indiceDistanzaMinore;
            //DISTAZNE OK

            for (int counter = 0; counter < nodi.Count - 1; counter++)
            {
                indiceDistanzaMinore = TrovaMinimoDistanzaNonScelto(distanze, vcc); //OK
                for (int i = 0; i < archi.Count; i++)
                {
                    if (archi[i].V1 == nodi[indiceDistanzaMinore])
                    {
                        int indiceNodo2 = nodi.IndexOf(archi[i].V2);
                        if (distanze[indiceNodo2] == -1)
                        {
                            vcc[indiceNodo2].Costo = vcc[indiceDistanzaMinore].Costo + archi[i].Costo;
                            distanze[indiceNodo2] = vcc[indiceNodo2].Costo;
                        }
                        else if (distanze[indiceNodo2] > vcc[indiceDistanzaMinore].Costo + archi[i].Costo)
                        {
                            if (vcc[indiceNodo2].Scelto == false)
                            {
                                vcc[indiceNodo2].Costo = vcc[indiceDistanzaMinore].Costo + archi[i].Costo;
                                distanze[indiceNodo2] = vcc[indiceNodo2].Costo;
                            }
                        }
                    }
                    else if (archi[i].V2 == nodi[indiceDistanzaMinore])
                    {
                        int indiceNodo2 = nodi.IndexOf(archi[i].V1);
                        if (distanze[indiceNodo2] == -1)
                        {
                            vcc[indiceNodo2].Costo = vcc[indiceDistanzaMinore].Costo + archi[i].Costo;
                            distanze[indiceNodo2] = vcc[indiceNodo2].Costo;
                        }
                        else if (distanze[indiceNodo2] > vcc[indiceDistanzaMinore].Costo + archi[i].Costo)
                        {
                            if (vcc[indiceNodo2].Scelto == false)
                            {
                                vcc[indiceNodo2].Costo = vcc[indiceDistanzaMinore].Costo + archi[i].Costo;
                                distanze[indiceNodo2] = vcc[indiceNodo2].Costo;
                            }
                        }
                    }
                }
                vcc[indiceDistanzaMinore].Scelto = true;
            }

            return archiDisjkstra;

        }

        private static int TrovaMinimoDistanzaNonScelto(int[] distanze, List<VerticeConCosto> vcc) //OK
        {
            int index = -1;
            int valMin = -1;
            for (int i = 0; i < distanze.Length; i++)
            {
                if (vcc[i].Scelto == false)
                {
                    if ((distanze[i] != -1) && (valMin == -1 || distanze[i] < valMin))
                    {
                        index = i;
                        valMin = distanze[i];
                    }
                }
            }
            return index;
        }
    }
}
