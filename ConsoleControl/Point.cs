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
}
