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
                // Hapus hasil sebelumnya
                teksHasil.Text = "";

                // Ubah teks di sebelah browse button
                label4.Text = dialog.FileName;

                // Baca isi file
                string isifile = System.IO.File.ReadAllText(label4.Text);

                // Buat daftar huruf/nama akun
                // ...

                // Ubah dropdown list
                // ...

                // Buat graf
                createGraph(isifile);

                // Tampilkan hasil pencarian
                teksHasil.AppendText("append this");
            }
        }

        void DFS(string dari, string ke)
        {
            // Jika memilih pencarian DFS
        }

        void BFS(string dari, string ke)
        {
            // Jika memilih pencarian BFS
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

            //create the graph content
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;

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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
