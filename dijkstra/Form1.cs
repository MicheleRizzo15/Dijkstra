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
        #region Vaiabili
        /// <summary>
        /// Rappresenta tutti i vertici
        /// </summary>
        List<Vertice> verticiTotali;
        /// <summary>
        /// Rappresenta tutti gli archi
        /// </summary>
        List<Arco> archiTotali;
        /// <summary>
        /// rappresenta i collegamenti dopo aver applicato dijkstra
        /// </summary>
        List<Arco> archiDijkstra;
        /// <summary>
        /// Rappresentano i vertici tra i quali creare il collegamento (v1tmp usato anche per sceglere il vertice da eliminare)
        /// </summary>
        Vertice v1tmp, v2tmp;
        /// <summary>
        /// Rappresenta il vertice dal quale partire a calcolare l'algoritmo di Dijkstra
        /// </summary>
        Vertice verticePartenzDijkstra;
        /// <summary>
        /// true se si ha seleizonato il primo vertice per il collegamento (per seleizonare il secondo vertice), false altriemnti (per selezionare il primo vertice)
        /// </summary>
        bool selezionaVertice;
        /// <summary>
        /// rappresenta se visualizzare il grafo completo o il grafo con dijkstra
        /// </summary>
        bool dijkstra;
        /// <summary>
        /// rappresenta la tabella dei vari collegamnti dopo dijkstra
        /// </summary>
        List<string> testo;
        #endregion

        #region costruttori e load
        public Form1()
        {
            InitializeComponent();
            //inizializzo i vari componenti e le varie variabili con valori consoni
            //inizio dal grafo normale e non da dijkstra
            selezionaVertice = false;
            dijkstra = false;
            verticiTotali = new List<Vertice>();
            archiTotali = new List<Arco>();
            archiDijkstra = new List<Arco>();
            comboBox1.DataSource = null;
            comboBox1.DataSource = Enum.GetValues(typeof(Operazione));
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox1.Visible = false;
            label6.Visible = false;
            verticePartenzDijkstra = null;

        }

        /// <summary>
        /// Inizialmente non scrivo x e y se non ho il mouse sul pannello
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "X: --";
            label2.Text = "Y: --";
        }
        #endregion

        #region operazioni pannello

        /// <summary>
        /// Se muovo il mouse nel pannello aggiorno x e y
        /// Se sono in modalità collegamento allora disegno la linea tra il nodo che voglio collegare (se disponibile) e la posizone del mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "X: " + e.X.ToString(); //aggiorno x e y
            label2.Text = "Y: " + e.Y.ToString();
            if (comboBox1.SelectedIndex == (int)Operazione.disegna_arco && selezionaVertice == true) //se sono in crea collegamento e ho già un vertice di partenza disegno la linea che collega il vertice di partenza al mouse
            {
                DisegnaTotali(); //disegno tutto quanto prima
                disegnaVertice(v1tmp); //disegno il vertice seleizonato in rosso (sovrappongo)
                Graphics g = panel1.CreateGraphics();
                Pen penna = new Pen(Color.Black, 1);
                g.DrawLine(penna, new Point(v1tmp.X, v1tmp.Y), new Point(e.X, e.Y)); //disegno la linea di collegamento
            }
        }
        /// <summary>
        /// Se esco con il mosue metto x e y = --
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            label1.Text = "X: --";
            label2.Text = "Y: --";
        }
        /// <summary>
        /// clik sul pannello eseguo l'operazione richiesta nella combobox se possibile e calcolo dijkstyra ad ogni modifica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dijkstra == false) //eseguo le operaizoni solo se visualizzo il grafo normale
            {
                #region esecuzione operazione
                switch (comboBox1.SelectedIndex) //scelgo l'operaizone da fare
                {
                    #region creo un vertice
                    case (int)Operazione.disegna_vertice:
                        CreaVertice fcv = new CreaVertice(e.X, e.Y);
                        if (fcv.ShowDialog() == DialogResult.OK) //visualizzo il form e se confermo
                        {
                            if (fcv.Vertice.Sovrapponi(verticiTotali) == false) //controllo che il vertice che voglio creare non si sovrapponga
                            {
                                if (verticiTotali.Contains(fcv.Vertice) == false) //controllo se il vertice che voglio creare non è già contenuto
                                {
                                    verticiTotali.Add(fcv.Vertice); //lo aggiorno
                                    if (verticePartenzDijkstra == null) //se non ho ancora un nodo di partenza lo setto
                                    {
                                        verticePartenzDijkstra = verticiTotali[0];
                                    }
                                    MessageBox.Show("Vertice aggiunto con successo"); //conferma
                                    DisegnaTotali(); //disegno tutto ciò che posso
                                }
                                else
                                {
                                    MessageBox.Show("Vertice già presente"); //vertice già inserito
                                }
                            }
                            else
                            {
                                MessageBox.Show("Impossibile aggiungere il vertice perchè si sovrappone"); //vertice si sovrappone
                            }
                        }

                        ; break;
                    #endregion

                    #region creo un collegamento
                    case (int)Operazione.disegna_arco:
                        if (!selezionaVertice) //se non ho ancora scelto un vertice di partenza
                        {
                            //v1tmp
                            v1tmp = Logica.TrovaVertice(e.X, e.Y, verticiTotali); //trovo il vertice che ho premuto
                            if (v1tmp != null)
                            {
                                selezionaVertice = true; //per seleizonare il secondo vertice
                                MessageBox.Show($"Il vertice selezionato è: {v1tmp.Nome}");
                                disegnaVertice(v1tmp); //evidenzio di risso il vertice che ho seleizonato (lo ridisegno di rosso)
                            }
                            else
                            {
                                MessageBox.Show("Impossibile trovare il vertice di riferimento");
                            }
                        }
                        else //scelgo il secondo vertice
                        {
                            //v2tmp
                            v2tmp = Logica.TrovaVertice(e.X, e.Y, verticiTotali); //trovo il vertice scelto
                            if (v2tmp != null)
                            {
                                if (v2tmp != v1tmp) //se i vertici sono diversi creo il form e chiedo il costo del collegamento
                                {
                                    MessageBox.Show($"Il vertice selezionato è: {v2tmp.Nome}");
                                    CreaArco fca = new CreaArco(v1tmp, v2tmp);
                                    if (fca.ShowDialog() == DialogResult.OK)
                                    {
                                        if (archiTotali.Contains(fca.Arco) == false) //se non è già contenuto tale collegamento lo aggiungo alla lista
                                        {
                                            archiTotali.Add(fca.Arco);
                                            MessageBox.Show("Arco aggiunto con successo");
                                            disegnaVertice(v2tmp); //disegno il vertice di rosso e poi disegno tutto (non si vede perchè veloce)

                                            DisegnaTotali(); //disegno tutto ciò che posso disegnare
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
                                    v1tmp = null; //ripulisco e resetto i vari valori
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
                    #endregion

                    #region cancellazione di un vertice
                    case (int)Operazione.cancella_vertice:
                        bool primo = false; //rappresneta se è il vertice che si vuole eliminare è anche il vertice scelto per la partenza del dijkstra
                        v1tmp = Logica.TrovaVertice(e.X, e.Y, verticiTotali); //trovo il vertice premuto
                        if (v1tmp == verticePartenzDijkstra) //se il vertice che voglio eliminare è anche quello di partenza di dijkstra allora dovò aggiornarlo poi (il vertice di partenza di dijkstra
                        {
                            primo = true;
                        }
                        if (v1tmp != null) //se ho trovato il nodo che voglio eliminare
                        {
                            archiTotali.RemoveAll(x => x.V1 == v1tmp || x.V2 == v1tmp); //elimino tutti i collegamenti che lo riguardano
                            verticiTotali.Remove(v1tmp); //elimino il vertice
                            MessageBox.Show($"Il vertice: {v1tmp.Nome} è stato eliminato con successo");
                            if (primo == true) //se dovevo eliminare il vertice che partiva dijkstra allora aggiorno il vertice di partenza se posso, sennò metto a null tale nodo
                            {
                                if (verticiTotali.Count > 0)//se ci sono altri nodi tra i quali scegliere quello di partenza
                                {
                                    verticePartenzDijkstra = verticiTotali[0];//scelgo il primo nodo disponibile come nodo di partenza
                                }
                                else //vuol dire che non ho nodi, quindi non può esserci un nodo di partenza
                                {
                                    verticePartenzDijkstra = null;//in questo modo il primo nodo che inserirò sarà anche quello di partenza
                                }
                            }
                            DisegnaTotali(); //disegno tutto ciò che posso
                            v1tmp = null; //pulisco la variabile utilizzata per sceglere il vertice da cancellare
                        }
                        else
                        {
                            MessageBox.Show("Impossibile trovare il vertice di riferimento");
                        }
                        break;
                    #endregion

                    #region cancellazione collegamenti in vicinanza di dove premo
                    case (int)Operazione.cancella_arco:
                        List<Arco> archiTMP = Logica.TrovaArchi(archiTotali, e.X, e.Y); //cerco gli archi da eliminare
                        if (archiTMP != null)
                        {
                            archiTMP.ForEach(delegate (Arco a) { archiTotali.RemoveAll(x => x == a); }); //elimino tutti gli archi selezionati
                            MessageBox.Show($"Archi eliminati con successo: {archiTMP.Count}");
                            DisegnaTotali(); //ridisegno il grafo con i nodi eliminati
                            archiTMP = null; //ripulisco la variabile usata per gli archi da eliminare
                        }
                        else
                        {
                            MessageBox.Show("Impossibile trovare un arco da eliminare");
                        }
                            ; break;
                    #endregion

                    #region cambio nodo di partenza da dove calcolare dijkstra
                    case (int)Operazione.scegli_partenza:
                        if (archiTotali.Count >= 3 && verticiTotali.Count >= 3)//se posso calcolare dijkstra allora posso anche cambiare nodo di paretnza
                        {
                            Vertice prev = verticePartenzDijkstra;//salvo il valore attuale in modo da poterlo ripristinare
                            verticePartenzDijkstra = Logica.TrovaVertice(e.X, e.Y, verticiTotali); //trovo il vertice richiesto e aggiorno il nodo di partenza

                            if (verticePartenzDijkstra != null)
                            {
                                MessageBox.Show($"Il vertice selezionato è: {verticePartenzDijkstra.Nome} ed è la partenza");

                            }
                            else
                            {
                                MessageBox.Show("Impossibile trovare il vertice");
                                verticePartenzDijkstra = prev; //se non ho selezionato nulla rimetto il vertice precedente
                            }
                        }
                        else
                        {
                            MessageBox.Show("Impossibile trovare un vertice di partenza perchè non ci sono abbastanza collegamenti");
                        }
                        break;
                        #endregion
                }
                #endregion

                #region calcolo dijkstra se possibile dopo ogni operaizone


                if (archiTotali.Count >= 3 && verticiTotali.Count >= 3 && verticePartenzDijkstra != null)
                {
                    archiDijkstra = Logica.Disjkstra(verticiTotali, archiTotali, verticePartenzDijkstra, out testo);//calcolo dei collegamenti di dijkstra
                    DisegnaTotali();//disegna tutto ciò che si può disegnare
                }
                else
                {
                    archiDijkstra = new List<Arco>(); // crea una lista vuota di archi 
                }
                #endregion

            }
            else
            {
                MessageBox.Show("Le modifiche possono essere fatte solo sul grafo normale e non quello di dijkstra");
            }
        }

        /// <summary>
        /// Paint del pannello
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

        #region metodi di visualizzaizone

        /// <summary>
        /// Disegna tutti i vertici come cerchio di raggio Vertice.Raggio (statico) e con al centro il nome
        /// </summary>
        private void DisegnaVertici()
        {
            Graphics g = panel1.CreateGraphics();
            Pen penna = new Pen(Color.Black, 1);
            foreach (Vertice item in verticiTotali) //disegna tutti i vertici come cerchio con all'interno il nome
            {
                g.DrawString(item.Nome, new Font("arial", 12), new SolidBrush(Color.Black), new Point(Convert.ToInt32(item.X - Vertice.Raggio), Convert.ToInt32(item.Y - Vertice.Raggio)));
                g.DrawEllipse(penna, item.X - Vertice.Raggio, item.Y - Vertice.Raggio, Vertice.Raggio * 2, Vertice.Raggio * 2);
            }
        }
        /// <summary>
        /// Disegna i collegamenti tra i vari archi (dijkstra o no in base a cosa richiesto) con a metà in rosso il costo
        /// </summary>
        public void DisegnaArchi()
        {
            Graphics g = panel1.CreateGraphics();
            Pen penna = new Pen(Color.Gray, 1);
            if (dijkstra == false) //disegna i non dijstra
            {
                foreach (Arco item in archiTotali)//disegno i collegamenti totali
                {
                    g.DrawString(item.Costo.ToString(), new Font("arial", 12), new SolidBrush(Color.Red), new Point(Convert.ToInt32(Math.Abs((item.V1.X + item.V2.X) / 2)), Convert.ToInt32(Math.Abs((item.V1.Y + item.V2.Y) / 2))));
                    g.DrawLine(penna, new Point(item.V1.X, item.V1.Y), new Point(item.V2.X, item.V2.Y));
                }
            }
            else //disegna i collegamenti dijkstra e la tabella (textbox multilinea)
            {
                foreach (Arco item in archiDijkstra) //disegno i collegamenti di dijkstra
                {
                    g.DrawString(item.Costo.ToString(), new Font("arial", 12), new SolidBrush(Color.Red), new Point(Convert.ToInt32(Math.Abs((item.V1.X + item.V2.X) / 2)), Convert.ToInt32(Math.Abs((item.V1.Y + item.V2.Y) / 2))));
                    g.DrawLine(penna, new Point(item.V1.X, item.V1.Y), new Point(item.V2.X, item.V2.Y));
                }
                textBox1.Text = string.Empty;
                if (testo != null) //se ho disponibile la tabella
                {
                    foreach (string item in testo)
                    {
                        if (item != null) //se item non è null per essere scritto nella tabella
                        {
                            textBox1.AppendText(item);
                            textBox1.AppendText(Environment.NewLine);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Disegna il vertice richiesto di rosso
        /// </summary>
        /// <param name="v">vertice da disegnare in rosso</param>
        public void disegnaVertice(Vertice v)
        {
            Graphics g = panel1.CreateGraphics();
            Pen penna = new Pen(Color.Red, 1);
            g.DrawString(v.Nome, new Font("arial", 12), new SolidBrush(Color.Black), new Point(Convert.ToInt32(v.X - Vertice.Raggio), Convert.ToInt32(v.Y - Vertice.Raggio)));
            g.DrawEllipse(penna, v.X - Vertice.Raggio, v.Y - Vertice.Raggio, Vertice.Raggio * 2, Vertice.Raggio * 2);

        }

        /// <summary>
        /// Disegna sia i vertici che i collegamenti e aggiorna le varie lable
        /// </summary>
        public void DisegnaTotali()
        {
            panel1.Refresh();
            Graphics g = panel1.CreateGraphics();
            Pen penna = new Pen(Color.Black, 1);
            label4.Text = "Count Archi: " + archiTotali.Count; //lable del numero di archi
            label3.Text = "Count Vertici: " + verticiTotali.Count; //lable del numer di vertici
            if (archiDijkstra != null)//lable del numero di collegamenti dijkstra
            {
                label5.Text = "Count archi dijkstra: " + archiDijkstra.Count;
            }
            else
            {
                label5.Text = "Count archi dijkstra: 0";
            }

            if (verticePartenzDijkstra != null) //se il nodo di parenza è presente allora lo visualizza il nome, sennò no
            {
                label6.Visible = true;
                label6.Text = "Nodo di partenza di Dijkstra: " + verticePartenzDijkstra.Nome;
            }
            else
            {
                label6.Visible = false;
            }

            DisegnaVertici();
            DisegnaArchi();
        }
        #endregion

        #region combonox e bottoni
        /// <summary>
        /// cambio valore combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            v1tmp = null; //ogni volta resetto tutto quando cambio (resetto la psìossibilità di disegnare archi)
            v2tmp = null;
            selezionaVertice = false;
            DisegnaTotali(); //disegno tutto
        }

        /// <summary>
        /// Bottone per visualizzare il grafo di dijkstra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            dijkstra = true; //setto la visualizzazione del grafo di dijkstra
            textBox1.Visible = true; //mostro anche la tabella se disponibile
            DisegnaTotali();

        }

        /// <summary>
        /// Bottone di reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //resetto tutte le variabili
            if (MessageBox.Show("Sei sicuro di voler resettare tutto? ", "Reset", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                verticiTotali = new List<Vertice>();
                archiTotali = new List<Arco>();
                verticePartenzDijkstra = null;
                v1tmp = null;
                v2tmp = null;
                archiDijkstra = new List<Arco>();
                testo = new List<string>();
                dijkstra = false;
                selezionaVertice = false;
                MessageBox.Show("Reset avvenuto con successo");
                DisegnaTotali();
            }
        }

        /// <summary>
        /// Bottone per visualizzare il grafo completo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            dijkstra = false;
            textBox1.Visible = false;
            DisegnaTotali();
        }
        #endregion

    }
}
