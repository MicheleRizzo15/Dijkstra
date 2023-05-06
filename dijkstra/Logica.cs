using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
{
    /// <summary>
    /// Classe statica che raccoglie la logica di funzioamento dell'algorimto di Dijkstra e implementa alcune funzioni utili e usate nelle varie classi e form
    /// </summary>
    /// 

    public static class Logica
    {
        #region Trova archi-Nodi a partire da x e y in una lista
        /// <summary>
        /// Ritorna il riferimento al vertice più vicino del punto con x e y passato contenuta nella lista passata
        /// </summary>
        /// <param name="x">ascissa richiesta</param>
        /// <param name="y">ordinata richiesta</param>
        /// <param name="lv">lista di vertici dove cercare</param>
        /// <returns>riferimento al vertice, null se non disponibile</returns>
        public static Vertice TrovaVertice(int x, int y, List<Vertice> lv)
        {
            for (int i = 0; i < lv.Count; i++)
            {
                if (GetDistanza(lv[i], x, y) <= Vertice.Raggio * 2) //il più vicino è la prima occorrenza che dista meno di 2* raggio
                {
                    return lv[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Ritorna il riferimento all'arco più vicino del punto con x e y passato contenuta nella lista passata
        /// Possono essere eliminati più archi contemporanemante
        /// </summary>
        /// <param name="la">lista degli archi</param>
        /// <param name="x">ascissa richiesta</param>
        /// <param name="y">ordinata richiesta</param>
        /// <returns>riferimento al vertice, null se non disponibile</returns>
        public static List<Arco> TrovaArchi(List<Arco> la, int x, int y)
        {
            List<Arco> archiTrovati = new List<Arco>();
            for (int i = 0; i < la.Count; i++)
            {
                //forumla distanza del punto x,y dalla retta che unisce i vertici dei punti del collegamento (formula matematica retta per due punti e distanza punto-retta)
                if (Convert.ToInt32(Math.Abs(((la[i].V2.Y - la[i].V1.Y) * x) + ((la[i].V1.X - la[i].V2.X) * y) - ((la[i].V1.X) * (la[i].V2.Y)) + (la[i].V1.Y) * (la[i].V2.X)) / Math.Sqrt(Math.Pow((la[i].V2.Y - la[i].V1.Y), 2) + Math.Pow((la[i].V1.X - la[i].V2.X), 2))) <= Vertice.Raggio)
                {
                    archiTrovati.Add(la[i]); //posso eliminare più archi
                }
            }
            if (archiTrovati.Count > 0)
            {
                return archiTrovati; //se trovo almeno un arco
            }
            else
            {
                return null; //se non trovo nessun arco
            }
        }

        #endregion

        #region Distanza tra due punti

        /// <summary>
        /// Distanza euclidea tra 2 punti
        /// </summary>
        /// <param name="v1">vertice 1</param>
        /// <param name="v2">vertice 2</param>
        /// <returns>ritorna la distnza tra i 2 punti passati come parametri</returns>
        public static float GetDistanza(Vertice v1, Vertice v2)
        {
            //formula distanza euclidea tra 2 punti
            return (float)(Math.Sqrt((double)(((double)Math.Pow((v2.X - v1.X), 2)) + (double)(Math.Pow((v2.Y - v1.Y), 2)))));
        }

        /// <summary>
        /// Override del metodo della disatnza tra 2 punti, passando però l'ascissa e l'ordinata del secondo punto
        /// </summary>
        /// <param name="v1">vertice 1</param>
        /// <param name="x">ascissa vertice 2</param>
        /// <param name="y">ascissa vertice 2</param>
        /// <returns>disatzna euclidea tra v1 e il punto identificato da x e y passati</returns>
        public static float GetDistanza(Vertice v1, int x, int y)
        {
            //formula disatzna euclidea tra 2 punti
            return (float)(Math.Sqrt((double)(((double)Math.Pow((x - v1.X), 2)) + (double)(Math.Pow((y - v1.Y), 2)))));
        }

        #endregion

        #region Dijkstra

        #region classe supporto
        /// <summary>
        /// Classe usata per implementare l'algoritmo dijkstra
        /// Privata in quanto deve essere utilizzata solo in Logica e sfrutta la classe Vertice
        /// </summary>
        private class VerticeConCosto
        {
            #region Variabili e proprietà
            /// <summary>
            /// Vertice che si vuole analizzare
            /// </summary>
            Vertice v;
            /// <summary>
            /// Costo assoluto per raggiungere il vertice v dal nodo di partenza
            /// </summary>
            int costo;
            /// <summary>
            /// Se il nodo è già stato analizzato questo campo diventa false
            /// </summary>
            bool scelto;
            /// <summary>
            /// Vertice precedente collegato al vertice v, utile per ricostruire i collegamenti e la tabella
            /// </summary>
            Vertice collegato;
            /// <summary>
            /// Vertice che si sta analizzando
            /// </summary>
            public Vertice Nodo { get => v; set => v = value; }
            /// <summary>
            /// Costo assoluto per raggiungere il Vertice Nodo dal vertice partenza
            /// </summary>
            public int Costo { get => costo; set => costo = value; }
            /// <summary>
            /// Se il nodo è già stato analizzato questo campo diventa false
            /// </summary>
            public bool Scelto { get => scelto; set => scelto = value; }
            /// <summary>
            /// Vertice precedente collegato al vertice v, utile per ricostruire i collegamenti e la tabella
            /// </summary>
            public Vertice Collegato { get => collegato; set => collegato = value; }
            #endregion
        }
        #endregion

        #region Implementazione algoritmo Dijkstra
        /// <summary>
        /// Esegue algoritmo dijkstra con i nodi e i collegamenti passati partendo da un vertice di partenza
        /// Passa in out una lista di stringhe che rappresenta la tabella dei collegamento con Nodo, costo totale, nodo precedente
        /// Restituisce una lista di collegamenti che è possibile stampare
        /// </summary>
        /// <param name="nodi">nodi totali</param>
        /// <param name="archi">archi totali</param>
        /// <param name="partenza">nodo di partenza</param>
        /// <param name="testo">rappresenta la tabella dei collegamenti del tipo nodo-costoTotale-NodoPrecedente</param>
        /// <returns>lista di collegamenti minimi</returns>
        public static List<Arco> Disjkstra(List<Vertice> nodi, List<Arco> archi, Vertice partenza, out List<string> testo)
        {
            #region idea algoritmo dijkstra
            /* IDEA ALGORITMO DIJKSTRA
             * L'idea che sta alla base è la seguente:
             * Inizialmente tutte le distanze dal nodo di partenza agli altri sono -1 (non connessi)
             * Successivamente analizzo il nodo di partenza e pongo la sua distanza con se stesso pari a 0, analizzo i vari collegamneti e setto er ogni nodo connesso
             *      la distanza assoluta (il costo del collegamento) nel nodo (costo inizio-fine)
             *      aggiorno l'array delle distanze con la distanza (il primo passaggio)
             *      Ogni nodo raggiunto aggancio come nodo collegato quello di partenza (così da sapere per ogni nodo la disatzna e il nodo dal quale o verso il quale è connesso)
             *      Successivamente impongo che il nodo analizzato (quello di partenza) è stato già considerato e quindi lo tolgo dai nodi da analizzare (seleizonato = true)
             * 
             * 
             * Scelgo ora il nodo la cui distanza assoluta è minore e che non è ancora stato analizzato
             *      analizzo i vari collegamenti e setto per ogni nodo connesso
             *                  la distanza assoluta (il costo per arrivare al nodo "analizzato" + costo del collegamento per arrivare al nodo desiderato o collegato a quello analizzato e
             *                          se prima non era connesso (distanza = -1) aggiorno inserendo il costo nuovo, aggiornando il nodo raggiunto e l'array delle distanze e il nodo predecessore del nodo raggiunto con il nodo di partenza (quello analizzato)
             *                          se prima lo raggiungevo con un collegamento di costo superiore aggiorno il costo nuovo, aggiorno il nodo raggiunto e l'array delle distanze e il nodo predecessore del nodo raggiunto con il nodo di partenza (quello analizzato)
             *                  Setto il nodo seleizonato con selezionato = true
             *      Ripeto l'algoritmo per tante volte quante sono i nodi
             *
             *
             *Per i collegamenti:
             *      Analizzo i nodi con i vari nodi collegati
             *      Cerco all'interno di tutti i collegamenti il collegamento che unisce i 2 nodi
             *      se lo trovo lo aggiungo alla lista dei nodi, altrimenti no
             * 
             * Per la tabella
             *      Scorro tutti i nodi e inserisco il nome, il costo totale per raggiungere tale nodo e il nodo a lui collegato (quello dal quale si arriva)
             */
            #endregion

            testo = new List<string>(); //tabella con collegamenti del tipo Nodo-CostoTotale-NodoPrecednte
            List<Arco> archiDisjkstra = new List<Arco>(); //collegamenti minimi da ritornare
            List<VerticeConCosto> vcc = new List<VerticeConCosto>(); // vettore con i VerticiPrivati utili per il calcolo dell'algoritmo
            VerticeConCosto vccTmp;//VerticePrivato usato per l'algoritmo
            int[] distanze = new int[nodi.Count]; //vettore delle distanze di ogni nodo dal nodo di partenza. L'indice del vettore è l'indice del nodo nella lista di nodi totali
            int indiceNodoDiPartenza = nodi.IndexOf(partenza); //indice del nodo di partenza

            #region Inizializzazione distanze e lista
            //---->INIZIALIZZAZIONE
            for (int i = 0; i < nodi.Count; i++) //tutte le distanze sono -1 in quanto infinite inizialmente
            {
                vccTmp = new VerticeConCosto();
                vccTmp.Nodo = nodi[i];
                vccTmp.Scelto = false;
                vccTmp.Costo = -1;
                distanze[i] = -1;
                vcc.Add(vccTmp); //creo e popolo la lista di tutti i Vertici con le informazioni che mi servono per eseguire l'algorimto come precedente, scelto, costo totale
            }
            //<----INIZIALIZZAIZONE
            #endregion

            int indiceNodo; //indice del nodo del quale ci si collega, nodo che si raggiunge da aggiornare nella lista di nodiPrivati creata per l'algoritmo
            distanze[indiceNodoDiPartenza] = 0; //il nodo di partenza dista 0 da lui stesso
            vcc[indiceNodoDiPartenza].Costo = 0; //aggiorno il nodo di partenza
            vcc[indiceNodoDiPartenza].Scelto = true; //sarà sicuramente il più vicino, quindi, lo scelgo settandolo a true e impedendone le successive modifiche (per evitare che venga considerato nella scelta tra i nodi non selezionati più vicini)

            #region Prima riga della tabella
            //------>PRIMA RIGA DELLA TABELLA
            for (int i = 0; i < archi.Count; i++) //primo giro con inizializzazione distanze del primo passo
            {
                if (archi[i].V1 == nodi[indiceNodoDiPartenza]) //se l'arco inizia dal nodo di partenza
                {
                    indiceNodo = nodi.IndexOf(archi[i].V2); //indice del secondo nodo (dove termina il collegamento)
                    vcc[indiceNodo].Costo = archi[i].Costo; // aggiorno le informazioni del nodo che si raggiunge
                    vcc[indiceNodo].Collegato = archi[i].V1; //il nodo di indiceNodo viene raggiunto dal nodo di partenza ( v1 del collegamento )
                    distanze[indiceNodo] = archi[i].Costo; //aggiorno la distanza del nodo al quale si arriva con il collegamento con il costo in quanto al primo passaggio è -1 (default)
                }
                else if (archi[i].V2 == nodi[indiceNodoDiPartenza])//se l'arco finisce nel nodo di partenza
                {
                    indiceNodo = nodi.IndexOf(archi[i].V1);//indice del primo nodo (dove inizia il collegamento)
                    vcc[indiceNodo].Costo = archi[i].Costo; //aggiorno le informazioni del nodo che si raggiunge
                    vcc[indiceNodo].Collegato = archi[i].V2; //il nodo di indiceNodo viene raggiunto dal nodo di partenza ( v2 del collegamento )
                    distanze[indiceNodo] = archi[i].Costo;//aggiorno la distanza del nodo al quale si arriva con il collegamento con il costo in quanto al primo passaggio è -1 (default)
                }
            }
            //<-------PRIMA RIGA DELLA TABELLA
            #endregion

            int indiceDistanzaMinore; //indica l'indice del nodo non ancora analizzato con la disatzna minore dal nodo di partenza

            #region algoritmo per il calcolo delle distanze minori
            for (int counter = 0; counter < nodi.Count - 1; counter++) //tanti giri quanti sono i nodi
            {
                indiceDistanzaMinore = TrovaMinimoDistanzaNonScelto(distanze, vcc); //nodo più "vicino" al nodo di partenza non ancora scelto
                if (indiceDistanzaMinore != -1) //se esiste allora procedo, altrimenti no
                {
                    for (int i = 0; i < archi.Count; i++) //scorro tutti gli archi per fare i vari calcoli
                    {
                        #region se il nodo analizzato è di partenza di tale collegamento
                        if (archi[i].V1 == nodi[indiceDistanzaMinore]) //se il collegamento riguarda il nodo scelto che voglio analizzare procedo
                        {
                            int indiceNodo2 = nodi.IndexOf(archi[i].V2); //salvo l'indice del secondo nodo (quello che si raggiunge)
                            #region se non raggiungevo il secondo nodo del collegamento
                            if (distanze[indiceNodo2] == -1) //se non raggiungevo l'altro nodo allora aggiorno con il costo del collegamento + costo per raggiungere il nodo corrente
                            {
                                vcc[indiceNodo2].Costo = vcc[indiceDistanzaMinore].Costo + archi[i].Costo; //aggiorno distanza nella lista di nodi privati
                                distanze[indiceNodo2] = vcc[indiceNodo2].Costo; //aggiorno distanza nell'array delle disatzne
                                vcc[indiceNodo2].Collegato = archi[i].V1; //aggiorno il predecessore del nodo collegato con il nodo selezionato
                            }
                            #endregion

                            #region se ho trovato un percorso migliore per raggiungere il secondo nodo del collegamento
                            else if (distanze[indiceNodo2] > vcc[indiceDistanzaMinore].Costo + archi[i].Costo) //se ho trovato un percorso migliore allora aggiorno le distanze allo stesso modo
                            {
                                if (vcc[indiceNodo2].Scelto == false) //se non ho già confermato che il nodo raggiunto era già ottimo allora aggiono le sue informazioni (non del nodo corrente ma di quello da raggiungere)
                                {
                                    vcc[indiceNodo2].Costo = vcc[indiceDistanzaMinore].Costo + archi[i].Costo; //aggiorno il tutto del nodo che viene raggiunto
                                    distanze[indiceNodo2] = vcc[indiceNodo2].Costo;
                                    vcc[indiceNodo2].Collegato = archi[i].V1; //aggiorno il predecessore del nodo collegato con il nodo selezionato
                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region se il nodo analizzato è di arrivo di tale collegamento
                        else if (archi[i].V2 == nodi[indiceDistanzaMinore]) //come sopra ma invertendo il collegamento (se serve), ovvero in questo caso il nodo è l'arrivo del collegamento, non l'inzio
                        {
                            int indiceNodo2 = nodi.IndexOf(archi[i].V1);
                            #region se non raggiungevo il primo nodo del collegamento 
                            if (distanze[indiceNodo2] == -1) //se non era raggiungibile lo aggiorno
                            {
                                vcc[indiceNodo2].Costo = vcc[indiceDistanzaMinore].Costo + archi[i].Costo;
                                distanze[indiceNodo2] = vcc[indiceNodo2].Costo;
                                vcc[indiceNodo2].Collegato = archi[i].V2; //aggiorno il predecessore del nodo collegato con il nodo selezionato
                            }
                            #endregion

                            #region se ho trovato un percorso milgliore per raggiungere il primo nodo del collegamento
                            else if (distanze[indiceNodo2] > vcc[indiceDistanzaMinore].Costo + archi[i].Costo) //se trovo un percorso migliore lo aggiorno
                            {
                                if (vcc[indiceNodo2].Scelto == false)
                                {
                                    vcc[indiceNodo2].Costo = vcc[indiceDistanzaMinore].Costo + archi[i].Costo;
                                    distanze[indiceNodo2] = vcc[indiceNodo2].Costo;
                                    vcc[indiceNodo2].Collegato = archi[i].V2; //aggiorno il predecessore del nodo collegato con il nodo selezionato
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }

                    vcc[indiceDistanzaMinore].Scelto = true; //il nodo che aveva distanza minore è il nodo che ho analizzato, quindi lo metto true così non lo modificherò con successivi aggiornamenti
                }
            }
            #endregion

            #region algoritmo per sceglere i collegamenti da tenere
            for (int i = 0; i < vcc.Count; i++) //scorro tutti i nodi
            {
                for (int j = 0; j < archi.Count; j++) //analizzo tutti gli archi
                {
                    //se l'inizo e la fine dei vertici del collegamento coincidono con il nodo analizzato e il nodo a lui direttament collegato aggiungo tale collegamento nella lista di collegamenti che ritorno
                    //uso il collegatoIsPrecedente per creare in maniera corretta l'if e far combaciare V1 e V2 con il nodo e il suo successivo/predecessore in modo da non creare errori

                    if ((archi[j].V1 == vcc[i].Collegato && archi[j].V2 == vcc[i].Nodo) || (archi[j].V2 == vcc[i].Collegato && archi[j].V1 == vcc[i].Nodo))
                    {
                        archiDisjkstra.Add(archi[j]);
                    }

                }
            }
            #endregion

            #region algorimto per creare la tabella
            testo.Add(String.Format($"Nodo | Costo totale | Precedente"));
            //il formato è NodoDesiderato | costo totale | nodo precedente dal quale si raggiunge
            testo.Add("=================================");
            string s;

            for (int i = 0; i < vcc.Count; i++) //scorro tutti i nodi
            {
                s = null;
                if (i == indiceNodoDiPartenza) //il nodo di partenza non ha costo, ne predecessori
                {
                    s = vcc[i].Nodo.Nome.ToString();
                    while (s.Length < 5)
                    {
                        s += " ";
                    }
                    s += "| ";
                    s += "0";
                    while (s.Length < 13)
                    {
                        s += " ";
                    }
                    s += "| ";
                    s += vcc[i].Nodo.Nome.ToString();
                }
                else
                {
                    if (vcc[i].Collegato != null) //se c'è un nodo collegato
                    {
                        s = vcc[i].Nodo.Nome.ToString(); //il nome è il nodo che analizzo
                        while (s.Length < 5)
                        {
                            s += " ";
                        }
                        s += "| ";
                        s += vcc[i].Costo.ToString(); //il costo è quello relativo al nodo che analizzo
                        while (s.Length < 13)
                        {
                            s += " ";
                        }
                        s += "| ";
                        s += vcc[i].Collegato.Nome.ToString(); //il nodo che devo utilizzare per raggiungerlo è quello a lui collegato  per come abbiamo impostato il tutto [una volta trovato il nodo collegato al nostro analizzato ci spostavamo sul nodo collegato e mettevamo Collegato=nodo analizzato
                    }
                    else //se non ha nodi collegati vuol dire che non è raggiungibile
                    {
                        s = vcc[i].Nodo.Nome.ToString(); //il nodo non è raggiungibile
                        while (s.Length < 5)
                        {
                            s += " ";
                        }
                        s += "| ";
                        s += "---";
                        while (s.Length < 13)
                        {
                            s += " ";
                        }
                        s += "| ---";
                    }
                }
                testo.Add(s);
                testo.Add(String.Format("---------------------------------"));

            }
            #endregion

            return archiDisjkstra;

        }

        /// <summary>
        /// Restituisce l'indice del nodo con costo assoluto minimo che non è ancora stato analizzato sfruttando le varie distanze passate
        /// </summary>
        /// <param name="distanze">vettore delle distanze assolute dei vari nodi, l'indice dei nodi corrisponde alla posizone nell'array</param>
        /// <param name="vcc">nodi tra i quali cercare, così ho modo di vedere quale non è ancora stato scelto</param>
        /// <returns>indice del nodo con costo assoluto minore tra i nodi non analizzati (scelto = false)</returns>
        private static int TrovaMinimoDistanzaNonScelto(int[] distanze, List<VerticeConCosto> vcc)
        {
            int index = -1; //indice del nodo minore
            int valMin = -1; //valore minimo della disatzna
            for (int i = 0; i < distanze.Length; i++) //scorro tutti i nodi
            {
                if (vcc[i].Scelto == false) //se non lo ho scelto analizzo
                {
                    if ((distanze[i] != -1) && (valMin == -1 || distanze[i] < valMin)) //se la disatnza non è valore di default (è collegato quindi e raggiungibile) e o non ho trovato ancora nulla o è migliore di ciò che avevo trovato aggionro i dati che ritornerò
                    {
                        index = i;
                        valMin = distanze[i];
                    }
                }
            }
            return index;
        }
        #endregion
        #endregion
    }
}
