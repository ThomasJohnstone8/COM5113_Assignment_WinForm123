using System;
using System.Collections.Generic;

namespace COM5113_Assignment_WinForm
{
    // Coordinate value
    public struct Coord
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Coord(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Coord c)
                return c.Row == Row && c.Col == Col;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }
    }

    // Node used in search algorithms
    internal class SearchNode
    {
        public Coord Position { get; set; }
        public SearchNode? Predecessor { get; set; }

        public int CostFromStart { get; set; }
        public int EstimatedTotalCost { get; set; }

        // stores position and previous node
        public SearchNode(Coord pos, SearchNode? pred = null)
        {
            Position = pos;
            Predecessor = pred;
            CostFromStart = 0;
            EstimatedTotalCost = 0;
        }
    }

    internal static class SearchUtilities
    {
        public static LinkedList<Coord> buildPathList(SearchNode goalNode)
        {
            LinkedList<Coord> path = new LinkedList<Coord>();
            SearchNode? current = goalNode;

            while (current != null)
            {
                path.AddFirst(current.Position);
                current = current.Predecessor;
            }

            return path;
        }

        public static IEnumerable<Coord> GetNeighbours(Coord c)
        {
            yield return new Coord(c.Row - 1, c.Col); // North
            yield return new Coord(c.Row, c.Col + 1); // East
            yield return new Coord(c.Row + 1, c.Col); // South
            yield return new Coord(c.Row, c.Col - 1); // West
        }

        public static bool IsWalkable(int[,] map, Coord c)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            if (c.Row < 0 || c.Col < 0 || c.Row >= rows || c.Col >= cols)
                return false;

            return map[c.Row, c.Col] != 0;
        }

        public static int Heuristic(Coord a, Coord b)
        {
            return Math.Abs(a.Row - b.Row) + Math.Abs(a.Col - b.Col);
        }
    }
}
