using System;

namespace Assessment
{
    // Breadth First Search algorithm
    internal class BreadthFirst : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            // This then sets up the lists used for the search
            var open = new Queue<SearchNode>();     // places to check
            var closed = new Queue<SearchNode>();   // places already checked

            // This then adds the starting point
            var startNode = new SearchNode(start);
            open.Enqueue(startNode);

            SearchNode? current = null;

            // Search until we find the goal or run out of nodes
            while (!open.IsEmpty())
            {
                // This gets the next location to check
                current = open.Pop();

                // This then checks if we reached the goal
                if (current.Position.Row == goal.Row &&
                    current.Position.Col == goal.Col)
                {
                    path = SearchUtilities.BuildPathList(current);
                    return true;
                }

                // Add current to visited list
                closed.Enqueue(current);

                // Check all neighbours
                Coord[] neighbours = SearchUtilities.GenerateNeighbours(current.Position);

                foreach (Coord nextPos in neighbours)
                {
                    // Skip walls
                    if (nextPos.Row < 0 || nextPos.Row >= map.GetLength(0) ||
                        nextPos.Col < 0 || nextPos.Col >= map.GetLength(1) ||
                        map[nextPos.Row, nextPos.Col] == 0)
                    {
                        continue;
                    }

                    // Make a new node for the neighbour
                    var nextNode = new SearchNode(nextPos, 0, 0, current);

                    // Skip neighbours already checked
                    if (open.Contains(nextNode) || closed.Contains(nextNode))
                    {
                        continue;
                    }

                    // Add neighbour to be checked later
                    open.Enqueue(nextNode);
                }
            }

            // No path found
            return false;
        }
    }
}
