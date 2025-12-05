using Assessment;
using System;

namespace Assessment
{
    // The algorithms the user can choose
    enum Algorithm
    {
        BreadthFirst,
        DepthFirst,
        HillClimbing
    }

    internal class PathFinderFactory
    {
        // This returns the right algorithm object
        public static PathFinderInterface NewPathFinder(Algorithm algorithm)
        {
            PathFinderInterface pathFinder;

            switch (algorithm)
            {
                case Algorithm.DepthFirst:
                    pathFinder = new DepthFirst();
                    break;

                case Algorithm.HillClimbing:
                    pathFinder = new HillClimbing();
                    break;

                default:
                    // Breadth First is the default option
                    pathFinder = new BreadthFirst();
                    break;
            }

            return pathFinder;
        }
    }
}
