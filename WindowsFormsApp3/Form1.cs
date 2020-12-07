using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private List<Circle> Snake2 = new List<Circle>();
        private Circle food = new Circle();
        private Circle nofood = new Circle();

        public Form1()
        {
            InitializeComponent();


            new Settings();


            gameTimer.Interval = 1000 / Settings.x;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Tick += UpdateScreen2;
            gameTimer.Start();


            StartGame();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;


            new Settings();


            Snake.Clear();
            Snake2.Clear();
            player1();
            player2();
            
            


            lblScore.Text = Settings.Wynik.ToString();
            lblScore2.Text = Settings.Wynik2.ToString();
            GenerateFood();
            GenerateNoFood();

        }
        private void player1()
        {
            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);
        }
        private void player2()
        {
            Circle head2 = new Circle { X = 12, Y = 5 };
            Snake2.Add(head2);
        }

        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }

        private void GenerateNoFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            nofood = new Circle();
            nofood.X = random.Next(0, maxXPos);
            while (nofood.X == food.X)
                nofood.X = random.Next(0, maxXPos);
            nofood.Y = random.Next(0, maxYPos);
            while (nofood.Y == food.Y)
                nofood.Y = random.Next(0, maxYPos);
        }

        private void UpdateScreen(object sender, EventArgs e)
        {

            if (Settings.GameOver)
            {

                if (Input.KeyPressed(Keys.Space))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.KeyPressed(Keys.D) && Settings.direction != Direction.Left)
                    Settings.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.A) && Settings.direction != Direction.Right)
                    Settings.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.W) && Settings.direction != Direction.Down)
                    Settings.direction = Direction.Up;
                else if (Input.KeyPressed(Keys.S) && Settings.direction != Direction.Up)
                    Settings.direction = Direction.Down;
                MovePlayer();

            }

            pbCanvas.Invalidate();
        }
        private void UpdateScreen2(object sender, EventArgs e)
        {
            if (Settings.GameOver)
            {

                if (Input.KeyPressed(Keys.Space))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.KeyPressed(Keys.Right) && Settings.direction2 != Direction2.Left)
                    Settings.direction2 = Direction2.Right;
                else if (Input.KeyPressed(Keys.Left) && Settings.direction2 != Direction2.Right)
                    Settings.direction2 = Direction2.Left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction2 != Direction2.Down)
                    Settings.direction2 = Direction2.Up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction2 != Direction2.Up)
                    Settings.direction2 = Direction2.Down;
                MovePlayer();
            }
            pbCanvas.Invalidate();
        }
    private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {

                for (int i = 0; i < Snake.Count; i++)
                {
                    Brush snakeColour;
                    if (i == 0)
                        snakeColour = Brushes.Red;
                    else
                        snakeColour = Brushes.Black;


                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].X * Settings.Width,
                                      Snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));


                    canvas.FillEllipse(Brushes.Green,
                        new Rectangle(food.X * Settings.Width,
                             food.Y * Settings.Height, Settings.Width, Settings.Height));

                    canvas.FillEllipse(Brushes.PaleVioletRed,
                        new Rectangle(nofood.X * Settings.Width,
                             nofood.Y * Settings.Height, Settings.Width, Settings.Height));

                }
                for (int i = 0; i < Snake2.Count; i++)
                {
                    Brush snake2Colour;
                    if (i == 0)
                        snake2Colour = Brushes.Blue;
                    else
                        snake2Colour = Brushes.Black;


                    canvas.FillEllipse(snake2Colour,
                        new Rectangle(Snake2[i].X * Settings.Width,
                                      Snake2[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));
                }
            }
            else
            {
                int Wynik3;
                if (Settings.Wynik > Settings.Wynik2)
                {
                    Wynik3 = Settings.Wynik;
                }
                else if (Settings.Wynik < Settings.Wynik2)
                {
                    Wynik3 = Settings.Wynik2;
                }
                else
                {
                    Wynik3 = 0;
                }
                if (Wynik3 != 0)
                { 
                string gameOver = "Wygrywa gracz z wynikiem  " +Wynik3+"\nNaciśnij spację aby ponowić";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
                }
                else
                {
                    string gameOver = "Remis!";
                    lblGameOver.Text = gameOver;
                    lblGameOver.Visible = true;
                }
            }
        }


        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {

                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }



                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;


                    if (Snake[i].X >= maxXPos)
                    {
                        Snake[i].X = 0;
                       
                    }
                    else if(Snake[0].Y >= maxYPos)
                    {
                        Snake[i].Y = 0;
                    }
                    else if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxXPos;
                    }
                    else if(Snake[i].Y < 0)
                    { 
                        Snake[i].Y = maxYPos;
                    }



                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X &&
                           Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }
                    for (int j = 1; j < Snake2.Count; j++)
                    {
                        if (Snake[i].X == Snake2[j].X &&
                           Snake[i].Y == Snake2[j].Y)
                        {
                            Die();
                        }
                    }

                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }

                    if (Snake[0].X == nofood.X && Snake[0].Y == nofood.Y)
                    {
                        EatNo();
                    }
                }
                else
                {

                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
  
        
            for (int i = Snake2.Count - 1; i >= 0; i--)
            {

                if (i == 0)
                {
                    switch (Settings.direction2)
                    {
                        case Direction2.Right:
                            Snake2[i].X++;
                            break;
                        case Direction2.Left:
                            Snake2[i].X--;
                            break;
                        case Direction2.Up:
                            Snake2[i].Y--;
                            break;
                        case Direction2.Down:
                            Snake2[i].Y++;
                            break;
                    }



                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;


                    if (Snake2[i].X >= maxXPos)
                    {
                        Snake2[i].X = 0;

                    }
                    else if (Snake2[0].Y >= maxYPos)
                    {
                        Snake2[i].Y = 0;
                    }
                    else if (Snake2[i].X < 0)
                    {
                        Snake2[i].X = maxXPos;
                    }
                    else if (Snake2[i].Y < 0)
                    {
                        Snake2[i].Y = maxYPos;
                    }



                    for (int j = 1; j < Snake2.Count; j++)
                    {
                        if (Snake2[i].X == Snake2[j].X &&
                           Snake2[i].Y == Snake2[j].Y)
                        {
                            Die();
                        }
                    }
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake2[i].X == Snake[j].X &&
                           Snake2[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }


                    if (Snake2[0].X == food.X && Snake2[0].Y == food.Y)
                    {
                        Eat2();
                    }

                    if (Snake2[0].X == nofood.X && Snake2[0].Y == nofood.Y)
                    {
                        EatNo();
                    }
                }
                else
                {

                    Snake2[i].X = Snake2[i - 1].X;
                    Snake2[i].Y = Snake2[i - 1].Y;
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void Eat()
        {

            Circle circle = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);


            Settings.Wynik += Settings.Punkty;
            lblScore.Text = Settings.Wynik.ToString();

            GenerateFood();
            GenerateNoFood();
        }
        private void Eat2()
        {

            Circle circle = new Circle
            {
                X = Snake2[Snake2.Count - 1].X,
                Y = Snake2[Snake2.Count - 1].Y
            };
            Snake2.Add(circle);


            Settings.Wynik2 += Settings.Punkty;
            lblScore2.Text = Settings.Wynik2.ToString();

            GenerateFood();
            GenerateNoFood();
        }

        private void EatNo()
        {
            Die();
        }
    

        private void Die()
        {
            Settings.GameOver = true;
        }

        private void pbCanvas_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
