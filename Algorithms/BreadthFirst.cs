using COM5113_Assignment_WinForm.Path;
using System.Collections.Generic;
using COM5113_Assignment_WinForm.MapInfo;

namespace COM5113_Assignment_WinForm.Algorithms
{
    internal class BreadthFirst : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            // makes new queue and closed set
            var open = new Queue<SearchNode>();
            var closedSet = new HashSet<(int, int)>();

            open.Enqueue(new SearchNode(start));

            while (!open.IsEmpty)
            {
                var current = open.Dequeue();

                // Check if we reached the goal
                if (current.Position.Equals(goal))
                {
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                closedSet.Add((current.Position.Row, current.Position.Col));

                // checks others squares
                foreach (var next in SearchUtilities.GetNeighbours(current.Position))
                {
                    // skips tiles that are not walkable
                    if (!SearchUtilities.IsWalkable(map, next))
                        continue;

                    var key = (next.Row, next.Col);

                    if (closedSet.Contains(key))
                        continue;

                    var node = new SearchNode(next, current);
                    open.Enqueue(node);
                }
            }

            path = new LinkedList<Coord>();
            return false;
        }

        public bool ExpandOnePly(int[,] map) => false;
    }
}
