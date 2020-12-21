using System;
using System.Linq;
using System.Collections.Generic;

public class Day12
{
    enum FerryDir
    {
        N,
        S,
        E,
        W,
        L,
        R,
        F
    };

    // This class is used only for part2 :(
    class Coord
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public string ToString() => $"({x},{y})";

        public void MoveByCardinal(FerryDir dir, int dist)
        {
            switch(dir)
            {
                case FerryDir.N:
                    y += dist;
                    break;

                case FerryDir.E:
                    x += dist;
                    break;

                case FerryDir.S:
                    y -= dist;
                    break;

                case FerryDir.W:
                    x -= dist;
                    break;

                default:
                    Console.WriteLine("Not a cardinal line!");
                    break;
            }
        }

        public void RotateAround(FerryDir whereDir, int degrees)
        {
            Console.WriteLine($"Rotate {ToString()} to {whereDir} by {degrees}");
            var value = (int)(degrees / 90.0); //1, 2, 3
            
            if (whereDir == FerryDir.L) value *= -1; 
            
            if (value < 0) value += 4;
            
            value %= 4;

            Console.WriteLine($"val {value}");

            switch(value)
            {
                case 1: //90
                    (x, y) = (y, -x);
                    break;

                case 2: //180
                    (x, y) = (-x, -y);
                    break;

                case 3: //270
                    (x, y) = (-y, x);
                    break;
            }
            Console.WriteLine($"rotated {ToString()}");
        }
        
    }

    static FerryDir Rotate(FerryDir currentDir, FerryDir whereDir, int degrees)
    {
        Console.WriteLine($"Rotate current: {currentDir} where: {whereDir} by: {degrees}");

        var stuff = new List<(FerryDir, int)> {
            (FerryDir.N, 0), 
            (FerryDir.E, 1),
            (FerryDir.S, 2), 
            (FerryDir.W, 3)
            };
        var current = stuff.Single(s => s.Item1 == currentDir).Item2;
        var value = (int)(degrees / 90.0);
        if (whereDir == FerryDir.L)
            value *= -1;

        Console.WriteLine($"current val: {current} to val: {value}");

        current = (current + value) % 4;
        if(current < 0)
            current += 4;

        Console.WriteLine($"after turn val: {current}");
        return stuff.Single(s => s.Item2 == current).Item1;
    }

    public static int WhereAreWe()
    {
        var coords = Follow(Values);
        return ManhatanDist(coords.x, coords.y);
    }

    public static int WhereAreWeReally()
    {
        var coords = FollowWaypoint(Values);
        return ManhatanDist(coords.x, coords.y);
    }

    private static int ManhatanDist(int x, int y) => Math.Abs(x) + Math.Abs(y);

    private static (int x, int y) Follow(List<(FerryDir, int)> follow)
    {
        int x = 0, y = 0;
        var head = FerryDir.E;

        void MoveByCardinal(FerryDir dir, int dist)
        {
            switch(dir)
            {
                case FerryDir.N:
                    y += dist;
                    break;

                case FerryDir.E:
                    x += dist;
                    break;

                case FerryDir.S:
                    y -= dist;
                    break;

                case FerryDir.W:
                    x -= dist;
                    break;

                default:
                    Console.WriteLine("Not a cardinal line!");
                    break;
            }
        }
        
        foreach(var ferryDir in follow)
        {
            switch(ferryDir.Item1)
            {
                case FerryDir.N:
                case FerryDir.E:
                case FerryDir.S:
                case FerryDir.W:
                    MoveByCardinal(ferryDir.Item1, ferryDir.Item2);
                    break;

                case FerryDir.F:
                    MoveByCardinal(head, ferryDir.Item2);
                    break;

                case FerryDir.L:
                case FerryDir.R:
                    head = Rotate(head, ferryDir.Item1, ferryDir.Item2);
                    break;
            }
        }

        return (x, y);
    }

    private static (int x, int y) FollowWaypoint(List<(FerryDir, int)> directions)
    {
        var ship = new Coord(0, 0);
        var waypoint = new Coord(10, 1);

        foreach (var dir in directions)
        {
            switch(dir.Item1)
            {
                case FerryDir.N:
                case FerryDir.E:
                case FerryDir.S:
                case FerryDir.W:
                    //Console.WriteLine("Movin waypoint to ")
                    waypoint.MoveByCardinal(dir.Item1, dir.Item2);
                    break;

                case FerryDir.F:
                    Console.WriteLine($"Movin ship {ship.ToString()} forward waypoint {waypoint.ToString()} {dir.Item2} times");
                    ship.x += waypoint.x * dir.Item2;
                    ship.y += waypoint.y * dir.Item2;
                    Console.WriteLine($"Ship is at {ship.ToString()}");
                    break;
                
                case FerryDir.L:
                case FerryDir.R:
                    waypoint.RotateAround(dir.Item1, dir.Item2); //I misunderstood, waypoint is always around ship
                    break;
            }
        }
        return (ship.x, ship.y);
    }

    private static List<(FerryDir, int)> TestValues = new List<(FerryDir, int)> {
        (FerryDir.F, 10),
        (FerryDir.N, 3),
        (FerryDir.F, 7),
        (FerryDir.R, 90),
        (FerryDir.F, 11)
    };

