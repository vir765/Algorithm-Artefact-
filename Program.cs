using System;
using System.IO;

namespace Assessment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This writes the program name
            Console.WriteLine("Algorithm Artefact\n");

            // This asks the user which map file to load
            Console.Write("Enter map name (test1,test2,test3,test4,test5,test6): ");
            string mapName = Console.ReadLine() ?? "test1";
            string fileName = mapName + "Map.txt";

            // This then checks if that file exists
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found.");
                return;
            }

            // This then loads the map and extract the start + goal positions
            int[,] map;
            Coord start, goal;
            LoadMap(fileName, out map, out start, out goal);

            // This then asks the user which algorithm they want to use
            Console.WriteLine("\nChoose an search algorithm:");
            Console.WriteLine("1 Breadth first search algorithm");
            Console.WriteLine("2 Depth first search algorithm");
            Console.WriteLine("3 Hill climbing algorithm");
            Console.Write("Enter algorithm number: ");

            // Then checks the users choice with the default choice being1 
            string choice = Console.ReadLine() ?? "1";

            // Pick the algorithm based on the number they entered _ is anything else except 2 or 3
            Algorithm selectedAlg = choice switch
            {
                "2" => Algorithm.DepthFirst,
                "3" => Algorithm.HillClimbing,
                _ => Algorithm.BreadthFirst
            };

            // This then creates the pathfinder object
            PathFinderInterface pathfinder =
                PathFinderFactory.NewPathFinder(selectedAlg);

            // This will then store the found path
            LinkedList<Coord> path = new LinkedList<Coord>();

            // This runs the algorithm
            bool found = pathfinder.FindPath(map, start, goal, ref path);

            if (!found)
            {
                Console.WriteLine("\nNo path could be found.");
                return;
            }

            // Then displays the final path on screen
            Console.WriteLine("\nPath found:\n");
            PrintMapWithPath(map, path);

            // Then saves the path to output text file
            string outputFile = $"{mapName}Path{selectedAlg}.txt";
            SavePathToFile(outputFile, path);

            Console.WriteLine($"\nPath saved to: {outputFile}");
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        // Then loads the map data from the text file
        static void LoadMap(string fileName, out int[,] map,
                            out Coord start, out Coord goal)
        {
            string[] lines = File.ReadAllLines(fileName);

            // First line has the rows and columns
            string[] dims = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int rows = int.Parse(dims[0]);
            int cols = int.Parse(dims[1]);

            map = new int[rows, cols];

            // Second line has the start position
            string[] sParts = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            start = new Coord(int.Parse(sParts[0]), int.Parse(sParts[1]));

            // Third line has the goal position
            string[] gParts = lines[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            goal = new Coord(int.Parse(gParts[0]), int.Parse(gParts[1]));

            // Then the rest of the lines are for the terrain
            for (int r = 0; r < rows; r++)
            {
                string[] rowData = lines[r + 3].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int c = 0; c < cols; c++)
                {
                    map[r, c] = int.Parse(rowData[c]);
                }
            }
        }

        // Shows the map and marks the path with '*'
        static void PrintMapWithPath(int[,] map, LinkedList<Coord> path)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Then stores the path positions in a quick lookup table
            bool[,] onPath = new bool[rows, cols];
            Coord coord = default;

            // This takes everything out of the path for marking
            LinkedList<Coord> tempPath = new LinkedList<Coord>();
            while (path.PopFront(ref coord))
            {
                onPath[coord.Row, coord.Col] = true;
                tempPath.PushBack(coord);
            }

            // Then makes the path the way it was
            while (tempPath.PopFront(ref coord))
            {
                path.PushBack(coord);
            }

            // This draws the map
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (onPath[r, c])
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write(map[r, c] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        // Saves the path to output file
        static void SavePathToFile(string fileName, LinkedList<Coord> path)
        {
            using StreamWriter writer = new StreamWriter(fileName);
            Coord coord = default;

            while (path.PopFront(ref coord))
            {
                writer.WriteLine($"{coord.Row} {coord.Col}");
            }
        }
    }
}
