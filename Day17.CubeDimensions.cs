using System;

public class Day17
{
    struct Borders
    {
        public Borders(int baseSize)
        {
            xMin = yMin = zMin = wMin = -baseSize;
            xMax = yMax = zMax = wMax =baseSize;
        }

        public int xMin { get; set; }
        public int xMax { get; set; }
        public int yMin { get; set; }
        public int yMax { get; set; }
        public int zMin { get; set; }
        public int zMax { get; set; }
        public int wMin { get; set; }
        public int wMax { get; set; }

        public int XRange() => xMax - xMin;
        public int YRange() => yMax - yMin;
        public int ZRange() => zMax - zMin;
        public int WRange() => wMax - wMin;

        public (
            (int min, int max) x, 
            (int min, int max) y, 
            (int min, int max) z, 
            (int min, int max) w, 
            (int x, int y, int z, int w) delta, 
            (int x, int y, int z, int w) offset)[] RangeCheckers => new [] {
            ((xMin, xMin), (yMin, yMax), (zMin, zMax), (wMin, wMax), (- xMin, 0, 0, 0), (- xMin, 0, 0, 0)),
            ((xMax, xMax), (yMin, yMax), (zMin, zMax), (wMin, wMax), (xMax, 0, 0, 0), (0, 0, 0, 0)),
            ((xMin, xMax), (yMin, yMin), (zMin, zMax), (wMin, wMax), (0, -yMin, 0, 0), (0, -yMin, 0, 0)),
            ((xMin, xMax), (yMax, yMax), (zMin, zMax), (wMin, wMax), (0, yMax, 0, 0), (0, 0, 0, 0)),
            ((xMin, xMax), (yMin, yMax), (zMin, zMin), (wMin, wMax), (0, 0, -zMin, 0), (0, 0, -zMin, 0)),
            ((xMin, xMax), (yMin, yMax), (zMax, zMax), (wMin, wMax), (0, 0, zMax, 0), (0, 0, 0, 0)),
            ((xMin, xMax), (yMin, yMax), (zMin, zMax), (wMin, wMin), (0, 0, 0, -wMin), (0, 0, 0, -wMin)),
            ((xMin, xMax), (yMin, yMax), (zMin, zMax), (wMax, wMax), (0, 0, 0, wMax), (0, 0, 0, 0))
        };
    }

    public class HyperCube
    {
        private Borders _borders;
        private bool[,,,] _values;

        public HyperCube(int initSize)
        {
            var total = initSize * 2 + 1;
            _borders = new Borders(initSize);
            _values = new bool[total, total, total, total];
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
                                for (int iw = range.w.min; iw <= range.w.max; iw++)
                                    if(_values[ix - _borders.xMin, iy - _borders.yMin, iz - _borders.zMin, iw - _borders.wMin]) return true;
                    
                    return false;
                }

                if(!ShouldExpand())
                    continue;

                Console.WriteLine($"Should expand x: {range.delta.x} y: {range.delta.y} z: {range.delta.z} w: {range.delta.w}");

                var newValues = new bool[
                    _borders.XRange() + 1 + range.delta.x, 
                    _borders.YRange() + 1 + range.delta.y, 
                    _borders.ZRange() + 1 + range.delta.z,
                    _borders.WRange() + 1 + range.delta.w];

                Clone(newValues, 
                    xOffset: range.offset.x,
                    yOffset: range.offset.y,
                    zOffset: range.offset.z,
                    wOffset: range.offset.w);

                _borders.xMin += -range.offset.x;
                _borders.xMax += range.delta.x - range.offset.x;
                _borders.yMin += -range.offset.y;
                _borders.yMax += range.delta.y - range.offset.y;
                _borders.zMin += -range.offset.z;
                _borders.zMax += range.delta.z - range.offset.z;
                _borders.wMin += -range.offset.w;
                _borders.wMax += range.delta.w - range.offset.w;
            }
        }

        private void Clone(bool[,,,] newValues, int xOffset = 0, int yOffset = 0, int zOffset = 0, int wOffset = 0)
        {
            for (int ix = 0; ix <= _borders.XRange(); ix++)
                for (int iy = 0; iy <= _borders.YRange(); iy++)
                    for (int iz = 0; iz <= _borders.ZRange(); iz++)
                        for (int iw = 0; iw <= _borders.WRange(); iw++)
                        {
                            //Console.WriteLine($"ix: {ix} iy: {iy} iz: {iz} xOffset: {xOffset} yOffset: {yOffset} zOffset: {zOffset}");
                            newValues[ix + xOffset, iy + yOffset, iz + zOffset, iw + wOffset] = _values[ix, iy, iz, iw];
                        }

            _values = newValues;
        }

        public void Set(int x, int y, int z, int w, bool val) => _values[x - _borders.xMin, y - _borders.yMin, z - _borders.zMin, w - _borders.wMin] = val;

        public void Print()
        {
            for (int iz = _borders.zMin; iz <= _borders.zMax; iz++)
            {
                Console.WriteLine($"z={iz}");
                for (int iy = _borders.yMin; iy <= _borders.yMax; iy++)
                {
                    for (int ix = _borders.xMin; ix <= _borders.xMax; ix++)
                    {
                        var c = _values[ix - _borders.xMin, iy - _borders.yMin, iz - _borders.zMin, 0]; //TODO: not full, only for w = 0
                        Console.Write(c ? '#' : '.');
                    }
                    Console.WriteLine('\n');
                }
            }
        }

        public int AmountOfActiveNeighbors(int x, int y, int z, int w)
        {
            var sum = 0;

            var x1 = Math.Max(x - 1, _borders.xMin);
            var x2 = Math.Min(x + 1, _borders.xMax);
            var y1 = Math.Max(y - 1, _borders.yMin);
            var y2 = Math.Min(y + 1, _borders.yMax);
            var z1 = Math.Max(z - 1, _borders.zMin);
            var z2 = Math.Min(z + 1, _borders.zMax);
            var w1 = Math.Max(w - 1, _borders.wMin);
            var w2 = Math.Min(w + 1, _borders.wMax);

            for (int ix = x1; ix <= x2; ix++)
                for (int iy = y1; iy <= y2; iy++)
                    for (int iz = z1; iz <= z2; iz++)
                        for (int iw = w1; iw <= w2; iw++)
                        {
                            if (ix == x && iy == y && iz == z && iw == w)
                                continue;

                            if(_values[ix - _borders.xMin, iy - _borders.yMin, iz - _borders.zMin, iw - _borders.wMin])
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
                    _borders.ZRange() + 1,
                    _borders.WRange() + 1];

            for (int ix = 0; ix <= _borders.XRange(); ix++)
                for (int iy = 0; iy <= _borders.YRange(); iy++)
                    for (int iz = 0; iz <= _borders.ZRange(); iz++)
                        for (int iw = 0; iw <= _borders.WRange(); iw++)
                        {
                            var activeNeighbors = AmountOfActiveNeighbors(ix + _borders.xMin, iy + _borders.yMin, iz + _borders.zMin, iw + _borders.wMin);
                            if (_values[ix, iy, iz, iw])
                                newValues[ix, iy, iz, iw] = (activeNeighbors == 2 || activeNeighbors == 3);
                            else
                                newValues[ix, iy, iz, iw] = (activeNeighbors == 3);
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
                        for (int iw = 0; iw <= _borders.WRange(); iw++)
                            if (_values[ix, iy, iz, iw])
                                sum++;

            return sum;
        }
    }

    public static int Part2()
    {
        var cube = new HyperCube(9);

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
                    cube.Set(x, y, 0, 0, true);
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