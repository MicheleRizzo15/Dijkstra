using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dijkstra
{
    public partial class CreaArco : Form
    {
        #region Variabili e proprietà
        /// <summary>
        /// i vertici di collegamento per l'arco richeisto
        /// </summary>
        Vertice v1, v2;
        /// <summary>
        /// Arco che si ritornerà
        /// </summary>
        Arco a;

        /// <summary>
        /// Arco creato
        /// </summary>
        public Arco Arco { get => a; private set => a = value; }

        #endregion

        #region costruttori
        /// <summary>
        /// Costruttore di default
        /// </summary>
        public CreaArco()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Costruttore di un arco tra i vertici richiesti
        /// </summary>
        /// <param name="v1">vertice di partenza</param>
        /// <param name="v2">vertice di arrivo</param>
        public CreaArco(Vertice v1, Vertice v2) : this()
        {
            textBox1.ReadOnly = true;//setta le textbox in readonly e assegna loro il nome del nodo
            textBox2.ReadOnly = true;
            textBox1.Text = v1.Nome;
            textBox2.Text = v2.Nome;
            this.v1 = v1;
            this.v2 = v2;
            a = null; //non crea l'arco se non richiesto
        }

        private void CreaArco_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Bottoni
        /// <summary>
        /// Bottone di annullamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;//si annulla l'operaizone
            Close();
        }

        /// <summary>
        /// Bottone di conferma creazione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            a = new Arco(Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)), v1, v2); //si crea un arco e si chiude il form
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion
    } 
}
