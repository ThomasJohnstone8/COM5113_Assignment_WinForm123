using COM5113_Assignment_WinForm.Path;
using System.Collections.Generic;
using COM5113_Assignment_WinForm.MapInfo;

namespace COM5113_Assignment_WinForm.Algorithms
{
    internal class Dijkstra : PathFinderInterface
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
                    // backtrack to build path
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // mark current as closed
                closed.Add((current.Position.Row, current.Position.Col));

                foreach (var next in SearchUtilities.GetNeighbours(current.Position))
                {
                    if (!SearchUtilities.IsWalkable(map, next))
                        continue;

                    int cost = current.CostFromStart + map[next.Row, next.Col];

                    var node = new SearchNode(next, current)
                    {
                        CostFromStart = cost
                    };

                    open.Enqueue(node, cost);
                }
            }
            // No path found = return false
            path = new LinkedList<Coord>();
            return false;
        }

        public bool ExpandOnePly(int[,] map) => false;
    }
}
