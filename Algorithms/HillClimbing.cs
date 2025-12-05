using COM5113_Assignment_WinForm.Path;
using COM5113_Assignment_WinForm.Algorithms;
using COM5113_Assignment_WinForm.MapInfo;

namespace COM5113_Assignment_WinForm.Algorithms
{
    internal class HillClimber : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            var current = new SearchNode(start);

            while (true)
            {
                if (current.Position.Equals(goal))
                {
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                SearchNode? best = null;
                int bestH = int.MaxValue;

                foreach (var next in SearchUtilities.GetNeighbours(current.Position))
                {
                    if (!SearchUtilities.IsWalkable(map, next)) continue;

                    int h = SearchUtilities.Heuristic(next, goal);

                    // Select the neighbour with the lowest heuristic value
                    if (h < bestH)
                    {
                        bestH = h;
                        best = new SearchNode(next, current);
                    }
                }

                // if no better square found, fail
                if (best == null)
                {
                    path = new LinkedList<Coord>();
                    return false;
                }

                current = best;
            }
        }

        public bool ExpandOnePly(int[,] map) => false;
    }
}
