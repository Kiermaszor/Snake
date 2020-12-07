namespace Snake
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };
    public enum Direction2
    {
        Up,
        Down,
        Left,
        Right
    };

    public class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int x { get; set; }
        public static int Wynik { get; set; }
        public static int Wynik2 { get; set; }
        public static int Punkty { get; set; }
        public static bool GameOver { get; set; }
        public static Direction direction { get; set; }
        public static Direction2 direction2 { get; set; }

        public Settings()
        {
            Width = 16;
            Height = 16;
            x = 8;
            Wynik = 0;
            Wynik2 = 0;
            Punkty = 100;
            GameOver = false;
            direction = Direction.Down;
            direction2 = Direction2.Down;
        }
    }


}
