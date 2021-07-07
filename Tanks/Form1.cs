using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public partial class Form1 : Form
    {
        // создаем логику игры
        GameLogic game;

        public Form1()
        {
            InitializeComponent();
            game = new GameLogic(pictureBox1);
            timer2.Interval = 6;
            progressBar1.Maximum = 200;
            progressBar1.Minimum = 0;
        }

        // выход из приложения
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //таймер, задает движение игре, отрисовывает игру-очки и т.п.
        private void timer2_Tick(object sender, EventArgs e)
        { 
            game.Update();
            pictureBox1.Refresh();
            progressBar1.Value = game.PlayerHP;
            labelScore.Text = $"Score: {game.PlayerScore}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Controller.ResetId();
                if (textBox1.Text.Length != 0) game = new GameLogic(pictureBox1, Int32.Parse(textBox1.Text));
                //else { game = new GameLogic(pictureBox1); }
                timer2.Start();
            }
            catch (Exception) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.W:
                    {
                        game.StartRun(0);
                        game.ChangePlayerOrientier(Orientier.Up);
                        break;
                    }

                case Keys.S:
                    {
                        game.StartRun(0);
                        game.ChangePlayerOrientier(Orientier.Down); 
                        break;
                    }

                case Keys.A:
                    {
                        game.StartRun(0);
                        game.ChangePlayerOrientier(Orientier.Left);
                        break;
                    }

                case Keys.D:
                    {
                        game.StartRun(0);
                        game.ChangePlayerOrientier(Orientier.Right);
                        break;
                    }
                case Keys.M:
                    game.AddBulletTask(0);
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            game.StopRun(0);
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    {
                        break;
                    }
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }
    }
}
