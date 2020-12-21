using System;
using System.Linq;
using System.Collections.Generic;

public class Day13
{
    public static (int id, long when) NextBusId()
    {
        var busIds = Values
            .Where(v => v != "x")
            .Select(v => Int32.Parse(v));

        // foreach (var item in busIds)
        //     Console.WriteLine($"busId: {item}");

        var nextArrive = busIds
            .Select(id => (id: id, when: -CurrentTimestamp % id + id))
            .OrderBy(v => v.when);

        foreach (var (busId, when) in nextArrive)
            Console.WriteLine($"busId: {busId} in {when}");
        
        return nextArrive.First();
    }

    public static long Part1()
    {
        var next = NextBusId();
        return next.id * next.when;
    }

    public static long Part2()
    {
        int offset = 0;
        var busIds = new List<(int busId, int offset)>();
        foreach (var val in Values)
        {
            if(val != "x")
                busIds.Add((busId: Int32.Parse(val), offset: offset));
            offset++;
        }

        foreach (var (busId, offs) in busIds)
            Console.WriteLine($"busId: {busId} offset {offs}");

        long timestamp = 0;
        long increment = 1;

        foreach (var v in busIds)
        {
            while (true)
            {
                if((timestamp + v.offset) % v.busId == 0)
                {
                    increment *= v.busId;
                    break;
                }
                timestamp += increment;
            }
        }

        return timestamp;
    }

    public static long CurrentTimestamp = 1005595;
    public static List<string> Values = new List<string>{"41","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","37","x","x","x","x","x","557","x","29","x","x","x","x","x","x","x","x","x","x","13","x","x","x","17","x","x","x","x","x","23","x","x","x","x","x","x","x","419","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","19"};
    
    public static List<string> TestValues1 = new List<string>{"7","13","x","x","59","x","31","19"};
    public static List<string> TestValues2 = new List<string>{"17","x","13","19" }; //3417
    public static List<string> TestValues3 = new List<string>{"67","7","59","61"}; //754018
    public static List<string> TestValues4 = new List<string>{"67","x","7","59","61"}; //779210
    public static List<string> TestValues5 = new List<string>{"67","7","x","59","61"}; //1261476
    public static List<string> TestValues6 = new List<string>{"1789","37","47","1889"}; //1202161486
}