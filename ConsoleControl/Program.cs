using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControl
{
	enum Level
	{
		easy,
		normal,
		hard,
		professional,
		master,
	}
	class Program
	{
		private static Direction direction = Direction.right;
		private static bool end = false;
		private static int delay;
		private static Level level;

		static void Main(string[] args)
		{
			Console.WriteLine("Введите уровень сложности. (1-5) 1 - самый лёгкий, 5 - самый сложный");
			level = (Level)int.Parse(Console.ReadLine());
			
			SetDelay();

			Snake sn = new Snake(); //Вызывается статический конструктор точки и задаются основные параметры.
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
					case ConsoleKey.F:
						end = true;
						break;
					default:
						break;
				}
			}
			
		}
		private static void SetDelay()
		{
			switch (level)
			{
				case Level.easy:
					delay = 300;
					break;
				case Level.normal:
					delay = 200;
					break;
				case Level.hard:
					delay = 100;
					break;
				case Level.professional:
					delay = 70;
					break;
				case Level.master:
					delay = 50;
					break;
				default:
					break;
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
				if (direction == Direction.up || direction == Direction.down)
				{
					await Task.Delay(delay * 3); //Движение по y
				}
				else
				{
					await Task.Delay(delay); //Движение по х
				}
			}
		}
	}
}
