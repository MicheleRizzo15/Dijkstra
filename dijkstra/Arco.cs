using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
{
    /// <summary>
    /// Classe che rappresenta un collegamento tra 2 vertici
    /// Caratterizzato da un vertice di partenza ed uno di arrivo ed un costo logico del collegamento
    /// </summary>
    public class Arco
    {
        #region Variabili e proprietà
        /// <summary>
        /// costo logico del collegamento
        /// </summary>
        int costo;
        /// <summary>
        /// Vertice di partenza e di arrivo
        /// </summary>
        Vertice v1, v2;
        /// <summary>
        /// Costo logico del collegamento
        /// </summary>
        public int Costo { get => costo; }
        /// <summary>
        /// Vertice di partenza
        /// </summary>
        public Vertice V1 { get => v1; }
        /// <summary>
        /// Vertice di arrivo
        /// </summary>
        public Vertice V2 { get => v2; }
        #endregion

        #region Costruttori
        /// <summary>
        /// Crea un collegamento dal costo desiderato tra i 2 vertici
        /// </summary>
        /// <param name="costo">costo logico del collegamento</param>
        /// <param name="v1">vertice di partenza</param>
        /// <param name="v2">vertice di arrivo</param>
        public Arco(int costo, Vertice v1, Vertice v2)
        {
            this.costo = costo;
            this.v1 = v1;
            this.v2 = v2;
        }

        #endregion

        #region Override
        /// <summary>
        /// Due collegamenti sono uguali se nodo di partenza e di arrivo (o viceversa) sono uguali
        /// </summary>
        /// <param name="obj">collegamento da verificare</param>
        /// <returns>true se sono uguali, false altrienti</returns>
        public override bool Equals(object obj)
        {
            return (this.v1 == (obj as Arco).v1 || this.v1 == (obj as Arco).v2) && (this.v2 == (obj as Arco).v1 || this.v2 == (obj as Arco).v2);
        }

        #endregion

    }
}
