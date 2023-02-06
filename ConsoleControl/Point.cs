using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControl
{
    enum Direction
	{
		right,
		left,
		up,
		down,
	}
    class Point
    {
        public static int borderBottom;
        public static int borderRight;
        private static bool _borderIsDead;

        private int posX;
        private int posY;

        public int PosX
        {
            get => posX;
            set
            {
                if (value < 0 || value >= borderRight)
                {
                    if (_borderIsDead)
                    {
                        throw new Exception("Out of border");
                    }
                    else
                    {
                        posX = value == borderRight ? 0 : borderRight - 1;
                        return;
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
                
                if (value < 0 || value >= borderBottom)
                {
                    if (_borderIsDead)
                    {
                        throw new Exception("Out of border");
                    }
                    else
                    {
                        posY = value == borderBottom ? 0 : borderBottom - 1;
                        return;
                    }
                }
                posY = value;
            }
        }
        static Point()
        {
            Console.WriteLine("Введите высоту поля. (Не больше 10)");
            borderBottom = int.Parse(Console.ReadLine()) % 11;
            Console.WriteLine("Введите длину поляж. (Не больше 20)");
            borderRight = int.Parse(Console.ReadLine()) % 21;
            Console.WriteLine("Границы убивают змею?");
            _borderIsDead = Convert.ToBoolean(int.Parse(Console.ReadLine()) % 2);
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
}
