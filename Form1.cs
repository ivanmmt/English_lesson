using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private int x = 50, y = 50;
        private int dx = 8, dy = 8;
        private int ballSize = 90;
        private System.Windows.Forms.Timer timer;
        private Color ballColor = Color.FromArgb(255, 50, 180);
        private Random rand = new Random();   // для випадкових кольорів

        public Form1()
        {
            InitializeComponent();

            this.Text = "Прыгающий шарик";
            this.Size = new Size(1024, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 16;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            x += dx;
            y += dy;

            bool colorChanged = false;

            // Відбивання від лівої/правої стінки
            if (x <= 0 || x >= this.ClientSize.Width - ballSize)
            {
                dx = -dx;
                colorChanged = true;
            }

            // Відбивання від верхньої/нижньої стінки
            if (y <= 0 || y >= this.ClientSize.Height - ballSize)
            {
                dy = -dy;
                colorChanged = true;
            }

            // Якщо був удар — змінюємо колір
            if (colorChanged)
            {
                ballColor = Color.FromArgb(
                    rand.Next(100, 256),   // R
                    rand.Next(100, 256),   // G
                    rand.Next(100, 256));  // B
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Тінь
            g.FillEllipse(new SolidBrush(Color.FromArgb(90, 0, 0, 0)), x + 18, y + 18, ballSize, ballSize);

            // Шарик
            g.FillEllipse(new SolidBrush(ballColor), x, y, ballSize, ballSize);

            // Блик
            g.FillEllipse(new SolidBrush(Color.FromArgb(220, 255, 255, 255)), x + 22, y + 22, 28, 28);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Application.Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}