using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleControl
{

	class Snake
	{
		private List<Point> points = new List<Point>() { new Point() };
		private static Random rnd = new Random();
		private Point randomPoint = new Point()
		{
			PosX = rnd.Next(Point.BORDER_RIGHT - 1),
			PosY = rnd.Next(Point.BORDER_BOTTOM - 1)
		};

        public void Add(Point p)
		{
			points.Add(p);
		}
		public void Refresh()
		{
			Console.Clear();
			for (int i = 0; i <= Point.BORDER_BOTTOM; i++)
			{
				for (int j = 0; j <= Point.BORDER_RIGHT; j++)
				{
					if (j == Point.BORDER_RIGHT || i == Point.BORDER_BOTTOM)
					{

						if (j == Point.BORDER_RIGHT)
						{
							Console.Write('|');
						}
						if (i == Point.BORDER_BOTTOM)
						{
							Console.Write('-');
						}
					}
					else
					{
						Point temp = new Point(j, i);
						if (points.Contains(temp) || temp.Equals(randomPoint))
						{
							Console.Write('*');
						}
						else
						{
							Console.Write(' ');
						}
					}
                }
				Console.WriteLine();
			}
			
		}
		public void Move(Direction direction)
		{
			for (int i = points.Count - 1; i > 0; i--)
			{
				Point current = points[i];// делаем, чтобы не производить обращение 2 раза
				Point next = points[i - 1];
				current.PosX = next.PosX;
				current.PosY = next.PosY;
			}
			
			Point head = points[0];
			head.MovePoint(direction);
            if (!IsSet())
            {
                throw new Exception("End of game");
            }
            if (head.Equals(randomPoint))
			{
				
				Point newP = new Point(randomPoint);
				points.Add(newP);
				randomPoint = new Point(rnd.Next(Point.BORDER_RIGHT), rnd.Next(Point.BORDER_BOTTOM));
			}
            
        }

		private bool IsSet()
		{
			var groups = points.GroupBy(g => g);
			foreach (var group in groups)
			{
				if (group.Count() > 1) return false;
			}
			return true;
		}


	}


	class Point
	{
		public const int BORDER_BOTTOM = 10;
		public const int BORDER_RIGHT = 25;
		
		private int posX;
		private int posY;
		
		public int PosX
		{
			get => posX;
			set
			{
				if (value < 0 || value >= BORDER_RIGHT)
				{
					throw new Exception("Out of border");
				}
				posX = value;
			}
		}
		public int PosY
		{
			get => posY;
			set
			{
                if (value < 0 || value >= BORDER_BOTTOM)
                {
                    throw new Exception("Out of border");
                }
                posY = value;
			}
		}

		public Point(Point p)
		{
			PosX = p.PosX;
			PosY = p.PosY;
		}
		public Point(int x = 0, int y = 0)
		{
			PosX = x;
			PosY = y;
		}

		public void MovePoint(Direction direction)
		{
			switch (direction)
			{
				case Direction.right:
					PosX += 1;
					break;
				case Direction.left:
					PosX -= 1;
					break;
				case Direction.up:
					PosY -= 1;
					break;
				case Direction.down:
					PosY += 1;
					break;
				default:
					break;
			}
		}
		public void Refresh()
		{
			Console.Clear();
			for (int i = 0; i < PosY; i++)
			{
				Console.WriteLine();
			}
			for (int i = 0; i < PosX; i++)
			{
				Console.Write(' ');
			}
			Console.Write('*');
			Console.WriteLine();
			for (int i = 0; i < BORDER_BOTTOM - PosY; i++)
			{
				Console.WriteLine();
			}
			Console.WriteLine($"\nPosX: {PosX}; PosY {BORDER_BOTTOM - PosY}");
		}

		public override bool Equals(object obj)
		{
			return (obj as Point)?.PosX == PosX
				&& (obj as Point)?.PosY == PosY;
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
		public override string ToString()
		{
			return $"X: {PosX}; Y: {PosY}";
		}
	}

	enum Direction
	{
		right,
		left,
		up,
		down,
	}

	class Program
	{
		private static Direction direction = Direction.right;
		private static bool end = false;
		private const int delay = 50;

		static void Main(string[] args)
		{
			//Point p = new Point() {PosX = 0, PosY = Point.BORDER_BOTTOM };
			//Go(p);

			Snake sn = new Snake();
			Go(sn);

			while (!end)
			{
				var dir = Console.ReadKey();
				switch (dir.Key)
				{
					case ConsoleKey.UpArrow:
						if(direction != Direction.down)
							direction = Direction.up;
						break;
					case ConsoleKey.DownArrow:
                        if (direction != Direction.up)
                            direction = Direction.down;
						break;
					case ConsoleKey.LeftArrow:
                        if (direction != Direction.right)
                            direction = Direction.left;
						break;
					case ConsoleKey.RightArrow:
                        if (direction != Direction.left)
                            direction = Direction.right;
						break;
					default:
						end = true;
						break;
				}
			}
			
		}

		static async void Go(Point p)
		{
			while (!end)
			{
				p.Refresh();
				p.MovePoint(direction);
				await Task.Delay(delay);
			}
		}

		static async void Go(Snake sn)
		{
			while (!end)
			{
				sn.Refresh();
				try
				{
                    sn.Move(direction);
                }
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					return;
				}
				await Task.Delay(delay);
			}
		}
	}
}
