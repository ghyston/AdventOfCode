using System;
using System.Linq;
using System.Collections.Generic;

public class Day10
{
    public static int MultiplyJoints()
    {
        CalculateJolts(out int one, out int three);
        //Console.WriteLine($"one: {one} two: {three}");
        return one * (three + 1);
    }

    public static long CalculateAllPossibleJoints()
    {
        var orderedDesc = new List<int>(Values)
            .OrderByDescending(v => v);

        var ways = new Dictionary<int, long>(); // key is value
        ways[orderedDesc.First()] = 1;

        foreach (var joint in orderedDesc.Skip(1))
        {
            long result = 0;

            for (int i = 1; i <= 3; i++)
                if(ways.TryGetValue(joint + i, out long val))
                    result += val;

            Console.WriteLine($"ways[{joint}] is {result}");
            ways[joint] = result;
        }

        return ways[orderedDesc.Last()];
    }

    private static void CalculateJolts(out int one, out int three)
    {
        one = 0;
        three = 0;
        var last = 0;
        var ordered = new List<int>(Values).OrderBy(v => v);
        foreach (var joint in ordered)
        {
            Console.WriteLine($"joint: {joint} one: {one} two: {three}");

            switch(joint - last)
            {
                case 1: one++; break;
                case 3: three++; break;
                default: Console.WriteLine("WRONG JOLTAGE!"); break;
            }

            last = joint;
        }
    }

    public static int[] Values = new [] {
        0,
        56,
        139,
        42,
        28,
        3,
        87,
        142,
        57,
        147,
        6,
        117,
        95,
        2,
        112,
        107,
        54,
        146,
        104,
        40,
        26,
        136,
        127,
        111,
        47,
        8,
        24,
        13,
        92,
        18,
        130,
        141,
        37,
        81,
        148,
        31,
        62,
        50,
        80,
        91,
        33,
        77,
        1,
        96,
        100,
        9,
        120,
        27,
        97,
        60,
        102,
        25,
        83,
        55,
        118,
        19,
        113,
        49,
        133,
        14,
        119,
        88,
        124,
        110,
        145,
        65,
        21,
        7,
        74,
        72,
        61,
        103,
        20,
        41,
        53,
        32,
        44,
        10,
        34,
        121,
        114,
        67,
        69,
        66,
        82,
        101,
        68,
        84,
        48,
        73,
        17,
        43,
        140
    };
}