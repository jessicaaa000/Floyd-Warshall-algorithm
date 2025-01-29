using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloydWarshallProj
{
    public partial class Background : Form
    {
        private int numOfThreads = 1;
        public Background()
        {
            InitializeComponent();
            Console.SetOut(new ListBoxWriter(listBox1));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numOfThreads = trackBar1.Value;
            labelThreads.Text = "Threads: " + numOfThreads;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = this.textBoxFilePath.Text;
            Task.Run(() => FloydWarshallRunner.Run(numOfThreads, filePath));
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void choosingLabel_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class ListBoxWriter : TextWriter
    {
        private ListBox listBox;
        private delegate void SafeCallDelegate(string text);

        public ListBoxWriter(ListBox listBox)
        {
            this.listBox = listBox;
        }

        public override void WriteLine(string value)
        {
            if (listBox.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteLine);
                listBox.Invoke(d, new object[] { value });
            }
            else
            {
                listBox.Items.Add(value);
                listBox.TopIndex = listBox.Items.Count - 1; // Auto-scroll
            }
        }

        public override void Write(char value) { }
        public override void Write(string value) { }

        public override Encoding Encoding => System.Text.Encoding.UTF8;
    }
    }

