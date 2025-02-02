namespace FloydWarshallProj
{
    partial class Background
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.labelThreads = new System.Windows.Forms.Label();
            this.labelFilePath = new System.Windows.Forms.Label();
            this.comboBoxFilePath = new System.Windows.Forms.ComboBox();
            this.buttonCSharp = new System.Windows.Forms.Button();
            this.buttonAsm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.Thistle;
            this.title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.title.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(337, 34);
            this.title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.title.Name = "title";
            this.title.Padding = new System.Windows.Forms.Padding(4);
            this.title.Size = new System.Drawing.Size(564, 36);
            this.title.TabIndex = 0;
            this.title.Text = "This app implements Floyd-Warshall algorithm.\r\n";
            this.title.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(356, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(527, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Firstly you should upload your graph to the project directory in forms of adjacen" +
    "cy matrix. ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(493, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Remember that the graph should be directed.";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 4;
            this.trackBar1.Location = new System.Drawing.Point(234, 307);
            this.trackBar1.Maximum = 64;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(951, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Value = 2;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(122, 357);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1063, 355);
            this.listBox1.TabIndex = 8;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // labelThreads
            // 
            this.labelThreads.AutoSize = true;
            this.labelThreads.Location = new System.Drawing.Point(146, 307);
            this.labelThreads.Name = "labelThreads";
            this.labelThreads.Size = new System.Drawing.Size(58, 13);
            this.labelThreads.TabIndex = 1;
            this.labelThreads.Text = "Threads: 1";
            this.labelThreads.Click += new System.EventHandler(this.labelThreads_Click);
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.Location = new System.Drawing.Point(339, 163);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(67, 13);
            this.labelFilePath.TabIndex = 10;
            this.labelFilePath.Text = "Wybierz plik:";
            // 
            // comboBoxFilePath
            // 
            this.comboBoxFilePath.FormattingEnabled = true;
            this.comboBoxFilePath.Location = new System.Drawing.Point(412, 160);
            this.comboBoxFilePath.Name = "comboBoxFilePath";
            this.comboBoxFilePath.Size = new System.Drawing.Size(471, 21);
            this.comboBoxFilePath.TabIndex = 14;
            // 
            // buttonCSharp
            // 
            this.buttonCSharp.BackColor = System.Drawing.Color.Plum;
            this.buttonCSharp.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10F);
            this.buttonCSharp.Location = new System.Drawing.Point(656, 209);
            this.buttonCSharp.Name = "buttonCSharp";
            this.buttonCSharp.Size = new System.Drawing.Size(150, 30);
            this.buttonCSharp.TabIndex = 12;
            this.buttonCSharp.Text = "Run in C#";
            this.buttonCSharp.UseVisualStyleBackColor = false;
            this.buttonCSharp.Click += new System.EventHandler(this.buttonCSharp_Click);
            // 
            // buttonAsm
            // 
            this.buttonAsm.BackColor = System.Drawing.Color.Plum;
            this.buttonAsm.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10F);
            this.buttonAsm.Location = new System.Drawing.Point(479, 209);
            this.buttonAsm.Name = "buttonAsm";
            this.buttonAsm.Size = new System.Drawing.Size(150, 30);
            this.buttonAsm.TabIndex = 13;
            this.buttonAsm.Text = "Run in ASM";
            this.buttonAsm.UseVisualStyleBackColor = false;
            this.buttonAsm.Click += new System.EventHandler(this.buttonAsm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(536, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Liczba procesorów logicznych: x";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // Background
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(1321, 800);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxFilePath);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.labelThreads);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.title);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this.buttonCSharp);
            this.Controls.Add(this.buttonAsm);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Background";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCSharp;
        private System.Windows.Forms.Button buttonAsm;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.Label labelThreads;
        private System.Windows.Forms.ComboBox comboBoxFilePath;
        private System.Windows.Forms.Label label3;
    }
}


