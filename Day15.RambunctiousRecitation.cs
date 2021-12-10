using System.Collections.Generic;
using System.Linq;

public class Day15
{
    public static int Part1()
    {
        var seq = new List<int> { 15,5,1,4,7,0 };
        
        var number = seq.Last();
        seq.RemoveAt(seq.Count - 1);

        do
        {
            var lastIndex = seq.LastIndexOf(number);
            seq.Add(number);

            number = (lastIndex == -1) 
                ? 0
                : (seq.Count - lastIndex - 1);

        } while (seq.Count < 2020);

        return seq.Last();
    }

    public static int Part2() // Optimized
    {
        var seq = new List<int> { 15,5,1,4,7,0 };

        var index = seq.Count;
        var number = seq.Last();
        seq.RemoveAt(seq.Count - 1);

        var stat = seq
            .Select((val, index) => new {val, index})
            .ToDictionary(i => i.val, i => i.index + 1);

        do
        {
            var nextNumber = stat.TryGetValue(number, out int lastIndex) 
                ? index - lastIndex
                : 0;
            
            stat[number] = index;
            number = nextNumber;
            index++;
            
        } while (index < 30000000);

        return number;
    }
}