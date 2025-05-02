namespace kfu13
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button buttonLoadJson;
        private System.Windows.Forms.Button buttonLoadXml;
        private System.Windows.Forms.Button buttonLoadToDb;
        private System.Windows.Forms.Button buttonShow;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelTop;
        private OpenFileDialog openFileDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            treeView1 = new TreeView();
            dataGridView1 = new DataGridView();
            splitter1 = new Splitter();
            buttonLoadJson = new Button();
            buttonLoadXml = new Button();
            buttonLoadToDb = new Button();
            buttonShow = new Button();
            buttonClose = new Button();
            pictureBox1 = new PictureBox();
            panelTop = new Panel();
            openFileDialog = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Left;
            treeView1.Location = new Point(0, 64);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(302, 498);
            treeView1.TabIndex = 7;
            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(311, 64);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(569, 498);
            dataGridView1.TabIndex = 9;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(302, 64);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(9, 498);
            splitter1.TabIndex = 8;
            splitter1.TabStop = false;
            // 
            // buttonLoadJson
            // 
            buttonLoadJson.AutoSize = true;
            buttonLoadJson.Location = new Point(10, 11);
            buttonLoadJson.Name = "buttonLoadJson";
            buttonLoadJson.Size = new Size(105, 41);
            buttonLoadJson.TabIndex = 1;
            buttonLoadJson.Text = "Загрузить JSON";
            buttonLoadJson.Click += buttonLoadJson_Click;
            // 
            // buttonLoadXml
            // 
            buttonLoadXml.AutoSize = true;
            buttonLoadXml.Location = new Point(131, 11);
            buttonLoadXml.Name = "buttonLoadXml";
            buttonLoadXml.Size = new Size(105, 41);
            buttonLoadXml.TabIndex = 2;
            buttonLoadXml.Text = "Загрузить XML";
            buttonLoadXml.Click += buttonLoadXml_Click;
            // 
            // buttonLoadToDb
            // 
            buttonLoadToDb.AutoSize = true;
            buttonLoadToDb.Location = new Point(252, 11);
            buttonLoadToDb.Name = "buttonLoadToDb";
            buttonLoadToDb.Size = new Size(131, 41);
            buttonLoadToDb.TabIndex = 3;
            buttonLoadToDb.Text = "Загрузить в БД";
            buttonLoadToDb.Click += buttonLoadToDb_Click;
            // 
            // buttonShow
            // 
            buttonShow.AutoSize = true;
            buttonShow.Location = new Point(399, 11);
            buttonShow.Name = "buttonShow";
            buttonShow.Size = new Size(88, 41);
            buttonShow.TabIndex = 4;
            buttonShow.Text = "Показать";
            buttonShow.Click += buttonShow_Click;
            // 
            // buttonClose
            // 
            buttonClose.AutoSize = true;
            buttonClose.Location = new Point(500, 11);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(82, 41);
            buttonClose.TabIndex = 5;
            buttonClose.Text = "Закрыть";
            buttonClose.Click += buttonClose_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(719, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(158, 55);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.LightGray;
            panelTop.Controls.Add(buttonLoadJson);
            panelTop.Controls.Add(buttonLoadXml);
            panelTop.Controls.Add(buttonLoadToDb);
            panelTop.Controls.Add(buttonShow);
            panelTop.Controls.Add(buttonClose);
            panelTop.Controls.Add(pictureBox1);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(880, 64);
            panelTop.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 562);
            Controls.Add(dataGridView1);
            Controls.Add(splitter1);
            Controls.Add(treeView1);
            Controls.Add(panelTop);
            Name = "MainForm";
            Text = "Оператор мобильной связи";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
