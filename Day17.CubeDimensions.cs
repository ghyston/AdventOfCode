using System;

public class Day17
{
    struct Borders
    {
        public Borders(int baseSize)
        {
            xMin = yMin = zMin = -baseSize;
            xMax = yMax = zMax = baseSize;
        }

        public int xMin { get; set; }
        public int xMax { get; set; }
        public int yMin { get; set; }
        public int yMax { get; set; }
        public int zMin { get; set; }
        public int zMax { get; set; }

        public int XRange() => xMax - xMin;
        public int YRange() => yMax - yMin;
        public int ZRange() => zMax - zMin;

        public (
            (int min, int max) x, 
            (int min, int max) y, 
            (int min, int max) z, 
            (int x, int y, int z) delta, 
            (int x, int y, int z) offset)[] RangeCheckers => new [] {
            ((xMin, xMin), (yMin, yMax), (zMin, zMax), (- xMin, 0, 0), (- xMin, 0, 0)),
            ((xMax, xMax), (yMin, yMax), (zMin, zMax), (xMax, 0, 0), (0, 0, 0)),

            ((xMin, xMax), (yMin, yMin), (zMin, zMax), (0, -yMin, 0), (0, -yMin, 0)),
            ((xMin, xMax), (yMax, yMax), (zMin, zMax), (0, yMax, 0), (0, 0, 0)),

            ((xMin, xMax), (yMin, yMax), (zMin, zMin), (0, 0, -zMin), (0, 0, -zMin)),
            ((xMin, xMax), (yMin, yMax), (zMax, zMax), (0, 0, zMax), (0, 0, 0))
        };
    }

    public class Cube
    {
        private Borders _borders;
        private bool[,,] _values;

        public Cube(int initSize)
        {
            var total = initSize * 2 + 1;
            _borders = new Borders(initSize);
            _values = new bool[total, total, total];
        }

        public void ExpandIfNeeded()
        {
            foreach (var range in _borders.RangeCheckers)
            {
                bool ShouldExpand() 
                {
                    for (int ix = range.x.min; ix <= range.x.max; ix++)
                        for (int iy = range.y.min; iy <= range.y.max; iy++)
                            for (int iz = range.z.min; iz <= range.z.max; iz++)
                                if(_values[ix - _borders.xMin, iy - _borders.yMin, iz - _borders.zMin]) return true;
                    
                    return false;
                }

                if(!ShouldExpand())
                    continue;

                Console.WriteLine($"Should expand x: {range.delta.x} y: {range.delta.y} z: {range.delta.z}");

                var newValues = new bool[
                    _borders.XRange() + 1 + range.delta.x, 
                    _borders.YRange() + 1 + range.delta.y, 
                    _borders.ZRange() + 1 + range.delta.z];

                Clone(newValues, 
                    xOffset: range.offset.x,
                    yOffset: range.offset.y,
                    zOffset: range.offset.z);

                _borders.xMin += -range.offset.x;
                _borders.xMax += range.delta.x - range.offset.x;
                _borders.yMin += -range.offset.y;
                _borders.yMax += range.delta.y - range.offset.y;
                _borders.zMin += -range.offset.z;
                _borders.zMax += range.delta.z - range.offset.z;
            }
        }

        private void Clone(bool[,,] newValues, int xOffset = 0, int yOffset = 0, int zOffset = 0)
        {
            for (int ix = 0; ix <= _borders.XRange(); ix++)
                for (int iy = 0; iy <= _borders.YRange(); iy++)
                    for (int iz = 0; iz <= _borders.ZRange(); iz++)
                    {
                        //Console.WriteLine($"ix: {ix} iy: {iy} iz: {iz} xOffset: {xOffset} yOffset: {yOffset} zOffset: {zOffset}");
                        newValues[ix + xOffset, iy + yOffset, iz + zOffset] = _values[ix, iy, iz];
                    }

            _values = newValues;
        }

