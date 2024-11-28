using System.Windows.Forms;

namespace CFF_Clock
{
    public partial class Form1 : Form
    {
        private Image Background;
        private Image HandHour;
        private Image HandMinute;
        private Image HandSecond;
        private System.Windows.Forms.Timer repaintTimer;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        private DateTime prev;

        public Form1()
        {
            InitializeComponent();

            Background = Properties.Resources.background;
            HandHour = Properties.Resources.hand_minute;
            HandMinute = Properties.Resources.hand_hour;
            HandSecond = Properties.Resources.hand_second;

            if (File.Exists("background.png"))
            {
                Background = Image.FromFile("background.png");
            }
            if (File.Exists("hand_hour.png"))
            {
                HandHour = Image.FromFile("hand_hour.png");
            }
            if (File.Exists("hand_minute.png"))
            {
                HandMinute = Image.FromFile("hand_minute.png");
            }
            if (File.Exists("hand_second.png"))
            {
                HandSecond = Image.FromFile("hand_second.png");
            }


            repaintTimer = new();
            repaintTimer.Interval = 1;
            repaintTimer.Tick += (s, e) => Redraw();
            repaintTimer.Start();
            DoubleBuffered = true;

            this.MouseWheel += (s, e) =>
            {
                int delta = e.Delta > 0 ? 20 : -20; // Adjust size in steps of 20
                int newWidth = Math.Max(this.Width + delta, 100); // Minimum width
                int newHeight = Math.Max(this.Height + delta, 100); // Minimum height
                this.Size = new Size(newWidth, newHeight);
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close(); // Close the form when Escape key is pressed
                }
            };

            this.TopMost = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Draw(e);
        }

        private void Draw(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            float hourAngle = 360f / 12f * (DateTime.Now.Hour + DateTime.Now.Minute / 60f);
            float minuteAngle = 360f / 60f * DateTime.Now.Minute;
            float secondAngle = 360f / 60f * DateTime.Now.Second + 360f / 60f / 1000f * DateTime.Now.Millisecond;
            if (DateTime.Now.Second == 0 && DateTime.Now.Millisecond < 500)
            {
                secondAngle = 0;
            }

            int width = this.Width;
            // Draw background
            e.Graphics.DrawImage(Background, 0, 0, width, width);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            // Draw hands
            e.Graphics.TranslateTransform(width / 2, width / 2);
            e.Graphics.RotateTransform(hourAngle);
            e.Graphics.TranslateTransform(-width / 2, -width / 2);
            e.Graphics.DrawImage(HandHour, 0, 0, width, width);

            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(width / 2, width / 2);
            e.Graphics.RotateTransform(minuteAngle);
            e.Graphics.TranslateTransform(-width / 2, -width / 2);
            e.Graphics.DrawImage(HandMinute, 0, 0, width, width);

            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(width / 2, width / 2);
            e.Graphics.RotateTransform(secondAngle);
            e.Graphics.TranslateTransform(-width / 2, -width / 2);
            e.Graphics.DrawImage(HandSecond, 0, 0, width, width);
        }

        private void Redraw()
        {
            if (!isDragging)
            {
                this.Invalidate();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = this.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = Point.Subtract(Cursor.Position, new Size(lastCursor));
                this.Location = Point.Add(lastForm, new Size(delta));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void Form1_Scroll(object sender, ScrollEventArgs e)
        {
            int delta = (e.NewValue - e.OldValue) > 0 ? 20 : -20; // Adjust size in steps of 20
            int newWidth = Math.Max(this.Width + delta, 100); // Minimum width
            int newHeight = Math.Max(this.Height + delta, 100); // Minimum height
            this.Size = new Size(newWidth, newHeight);
        }
    }
}
