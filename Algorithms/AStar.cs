using COM5113_Assignment_WinForm.Path;
using System.Collections.Generic;
using COM5113_Assignment_WinForm.MapInfo;

namespace COM5113_Assignment_WinForm.Algorithms
{
    internal class AStar : PathFinderInterface
    {
        // number of times the open list was updated
        public static int OpenSortCount = 0;

        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            // Reset counter
            OpenSortCount = 0;

            // Open list ordered by f = g + h (f = goal + heuristic)
            var open = new PriorityQueue<SearchNode, int>();
            var closed = new HashSet<(int, int)>();

            var startNode = new SearchNode(start)
            {
                CostFromStart = 0,
                EstimatedTotalCost = SearchUtilities.Heuristic(start, goal)
            };

            open.Enqueue(startNode, startNode.EstimatedTotalCost);
            OpenSortCount++; // count initial enqueue

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

                    var key = (next.Row, next.Col);
                    if (closed.Contains(key))
                        continue;

                    int g = current.CostFromStart + map[next.Row, next.Col];
                    int h = SearchUtilities.Heuristic(next, goal);
                    int f = g + h;

                    var node = new SearchNode(next, current)
                    {
                        CostFromStart = g,
                        EstimatedTotalCost = f
                    };

                    open.Enqueue(node, f);
                    OpenSortCount++; // count each enqueue
                }
            }

            path = new LinkedList<Coord>();
            return false;
        }

        public bool ExpandOnePly(int[,] map)
        {
            return false;
        }
    }
}
