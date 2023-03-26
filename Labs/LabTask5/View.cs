using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CSGraphicsProgram.Labs.LabTask5
{
    public enum MoveDirection : System.Byte { Up, Down, Left, Right, None }

    public partial class View : Form
    {
        public const System.Int32 FrameWidth = 58, FrameHeight = 70;
        public static readonly System.Drawing.Image Background = Image.FromFile(
            @"C:\Users\Byter\Documents\Projects\C#Lang\Student\cs-graphics-program\Labs\LabTask5\Assets\background.png");

        protected System.Drawing.Point CursorPosition { get; private set; } = new();
        protected LabTask5.TaskLogic TaskLogic { get; private set; } = default!;
        protected System.String AnimationPackPath { get; private set; } = string.Empty;
        public View() : base()
        {
            this.InitializeComponent(); this.TaskLogic = new TaskLogic(this, this.pictureBox);
            this.CursorPosition = new Point();

            this.Load += new EventHandler(this.View_Load);

        }

        private void View_Load(object? sender, EventArgs args)
        {
            this.AnimationPackPath = @"C:\Users\Byter\Documents\Projects\C#Lang\Student\cs-graphics-program\Labs\LabTask5\Assets";

            this.TaskLogic.RegisterSceneObject(new PlayerObject(5, "player"));
            this.TaskLogic.RegisterSceneObject(new AnimatorObject(this.AnimationPackPath, "scene_animator"));

            this.TaskLogic.RunSceneHandler();
            //foreach (var folders in (new DirectoryInfo(this.AnimationPackPath)).GetDirectories())
            //{ this.comboBox.Items.Add(folders.Name); }
        }
    }
}
