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
            panelTop.Size = new Size(1006, 68);
            panelTop.TabIndex = 0;
            // 
            // buttonLoadJson
            // 
            buttonLoadJson.AutoSize = true;
            buttonLoadJson.Location = new Point(12, 12);
            buttonLoadJson.Name = "buttonLoadJson";
            buttonLoadJson.Size = new Size(120, 44);
            buttonLoadJson.TabIndex = 1;
            buttonLoadJson.Text = "Загрузить JSON";
            buttonLoadJson.Click += buttonLoadJson_Click;
            // 
            // buttonLoadXml
            // 
            buttonLoadXml.AutoSize = true;
            buttonLoadXml.Location = new Point(150, 12);
            buttonLoadXml.Name = "buttonLoadXml";
            buttonLoadXml.Size = new Size(120, 44);
            buttonLoadXml.TabIndex = 2;
            buttonLoadXml.Text = "Загрузить XML";
            buttonLoadXml.Click += buttonLoadXml_Click;
            // 
            // buttonLoadToDb
            // 
            buttonLoadToDb.AutoSize = true;
            buttonLoadToDb.Location = new Point(288, 12);
            buttonLoadToDb.Name = "buttonLoadToDb";
            buttonLoadToDb.Size = new Size(150, 44);
            buttonLoadToDb.TabIndex = 3;
            buttonLoadToDb.Text = "Загрузить в БД";
            buttonLoadToDb.Click += buttonLoadToDb_Click;
            // 
            // buttonShow
            // 
            buttonShow.AutoSize = true;
            buttonShow.Location = new Point(456, 12);
            buttonShow.Name = "buttonShow";
            buttonShow.Size = new Size(100, 44);
            buttonShow.TabIndex = 4;
            buttonShow.Text = "Показать";
            buttonShow.Click += buttonShow_Click;
            // 
            // buttonClose
            // 
            buttonClose.AutoSize = true;
            buttonClose.Location = new Point(572, 12);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(94, 44);
            buttonClose.TabIndex = 5;
            buttonClose.Text = "Закрыть";
            buttonClose.Click += buttonClose_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(678, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(180, 59);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Left;
            treeView1.Location = new Point(0, 68);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(345, 532);
            treeView1.TabIndex = 7;
            //treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick_1);
            treeView1.Location = new Point(0, 68);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(345, 532);
            treeView1.TabIndex = 7;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(345, 68);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(10, 532);
            splitter1.TabIndex = 8;
            splitter1.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(355, 68);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(651, 532);
            dataGridView1.TabIndex = 9;
            //dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(355, 68);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(651, 532);
            dataGridView1.TabIndex = 9;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1006, 600);
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
