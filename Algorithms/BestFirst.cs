using COM5113_Assignment_WinForm.Path;
using System.Collections.Generic;
using COM5113_Assignment_WinForm.MapInfo;

namespace COM5113_Assignment_WinForm.Algorithms
{
    internal class BestFirst : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            var open = new PriorityQueue<SearchNode, int>();
            var closed = new HashSet<(int, int)>();

            open.Enqueue(new SearchNode(start), 0);

            while (open.Count > 0)
            {
                var current = open.Dequeue();

                // Check if we reached the goal
                if (current.Position.Equals(goal))
                {
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                closed.Add((current.Position.Row, current.Position.Col));

                // checks others squares
                foreach (var next in SearchUtilities.GetNeighbours(current.Position))
                {
                    // Skip tiles that are not walkable
                    if (!SearchUtilities.IsWalkable(map, next))
                        continue;

                    if (closed.Contains((next.Row, next.Col)))
                        continue;

                    // Use only heuristic for Best-First Search
                    int h = SearchUtilities.Heuristic(next, goal);
                    open.Enqueue(new SearchNode(next, current), h);
                }
            }

            // No path found = return false
            path = new LinkedList<Coord>();
            return false;
        }
        public bool ExpandOnePly(int[,] map) => false;
    }
}
