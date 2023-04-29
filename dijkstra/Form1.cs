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
        List<Arco> archiTotali;
        Vertice v1tmp, v2tmp;
        bool selezionaVertice;
        public Form1()
        {
            InitializeComponent();
            selezionaVertice = false;
            verticiTotali = new List<Vertice>();
            archiTotali = new List<Arco>();
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
            label1.Text = "X: " + e.X.ToString();
            label2.Text = "Y: " + e.Y.ToString();
            if (comboBox1.SelectedIndex == (int)Operazione.disegna_arco && selezionaVertice == true)
            {
                DisegnaTotali();
                disegnaVertice(v1tmp);
                Graphics g = panel1.CreateGraphics();
                Pen penna = new Pen(Color.Black, 1);
                g.DrawLine(penna, new Point(v1tmp.X, v1tmp.Y), new Point(e.X, e.Y));
            }
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
                g.DrawString(item.Nome, new Font("arial", 12), new SolidBrush(Color.Black), new Point(Convert.ToInt32(item.X - Vertice.Raggio), Convert.ToInt32(item.Y - Vertice.Raggio)));
                g.DrawEllipse(penna, item.X - Vertice.Raggio, item.Y - Vertice.Raggio, Vertice.Raggio * 2, Vertice.Raggio * 2);
            }
        }



        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case (int)Operazione.disegna_vertice:
                    CreaVertice fcv = new CreaVertice(e.X, e.Y);
                    if (fcv.ShowDialog() == DialogResult.OK)
                    {
                        if (fcv.V.Sovrapponi(verticiTotali) == false)
                        {
                            //Vertice tmp = new Vertice(fcv.V);
                            //verticiTotali.Add(tmp);
                            if (verticiTotali.Contains(fcv.V) == false)
                            {
                                verticiTotali.Add(fcv.V);
                                MessageBox.Show("Vertice aggiunto con successo");
                                DisegnaTotali();
                            }
                            else
                            {
                                MessageBox.Show("Vertice già presente");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Impossibile aggiungere il vertice perchè si sovrappone");
                        }
                    }

                    ; break;
                case (int)Operazione.disegna_arco:
                    if (!selezionaVertice)
                    {
                        //v1tmp
                        v1tmp = Logica.TrovaVertice(e.X, e.Y, verticiTotali);
                        if (v1tmp != null)
                        {
                            selezionaVertice = true;
                            MessageBox.Show($"Il vertice selezionato è: {v1tmp.Nome}");
                            disegnaVertice(v1tmp);
                        }
                        else
                        {
                            MessageBox.Show("Impossibile trovare il vertice di riferimento");
                        }
                    }
                    else
                    {
                        //v2tmp
                        v2tmp = Logica.TrovaVertice(e.X, e.Y, verticiTotali);
                        if (v2tmp != null)
                        {
                            if (v2tmp != v1tmp)
                            {
                                MessageBox.Show($"Il vertice selezionato è: {v2tmp.Nome}");
                                CreaArco fca = new CreaArco(v1tmp, v2tmp);
                                if (fca.ShowDialog() == DialogResult.OK)
                                {
                                    if (archiTotali.Contains(fca.Arco) == false)
                                    {
                                        archiTotali.Add(fca.Arco);
                                        MessageBox.Show("Arco aggiunto con successo");
                                        disegnaVertice(v2tmp);
                                        DisegnaTotali();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Arco già presente");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Operazione annullata");
                                }
                                v1tmp = null;
                                v2tmp = null;
                                selezionaVertice = false;
                            }
                            else
                            {
                                MessageBox.Show("Vertice già selezionato");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Impossibile trovare il vertice di riferimento");
                        }
                    }

                    ; break;

                case (int)Operazione.cancella_vertice:
                    v1tmp = Logica.TrovaVertice(e.X, e.Y, verticiTotali);
                    if (v1tmp != null)
                    {
                        archiTotali.RemoveAll(x => x.V1 == v1tmp || x.V2 == v1tmp);
                        verticiTotali.Remove(v1tmp);
                        MessageBox.Show($"Il vertice: {v1tmp.Nome} è stato eliminato con successo");
                        DisegnaTotali();
                        v1tmp = null;
                    }
                    else
                    {
                        MessageBox.Show("Impossibile trovare il vertice di riferimento");
                    }
                    ; break;
                case (int)Operazione.cancella_arco:
                    List<Arco> archiTMP = Logica.TrovaArchi(archiTotali, e.X, e.Y);
                    if (archiTMP != null)
                    {
                        archiTMP.ForEach(delegate (Arco a) { archiTotali.RemoveAll(x => x == a); });
                        MessageBox.Show($"Archi eliminati con successo: {archiTMP.Count}");
                        DisegnaTotali();
                        archiTMP = null;
                    }
                    else
                    {
                        MessageBox.Show("Impossibile trovare un arco da eliminare");
                    }
                        ; break;
            }
        }


        public void DisegnaArchi()
        {
            Graphics g = panel1.CreateGraphics();
            Pen penna = new Pen(Color.Gray, 1);
            foreach (Arco item in archiTotali)
            {
                //g.DrawEllipse(penna, item.X, item.Y, 1, 1);
                g.DrawString(item.Costo.ToString(), new Font("arial", 12), new SolidBrush(Color.Red), new Point(Convert.ToInt32(Math.Abs((item.V1.X + item.V2.X) / 2)), Convert.ToInt32(Math.Abs((item.V1.Y + item.V2.Y) / 2))));
                g.DrawLine(penna, new Point(item.V1.X, item.V1.Y), new Point(item.V2.X, item.V2.Y));
            }
        }

        public void disegnaVertice(Vertice v)
        {
            Graphics g = panel1.CreateGraphics();
            Pen penna = new Pen(Color.Red, 1);
            //g.DrawEllipse(penna, item.X, item.Y, 1, 1);
            g.DrawString(v.Nome, new Font("arial", 12), new SolidBrush(Color.Black), new Point(Convert.ToInt32(v.X - Vertice.Raggio), Convert.ToInt32(v.Y - Vertice.Raggio)));
            g.DrawEllipse(penna, v.X - Vertice.Raggio, v.Y - Vertice.Raggio, Vertice.Raggio * 2, Vertice.Raggio * 2);

        }

        public void DisegnaTotali()
        {
            panel1.Refresh();
            Graphics g = panel1.CreateGraphics();
            Pen penna = new Pen(Color.Black, 1);
            label4.Text = "Count archi: " + archiTotali.Count;
            label3.Text = "Count Vertici: " + verticiTotali.Count;
            DisegnaVertici();
            DisegnaArchi();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            v1tmp = null;
            v2tmp = null;
            selezionaVertice = false;
            DisegnaTotali();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
