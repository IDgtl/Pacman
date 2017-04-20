using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public partial class GameWindow : Form
    {
        private Bitmap background;
        private BattleField bf;
        public static int FPS = 50;
        public GameWindow()
        {
            InitializeComponent();
            GameInit();
        }

        private void GameInit()
        {
            System.Timers.Timer drawTimer = new System.Timers.Timer(1000/FPS);

            this.Load += GameWindow_Load;
            this.Paint += GameWindow_Paint;
            drawTimer.Elapsed += DrawTimer_Elapsed;
            this.KeyDown += GameWindow_KeyDown;
            drawTimer.Start();
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                bf.ChangePacmanDirection(Direction.Right);
            }
            else if (e.KeyCode == Keys.Left)
            {
                bf.ChangePacmanDirection(Direction.Left);
            }
            else if (e.KeyCode == Keys.Up)
            {
                bf.ChangePacmanDirection(Direction.Up);
            }
            else if (e.KeyCode == Keys.Down)
            {
                bf.ChangePacmanDirection(Direction.Down);
            }
        }

        private void DrawTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bf.DrawGraphic();

            Invalidate();
        }

        private void GameWindow_Paint(object sender, PaintEventArgs e)
        {
            if (background != null)
            {
                e.Graphics.DrawImageUnscaled(background, Point.Empty);
            }
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            if (background == null)
            {
                background = new Bitmap(ClientSize.Width, ClientSize.Height);
            }
            bf = new BattleField(background);
        }
    }
}
