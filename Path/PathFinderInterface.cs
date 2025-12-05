using COM5113_Assignment_WinForm.Algorithms;

namespace COM5113_Assignment_WinForm.Path

{
    using Grid = int[,];

    // Interface for pathfinding algorithms
    internal interface PathFinderInterface
    {
        bool FindPath(Grid map, Coord start, Coord goal, ref LinkedList<Coord> path);

        bool ExpandOnePly(Grid map);
    }
}
