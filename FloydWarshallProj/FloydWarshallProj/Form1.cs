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
        private int numOfThreads = Environment.ProcessorCount;
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

        private void buttonCSharp_Click(object sender, EventArgs e)
        {
            // Pobierz liczbę dostępnych wątków (procesorów logicznych)
            int numOfThreads = Environment.ProcessorCount;

            // Pobranie katalogu, w którym znajduje się plik wykonywalny (.exe)
            string binDir = AppDomain.CurrentDomain.BaseDirectory;

            // Tworzymy poprawną ścieżkę do folderu TestFiles
            string testFolderDir = Path.Combine(binDir, "TestFolder");

            // Pobieramy nazwę pliku wybranego w ComboBox
            string selectedFileName = this.comboBoxFilePath.Text;

            // Tworzymy pełną ścieżkę do pliku
            string filePath = Path.Combine(testFolderDir, selectedFileName);
            FloydWarshallRunner runner = new FloydWarshallRunner();
            label3.Text = "Liczba procesorów logicznych: " + numOfThreads.ToString();
            // Teraz wywołujemy metodę na obiekcie klasy
            runner.RunFloydWarshallCSharp(numOfThreads, filePath);
            trackBar1.Value = numOfThreads;
            labelThreads.Text = "Threads: " + numOfThreads;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Pobranie katalogu, w którym znajduje się plik wykonywalny (.exe)
            string binDir = AppDomain.CurrentDomain.BaseDirectory;

            // Tworzymy ścieżkę do folderu TestFolder w katalogu bin
            string testFolderDir = Path.Combine(binDir, "TestFolder");

            if (Directory.Exists(testFolderDir))
            {
                string[] files = Directory.GetFiles(testFolderDir, "*.txt"); // Pobiera tylko pliki .txt

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    comboBoxFilePath.Items.Add(fileName);
                }
            }
            else
            {
                MessageBox.Show($"Folder {testFolderDir} nie istnieje!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAsm_Click(object sender, EventArgs e)
        {
            // Pobierz liczbę dostępnych wątków (procesorów logicznych)
            int numOfThreads = Environment.ProcessorCount;
            // Pobranie katalogu, w którym znajduje się plik wykonywalny (.exe)
            string binDir = AppDomain.CurrentDomain.BaseDirectory;

            // Tworzymy poprawną ścieżkę do folderu TestFiles
            string testFolderDir = Path.Combine(binDir, "TestFolder");

            // Pobieramy nazwę pliku wybranego w ComboBox
            string selectedFileName = this.comboBoxFilePath.Text;
            // Tworzymy pełną ścieżkę do pliku
            string filePath = Path.Combine(testFolderDir, selectedFileName);
            FloydWarshallRunner runner = new FloydWarshallRunner();
            label3.Text = "Liczba procesorów logicznych: " + numOfThreads.ToString();
            // Teraz wywołujemy metodę na obiekcie klasy
            runner.RunFloydWarshallAsm(numOfThreads, filePath);
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

        private void labelThreads_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
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

