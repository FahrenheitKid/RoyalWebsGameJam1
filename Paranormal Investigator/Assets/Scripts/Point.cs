using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Point
{
    public int x;
    public int y;

    static public Point zero = new Point(0, 0);

    public Point(int x_, int y_)
    {
        x = x_;
        y = y_;
    }

    public void Add(Point p)
    {
        x += p.x;
        y += p.y;
    }

    public void Add(int _x, int _y)
    {
        x += _x;
        y += _y;
    }

    public static Point byMagnitude(Point p, int magnitude)
    {
        return new Point(p.x * magnitude, p.y * magnitude);
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }

    public static float Distance(Point a, Point b)
    {
        if (a == null || b == null)return -1;

        return Vector2.Distance(new Vector2(a.x, a.y), new Vector2(b.x, b.y));
    }

    public UtilityTools.Directions? GetDirectionTo(Point target)
    {
        return UtilityTools.GetDirectionToPoint(this, target);
    }

    public UtilityTools.Directions[] GetSharedAxis(Point target)
    {
        if (target == null)return null;

        if (target.x == x && target.y != y)
        {
            return UtilityTools.verticals;
        }
        else if (target.x != x && target.y == y)
        {
            return UtilityTools.horizontals;
        }
        else
        {
            return null;
        }
    }

    public Point getNeighbourPoint(UtilityTools.Directions dir)
    {
        Point p = new Point(x, y);

        switch (dir)
        {
            case UtilityTools.Directions.up:
                p.y--;
                break;

            case UtilityTools.Directions.upRight:
                p.y--;
                p.x++;

                break;

            case UtilityTools.Directions.right:
                p.x++;
                break;

            case UtilityTools.Directions.downRight:
                p.y++;
                p.x++;
                break;

            case UtilityTools.Directions.down:
                p.y++;
                break;

            case UtilityTools.Directions.downLeft:
                p.y++;
                p.x--;
                break;

            case UtilityTools.Directions.left:
                p.x--;
                break;

            case UtilityTools.Directions.upLeft:
                p.y--;
                p.x--;
                break;
        }

        return p;
    }

    public bool IsAtEdge(int width, int height, UtilityTools.Directions direction)
    {
        if (width <= 0 || height <= 0)return false;

        switch (direction)
        {
            case UtilityTools.Directions.left:
                return x <= 0;

            case UtilityTools.Directions.right:
                return x >= width - 1;

            case UtilityTools.Directions.up:
                return y <= 0;

            case UtilityTools.Directions.down:
                return y >= height - 1;
        }

        return false;
    }

    public bool IsAtEdge(int width, int height)
    {
        if (width <= 0 || height <= 0)return false;

        foreach (UtilityTools.Directions dir in UtilityTools.axials)
        {
            if (IsAtEdge(width, height, dir))return true;
        }

        return false;
    }

    public static bool IsStraightLine(List<Point> points)
    {
        points.OrderBy(p => p.x);

        bool isLine = true;
        for (int i = 0; i < points.Count - 1; i++)
        {
            //if distance is higher than one, they're not neighbors so not a straight line
            if (Mathf.Abs(points[i].x - points[i + 1].x) > 1)
            {
                isLine = false;
            }
        }

        if (isLine)return true;
        isLine = true;
        points.OrderBy(p => p.y);
        for (int i = 0; i < points.Count - 1; i++)
        {
            //if distance is higher than one, they're not neighbors so not a straight line
            if (Mathf.Abs(points[i].y - points[i + 1].y) > 1)
            {
                isLine = false;
                return isLine;
            }
        }

        return isLine;
    }

    public bool isMyNeighbour(Point p, bool diagonals)
    {
        if (p.Equals(getNeighbourPoint(UtilityTools.Directions.up)) ||
            p.Equals(getNeighbourPoint(UtilityTools.Directions.down)) ||
            p.Equals(getNeighbourPoint(UtilityTools.Directions.left)) ||
            p.Equals(getNeighbourPoint(UtilityTools.Directions.right)))
        {
            return true;
        }

        if (diagonals)
        {
            if (p.Equals(getNeighbourPoint(UtilityTools.Directions.upRight)) ||
                p.Equals(getNeighbourPoint(UtilityTools.Directions.downRight)) ||
                p.Equals(getNeighbourPoint(UtilityTools.Directions.downLeft)) ||
                p.Equals(getNeighbourPoint(UtilityTools.Directions.upLeft)))
            {
                return true;
            }
        }

        return false;
    }

    public bool areAnyMyNeighbour(IList<Point> points, bool diagonals)
    {
        foreach (Point p in points)
        {
            if (isMyNeighbour(p, diagonals))return true;
        }
        return false;
    }

    public void Copy(Point p)
    {
        x = p.x;
        y = p.y;
    }

    public bool Equals(Point p)
    {
        return (p.x == x && p.y == y);
    }

    public bool Equals(int x, int y)
    {
        return (this.x == x && this.y == y);
    }

    public Point Clone()
    {
        return new Point(x, y);
    }

    public static Point Clone(Point p)
    {
        return new Point(p.x, p.y);
    }

    public string print()
    {
        return ("(" + x + ", " + y + ")");
    }
}