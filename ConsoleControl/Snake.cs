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
}
