using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Snake gameSnake;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(gameSnake == null)
            {
                return;
            }
            label1.Text = e.KeyCode.ToString();
            char c = (char)e.KeyCode;
            switch( c )
            {
                case 'A':
                    gameSnake.HeadDiretion = Snake.Diretion.left;
                    break;
                case 'S':
                    gameSnake.HeadDiretion = Snake.Diretion.down;
                    break;
                case 'W':
                    gameSnake.HeadDiretion = Snake.Diretion.up;
                    break;
                case 'D':
                    gameSnake.HeadDiretion = Snake.Diretion.right;
                    break;
            }
        }

        private void InitGameSnake()
        {
            gameSnake = new Snake(
                this.canvasBox.CreateGraphics(),
                10,
                canvasBox.Width,
                canvasBox.Height,
                Color.Pink);

            for (int i = 0; i < 40; i++)
            {
                gameSnake.Elongate();
            }
            snakeTimer.Start();
        }

        private void DropSnake()
        {
            gameSnake = null;
            snakeTimer.Stop();
        }

        private void snakeTimer_Tick(object sender, EventArgs e)
        {
            gameSnake.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click!");
            //button1.Visible = false;
            InitGameSnake();
            button1.Hide();
            button1.Enabled = false;
        }

    }
}
