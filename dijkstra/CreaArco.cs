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
        Vertice v1, v2;
        Arco a;

        public Arco Arco { get => a; private set => a = value; }

        public CreaArco()
        {
            InitializeComponent();
        }

        public CreaArco(Vertice v1, Vertice v2) : this()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox1.Text = v1.Nome;
            textBox2.Text = v2.Nome;
            this.v1 = v1;
            this.v2 = v2;
            a = null;
        }
        private void CreaArco_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = new Arco(Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)), v1, v2);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
