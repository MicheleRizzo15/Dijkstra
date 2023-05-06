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
    public partial class CreaVertice : Form
    {
        #region Variabili e proprietà
        /// <summary>
        /// Vertice che verrà creato
        /// </summary>
        Vertice v;

        /// <summary>
        /// Vertice creato
        /// </summary>
        internal Vertice Vertice { get => v; }

        #endregion

        #region Costruttori
        /// <summary>
        /// Costruttore di default
        /// </summary>
        public CreaVertice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Costrurroe di un vertice con x e y desiderati
        /// </summary>
        /// <param name="x">ascissa del vertice da creare</param>
        /// <param name="y">ordinata del vertice da creare</param>
        public CreaVertice(int x, int y) : this()
        {
            textBox2.Text = x.ToString(); //Setta le textbox in readonly e mette il valore delle x e y richieste
            textBox2.ReadOnly = true;
            textBox3.Text = y.ToString();
            textBox3.ReadOnly = true;
        }

        private void CreaVertice_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Bottoni
        /// <summary>
        /// Bottone di conferma creazione vertice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != string.Empty) //se c'è un nome valido
            {
                if (textBox1.Text.Trim().Length > 5) //se il nome è troppo segnalo errore
                {
                    MessageBox.Show("Nome troppo lungo (supera i 5 caratteri)");
                }
                else //il nome deve essere cosono con le dimensioni, si crea il vertice e si chiude il form
                {
                    v = new Vertice(textBox1.Text.Trim(), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Impossibile creare un vertice senza nome");
            }
        }

        /// <summary>
        /// Bottone di annulla operazione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}
