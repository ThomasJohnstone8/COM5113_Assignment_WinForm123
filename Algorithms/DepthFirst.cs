using COM5113_Assignment_WinForm.Path;
using COM5113_Assignment_WinForm.MapInfo;

namespace COM5113_Assignment_WinForm.Algorithms
{
    internal class DepthFirst : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            LinkedList<SearchNode> stack = new LinkedList<SearchNode>();
            var visited = new HashSet<(int, int)>();

            stack.AddFirst(new SearchNode(start));

            while (!stack.IsEmpty)
            {
                var current = stack.RemoveFirst();

                // Check if we reached the goal
                if (current.Position.Equals(goal))
                {
                    // backtrack to build path
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                visited.Add((current.Position.Row, current.Position.Col));

                foreach (var next in SearchUtilities.GetNeighbours(current.Position))
                {
                    if (!SearchUtilities.IsWalkable(map, next))
                        continue;

                    if (!visited.Contains((next.Row, next.Col)))
                        stack.AddFirst(new SearchNode(next, current));
                }
            }

            path = new LinkedList<Coord>();
            return false;
        }

        public bool ExpandOnePly(int[,] map) => false;
    }
}

