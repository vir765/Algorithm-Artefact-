using System;

namespace Assessment
{
    using Grid = int[,];

    // All search algorithms must follow this method pattern
    internal interface PathFinderInterface
    {
        // This finds a path from start to goal and returns it in path
        bool FindPath(Grid map, Coord start, Coord goal, ref LinkedList<Coord> path);
    }
}
