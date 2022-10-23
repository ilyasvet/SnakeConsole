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
		private Point randomPoint = new Point();

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
			if (head.Equals(randomPoint))
			{
				Random rnd = new Random();
				Point newP = new Point(randomPoint);
				points.Add(newP);
				randomPoint = new Point(rnd.Next(Point.BORDER_RIGHT), rnd.Next(Point.BORDER_BOTTOM));
			}
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
				while (value < 0 || value > BORDER_RIGHT)
				{
					if (value < 0)
					{
						value += BORDER_RIGHT + 1;
					}
					else
					{
						value -= BORDER_RIGHT + 1;
					}
				}
				posX = value;
			}
		}
		public int PosY
		{
			get => posY;
			set
			{
				while (value < 0 || value > BORDER_BOTTOM)
				{
					if (value < 0)
					{
						value += BORDER_BOTTOM + 1;
					}
					else
					{
						value -= BORDER_BOTTOM + 1;
					}
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
						direction = Direction.up;
						break;
					case ConsoleKey.DownArrow:
						direction = Direction.down;
						break;
					case ConsoleKey.LeftArrow:
						direction = Direction.left;
						break;
					case ConsoleKey.RightArrow:
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
				sn.Move(direction);
				await Task.Delay(delay);
			}
		}
	}
}
