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
    public partial class Form1 : Form
    {
        List<Vertice> verticiTotali;
        public Form1()
        {
            InitializeComponent();
            verticiTotali = new List<Vertice>();
            comboBox1.DataSource = null;
            comboBox1.DataSource = Enum.GetValues(typeof(Operazione));
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "X: --";
            label2.Text = "Y: --";
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "X: "+ e.X.ToString();
            label2.Text = "Y: " + e.Y.ToString();
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            label1.Text = "X: --";
            label2.Text = "Y: --";
        }

        private void DisegnaVertici()
        {
            Graphics g = panel1.CreateGraphics();
            Pen penna = new Pen(Color.Black, 1);
            foreach (Vertice item in verticiTotali)
            {
                //g.DrawEllipse(penna, item.X, item.Y, 1, 1);
                g.DrawString(item.Nome, new Font("arial", 12), new SolidBrush(Color.Black), new Point(Convert.ToInt32(item.X-Vertice.Raggio), Convert.ToInt32(item.Y - Vertice.Raggio)));
                g.DrawEllipse(penna, item.X - Vertice.Raggio, item.Y - Vertice.Raggio, Vertice.Raggio * 2, Vertice.Raggio * 2);
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBox1.SelectedIndex == (int)Operazione.disegna_vertice)
            {
                CreaVertice fcv = new CreaVertice(e.X, e.Y);
                if (fcv.ShowDialog()==DialogResult.OK)
                {
                    if (fcv.V.Sovrapponi(verticiTotali)==false)
                    {
                        //Vertice tmp = new Vertice(fcv.V);
                        //verticiTotali.Add(tmp);
                        verticiTotali.Add(fcv.V);
                        MessageBox.Show("Vertice aggiunto con successo");
                        label3.Text = "Count Vertici: " + verticiTotali.Count;
                        DisegnaVertici();
                    }
                    else
                    {
                        MessageBox.Show("Impossibile aggiungere il vertice perchè si sovrappone");
                    }
                }

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