//^([N|E|S|W|L|R|F|])(\d*)$
//(FerryDir.$1, $2),
    private static List<(FerryDir, int)> Values = new List<(FerryDir, int)> {
        (FerryDir.N, 3),
        (FerryDir.L, 90),
        (FerryDir.F, 63),
        (FerryDir.W, 5),
        (FerryDir.F, 46),
        (FerryDir.E, 3),
        (FerryDir.F, 22),
        (FerryDir.N, 2),
        (FerryDir.R, 90),
        (FerryDir.F, 68),
        (FerryDir.E, 4),
        (FerryDir.W, 3),
        (FerryDir.R, 90),
        (FerryDir.S, 3),
        (FerryDir.W, 4),
        (FerryDir.R, 180),
        (FerryDir.E, 1),
        (FerryDir.S, 5),
        (FerryDir.F, 90),
        (FerryDir.N, 4),
        (FerryDir.E, 3),
        (FerryDir.N, 1),
        (FerryDir.R, 90),
        (FerryDir.F, 74),
        (FerryDir.R, 90),
        (FerryDir.E, 2),
        (FerryDir.R, 90),
        (FerryDir.W, 1),
        (FerryDir.S, 3),
        (FerryDir.W, 4),
        (FerryDir.F, 5),
        (FerryDir.S, 1),
        (FerryDir.E, 5),
        (FerryDir.S, 1),
        (FerryDir.E, 4),
        (FerryDir.R, 90),
        (FerryDir.E, 5),
        (FerryDir.L, 90),
        (FerryDir.E, 4),
        (FerryDir.R, 90),
        (FerryDir.E, 2),
        (FerryDir.F, 57),
        (FerryDir.N, 1),
        (FerryDir.L, 90),
        (FerryDir.F, 59),
        (FerryDir.R, 90),
        (FerryDir.N, 1),
        (FerryDir.W, 3),
        (FerryDir.S, 2),
        (FerryDir.L, 90),
        (FerryDir.N, 3),
        (FerryDir.E, 1),
        (FerryDir.F, 56),
        (FerryDir.L, 180),
        (FerryDir.S, 3),
        (FerryDir.R, 90),
        (FerryDir.F, 88),
        (FerryDir.E, 3),
        (FerryDir.F, 59),
        (FerryDir.W, 1),
        (FerryDir.N, 2),
        (FerryDir.F, 52),
        (FerryDir.W, 4),
        (FerryDir.F, 69),
        (FerryDir.W, 2),
        (FerryDir.F, 10),
        (FerryDir.W, 1),
        (FerryDir.R, 180),
        (FerryDir.W, 1),
        (FerryDir.R, 90),
        (FerryDir.F, 14),
        (FerryDir.L, 90),
        (FerryDir.W, 1),
        (FerryDir.S, 5),
        (FerryDir.L, 90),
        (FerryDir.S, 3),
        (FerryDir.R, 90),
        (FerryDir.E, 3),
        (FerryDir.F, 35),
        (FerryDir.R, 90),
        (FerryDir.E, 3),
        (FerryDir.S, 3),
        (FerryDir.F, 45),
        (FerryDir.E, 2),
        (FerryDir.R, 90),
        (FerryDir.F, 86),
        (FerryDir.E, 1),
        (FerryDir.E, 4),
        (FerryDir.F, 35),
        (FerryDir.L, 180),
        (FerryDir.S, 1),
        (FerryDir.L, 90),
        (FerryDir.N, 2),
        (FerryDir.F, 71),
        (FerryDir.L, 180),
        (FerryDir.W, 3),
        (FerryDir.S, 4),
        (FerryDir.R, 90),
        (FerryDir.N, 5),
        (FerryDir.F, 93),
        (FerryDir.W, 4),
        (FerryDir.F, 74),
        (FerryDir.L, 180),
        (FerryDir.E, 2),
        (FerryDir.R, 180),
        (FerryDir.F, 11),
        (FerryDir.S, 5),
        (FerryDir.F, 28),
        (FerryDir.S, 3),
        (FerryDir.F, 93),
        (FerryDir.W, 2),
        (FerryDir.N, 4),
        (FerryDir.F, 26),
        (FerryDir.R, 90),
        (FerryDir.S, 4),
        (FerryDir.L, 90),
        (FerryDir.N, 1),
        (FerryDir.L, 90),
        (FerryDir.E, 2),
        (FerryDir.L, 90),
        (FerryDir.F, 3),
        (FerryDir.E, 4),
        (FerryDir.F, 43),
        (FerryDir.R, 90),
        (FerryDir.W, 4),
        (FerryDir.R, 90),
        (FerryDir.E, 3),
        (FerryDir.S, 1),
        (FerryDir.R, 180),
        (FerryDir.L, 90),
        (FerryDir.F, 62),
        (FerryDir.L, 90),
        (FerryDir.E, 5),
        (FerryDir.R, 90),
        (FerryDir.W, 3),
        (FerryDir.L, 180),
        (FerryDir.F, 40),
        (FerryDir.F, 20),
        (FerryDir.N, 2),
        (FerryDir.L, 270),
        (FerryDir.E, 1),
        (FerryDir.F, 14),
        (FerryDir.W, 3),
        (FerryDir.S, 5),
        (FerryDir.R, 90),
        (FerryDir.F, 3),
        (FerryDir.S, 2),
        (FerryDir.L, 90),
        (FerryDir.W, 5),
        (FerryDir.L, 270),
        (FerryDir.W, 1),
        (FerryDir.R, 90),
        (FerryDir.F, 11),
        (FerryDir.R, 90),
        (FerryDir.E, 3),
        (FerryDir.N, 1),
        (FerryDir.E, 3),
        (FerryDir.F, 19),
        (FerryDir.S, 5),
        (FerryDir.L, 180),
        (FerryDir.N, 4),
        (FerryDir.E, 2),
        (FerryDir.R, 180),
        (FerryDir.E, 5),
        (FerryDir.S, 2),
        (FerryDir.W, 4),
        (FerryDir.S, 3),
        (FerryDir.W, 1),
        (FerryDir.F, 4),
        (FerryDir.L, 90),
        (FerryDir.S, 2),
        (FerryDir.W, 4),
        (FerryDir.S, 5),
        (FerryDir.F, 21),
        (FerryDir.L, 180),
        (FerryDir.W, 4),
        (FerryDir.S, 3),
        (FerryDir.L, 90),
        (FerryDir.S, 4),
        (FerryDir.L, 90),
        (FerryDir.E, 1),
        (FerryDir.F, 28),
        (FerryDir.L, 180),
        (FerryDir.S, 3),
        (FerryDir.E, 2),
        (FerryDir.N, 3),
        (FerryDir.L, 180),
        (FerryDir.W, 3),
        (FerryDir.L, 90),
        (FerryDir.F, 99),
        (FerryDir.S, 2),
        (FerryDir.F, 63),
        (FerryDir.E, 2),
        (FerryDir.N, 3),
        (FerryDir.R, 90),
        (FerryDir.E, 3),
        (FerryDir.L, 90),
        (FerryDir.E, 5),
        (FerryDir.L, 90),
        (FerryDir.N, 4),
        (FerryDir.F, 39),
        (FerryDir.R, 180),
        (FerryDir.S, 3),
        (FerryDir.R, 90),
        (FerryDir.N, 3),
        (FerryDir.F, 7),
        (FerryDir.E, 3),
        (FerryDir.S, 2),
        (FerryDir.E, 2),
        (FerryDir.F, 98),
        (FerryDir.S, 1),
        (FerryDir.F, 87),
        (FerryDir.E, 1),
        (FerryDir.S, 3),
        (FerryDir.F, 49),
        (FerryDir.N, 1),
        (FerryDir.W, 2),
        (FerryDir.F, 4),
        (FerryDir.L, 270),
        (FerryDir.F, 91),
        (FerryDir.L, 90),
        (FerryDir.E, 1),
        (FerryDir.S, 4),
        (FerryDir.R, 180),
        (FerryDir.F, 43),
        (FerryDir.S, 3),
        (FerryDir.E, 3),
        (FerryDir.R, 90),
        (FerryDir.F, 46),
        (FerryDir.W, 2),
        (FerryDir.R, 90),
        (FerryDir.W, 5),
        (FerryDir.F, 13),
        (FerryDir.R, 180),
        (FerryDir.F, 52),
        (FerryDir.N, 4),
        (FerryDir.F, 28),
        (FerryDir.N, 3),
        (FerryDir.R, 90),
        (FerryDir.E, 5),
        (FerryDir.S, 3),
        (FerryDir.F, 82),
        (FerryDir.R, 90),
        (FerryDir.W, 3),
        (FerryDir.L, 90),
        (FerryDir.F, 33),
        (FerryDir.S, 5),
        (FerryDir.R, 90),
        (FerryDir.R, 90),
        (FerryDir.S, 5),
        (FerryDir.F, 24),
        (FerryDir.R, 90),
        (FerryDir.N, 4),
        (FerryDir.F, 89),
        (FerryDir.W, 1),
        (FerryDir.S, 4),
        (FerryDir.F, 80),
        (FerryDir.W, 3),
        (FerryDir.L, 270),
        (FerryDir.F, 11),
        (FerryDir.L, 90),
        (FerryDir.W, 2),
        (FerryDir.N, 3),
        (FerryDir.F, 18),
        (FerryDir.R, 90),
        (FerryDir.W, 2),
        (FerryDir.R, 90),
        (FerryDir.E, 1),
        (FerryDir.R, 270),
        (FerryDir.N, 3),
        (FerryDir.R, 180),
        (FerryDir.S, 4),
        (FerryDir.F, 36),
        (FerryDir.S, 3),
        (FerryDir.L, 90),
        (FerryDir.N, 2),
        (FerryDir.L, 90),
        (FerryDir.N, 2),
        (FerryDir.E, 1),
        (FerryDir.F, 48),
        (FerryDir.E, 5),
        (FerryDir.L, 180),
        (FerryDir.S, 3),
        (FerryDir.F, 81),
        (FerryDir.E, 4),
        (FerryDir.L, 90),
        (FerryDir.W, 3),
        (FerryDir.F, 31),
        (FerryDir.E, 5),
        (FerryDir.R, 90),
        (FerryDir.F, 66),
        (FerryDir.S, 4),
        (FerryDir.W, 3),
        (FerryDir.L, 90),
        (FerryDir.E, 3),
        (FerryDir.N, 4),
        (FerryDir.F, 85),
        (FerryDir.L, 90),
        (FerryDir.F, 58),
        (FerryDir.E, 5),
        (FerryDir.L, 90),
        (FerryDir.S, 1),
        (FerryDir.W, 3),
        (FerryDir.F, 79),
        (FerryDir.S, 4),
        (FerryDir.F, 60),
        (FerryDir.N, 2),
        (FerryDir.F, 42),
        (FerryDir.S, 3),
        (FerryDir.W, 3),
        (FerryDir.R, 90),
        (FerryDir.E, 1),
        (FerryDir.N, 1),
        (FerryDir.L, 90),
        (FerryDir.F, 15),
        (FerryDir.E, 4),
        (FerryDir.F, 98),
        (FerryDir.L, 90),
        (FerryDir.R, 90),
        (FerryDir.S, 4),
        (FerryDir.E, 1),
        (FerryDir.F, 19),
        (FerryDir.E, 2),
        (FerryDir.S, 4),
        (FerryDir.R, 90),
        (FerryDir.W, 2),
        (FerryDir.L, 180),
        (FerryDir.N, 3),
        (FerryDir.E, 2),
        (FerryDir.S, 3),
        (FerryDir.F, 34),
        (FerryDir.S, 4),
        (FerryDir.S, 4),
        (FerryDir.L, 180),
        (FerryDir.S, 1),
        (FerryDir.R, 90),
        (FerryDir.S, 4),
        (FerryDir.S, 1),
        (FerryDir.L, 90),
        (FerryDir.E, 3),
        (FerryDir.F, 28),
        (FerryDir.R, 90),
        (FerryDir.W, 1),
        (FerryDir.N, 2),
        (FerryDir.E, 5),
        (FerryDir.F, 48),
        (FerryDir.E, 4),
        (FerryDir.S, 1),
        (FerryDir.W, 2),
        (FerryDir.F, 95),
        (FerryDir.W, 2),
        (FerryDir.N, 2),
        (FerryDir.L, 90),
        (FerryDir.E, 2),
        (FerryDir.L, 90),
        (FerryDir.W, 3),
        (FerryDir.S, 2),
        (FerryDir.L, 270),
        (FerryDir.W, 4),
        (FerryDir.L, 90),
        (FerryDir.N, 4),
        (FerryDir.R, 90),
        (FerryDir.E, 4),
        (FerryDir.R, 270),
        (FerryDir.W, 4),
        (FerryDir.F, 6),
        (FerryDir.W, 2),
        (FerryDir.N, 1),
        (FerryDir.E, 1),
        (FerryDir.F, 19),
        (FerryDir.W, 2),
        (FerryDir.N, 1),
        (FerryDir.F, 54),
        (FerryDir.W, 2),
        (FerryDir.L, 90),
        (FerryDir.S, 1),
        (FerryDir.L, 90),
        (FerryDir.F, 80),
        (FerryDir.E, 1),
        (FerryDir.S, 5),
        (FerryDir.E, 5),
        (FerryDir.F, 80),
        (FerryDir.R, 90),
        (FerryDir.L, 270),
        (FerryDir.E, 4),
        (FerryDir.F, 93),
        (FerryDir.N, 4),
        (FerryDir.E, 5),
        (FerryDir.S, 1),
        (FerryDir.E, 1),
        (FerryDir.R, 90),
        (FerryDir.F, 63),
        (FerryDir.N, 3),
        (FerryDir.R, 90),
        (FerryDir.E, 1),
        (FerryDir.N, 2),
        (FerryDir.L, 90),
        (FerryDir.W, 5),
        (FerryDir.R, 90),
        (FerryDir.R, 270),
        (FerryDir.N, 1),
        (FerryDir.E, 4),
        (FerryDir.L, 180),
        (FerryDir.E, 4),
        (FerryDir.F, 19),
        (FerryDir.L, 90),
        (FerryDir.F, 27),
        (FerryDir.W, 2),
        (FerryDir.S, 2),
        (FerryDir.W, 5),
        (FerryDir.S, 1),
        (FerryDir.F, 54),
        (FerryDir.S, 4),
        (FerryDir.R, 90),
        (FerryDir.F, 85),
        (FerryDir.W, 2),
        (FerryDir.F, 13),
        (FerryDir.R, 90),
        (FerryDir.F, 73),
        (FerryDir.S, 5),
        (FerryDir.E, 2),
        (FerryDir.S, 2),
        (FerryDir.F, 12),
        (FerryDir.W, 5),
        (FerryDir.F, 23),
        (FerryDir.N, 1),
        (FerryDir.E, 1),
        (FerryDir.F, 38),
        (FerryDir.N, 2),
        (FerryDir.W, 2),
        (FerryDir.N, 3),
        (FerryDir.E, 2),
        (FerryDir.L, 270),
        (FerryDir.F, 7),
        (FerryDir.L, 90),
        (FerryDir.S, 3),
        (FerryDir.L, 90),
        (FerryDir.S, 3),
        (FerryDir.F, 86),
        (FerryDir.E, 5),
        (FerryDir.R, 90),
        (FerryDir.E, 1),
        (FerryDir.F, 52),
        (FerryDir.L, 180),
        (FerryDir.S, 4),
        (FerryDir.L, 180),
        (FerryDir.W, 4),
        (FerryDir.F, 41),
        (FerryDir.R, 90),
        (FerryDir.E, 3),
        (FerryDir.F, 70),
        (FerryDir.R, 270),
        (FerryDir.N, 3),
        (FerryDir.F, 32),
        (FerryDir.S, 2),
        (FerryDir.E, 5),
        (FerryDir.R, 180),
        (FerryDir.F, 20),
        (FerryDir.W, 3),
        (FerryDir.F, 54),
        (FerryDir.E, 2),
        (FerryDir.F, 34),
        (FerryDir.F, 61),
        (FerryDir.S, 5),
        (FerryDir.W, 1),
        (FerryDir.L, 90),
        (FerryDir.S, 5),
        (FerryDir.N, 5),
        (FerryDir.W, 2),
        (FerryDir.R, 180),
        (FerryDir.W, 2),
        (FerryDir.L, 90),
        (FerryDir.E, 5),
        (FerryDir.S, 4),
        (FerryDir.L, 90),
        (FerryDir.S, 4),
        (FerryDir.L, 180),
        (FerryDir.F, 84),
        (FerryDir.S, 1),
        (FerryDir.W, 1),
        (FerryDir.L, 90),
        (FerryDir.F, 92),
        (FerryDir.F, 46),
        (FerryDir.N, 1),
        (FerryDir.F, 22),
        (FerryDir.F, 24),
        (FerryDir.L, 90),
        (FerryDir.N, 5),
        (FerryDir.W, 4),
        (FerryDir.R, 270),
        (FerryDir.F, 79),
        (FerryDir.N, 1),
        (FerryDir.W, 1),
        (FerryDir.F, 68),
        (FerryDir.R, 90),
        (FerryDir.W, 5),
        (FerryDir.R, 180),
        (FerryDir.N, 5),
        (FerryDir.L, 90),
        (FerryDir.L, 180),
        (FerryDir.S, 1),
        (FerryDir.W, 4),
        (FerryDir.N, 1),
        (FerryDir.L, 180),
        (FerryDir.S, 1),
        (FerryDir.N, 4),
        (FerryDir.E, 4),
        (FerryDir.R, 90),
        (FerryDir.E, 1),
        (FerryDir.E, 4),
        (FerryDir.F, 58),
        (FerryDir.S, 4),
        (FerryDir.E, 5),
        (FerryDir.F, 49),
        (FerryDir.N, 1),
        (FerryDir.E, 2),
        (FerryDir.S, 4),
        (FerryDir.L, 90),
        (FerryDir.W, 2),
        (FerryDir.F, 67),
        (FerryDir.E, 2),
        (FerryDir.N, 5),
        (FerryDir.W, 1),
        (FerryDir.L, 90),
        (FerryDir.E, 5),
        (FerryDir.F, 82),
        (FerryDir.N, 5),
        (FerryDir.F, 91),
        (FerryDir.W, 5),
        (FerryDir.R, 90),
        (FerryDir.F, 17),
        (FerryDir.W, 5),
        (FerryDir.S, 2),
        (FerryDir.R, 90),
        (FerryDir.N, 2),
        (FerryDir.R, 90),
        (FerryDir.N, 5),
        (FerryDir.E, 4),
        (FerryDir.L, 90),
        (FerryDir.N, 1),
        (FerryDir.F, 26),
        (FerryDir.N, 3),
        (FerryDir.E, 3),
        (FerryDir.F, 19),
        (FerryDir.L, 270),
        (FerryDir.R, 90),
        (FerryDir.E, 3),
        (FerryDir.F, 21),
        (FerryDir.L, 180),
        (FerryDir.S, 4),
        (FerryDir.F, 50),
        (FerryDir.S, 4),
        (FerryDir.W, 2),
        (FerryDir.F, 56),
        (FerryDir.F, 49),
        (FerryDir.N, 2),
        (FerryDir.E, 3),
        (FerryDir.R, 180),
        (FerryDir.E, 4),
        (FerryDir.F, 5),
        (FerryDir.F, 17),
        (FerryDir.E, 2),
        (FerryDir.R, 90),
        (FerryDir.N, 3),
        (FerryDir.F, 96),
        (FerryDir.L, 180),
        (FerryDir.E, 4),
        (FerryDir.F, 64),
        (FerryDir.W, 5),
        (FerryDir.R, 90),
        (FerryDir.W, 5),
        (FerryDir.S, 5),
        (FerryDir.F, 92),
        (FerryDir.E, 5),
        (FerryDir.F, 10),
        (FerryDir.N, 1),
        (FerryDir.W, 1),
        (FerryDir.F, 94),
        (FerryDir.R, 90),
        (FerryDir.W, 4),
        (FerryDir.F, 22),
        (FerryDir.S, 1),
        (FerryDir.W, 4),
        (FerryDir.F, 38),
        (FerryDir.W, 1),
        (FerryDir.F, 17),
        (FerryDir.E, 3),
        (FerryDir.L, 90),
        (FerryDir.F, 3),
        (FerryDir.S, 1),
        (FerryDir.L, 90),
        (FerryDir.F, 27),
        (FerryDir.W, 4),
        (FerryDir.F, 31),
        (FerryDir.S, 5),
        (FerryDir.W, 4),
        (FerryDir.N, 2),
        (FerryDir.E, 5),
        (FerryDir.F, 44),
        (FerryDir.W, 2),
        (FerryDir.E, 4),
        (FerryDir.F, 54),
        (FerryDir.L, 180),
        (FerryDir.E, 5),
        (FerryDir.L, 90),
        (FerryDir.N, 1),
        (FerryDir.E, 5),
        (FerryDir.N, 4),
        (FerryDir.L, 180),
        (FerryDir.L, 270),
        (FerryDir.W, 3),
        (FerryDir.F, 80),
        (FerryDir.S, 2),
        (FerryDir.F, 49),
        (FerryDir.E, 4),
        (FerryDir.F, 46),
        (FerryDir.E, 2),
        (FerryDir.E, 5),
        (FerryDir.L, 270),
        (FerryDir.F, 12),
        (FerryDir.F, 63),
        (FerryDir.L, 90),
        (FerryDir.N, 2),
        (FerryDir.E, 5),
        (FerryDir.N, 3),
        (FerryDir.F, 85),
        (FerryDir.R, 270),
        (FerryDir.S, 3),
        (FerryDir.F, 71),
        (FerryDir.N, 4),
        (FerryDir.E, 5),
        (FerryDir.F, 36),
        (FerryDir.N, 5),
        (FerryDir.F, 23),
        (FerryDir.L, 90),
        (FerryDir.N, 2),
        (FerryDir.E, 3),
        (FerryDir.F, 93),
        (FerryDir.S, 5),
        (FerryDir.F, 1),
        (FerryDir.S, 2),
        (FerryDir.F, 29),
        (FerryDir.L, 90),
        (FerryDir.F, 17),
        (FerryDir.R, 180),
        (FerryDir.S, 4),
        (FerryDir.R, 90),
        (FerryDir.E, 2),
        (FerryDir.S, 3),
        (FerryDir.W, 5),
        (FerryDir.R, 90),
        (FerryDir.S, 3),
        (FerryDir.R, 90),
        (FerryDir.W, 4),
        (FerryDir.F, 62),
        (FerryDir.L, 180),
        (FerryDir.S, 4),
        (FerryDir.L, 90),
        (FerryDir.N, 2),
        (FerryDir.F, 46),
        (FerryDir.N, 3),
        (FerryDir.R, 180),
        (FerryDir.E, 1),
        (FerryDir.R, 90),
        (FerryDir.F, 73),
        (FerryDir.S, 5),
        (FerryDir.F, 12),
        (FerryDir.L, 180),
        (FerryDir.F, 47),
        (FerryDir.L, 90),
        (FerryDir.F, 79),
        (FerryDir.N, 4),
        (FerryDir.R, 270),
        (FerryDir.W, 3),
        (FerryDir.N, 1),
        (FerryDir.W, 1),
        (FerryDir.N, 3),
        (FerryDir.F, 63),
        (FerryDir.S, 2),
        (FerryDir.F, 50),
        (FerryDir.R, 90),
        (FerryDir.F, 30),
        (FerryDir.N, 3),
        (FerryDir.F, 7),
        (FerryDir.N, 4),
        (FerryDir.L, 90),
        (FerryDir.S, 4),
        (FerryDir.N, 1),
        (FerryDir.E, 5),
        (FerryDir.S, 5),
        (FerryDir.F, 9),
        (FerryDir.L, 90),
        (FerryDir.L, 90),
        (FerryDir.F, 7),
        (FerryDir.N, 1),
        (FerryDir.R, 90),
        (FerryDir.F, 52),
        (FerryDir.E, 3),
        (FerryDir.L, 90),
        (FerryDir.N, 3),
        (FerryDir.F, 50),
        (FerryDir.L, 90),
        (FerryDir.F, 83),
        (FerryDir.E, 3),
        (FerryDir.F, 74),
        (FerryDir.L, 90),
        (FerryDir.N, 1),
        (FerryDir.L, 90),
        (FerryDir.F, 4),
        (FerryDir.N, 1),
        (FerryDir.F, 28),
        (FerryDir.E, 4),
        (FerryDir.F, 9),
        (FerryDir.E, 4),
        (FerryDir.S, 2),
        (FerryDir.W, 4),
        (FerryDir.L, 270),
        (FerryDir.S, 1),
        (FerryDir.W, 4),
        (FerryDir.F, 23),
        (FerryDir.E, 1),
        (FerryDir.F, 52),
        (FerryDir.E, 1),
        (FerryDir.L, 180),
        (FerryDir.E, 2),
        (FerryDir.N, 5),
        (FerryDir.L, 90),
        (FerryDir.W, 5),
        (FerryDir.L, 90),
        (FerryDir.S, 1),
        (FerryDir.E, 3),
        (FerryDir.R, 90),
        (FerryDir.E, 4),
        (FerryDir.L, 90),
        (FerryDir.S, 1),
        (FerryDir.W, 2),
        (FerryDir.N, 4),
        (FerryDir.W, 1),
        (FerryDir.S, 4),
        (FerryDir.E, 2),
        (FerryDir.L, 90),
        (FerryDir.E, 5),
        (FerryDir.S, 2),
        (FerryDir.L, 180),
        (FerryDir.F, 91),
        (FerryDir.N, 5),
        (FerryDir.W, 4),
        (FerryDir.N, 5),
        (FerryDir.F, 14),
        (FerryDir.S, 5),
        (FerryDir.R, 90),
        (FerryDir.S, 5),
        (FerryDir.L, 90),
        (FerryDir.F, 78),
        (FerryDir.N, 2),
        (FerryDir.W, 3),
        (FerryDir.R, 90),
        (FerryDir.F, 17),
        (FerryDir.N, 5),
        (FerryDir.W, 1),
        (FerryDir.F, 53),
        (FerryDir.W, 2),
        (FerryDir.F, 33),
        (FerryDir.R, 90),
        (FerryDir.E, 2),
        (FerryDir.F, 15),
        (FerryDir.L, 90),
        (FerryDir.E, 5),
        (FerryDir.F, 77),
        (FerryDir.L, 90),
        (FerryDir.S, 1),
        (FerryDir.F, 33)
    };
}