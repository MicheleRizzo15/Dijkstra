using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
{
    /// <summary>
    /// Classe che rappresenta il vertice
    /// Caratterizzata da Nome, ascissa e ordinata
    /// Nome univoco, così come posizione nel piano
    /// Variabile statica raggio utile per il disegno e la sovrapposizone
    /// </summary>
    public class Vertice
    {
        #region Variabili e proprietà
        /// <summary>
        /// Nome del vertice
        /// </summary>
        string nome;
        /// <summary>
        /// Coordinate carteisane del vertice
        /// </summary>
        int x, y;
        /// <summary>
        /// Raggio STATICO per tutti i vertice
        /// </summary>
        static float raggio = 10;
        /// <summary>
        /// Raggio dei vertici
        /// </summary>
        public static float Raggio { get => raggio; set => raggio = value; }
        /// <summary>
        /// Ascissa del vertice
        /// La combinazione tra ascissa e ordinata deve essere univoca
        /// </summary>
        public int X { get => x; }
        /// <summary>
        /// Ordinata del vertice
        /// La combinazione tra ascissa e ordinata deve essere univoca
        /// </summary>
        public int Y { get => y; }
        /// <summary>
        /// Nome del vertice, deve essere univoco
        /// </summary>
        public string Nome { get => nome; }

        #endregion

        #region Costruttori
        /// <summary>
        /// Crea un vertice
        /// </summary>
        /// <param name="nome"> nome del vertice</param>
        /// <param name="x"> ascissa del vertice</param>
        /// <param name="y"> ordinata del vertice</param>
        public Vertice(string nome, int x, int y)
        {
            this.nome = nome.Trim();
            this.x = x;
            this.y = y;
        }
        #endregion

        #region metodi istanza
        /// <summary>
        /// Calcola la distanza euclidea del nodo corrente con il nodo passato come parametro
        /// </summary>
        /// <param name="v1">Vertice dal quale si vuole calcolare la distanza</param>
        /// <returns>distanza eculidea tra nodo corrente e nodo passato</returns>
        private float GetDistanza(Vertice v1)
        {
            return Logica.GetDistanza(this, v1); //sfrutta la classe logica per il calcolo della distanza (funzione matematica)
        }
        /// <summary>
        /// Calcola se il nodo si sovrappone con gli altri nodi passati come lista
        /// </summary>
        /// <param name="listV"> lista di nodi del quale vedere se si sovrappone</param>
        /// <returns>true se il nodo si sovrappone ad uno dei nodi passati nella lista, false altrimenti</returns>
        public bool Sovrapponi(List<Vertice> listV)
        {
            foreach (Vertice item in listV)
            {
                if (this.GetDistanza(item) < (raggio * 4)) //logica secondo la quale si sovrappongono o no 2 vertici
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Override
        /// <summary>
        /// Due nodi sono equivalenti se hanno la stessa posizone o se hanno lo stesso nome
        /// </summary>
        /// <param name="obj">Nodo da conforntare</param>
        /// <returns> true se sono uguali, false altrimenti</returns>
        public override bool Equals(object obj)
        {
            return this.X == (obj as Vertice).X && this.Y == (obj as Vertice).Y || this.Nome == (obj as Vertice).Nome;
        }
        #endregion

    }
}