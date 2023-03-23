using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGraphicsProgram.Labs.LabTask5
{
    public class TaskLogic : System.Object
    {
        public interface ITaskView { void DrawAnimationFrame(System.String filepath); }

        public System.String PlayingAnimation { get; private set; } = string.Empty;
        public System.Int32 CurrentFrame { get; private set; } = default;

        protected System.Windows.Forms.Timer Timer { get; private set; } = new();
        protected TaskLogic.ITaskView? CurrentView { get; private set; } = default!;

        public TaskLogic(ITaskView taskview) : base() 
        { 
            this.CurrentView = taskview;
            this.Timer.Tick += new EventHandler(this.Timer_Tick); 
        }

        private void Timer_Tick(object? sender, EventArgs args)
        {
            var animation_list = Directory.GetFiles(this.PlayingAnimation);
            if (this.CurrentView != null)
            {
                this.CurrentView.DrawAnimationFrame(animation_list[this.CurrentFrame]);
            }
            this.CurrentFrame = (animation_list.Length <= this.CurrentFrame + 1) ? default(int) 
                : ++this.CurrentFrame;
        }

        public void PlayAnimation(string path, int speed)
        {
            (this.PlayingAnimation, Timer.Interval, CurrentFrame) = (path, speed, default(int));
            this.Timer.Start();
        }
    }
}
