using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CSGraphicsProgram.Labs.LabTask5
{
    public partial class View : Form, TaskLogic.ITaskView
    {
        public const System.Int32 FrameWidth = 58, FrameHeight = 70;
        public static readonly System.Drawing.Image Background = Image.FromFile(
            @"C:\Users\Byter\Documents\Projects\C#Lang\Student\cs-graphics-program\Labs\LabTask5\Assets\background.png");

        protected System.Drawing.Point CursorPosition { get; private set; } = new();
        protected LabTask5.TaskLogic TaskLogic { get; private set; } = default!;
        protected System.String CurrentPath { get; private set; } = string.Empty;

        private System.Drawing.Image SelectedFrame { get; set; } = default!;
        public View() : base()
        {
            this.InitializeComponent(); this.TaskLogic = new TaskLogic(this);
            this.CursorPosition = new Point(this.pictureBox.Width / 2, 100);
            this.pictureBox.BackgroundImage = View.Background;

            this.pictureBox.Paint += new PaintEventHandler(PictureBox_Paint);
            this.pictureBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);

            this.comboBox.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            this.update_button.Click += new EventHandler(Update_button_Click);

            this.trackBar.ValueChanged += new EventHandler(TrackBar_ValueChanged);
            this.TrackBar_ValueChanged(this, EventArgs.Empty);

            this.CurrentPath = @"C:\Users\Byter\Documents\Projects\C#Lang\Student\cs-graphics-program\Labs\LabTask5\Assets";
            foreach (var folders in (new DirectoryInfo(this.CurrentPath)).GetDirectories())
            { this.comboBox.Items.Add(folders.Name); }
        }

        private void PictureBox_MouseDown(object? sender, MouseEventArgs args)
        {
            this.CursorPosition = new Point(args.Location.X - View.FrameWidth / 2, 
                args.Location.Y - View.FrameWidth / 2);
            this.ComboBox_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void TrackBar_ValueChanged(object? sender, EventArgs args)
        {
            this.textBox.Text = ((this.trackBar.Maximum - this.trackBar.Value) * 10).ToString();
            this.ComboBox_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void PictureBox_Paint(object? sender, PaintEventArgs args)
        {
            var frame_geometry = new Rectangle(this.CursorPosition.X, this.CursorPosition.Y,
                View.FrameWidth, View.FrameHeight);
            if (this.SelectedFrame != null)
            {
                args.Graphics.Clear(Color.Transparent);
                args.Graphics.DrawImage(View.Background, new Rectangle(0, 0, this.pictureBox.Width,
                    this.pictureBox.Height));

                args.Graphics.DrawImage(this.SelectedFrame, frame_geometry);
            }
        }

        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs args)
        {
            if (this.comboBox.Text != string.Empty)
            {
                this.TaskLogic.PlayAnimation(string.Format("{0}\\{1}", this.CurrentPath, this.comboBox.Text),
                    (this.trackBar.Maximum - this.trackBar.Value) * 10 + 10);
            }
        }

        private void Update_button_Click(object? sender, EventArgs args)
        {
            var current_path = Directory.GetCurrentDirectory();
            using (var path_dialog = new FolderBrowserDialog() { InitialDirectory = current_path })
            {
                if (path_dialog.ShowDialog() != DialogResult.OK) return;
                this.CurrentPath = path_dialog.SelectedPath;
            }
            this.comboBox.Items.Clear();
            foreach (var folders in (new DirectoryInfo(this.CurrentPath)).GetDirectories())
            {
                this.comboBox.Items.Add(folders.Name);
            }
            if (this.comboBox.Items.Count > 0) this.comboBox.SelectedIndex = 0;
        }

        public void DrawAnimationFrame(string filepath)
        {
            this.SelectedFrame = Image.FromFile(filepath); this.pictureBox.Invalidate();
        }
    }
}
