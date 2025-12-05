namespace COM5113_Assignment_WinForm
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnLoadMap;
        private Button btnStart;
        private ComboBox cmbAlgorithms;
        private Panel mapPanel;
        private OpenFileDialog openFileDialog1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnLoadMap = new Button();
            btnStart = new Button();
            cmbAlgorithms = new ComboBox();
            mapPanel = new Panel();
            openFileDialog1 = new OpenFileDialog();
            SuspendLayout();
            // 
            // btnLoadMap
            // 
            btnLoadMap.Location = new Point(520, 561);
            btnLoadMap.Name = "btnLoadMap";
            btnLoadMap.Size = new Size(171, 69);
            btnLoadMap.TabIndex = 0;
            btnLoadMap.Text = "Load Map";
            btnLoadMap.Click += BtnLoadMap_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(720, 561);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(157, 69);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.Click += BtnStart_Click;
            // 
            // cmbAlgorithms
            // 
            cmbAlgorithms.Location = new Point(566, 24);
            cmbAlgorithms.Name = "cmbAlgorithms";
            cmbAlgorithms.Size = new Size(275, 40);
            cmbAlgorithms.TabIndex = 1;
            // 
            // mapPanel
            // 
            mapPanel.BorderStyle = BorderStyle.FixedSingle;
            mapPanel.Location = new Point(21, 24);
            mapPanel.Name = "mapPanel";
            mapPanel.Size = new Size(500, 500);
            mapPanel.TabIndex = 3;
            mapPanel.Paint += MapPanel_Paint;
            // 
            // MainForm
            // 
            ClientSize = new Size(889, 658);
            Controls.Add(btnLoadMap);
            Controls.Add(cmbAlgorithms);
            Controls.Add(btnStart);
            Controls.Add(mapPanel);
            Name = "MainForm";
            Text = "Pathfinding Visualiser";
            ResumeLayout(false);
        }
    }
}
