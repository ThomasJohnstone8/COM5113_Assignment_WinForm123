using COM5113_Assignment_WinForm.Path;
using System.Collections.Generic;
using COM5113_Assignment_WinForm.MapInfo;

namespace COM5113_Assignment_WinForm.Algorithms
{
    internal class Dijkstra : PathFinderInterface
    {

        public string LastErrorMessage { get; private set; } = "";

        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            LastErrorMessage = ""; // reset message each run

            var open = new PriorityQueue<SearchNode, int>();
            var closed = new HashSet<(int, int)>();

            open.Enqueue(new SearchNode(start), 0);

            while (open.Count > 0)
            {
                var current = open.Dequeue();

                // if the goal is reached
                if (current.Position.Equals(goal))
                {
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // Mark this node as processed
                closed.Add((current.Position.Row, current.Position.Col));

                foreach (var next in SearchUtilities.GetNeighbours(current.Position))
                {
                    // Skip walls and invalid tiles
                    if (!SearchUtilities.IsWalkable(map, next))
                        continue;

                    // Skip already processed nodes
                    if (closed.Contains((next.Row, next.Col)))
                        continue;

                    int cost = current.CostFromStart + map[next.Row, next.Col];

                    var node = new SearchNode(next, current)
                    {
                        CostFromStart = cost
                    };

                    open.Enqueue(node, cost);
                }
            }

            LastErrorMessage = "no path found.";

            path = new LinkedList<Coord>();
            return false;
        }

        public bool ExpandOnePly(int[,] map) => false;
    }
}
