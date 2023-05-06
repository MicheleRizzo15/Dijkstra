using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
{
    /// <summary>
    /// Enumeratore che rappresenta le operaizoni possibili
    /// </summary>
    public enum Operazione
    {
        disegna_vertice, //crea un vertice
        cancella_vertice, //cancella un vertice
        disegna_arco, //crea un arco
        cancella_arco, //cancella un arco
        scegli_partenza //cambia il punto di partenza dell'algoritmo di dijsktra
    }
}
