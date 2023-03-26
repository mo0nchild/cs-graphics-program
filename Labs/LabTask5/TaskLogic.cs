using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CSGraphicsProgram.Labs.LabTask5
{
    using Forms = System.Windows.Forms;
    using Task = CSGraphicsProgram.Labs.LabTask5;
    public interface IEngineScene : IEnumerable<ISceneObject>
    {
        public sealed class EngineException : System.Exception
        {
            public EngineException(System.String message) : base(message) { }
        }

        public Task::ISceneObject GetSceneObject(System.String scene_name);
        public void RegisterSceneObject(Task::ISceneObject scene_object);
        public void RunSceneHandler();
    }

    public interface ISceneObject : System.IDisposable, IEquatable<Task::ISceneObject>
    {
        public List<Task::ISceneObject> Children { get; set; }
        public System.String Name { get; set; }

        public void SetPosition(System.Int32 position_x, System.Int32 position_y);
        public void UpdateStateAction(Task::IEngineScene scene_engine, System.Drawing.Graphics graphics);
        public void InititalStateAction(Task::IEngineScene scene_engine);

    }

    public class TaskLogic : System.Object, Task::IEngineScene
    {
        private const System.Int32 TimerInteval = 60;

        protected List<Task::ISceneObject> SceneChildren { get; private set; } = new();
        protected Forms::Form SceneForm { get; private set; } = default!;
        private Forms::Timer ActionTimer { get; set; } = new() { Interval = TaskLogic.TimerInteval };

        public TaskLogic(Forms::Form scene_form, Forms::PictureBox picture_box) : base()
        {
            this.SceneForm = scene_form;
            picture_box.Paint += new PaintEventHandler(this.Picture_box_Paint);

            this.ActionTimer.Tick += (sender, args) => picture_box.Invalidate();
        }

        private void Picture_box_Paint(object? sender, PaintEventArgs args)
        {
            args.Graphics.Clear(Color.White);
            args.Graphics.DrawImage(View.Background, new Rectangle(0, 0, this.SceneForm.Width,
                this.SceneForm.Height));

            this.SceneChildren.ForEach(delegate (Task::ISceneObject scene_object)
            { scene_object.UpdateStateAction(this, args.Graphics); });
        }

        public ISceneObject GetSceneObject(string scene_name)
        {
            var scene_object = this.SceneChildren.Where<Task::ISceneObject>((Task::ISceneObject item) 
                => item.Name == scene_name).ToArray();

            if(scene_object.Length == 0) throw new Task::IEngineScene.EngineException("Объект не найден");
            return scene_object[0];
        }

        public void RegisterSceneObject(ISceneObject scene_object)
        {
            if (this.SceneChildren.Contains(scene_object) == true)
            {
                throw new Task::IEngineScene.EngineException("Объект уже зарегистрирован");
            }
            if (scene_object is Task::SceneObject.IUserInputController input_controller)
            {
                this.SceneForm.KeyDown += (sender, args) => input_controller.KeyInputOperation(args);
                this.SceneForm.KeyUp += (sender, args) => input_controller.KeyReleaseOperation(args);
            }
            this.SceneChildren.Add(scene_object);
        }

        public IEnumerator<Task::ISceneObject> GetEnumerator()
        {
            foreach(Task::ISceneObject scene_item in this.SceneChildren) { yield return scene_item; }
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public void RunSceneHandler() 
        {
            foreach (var scene_item in this.SceneChildren) scene_item.InititalStateAction(this);
            this.ActionTimer.Start();
        }
    }

    public abstract class SceneObject : System.Object, Task::ISceneObject
    {
        public interface IUserInputController
        {
            public void KeyReleaseOperation(Forms::KeyEventArgs key_args) { }
            public void KeyInputOperation(Forms::KeyEventArgs key_args) { }
        }

        public System.Drawing.Point Position { get; set; } = new();
        public virtual System.String Name { get; set; } = default!;
        public virtual List<Task::ISceneObject> Children { get; set; } = new();

        public SceneObject(System.String name) : base() => this.Name = name;

        public abstract void InititalStateAction(Task::IEngineScene scene_engine);
        public abstract void UpdateStateAction(Task::IEngineScene scene_engine, Graphics graphics);

        public virtual void Dispose() { }
        public bool Equals(Task::ISceneObject? other) => this.Name.Equals(other?.Name);

        public virtual void SetPosition(int position_x, int position_y)
        {
            this.Position = new Point(position_x, position_y);
            for(var index = 0; index < this.Children.Count; index++)
            {
                var scene_object = this.Children[index] as Task::SceneObject;
                if (scene_object != null) scene_object.SetPosition(position_x, position_y); 
            }
        }
    }

    public sealed class AnimatorObject : Task::SceneObject
    {
        public Dictionary<System.String, System.String> AnimationPack { get; private set; } = new();
        public AnimatorObject(string animation_path, string name) : base(name) 
        {
            var directory_info = new DirectoryInfo(animation_path);
            if (!directory_info.Exists) throw new Task::IEngineScene.EngineException("Анимация не найдена");

            foreach(var animation_item in directory_info.GetDirectories())
            {
                this.AnimationPack.Add(animation_item.Name, animation_item.FullName);
                Console.WriteLine($"{animation_item.Name}, {animation_item.FullName}");
            }
        }

        public System.String? CurrentAnimator { get; private set; } = default!;
        public System.Int32 CurrentFrame { get; private set; } = default!;

        private System.Boolean repeatAnimation  = default!, flippingFrame = default!;
 
        public override void InititalStateAction(Task::IEngineScene scene_engine) { }

        public override void UpdateStateAction(Task::IEngineScene scene_engine, Graphics graphics)
        {
            if (this.CurrentAnimator == null) return;
            var animation_list = Directory.GetFiles(this.CurrentAnimator);

            var animation_geometry = new Rectangle(this.Position, new Size(View.FrameWidth, View.FrameHeight));
            var image_frame = Image.FromFile(animation_list[this.CurrentFrame]);

            if (this.flippingFrame) image_frame.RotateFlip(RotateFlipType.RotateNoneFlipX);
            graphics.DrawImage(image_frame, animation_geometry);

            if(animation_list.Length <= this.CurrentFrame + 1)
            {
                if (this.repeatAnimation == false) this.CurrentAnimator = null;
                else this.CurrentFrame = default(int);
            }
            else ++this.CurrentFrame;
        }

        public System.Threading.Tasks.Task PlayAnimation(string animation_name, bool flip = false, bool repeat = true)
        {
            string? current_animation_name = default!;
            if(this.CurrentAnimator != null)
            {
                if (this.repeatAnimation == false) return System.Threading.Tasks.Task.CompletedTask;
                foreach (KeyValuePair<string, string> animation_item in this.AnimationPack)
                {
                    if (animation_item.Value == this.CurrentAnimator) current_animation_name = animation_item.Key;
                }
                if (current_animation_name == null || current_animation_name == animation_name) 
                    return System.Threading.Tasks.Task.CompletedTask;
            }
            if (!this.AnimationPack.ContainsKey(animation_name))
            {
                var error_message = string.Format("Анимация [{0}] не найдена", animation_name);
                throw new Task::IEngineScene.EngineException(error_message);
            }
            this.CurrentAnimator = this.AnimationPack[animation_name]; this.CurrentFrame = default(int);
            (this.flippingFrame, this.repeatAnimation) = (flip, repeat);

            return (repeat) ? System.Threading.Tasks.Task.CompletedTask 
                : System.Threading.Tasks.Task.Run(() => { while (this.CurrentAnimator != null); });
        }
    }

    public sealed class PlayerObject : Task::SceneObject, Task::SceneObject.IUserInputController
    {
        public enum MovingDirection : System.SByte { Positive = 1, Negative = -1, Idle = 0 };

        private Task::AnimatorObject? PlayerAnimator { get; set; } = default!;

        private PlayerObject.MovingDirection moving_direction_x = default, moving_direction_y = default;
        private System.Double MovingSpeed { get; set; } = default;

        private System.Int32 currentAttackState = default;
        private System.Boolean personFlip = default;

        public PlayerObject(double speed, string name) : base(name) { this.MovingSpeed = speed; }

        public override void InititalStateAction(Task::IEngineScene scene_engine) 
        {
            this.PlayerAnimator = scene_engine.GetSceneObject("scene_animator") as AnimatorObject;
            this.Children.Add(this.PlayerAnimator!);
        }

        public override void UpdateStateAction(IEngineScene scene_engine, Graphics graphics)
        {
            var movement_x = (int)this.moving_direction_x * this.MovingSpeed;
            var movement_y = (int)this.moving_direction_y * this.MovingSpeed;

            this.SetPosition(this.Position.X + (int)movement_x, this.Position.Y + (int)movement_y);
        }

        public void KeyReleaseOperation(Forms::KeyEventArgs key_args) 
        {
            if (key_args.KeyCode == Keys.Up || key_args.KeyCode == Keys.Down) this.moving_direction_y = default;
            if (key_args.KeyCode == Keys.Right || key_args.KeyCode == Keys.Left) this.moving_direction_x = default;

            if(this.moving_direction_x == MovingDirection.Idle && this.moving_direction_y == MovingDirection.Idle)
            {
                this.PlayerAnimator?.PlayAnimation("Idle", this.personFlip);
            }
        }

        public void KeyInputOperation(Forms::KeyEventArgs key_args) 
        {
            if (key_args.KeyCode == Keys.Space) 
            {
                var attack_animation = string.Format("Attack{0}", currentAttackState + 1);
                this.PlayerAnimator?.PlayAnimation(attack_animation, this.personFlip, false)
                    .ContinueWith((task) => this.PlayerAnimator?.PlayAnimation("Idle", this.personFlip, true));

                this.currentAttackState = (currentAttackState >= 2) ? default : ++currentAttackState;
                return;
            }
            switch (key_args.KeyCode)
            {
                case Keys.Up: this.moving_direction_y = MovingDirection.Negative; break;
                case Keys.Down: this.moving_direction_y = MovingDirection.Positive; break;
                case Keys.Right:
                    this.moving_direction_x = MovingDirection.Positive; this.personFlip = false; break;
                case Keys.Left: 
                    this.moving_direction_x = MovingDirection.Negative; this.personFlip = true; break;
            }
            this.PlayerAnimator?.PlayAnimation("Movement", this.personFlip);
        }
    }
}

