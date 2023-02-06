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
            PosX = rnd.Next(Point.borderRight - 1),
            PosY = rnd.Next(Point.borderBottom - 1)
        };

        public void Add(Point p)
        {
            points.Add(p);
        }
        public void Refresh()
        {
            Console.Clear();
            for (int i = 0; i <= Point.borderBottom; i++)
            {
                for (int j = 0; j <= Point.borderRight; j++)
                {
                    if (j == Point.borderRight || i == Point.borderBottom)
                    {

                        if (j == Point.borderRight)
                        {
                            Console.Write('|');
                        }
                        if (i == Point.borderBottom)
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
                randomPoint = new Point(rnd.Next(Point.borderRight), rnd.Next(Point.borderBottom));
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
}
