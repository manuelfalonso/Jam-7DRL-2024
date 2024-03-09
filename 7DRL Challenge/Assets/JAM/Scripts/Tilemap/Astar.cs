using JAM.TileMap;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JAM.Pathfinding
{
    public class Astar
    {
        private Spot[,] _spots;

        public Astar(int columns, int rows)
        {
            _spots = new Spot[columns, rows];
        }

        private static bool IsValidPath(Spot start, Spot end)
        {
            return end != null && start != null && end.Height < 1;
        }

        public List<Spot> CreatePath(Vector3Int[,] grid, Vector2Int start, Vector2Int end, int length)
        {
            Spot _end = null;
            Spot _start = null;
            var columns = _spots.GetUpperBound(0) + 1;
            var rows = _spots.GetUpperBound(1) + 1;
            _spots = new Spot[columns, rows];

            for (var i = 0; i < columns; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    _spots[i, j] = new Spot(grid[i, j].x, grid[i, j].y, grid[i, j].z);
                }
            }

            for (var i = 0; i < columns; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    if (TileMapManager.Instance.IsObstacle(new Vector3Int(i, j, 0))) { continue; }
                    
                    _spots[i, j].AddNeighboors(_spots, i, j);
                    
                    if (_spots[i, j].X == start.x && _spots[i, j].Y == start.y)
                    {
                        _start = _spots[i, j];
                    }
                    else if (_spots[i, j].X == end.x && _spots[i, j].Y == end.y)
                    {
                        _end = _spots[i, j];
                    }
                }
            }

            if (!IsValidPath(_start, _end)) { return null; }
            
            var openSet = new List<Spot>();
            var closedSet = new List<Spot>();

            openSet.Add(_start);

            while (openSet.Count > 0)
            {
                //Find shortest step distance in the direction of your goal within the open set
                var winner = 0;
                for (var i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].F < openSet[winner].F)
                    {
                        winner = i;
                    }
                    //tie breaking for faster routing
                    else if (openSet[i].F == openSet[winner].F)
                    {
                        if (openSet[i].H < openSet[winner].H)
                        {
                            winner = i;
                        }
                    }
                }

                var current = openSet[winner];

                //Found the path, creates and returns the path
                if (_end != null && openSet[winner] == _end)
                {
                    var path = new List<Spot>();
                    var temp = current;
                    
                    path.Add(temp);
                    
                    while (temp.previous != null)
                    {
                        path.Add(temp.previous);
                        temp = temp.previous;
                    }

                    if (length - (path.Count - 1) < 0)
                    {
                        path.RemoveRange(0, (path.Count - 1) - length);
                    }

                    return path;
                }

                openSet.Remove(current);
                closedSet.Add(current);
                
                //Finds the next closest step on the grid
                var neighboors = current.Neighboors;
                //look threw our current spots neighboors (current spot is the shortest F distance in openSet
                foreach (var n in neighboors)
                {
                    //Checks to make sure the neighboor of our current tile is not within closed set, and has a height of less than 1
                    if (closedSet.Contains(n) || n.Height >= 1) { continue; }
                    var tempG = current.G + 1; //gets a temp comparison integer for seeing if a route is shorter than our current path

                    var newPath = false;
                    if (openSet.Contains(n)) //Checks if the neighboor we are checking is within the openset
                    {
                        if (tempG < n.G) //The distance to the end goal from this neighboor is shorter so we need a new path
                        {
                            n.G = tempG;
                            newPath = true;
                        }
                    }
                    else //if its not in openSet or closed set, then it IS a new path and we should add it too openset
                    {
                        n.G = tempG;
                        newPath = true;
                        openSet.Add(n);
                    }

                    if (newPath) //if it is a newPath caclulate the H and F and set current to the neighboors previous
                    {
                        n.H = Heuristic(n, _end);
                        n.F = n.G + n.H;
                        n.previous = current;
                    }
                }
            }

            return null;
        }

        private int Heuristic(Spot a, Spot b)
        {
            //manhattan
            var dx = Math.Abs(a.X - b.X);
            var dy = Math.Abs(a.Y - b.Y);
            return 1 * (dx + dy);
        }
    }

    public class Spot
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public int Height = 0;
        public List<Spot> Neighboors;
        public Spot previous = null;

        public Spot(int x, int y, int height)
        {
            X = x;
            Y = y;
            F = 0;
            G = 0;
            H = 0;
            Neighboors = new List<Spot>();
            Height = height;
        }

        public void AddNeighboors(Spot[,] grid, int x, int y)
        {
            if (x < grid.GetUpperBound(0))
            {
                Neighboors.Add(grid[x + 1, y]);
            }
            if (x > 0)
            {
                Neighboors.Add(grid[x - 1, y]);
            }
            if (y < grid.GetUpperBound(1))
            {
                Neighboors.Add(grid[x, y + 1]);
            }
            if (y > 0)
            {
                Neighboors.Add(grid[x, y - 1]);
            }
        }
    }
}