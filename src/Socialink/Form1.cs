using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace Socialink
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text Files|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Clear combobox
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();

                // Hapus hasil sebelumnya
                teksHasil.Text = "";

                // Ubah teks di sebelah browse button
                label4.Text = dialog.FileName;

                // Baca isi file
                string isifile = System.IO.File.ReadAllText(label4.Text);

                // Buat daftar huruf/nama akun
                List<string> daftarHuruf = new List<string>(createDaftarHuruf());

                // Ubah dropdown list
                foreach (var huruf in daftarHuruf)
                {
                    comboBox1.Items.Add(huruf);
                    comboBox2.Items.Add(huruf);
                }

                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                button2.Enabled = true;
                button3.Enabled = true;

                // Buat graf
                createGraph(isifile);
            }
        }

        List<string> createDaftarHuruf()
        {
            string isifile = System.IO.File.ReadAllText(label4.Text);
            List<string> daftarHuruf = new List<string>();
            string[] isifile2 = isifile.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 1; i < isifile2.Length; i++)
            {
                string[] tempstring = isifile2[i].Split(' ');
                // Cek jika node sudah tercatat, jika belum maka add
                if (!daftarHuruf.Contains(tempstring[0]))
                {
                    daftarHuruf.Add(tempstring[0]);
                }
                if (!daftarHuruf.Contains(tempstring[1]))
                {
                    daftarHuruf.Add(tempstring[1]);
                }
            }
            daftarHuruf.Sort();
            return daftarHuruf;
        }

        void recFriend(bool[,] matriks, List<string> daftarHuruf, int akun)
        {
            // Jika memilih pencarian recommend friends
            int jumlahNode = comboBox1.Items.Count;
            List<int> friend = new List<int>(); 
            int[] recom = new int[jumlahNode]; 

            for (int i = 0; i < jumlahNode; i++)
            {
                if (matriks[akun, i])
                {
                    friend.Add(i);
                }
            }

            for(int i = 0; i < jumlahNode; i++){
                recom[i] = 0;
            }
            
            foreach(int node in friend)
            {
                for (int i = 0; i < jumlahNode; i++)
                {
                    if (matriks[node,i] && !friend.Contains(i) && i != akun)
                    {
                        recom[i] += 1;
                    }
                }
            }

            // Cetak hasil pencarian di textbox
            teksHasil.SelectionStart = teksHasil.Text.Length;
            teksHasil.SelectionAlignment = HorizontalAlignment.Center;
            teksHasil.Text = "";
            teksHasil.AppendText("==============\n");
            teksHasil.AppendText("Hasil Pencarian\n");
            teksHasil.AppendText("==============\n\n");
            teksHasil.AppendText("Daftar rekomendasi teman untuk akun ");
            teksHasil.AppendText(daftarHuruf[akun]);
            teksHasil.AppendText(":\n");


            int maks = recom.Max();

            if(maks == 0)
            {
                teksHasil.AppendText("Tidak ada rekomendasi teman untuk akun tersebut. :(");
            }

            while(maks!=0)
            {
                for (int i = 0; i < jumlahNode; i++)
                {
                    if (recom[i] == maks)
                    {
                        teksHasil.AppendText("\nNama akun: ");
                        teksHasil.AppendText(daftarHuruf[i]);
                        teksHasil.AppendText("\n");
                        teksHasil.AppendText(recom[i].ToString());
                        teksHasil.AppendText(" mutual friend");
                        if(recom[i]>1)
                        {
                            teksHasil.AppendText("s");
                        }
                        teksHasil.AppendText(":\n");
                        foreach (int mutual in friend)
                        {
                            if (matriks[i, mutual])
                            {
                                teksHasil.AppendText(daftarHuruf[mutual]);
                                teksHasil.AppendText("\n");
                            }
                        }

                        recom[i] = 0;
                    }
                }
                maks = recom.Max();
            }
            
        }

        void expDFS(bool[,] matriks, List<string> daftarHuruf, int a, int b)
        {
            // Jika memilih explore DFS dari a ke b
            Stack<int> stack = new Stack<int>();
            int jumlahNode = comboBox1.Items.Count;
            int i = a;
            int j = 0;
            bool[] visited = new bool[jumlahNode];

            for(int k = 0; k < jumlahNode; k++)
            {
                visited[k] = false;
            }
            stack.Push(a);
            visited[a] = true;
            bool selesai = false;

            while (!visited[b] && !selesai)
            {
                if (j == jumlahNode)
                {
                    j = stack.Pop();
                    if(stack.Count == 0)
                    {
                        selesai = true;
                    }
                    else
                    {
                        i = stack.Peek();
                        j++;
                    }
                }
                else if (matriks[i, j] && !visited[j])
                {
                    stack.Push(j);
                    visited[j] = true;
                    i = j;
                    j = 0;
                }
                else
                {
                    j++;
                }
            }

            Stack<int> printStack = new Stack<int>(stack.ToArray());

            if (!selesai)
            {
                createGraphToFrom(printStack.ToArray(), daftarHuruf);
            }

            // Cetak hasil pencarian di textbox
            teksHasil.SelectionStart = teksHasil.Text.Length;
            teksHasil.SelectionAlignment = HorizontalAlignment.Center;
            teksHasil.Text = "";
            teksHasil.AppendText("==============\n");
            teksHasil.AppendText("Hasil Pencarian\n");
            teksHasil.AppendText("==============\n\n");
            teksHasil.AppendText("Nama akun: ");
            teksHasil.AppendText(daftarHuruf[a]);
            teksHasil.AppendText(" dan ");
            teksHasil.AppendText(daftarHuruf[b]);
            teksHasil.AppendText("\n");

            if(!selesai)
            {
                int connection = printStack.Count() - 2;
                teksHasil.SelectionStart = teksHasil.Text.Length;
                teksHasil.SelectionFont = new Font(teksHasil.Font, FontStyle.Regular);
                teksHasil.SelectionColor = Color.SlateBlue;
                teksHasil.AppendText(connection.ToString());
                if (connection % 10 == 1 && connection % 100 != 11)
                {
                    teksHasil.AppendText("st");
                }
                else if (connection % 10 == 2 && connection % 100 != 12)
                {
                    teksHasil.AppendText("nd");
                }
                else if (connection % 10 == 3 && connection % 100 != 13)
                {
                    teksHasil.AppendText("rd");
                }
                else
                {
                    teksHasil.AppendText("th");
                }
                teksHasil.AppendText("-degree connection\n");

                foreach (int node in printStack)
                {
                    // Tulis hasil ke textbox
                    teksHasil.AppendText(daftarHuruf[node]);
                    if (stack.Peek() != node)
                    {
                        teksHasil.AppendText(" → ");
                    }

                }
            }
            else
            {
                // Tidak ketemu jalur
                teksHasil.AppendText("Tidak ada jalur koneksi yang tersedia.\n");
                teksHasil.AppendText("Anda harus memulai koneksi baru itu sendiri.");
            }
            
        }

        void expBFS(bool[,] matriks, List<string> daftarHuruf, int a, int b)
        {
            // Jika memilih explore BFS dari a ke b
            Queue<int> q = new Queue<int>();
            q.Enqueue(a);
            int jumlahNode = comboBox1.Items.Count;
            bool[] visited = new bool[jumlahNode];

            // catatan node mana sebelum mencapai node tersebut
            int[] prevroute = new int[jumlahNode];
            for(int i =0;i<jumlahNode;i++)
            {
                prevroute[i] = -1;
            }
            
            visited[a] = true;
            bool temu = false;

            while(q.Count!=0 && !temu)
            {
                int current = q.Dequeue();
                Console.Write("Currently checking: ");
                Console.WriteLine(daftarHuruf[current]);
                if (current!=b)
                {
                    // Jika current node bukan yang dicari
                    for (int i = 0; i < jumlahNode; i++)
                    {
                        // bangkitkan semua simpul tetangga
                        if (!visited[i] && matriks[i, current])
                        {
                            Console.Write("Adding to queue: ");
                            Console.WriteLine(daftarHuruf[i]);
                            visited[i] = true;
                            q.Enqueue(i);
                            prevroute[i] = current;

                        }
                    }
                }
                else
                {
                    // current = b
                    temu = true;
                }
                
            }

            // Cetak hasil pencarian di textbox
            teksHasil.SelectionStart = teksHasil.Text.Length;
            teksHasil.SelectionAlignment = HorizontalAlignment.Center;
            teksHasil.Text = "";
            teksHasil.AppendText("==============\n");
            teksHasil.AppendText("Hasil Pencarian\n");
            teksHasil.AppendText("==============\n\n");
            teksHasil.AppendText("Nama akun: ");
            teksHasil.AppendText(daftarHuruf[a]);
            teksHasil.AppendText(" dan ");
            teksHasil.AppendText(daftarHuruf[b]);
            teksHasil.AppendText("\n");

            // Ketemu atau semua visited dan tidak ketemu
            if (temu)
            {
                // jika ketemu
                int goal = b;
                Stack<int> route = new Stack<int>();
                while(goal!=a)
                {
                    route.Push(goal);
                    goal = prevroute[goal];
                }
                route.Push(goal);

                // buat graf
                createGraphToFrom(route.ToArray(), daftarHuruf);

                // cetak hasil
                int connection = route.Count() - 2;
                teksHasil.SelectionStart = teksHasil.Text.Length;
                teksHasil.SelectionFont = new Font(teksHasil.Font, FontStyle.Regular);
                teksHasil.SelectionColor = Color.SlateBlue;
                teksHasil.AppendText(connection.ToString());
                if (connection % 10 == 1 && connection % 100 != 11)
                {
                    teksHasil.AppendText("st");
                }
                else if (connection % 10 == 2 && connection % 100 != 12)
                {
                    teksHasil.AppendText("nd");
                }
                else if (connection % 10 == 3 && connection % 100 != 13)
                {
                    teksHasil.AppendText("rd");
                }
                else
                {
                    teksHasil.AppendText("th");
                }
                teksHasil.AppendText("-degree connection\n");

                int x = 0;
                while(route.Count!=0)
                {
                    x = route.Pop();
                    teksHasil.AppendText(daftarHuruf[x]);
                    if (route.Count>=1)
                    {
                        teksHasil.AppendText(" → ");
                    }
                }

            }
            else
            {
                // jika tidak ketemu
                teksHasil.AppendText("Tidak ada jalur koneksi yang tersedia.\n");
                teksHasil.AppendText("Anda harus memulai koneksi baru itu sendiri.");
            }

        }

        void createGraphToFrom(int[] arr, List<string> daftarHuruf)
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            string judul = "Graf Explore " + daftarHuruf[arr[0]] + " ke " + daftarHuruf[arr[arr.Length - 1]]; 
            form.Text = judul;

            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();

            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            string isifile = System.IO.File.ReadAllText(label4.Text);
            string[] isifile2 = isifile.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 1; i < isifile2.Length; i++)
            {
                string[] tempstring = isifile2[i].Split(' ');
                var edge = graph.AddEdge(tempstring[0], tempstring[1]);
                edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;

                int idx1 = daftarHuruf.FindIndex(a => a == tempstring[0]);
                int idx2 = daftarHuruf.FindIndex(a => a == tempstring[1]);
                for (int j = 0;j<arr.Length-1;j++)
                {
                    if((idx1==arr[j] && idx2==arr[j+1]) || (idx1==arr[j+1] && idx2==arr[j]))
                    {
                        edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                        edge.Attr.LineWidth = 2.0;
                    }
                }
            }
            for (int i = 0; i < daftarHuruf.Count; i++)
            {
                graph.FindNode(daftarHuruf[i]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                graph.FindNode(daftarHuruf[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Aquamarine;
            }

            //bind the graph to the viewer 
            viewer.Graph = graph;

            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();

            //show the form 
            form.Show();
        }

        void createGraph(string isifile)
        {
            //CONTOH GRAF

            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            form.Text = "Graf";

            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();

            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            string[] isifile2 = isifile.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            for(int i=1;i<isifile2.Length;i++)
            {
                string[] tempstring = isifile2[i].Split(' ');
                var edge = graph.AddEdge(tempstring[0], tempstring[1]);
                edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;

            }
            List<string> daftarHuruf = createDaftarHuruf();
            for(int i=0;i<daftarHuruf.Count;i++)
            {
                graph.FindNode(daftarHuruf[i]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                graph.FindNode(daftarHuruf[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Aquamarine;
            }
            
            //bind the graph to the viewer 
            viewer.Graph = graph;

            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();

            //show the form 
            form.Show();
        }

        bool[,] makeMatriks()
        {
            int n = comboBox1.Items.Count;
            bool[,] matriks = new bool[n, n];
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    matriks[i,j] = false;
                }
            }

            string isifile = System.IO.File.ReadAllText(label4.Text);
            string[] isifile2 = isifile.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            List<string> daftarHuruf = new List<string>(createDaftarHuruf());
            for (int i = 1; i < isifile2.Length; i++)
            {
                string[] tempstring = isifile2[i].Split(' ');
                int idx1 = daftarHuruf.FindIndex(a => a == tempstring[0]);
                int idx2 = daftarHuruf.FindIndex(a => a == tempstring[1]);

                matriks[idx1,idx2] = true;
                matriks[idx2,idx1] = true;

                
            }


            return matriks;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Button Recommend Friends
            bool[,] matriks = makeMatriks();
            List<string> daftarHuruf = new List<string>(createDaftarHuruf());
            int akun = daftarHuruf.FindIndex(a => a == comboBox1.Text);
            recFriend(matriks, daftarHuruf, akun);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Button Explore Friends
            bool[,] matriks = makeMatriks();
            List<string> daftarHuruf = new List<string>(createDaftarHuruf());
            int dari = daftarHuruf.FindIndex(a => a == comboBox1.Text);
            int ke = daftarHuruf.FindIndex(a => a == comboBox2.Text);

            // Cek apakah nilai combobox1 == combobox2
            if (dari==ke)
            {
                // Gagal explore
                MessageBox.Show("Harap pilih 2 akun yang berbeda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (DFSbutton.Checked)
                {
                    // Jika DFS dipilih
                    expDFS(matriks, daftarHuruf, dari, ke);
                }
                else if (BFSbutton.Checked)
                {
                    // Jika BFS
                    expBFS(matriks, daftarHuruf, dari, ke);
                }
            }
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "=========\nSocialink v1.0\n=========\n\nDibuat oleh:\n\n13519063 Melita\n13519070 Mhd. Hiro Agayeff Muslio\n13519171 Fauzan Yubairi Indrayadi";
            MessageBox.Show(msg, "About", MessageBoxButtons.OK);
        }

        private void guideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "1. Gunakan Browse untuk memilih file .txt.\n2. Untuk recommend, pilih akun lalu klik tombol Recommend Friends.\n3. Untuk explore, pilih akun kedua yang berbeda, lalu klik Explore.";
            MessageBox.Show(msg, "Help", MessageBoxButtons.OK);
        }
    }
}
