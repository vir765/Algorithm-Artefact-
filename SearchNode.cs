using System;

namespace Assessment
{
    // This stores a row and column together
    public readonly struct Coord
    {
        public int Row { get; }
        public int Col { get; }

        public Coord(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    // This represents one point in the search
    public class SearchNode
    {
        public Coord Position { get; }
        public int Cost { get; set; }
        public int Score { get; set; }
        public int Estimate => Cost + Score;

        public SearchNode? Predecessor { get; set; }

        public SearchNode(Coord pos, int cost = 0, int score = 0, SearchNode? pred = null)
        {
            Position = pos;
            Cost = cost;
            Score = score;
            Predecessor = pred;
        }
    }

    // This has small helper methods used by the algorithms
    public static class SearchUtilities
    {
        // This then builds the path by following predecessors backwards
        public static LinkedList<Coord> BuildPathList(SearchNode? goal)
        {
            LinkedList<Coord> path = new LinkedList<Coord>();
            SearchNode? node = goal;

            while (node != null)
            {
                path.PushFront(node.Position);
                node = node.Predecessor;
            }

            return path;
        }

        // This works out the Manhattan distance
        public static int ManhattanDistance(Coord current, Coord goal)
        {
            return Math.Abs(current.Row - goal.Row) + Math.Abs(current.Col - goal.Col);
        }

        // This then gets the neighbours in N, E, S, W order
        public static Coord[] GenerateNeighbours(Coord pos)
        {
            return new Coord[]
            {
                new Coord(pos.Row - 1, pos.Col),     // North
                new Coord(pos.Row,     pos.Col + 1), // East
                new Coord(pos.Row + 1, pos.Col),     // South
                new Coord(pos.Row,     pos.Col - 1)  // West
            };
        }
    }
}
