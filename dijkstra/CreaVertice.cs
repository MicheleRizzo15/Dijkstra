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
        Vertice v;

        internal Vertice V { get => v;}

        public CreaVertice()
        {
            InitializeComponent();
        }

        public CreaVertice(int x, int y):this()
        {
            textBox2.Text = x.ToString();
            textBox2.ReadOnly = true;
            textBox3.Text = y.ToString();
            textBox3.ReadOnly = true;
        }

        private void CreaVertice_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != string.Empty)
            {
                v = new Vertice(textBox1.Text.Trim(), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Impossibile creare un vertice senza nome");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
