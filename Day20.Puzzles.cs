using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class Day20
{
    enum SideDirection
    {
        Top,
        Right,
        Bottom,
        Left
    }

    // hash - lists of where it is found
    static Dictionary<int, List<(int tileId, bool flipped, SideDirection direction)>> Matches = new();

    private class Tile
    {
        public int Id { get; set; }

        public int Left { get; set; }
        public int LeftFlipped { get; set; }

        public int Right { get; set; }
        public int RightFlipped { get; set; }

        public int Top { get; set; }
        public int TopFlipped { get; set; }

        public int Bottom { get; set; }
        public int BottomFlipped { get; set; }

        public char[] Values { get; set; }

        public void PrepareSides()
        {
            var left = new bool[SideSize];
            var right = new bool[SideSize];
            var top = new bool[SideSize];
            var bottom = new bool[SideSize];

            for (int i = 0; i < SideSize; i++)
            {
                top[i] = Values[i] == '#';
                left[i] = Values[i * SideSize] == '#';
                right[i] = Values[(i + 1) * SideSize - 1] == '#';
                bottom[i] = Values[(SideSize - 1) * SideSize + i] == '#';
            }

            Top = ToInt(top, false);
            TopFlipped = ToInt(top, true);

            Bottom = ToInt(bottom, false);
            BottomFlipped = ToInt(bottom, true);

            Left = ToInt(left, false);
            LeftFlipped = ToInt(left, true);

            Right = ToInt(right, false);
            RightFlipped = ToInt(right, true);
        }

        public int ToInt(bool[] values, bool flipped)
        {
            var result = 0;
            for (int i = 0; i < values.Length; i++)
                if(values[flipped ? values.Length - i - 1 : i])
                    result += (int)Math.Pow(2, i);

            return result;
        }
    }


    private const int SideSize = 10;

    public static long PartOne()
    {
        using var lineReader = new StringReader(actualInput);
        string line;

        Dictionary<int, Tile> tiles = new();
        Tile currentTile = null;
        int lineNumber = 0;

        void AddMatch(int hash, int tileId, bool flipped, SideDirection direction)
        {
            if(!Matches.ContainsKey(hash))
                Matches[hash] = new List<(int tileId, bool flipped, SideDirection direction)>();
            Matches[hash].Add((tileId, flipped, direction));
        }

        void Flush()
        {
            currentTile.PrepareSides();
            tiles[currentTile.Id] = currentTile;

            AddMatch(currentTile.Bottom, currentTile.Id, false, SideDirection.Bottom);
            AddMatch(currentTile.BottomFlipped, currentTile.Id, true, SideDirection.Bottom);

            AddMatch(currentTile.Top, currentTile.Id, false, SideDirection.Top);
            AddMatch(currentTile.TopFlipped, currentTile.Id, true, SideDirection.Top);

            AddMatch(currentTile.Left, currentTile.Id, false, SideDirection.Left);
            AddMatch(currentTile.LeftFlipped, currentTile.Id, true, SideDirection.Left);

            AddMatch(currentTile.Right, currentTile.Id, false, SideDirection.Right);
            AddMatch(currentTile.RightFlipped, currentTile.Id, true, SideDirection.Right);
        }

        while ((line = lineReader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if(line.StartsWith("Tile "))
            {
                if(currentTile != null)
                    Flush();

                currentTile = new Tile()
                {
                    Id = Int32.Parse(line.Substring(5, 4)),
                    Values = new char[SideSize * SideSize]
                };
                lineNumber = 0;
                continue;
            }

            for (int i = 0; i < SideSize; i++)
                currentTile.Values[lineNumber * SideSize + i] = line[i];
            lineNumber++;
        }
        Flush();

        var ordered = Matches.OrderBy(m => m.Key);

        var zeros = ordered.Where(o => o.Value.Count == 0).Count();
        var ones = ordered.Where(o => o.Value.Count == 1).Count();
        var twos = ordered.Where(o => o.Value.Count == 2).Count();
        var moress = ordered.Where(o => o.Value.Count > 2).Count();
        
        
        var oneMatches = ordered.Where(o => o.Value.Count == 1); // matches, that have a pair (on edge)
        var flattered = oneMatches.SelectMany(o => o.Value);
        var tileIds = flattered.Select(o => o.tileId); // We need only tile ids and count
        var grouped = tileIds.GroupBy(tileId => tileId);  // corner tile have 4 matches: 2 edge sides, both flipped. Edge tiles have only 2, one flipped, one not

        var cornerTiles = grouped
            .Where(x => x.Count() == 4)
            .Select(c => c.Key);

        long result = 1;
        foreach (var tileId in cornerTiles)
            result *= tileId;

        return result;
    }

}

//1 2 4 8 16 32 64 128 256 512 
//0 1 2 3 4  5  6  7   8   9   
//. . # # #  .  .  #   #   #
//..###..### 223/115
//4 + 8 + 16 + 128 + 256 + 512
//
//2 + 16 + 32 + 64 + 128 + 256