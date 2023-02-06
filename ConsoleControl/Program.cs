using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleControl
{

	


	

	

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