        public void Set(int x, int y, int z, bool val) => _values[x - _borders.xMin, y - _borders.yMin, z - _borders.zMin] = val;

        public void Print()
        {
            for (int iz = _borders.zMin; iz <= _borders.zMax; iz++)
            {
                Console.WriteLine($"z={iz}");
                for (int iy = _borders.yMin; iy <= _borders.yMax; iy++)
                {
                    for (int ix = _borders.xMin; ix <= _borders.xMax; ix++)
                    {
                        var c = _values[ix - _borders.xMin, iy - _borders.yMin, iz - _borders.zMin];
                        Console.Write(c ? '#' : '.');
                    }
                    Console.WriteLine('\n');
                }
            }
        }

        public int AmountOfActiveNeighbors(int x, int y, int z)
        {
            var sum = 0;

            var x1 = Math.Max(x - 1, _borders.xMin);
            var x2 = Math.Min(x + 1, _borders.xMax);
            var y1 = Math.Max(y - 1, _borders.yMin);
            var y2 = Math.Min(y + 1, _borders.yMax);
            var z1 = Math.Max(z - 1, _borders.zMin);
            var z2 = Math.Min(z + 1, _borders.zMax);

            for (int ix = x1; ix <= x2; ix++)
                for (int iy = y1; iy <= y2; iy++)
                    for (int iz = z1; iz <= z2; iz++)
                    {
                        if (ix == x && iy == y && iz == z)
                            continue;
                        
                        if(_values[ix - _borders.xMin, iy - _borders.yMin, iz - _borders.zMin])
                            sum++;
                    }

            return sum;
        }

        public void DoStep()
        {
            Console.WriteLine("DoStep");

            var newValues = new bool[
                    _borders.XRange() + 1, 
                    _borders.YRange() + 1, 
                    _borders.ZRange() + 1];

            for (int ix = 0; ix <= _borders.XRange(); ix++)
                for (int iy = 0; iy <= _borders.YRange(); iy++)
                    for (int iz = 0; iz <= _borders.ZRange(); iz++)
                    {
                        var activeNeighbors = AmountOfActiveNeighbors(ix + _borders.xMin, iy + _borders.yMin, iz + _borders.zMin);
                        if (_values[ix, iy, iz])
                            newValues[ix, iy, iz] = (activeNeighbors == 2 || activeNeighbors == 3);
                        else
                            newValues[ix, iy, iz] = (activeNeighbors == 3);
                    }
            _values = newValues;
            ExpandIfNeeded();
        }

        public int ActiveCells()
        {
            var sum = 0;
            for (int ix = 0; ix <= _borders.XRange(); ix++)
                for (int iy = 0; iy <= _borders.YRange(); iy++)
                    for (int iz = 0; iz <= _borders.ZRange(); iz++)
                        if (_values[ix, iy, iz])
                            sum++;

            return sum;
        }
    }

    public static int Part1()
    {
        var cube = new Cube(9);

        var input = 
//@".#.
//..#
//###";
@"#...#...
#..#...#
..###..#
.#..##..
####...#
######..
...#..#.
##.#.#.#";




        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        Console.WriteLine($"Lines count: {lines.Length}");
        for (int y = 0; y < lines.Length; y++)
        {
            //Console.WriteLine($"Line #{y}: {lines[y]}");
            for (int x = 0; x < lines[y].Length; x++)
            {
                var c = lines[y][x];
                if(c == '#')
                    cube.Set(x, y, 0, true);
            }
        }

        //cube.Print();

        var stepsAmount = 6;
        for (int i = 0; i < stepsAmount; i++)
        {
            Console.WriteLine($"Active at step {i}: {cube.ActiveCells()}");
            //cube.Print();
            //Console.WriteLine($"Active for (0, 2, 0): {cube.AmountOfActiveNeighbors(0,2,0)}");
            
            cube.DoStep();
            //cube.Print();
        }

        return cube.ActiveCells();
    }
    
}