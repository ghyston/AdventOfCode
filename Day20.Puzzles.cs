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

	static SideDirection Opposite(SideDirection direction) => direction switch
	{
		SideDirection.Top    => SideDirection.Bottom,
		SideDirection.Bottom    => SideDirection.Top,
		SideDirection.Left    => SideDirection.Right,
		SideDirection.Right    => SideDirection.Left,
		_ => throw new ArgumentOutOfRangeException(nameof(direction), $"Not expected direction value: {direction}"),
	};

	static SideDirection NextClockwise(SideDirection direction) => direction switch
	{
		SideDirection.Top    => SideDirection.Right,
		SideDirection.Bottom    => SideDirection.Left,
		SideDirection.Left    => SideDirection.Top,
		SideDirection.Right    => SideDirection.Bottom,
		_ => throw new ArgumentOutOfRangeException(nameof(direction), $"Not expected direction value: {direction}"),
	};

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
		public char[] CoreValues { get; set; }

		public char ValueWithoutSides(int x, int y, SideDirection top) => top switch
		{
			SideDirection.Top    => CoreValues[y * CoreSize + x],
			SideDirection.Bottom => CoreValues[CoreSize * CoreSize - 1 - x - y * CoreSize],
			SideDirection.Right  => CoreValues[CoreSize - 1 - y + CoreSize * x],
			SideDirection.Left   => CoreValues[CoreSize * (CoreSize - 1) - CoreSize * x + y],
			_ => throw new ArgumentOutOfRangeException(nameof(top), $"Not expected direction value: {top}")
		};

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

		public void CreateCoreValues()
		{
			for (int y = 1; y < SideSize - 1; y++)
				for (int x = 1; x < SideSize - 1; x++)
					CoreValues[(y - 1 ) * CoreSize + x - 1] = Values[y * SideSize + x];
		}

		public int ByDirection(SideDirection direction) => direction switch
		{
			SideDirection.Top    => Top,
			SideDirection.Bottom => Bottom,
			SideDirection.Left   => Left,
			SideDirection.Right  => Right,
			_ => throw new ArgumentOutOfRangeException(nameof(direction), $"Not expected direction value: {direction}"),
		};

		public int ToInt(bool[] values, bool flipped)
		{
			var result = 0;
			for (int i = 0; i < values.Length; i++)
				if(values[flipped ? values.Length - i - 1 : i])
					result += (int)Math.Pow(2, i);

			return result;
		}

		public void Print(SideDirection direction)
		{
			Console.WriteLine($"Tile#{Id}");
			for (int y = 0; y < CoreSize; y++)
			{
				for (int x = 0; x < CoreSize; x++)
				{
					Console.Write(ValueWithoutSides(x, y, direction));
				}
				Console.WriteLine("");
				
			}
		}
	}


	private const int SideSize = 10;
	private const int CoreSize = SideSize - 2;

	public static long PartOne()
	{
		using var lineReader = new StringReader(testInput);
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
			currentTile.CreateCoreValues();
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
					Values = new char[SideSize * SideSize], 
					CoreValues = new char[CoreSize * CoreSize]
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

		
		// Part two:
		var horizontalDirection = SideDirection.Right;
		var verticalDirection = SideDirection.Bottom;

		var lefties = cornerTiles.Where(cornerTileID => {
			var cornerTile = tiles[cornerTileID];
			return
				Matches[cornerTile.ByDirection(horizontalDirection)]
					.Any(t => t.direction == Opposite(horizontalDirection)) &&
				Matches[cornerTile.ByDirection(verticalDirection)]
					.Any(t => t.direction == Opposite(verticalDirection));
		});

		var topLeft = lefties.First();

		List<List<(Tile, SideDirection)>> tileMatrix = new ();
		var next = tiles[topLeft];
		do
		{
			var newLine = new List<(Tile, SideDirection)>();
			do
			{
				//TODO: mistake is here, direction should be TOP, not BOTTOM
				newLine.Add((next, NextClockwise(horizontalDirection)));
				var nextRightMatch = Matches[next.ByDirection(horizontalDirection)]
					.FirstOrDefault(t => t.tileId != next.Id);
				next = nextRightMatch.tileId == 0 
					? null 
					: tiles[nextRightMatch.tileId];
				horizontalDirection = Opposite(nextRightMatch.direction);
			}
			while(next != null);
			tileMatrix.Add(newLine);

			var nextBottomMatch = Matches[newLine.First().Item1.ByDirection(verticalDirection)]
				.FirstOrDefault(t => t.tileId != newLine.First().Item1.Id);
			next = nextBottomMatch.tileId == 0 
				? null 
				: tiles[nextBottomMatch.tileId];
			verticalDirection = Opposite(nextBottomMatch.direction);
			horizontalDirection = NextClockwise(nextBottomMatch.direction);
		}
		while(next != null);

		const int InputSizeSize = 3;
		var image = new char[CoreSize * InputSizeSize * CoreSize * InputSizeSize];
		for (int y = 0; y < tileMatrix.Count; y++)
		{
			for (int x = 0; x < tileMatrix[y].Count ; x++)
			{
				var baseIndex = x * CoreSize + y * CoreSize * CoreSize * InputSizeSize;
				var tile = tileMatrix[y][x].Item1;
				tile.Print(tileMatrix[y][x].Item2);
				var direction = tileMatrix[y][x].Item2;
				//Console.WriteLine($"Base index: {baseIndex}");
				//Console.Write($"{tile.Id} ");
				for (int y1 = 0; y1 < CoreSize; y1++)
					for (int x1 = 0; x1 < CoreSize; x1++)
					{
						var index = baseIndex + x1 + y1 * CoreSize * InputSizeSize;
						var val = tile.ValueWithoutSides(x1, y1, direction);

						//Console.WriteLine($"x: {x1} y: {y1} i: {index} val: {val}");
						image[baseIndex + x1 + y1 * CoreSize * InputSizeSize] = val;

					}

				
			}
			Console.WriteLine("");
		}

		void PrintImage()
		{
			for (int y = 0; y < CoreSize * InputSizeSize; y++)
			{
				for (int x = 0; x < CoreSize * InputSizeSize; x++)
					Console.Write(image[x + y * CoreSize * InputSizeSize]);
				
				Console.WriteLine("");
			}
		}

		PrintImage();

		return 0;

	}

}


//1 2 4 8 16 32 64 128 256 512 
//0 1 2 3 4  5  6  7   8   9   
//. . # # #  .  .  #   #   #
//..###..### 223/115
//4 + 8 + 16 + 128 + 256 + 512
//
//2 + 16 + 32 + 64 + 128 + 256