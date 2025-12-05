using COM5113_Assignment_WinForm.Path;
using COM5113_Assignment_WinForm.MapInfo;

namespace COM5113_Assignment_WinForm.Algorithms
{
    internal class HillClimber : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            // Start from the given coordinates
            var current = new SearchNode(start);

            // Keep track of visited positions so it doesn't loop forever
            var visited = new HashSet<(int, int)>();
            visited.Add((start.Row, start.Col));

            while (true)
            {
                // Check if we reached the goal
                if (current.Position.Equals(goal))
                {
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                SearchNode? best = null;
                int bestH = int.MaxValue;

                // Examine each neighbour and choose the one with the lowest heuristic
                foreach (var next in SearchUtilities.GetNeighbours(current.Position))
                {
                    if (!SearchUtilities.IsWalkable(map, next))
                        continue;

                    var key = (next.Row, next.Col);

                    // Skip locations we've already visited
                    if (visited.Contains(key))
                        continue;

                    int h = SearchUtilities.Heuristic(next, goal);

                    if (h < bestH)
                    {
                        bestH = h;
                        best = new SearchNode(next, current);
                    }
                }

                // if there are no other options, the algorithm fails 
                if (best == null)
                {
                    path = new LinkedList<Coord>();
                    return false;
                }

                // Move to the chosen neighbouring square and mark it as visited
                current = best;
                visited.Add((current.Position.Row, current.Position.Col));
            }
        }

        public bool ExpandOnePly(int[,] map) => false;
    }
}
