using COM5113_Assignment_WinForm.Algorithms;
using COM5113_Assignment_WinForm.MapInfo;


namespace COM5113_Assignment_WinForm.Path
{
    // pathfinder factory checks algorithms selected and uses that algorithm
    internal static class PathFinderFactory
    {
        public static PathFinderInterface Create(string algorithm)
        {
            algorithm = algorithm.Trim().ToUpper();

            return algorithm switch
            {
                "BFS" => new BreadthFirst(),
                "DFS" => new DepthFirst(),
                "HILL" => new HillClimber(),
                "BESTFIRST" => new BestFirst(),
                "DIJKSTRA" => new Dijkstra(),
                "ASTAR" => new AStar(),
                _ => throw new ArgumentException($"Unknown algorithm: {algorithm}")
            };
        }
    }
}


