using System;
using System.Linq;

//Console.WriteLine($"Day1: {SumTo2020()}");
//Console.WriteLine($"Day2: {RightPasswordsCount()}");
//Console.WriteLine($"Day3 part 1: {TreesOnSlopeCount(3,1)}");
//Console.WriteLine($"Day3 part 2: {TreesOnAllSlopesCount()}");
//Console.WriteLine($"Day4 part 1: {ValidPassports()}");
//Console.WriteLine($"Day5 part 1: {BoardingPasses.HighestID()}");
//Console.WriteLine($"Day5 part 2: {BoardingPasses.MissingId()}");
//Console.WriteLine($"Day6 part 1: {Questions.GetUniqueSum()}");
//Console.WriteLine($"Day6 part 2: {Questions.GetEveryoneSum()}");
//Console.WriteLine($"Day7 part 1: {Day7.CanContainShinyBagCount()}");
//Console.WriteLine($"Day7 part 2: {Day7.ShinyBagCanContain()}");
//Console.WriteLine($"Day8 part 1: {Day8.ValueBeforeInf()}");
//Console.WriteLine($"Day8 part 2: {Day8.BruteforceResult()}");
//Console.WriteLine($"Day9 part 1: {Day9.FindIncorrect()}");
//Console.WriteLine($"Day9 part 2: {Day9.FindIncorrectSum()}");
//Console.WriteLine($"Day10 part 1: {Day10.MultiplyJoints()}");
Console.WriteLine($"Day10 part 2: {Day10.CalculateAllPossibleJoints()}");


static int SumTo2020()
{
    for (int i1 = 0; i1 < Input.Values.Length; i1++)
        for (int i2 = 0; i2 < Input.Values.Length; i2++)
            for (int i3 = 0; i3 < Input.Values.Length; i3++)
            {
                var f = Input.Values[i1];
                var s = Input.Values[i2];
                var t = Input.Values[i3];
                //Console.WriteLine($"Sum {f} and {s}: {f + s}");
                if(f + s + t== 2020)
                    return f * s * t;
            }
    return 0;
}

static int RightPasswordsCount()
{
    var correct = 0;
    foreach (var pass in Passwords.values)
    {
        //Console.WriteLine($"pass: {pass.Value} min: {pass.Min} max: {pass.Max} symb: {pass.Symbol}");
        var minChr = pass.Value[pass.Min-1];
        var maxChr = pass.Value[pass.Max-1];
        //Console.WriteLine($"maxChr: {minChr} maxChr: {maxChr}");
        
        var minIsRight = /*pass.Value.Count() > pass.Min && */pass.Value[pass.Min-1] == pass.Symbol;
        var maxIsRight = /*pass.Value.Count() > pass.Max && */pass.Value[pass.Max-1] == pass.Symbol;
        if(minIsRight ^ maxIsRight)
            correct++;
        /*var count = pass.Value.Count(p => p == pass.Symbol);
        if (count >= pass.Min && count <= pass.Max)
            correct++;*/
        
    }
    return correct;
}

static int TreesOnAllSlopesCount() => 
    TreesOnSlopeCount(1, 1) *
    TreesOnSlopeCount(3, 1) *
    TreesOnSlopeCount(5, 1) *
    TreesOnSlopeCount(7, 1) *
    TreesOnSlopeCount(1, 2);

static int TreesOnSlopeCount(int right, int down)
{
    var trees = 0;
    var width = Slopes.Values.First().Count();

    for (int y = down, x = right; y < Slopes.Values.Count(); y += down, x += right)
    {
        if (x >= width) x -= width;
        if (Slopes.Values[y][x] == '#') trees++;        
    }
    return trees;
}

static int ValidPassports()
    => Passports.Values.Count(p => p.IsValid());