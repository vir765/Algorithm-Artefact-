using System;

namespace Assessment
{
    internal class HillClimbing : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            // This then sets up the lists used for the search
            var open = new Stack<SearchNode>();      // places to check
            var closed = new Stack<SearchNode>();    // places already checked
            var tmpList = new LinkedList<SearchNode>(); // neighbours to sort

            // This then adds the starting point
            var startNode = new SearchNode(start);
            open.Push(startNode);

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
                closed.Push(current);

                // Reset neighbour list each loop
                tmpList = new LinkedList<SearchNode>();

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

                    // Make a node and score it
                    int score = SearchUtilities.ManhattanDistance(nextPos, goal);
                    var nextNode = new SearchNode(nextPos, 0, score, current);

                    // Skip nodes already seen
                    if (open.Contains(nextNode) || closed.Contains(nextNode))
                    {
                        continue;
                    }

                    // Add neighbour for sorting
                    tmpList.PushBack(nextNode);
                }

                // Sort neighbours by score
                var tempArray = new System.Collections.Generic.List<SearchNode>();
                SearchNode? holder = default;
                while (tmpList.PopFront(ref holder))
                {
                    tempArray.Add(holder);
                }
                tempArray.Sort((a, b) => a.Score.CompareTo(b.Score));

                // Add sorted neighbours to open list best first
                for (int i = tempArray.Count - 1; i >= 0; i--)
                {
                    open.Push(tempArray[i]);
                }
            }

            // No path found
            return false;
        }
    }
}
