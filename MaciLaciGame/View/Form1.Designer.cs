namespace MaciLaciGame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            újJátékToolStripMenuItem = new ToolStripMenuItem();
            kicsiPályaToolStripMenuItem = new ToolStripMenuItem();
            közepesPályaToolStripMenuItem = new ToolStripMenuItem();
            nagyPályaToolStripMenuItem = new ToolStripMenuItem();
            szünetToolStripMenuItem = new ToolStripMenuItem();
            kilépésToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            label1 = new ToolStripStatusLabel();
            timerLabel = new ToolStripStatusLabel();
            label2 = new ToolStripStatusLabel();
            basketLabel = new ToolStripStatusLabel();
            gameTimer = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { újJátékToolStripMenuItem, szünetToolStripMenuItem, kilépésToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(6, 3, 0, 3);
            menuStrip1.Size = new Size(1262, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // újJátékToolStripMenuItem
            // 
            újJátékToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { kicsiPályaToolStripMenuItem, közepesPályaToolStripMenuItem, nagyPályaToolStripMenuItem });
            újJátékToolStripMenuItem.Name = "újJátékToolStripMenuItem";
            újJátékToolStripMenuItem.Size = new Size(73, 24);
            újJátékToolStripMenuItem.Text = "Új játék";
            // 
            // kicsiPályaToolStripMenuItem
            // 
            kicsiPályaToolStripMenuItem.Name = "kicsiPályaToolStripMenuItem";
            kicsiPályaToolStripMenuItem.Size = new Size(188, 26);
            kicsiPályaToolStripMenuItem.Text = "Kicsi pálya";
            kicsiPályaToolStripMenuItem.Click += kicsiPályaToolStripMenuItem_Click;
            // 
            // közepesPályaToolStripMenuItem
            // 
            közepesPályaToolStripMenuItem.Name = "közepesPályaToolStripMenuItem";
            közepesPályaToolStripMenuItem.Size = new Size(188, 26);
            közepesPályaToolStripMenuItem.Text = "Közepes pálya";
            közepesPályaToolStripMenuItem.Click += közepesPályaToolStripMenuItem_Click;
            // 
            // nagyPályaToolStripMenuItem
            // 
            nagyPályaToolStripMenuItem.Name = "nagyPályaToolStripMenuItem";
            nagyPályaToolStripMenuItem.Size = new Size(188, 26);
            nagyPályaToolStripMenuItem.Text = "Nagy pálya";
            nagyPályaToolStripMenuItem.Click += nagyPályaToolStripMenuItem_Click;
            // 
            // szünetToolStripMenuItem
            // 
            szünetToolStripMenuItem.Name = "szünetToolStripMenuItem";
            szünetToolStripMenuItem.Size = new Size(67, 24);
            szünetToolStripMenuItem.Text = "Szünet";
            szünetToolStripMenuItem.Click += PauseClick;
            // 
            // kilépésToolStripMenuItem
            // 
            kilépésToolStripMenuItem.Name = "kilépésToolStripMenuItem";
            kilépésToolStripMenuItem.Size = new Size(71, 24);
            kilépésToolStripMenuItem.Text = "Kilépés";
            kilépésToolStripMenuItem.Click += QuitClick;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { label1, timerLabel, label2, basketLabel });
            statusStrip1.Location = new Point(0, 647);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1262, 26);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            label1.Name = "label1";
            label1.Size = new Size(72, 20);
            label1.Text = "Eltelt idő:";
            // 
            // timerLabel
            // 
            timerLabel.Name = "timerLabel";
            timerLabel.Size = new Size(17, 20);
            timerLabel.Text = "0";
            // 
            // label2
            // 
            label2.Name = "label2";
            label2.Size = new Size(206, 20);
            label2.Text = "Összegyűjtött kosarak száma: ";
            // 
            // basketLabel
            // 
            basketLabel.Name = "basketLabel";
            basketLabel.Size = new Size(17, 20);
            basketLabel.Text = "0";
            // 
            // gameTimer
            // 
            gameTimer.Interval = 1000;
            gameTimer.Tick += gameTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Maci Laci Game";
            Load += LoadGame;
            KeyDown += KeyPressed;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem újJátékToolStripMenuItem;
        private ToolStripMenuItem kicsiPályaToolStripMenuItem;
        private ToolStripMenuItem közepesPályaToolStripMenuItem;
        private ToolStripMenuItem nagyPályaToolStripMenuItem;
        private ToolStripMenuItem szünetToolStripMenuItem;
        private ToolStripMenuItem kilépésToolStripMenuItem;
        //   private ContextMenuStrip contextMenuStrip1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel label1;
        private ToolStripStatusLabel timerLabel;
        private ToolStripStatusLabel label2;
        private ToolStripStatusLabel basketLabel;
        private System.Windows.Forms.Timer gameTimer;
    }
}