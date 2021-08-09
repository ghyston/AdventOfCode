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
}