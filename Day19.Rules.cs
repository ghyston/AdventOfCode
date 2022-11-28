

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Day19
{

    private static Dictionary<int, List<List<int>>> parsedRules = new();
    private static Dictionary<int, char> finalRules = new();

    private static Stack<int> UsedRules = new();

    public static IEnumerable<int> DoesFitLine(int ruleNumber, int pos, string input)
    {
        //if(UsedRules.Any(ur => ur == ruleNumber))
        //    return null;

        UsedRules.Push(ruleNumber);

        if(pos > input.Length - 1)
        {
            UsedRules.Pop();
            return null;
        }

        if (finalRules.ContainsKey(ruleNumber))
        {
            UsedRules.Pop();
            return input[pos] == finalRules[ruleNumber] 
                ? new List<int> { 1 } 
                : null;
        }

        var rule = parsedRules[ruleNumber];

        List<(bool allPassed, int charsCount)> CheckSubrules(List<int> subrules, int cursor)
        {
            if(!subrules.Any())
                return new List<(bool, int)> { (true, cursor - pos) };

            var fits = DoesFitLine(subrules.First(), cursor, input);

            if(fits is null)
                return new List<(bool, int)> { (false, cursor - pos) }; //TODO: not sure about second part

            return fits.SelectMany(f => CheckSubrules(subrules.Skip(1).ToList(), cursor + f)).ToList();
        }

        var subrulesResults = rule
            .SelectMany(subrules => CheckSubrules(subrules, pos))
            .Where(t => t.allPassed);

        UsedRules.Pop();
        return subrulesResults.Any() 
            ? subrulesResults.Select(r => r.Item2)
            : null;
    }

    public static int PartTwo()
    {
        // Parse rules
        using var sr = new StringReader(rules);
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var splitted = line.Split(':');
            var number = int.Parse(splitted[0].Trim());
            Console.WriteLine(number);

            var theRest = splitted[1].Trim();
            if ((theRest == "a") || (theRest == "b"))
                finalRules[number] = theRest[0];
            else
            {
                var lineRules = theRest.Split('|');
                var sequences = new List<List<int>>();
                foreach (var lineRule in lineRules)
                {
                    var sequence = lineRule
                        .Trim()
                        .Split(' ')
                        .Select(id => int.Parse(id.Trim()))
                        .ToList();
                    sequences.Add(sequence);
                    //Console.WriteLine(string.Join(',', sequence));
                }
                parsedRules[number] = sequences;
            }
        }


        using var lineReader = new StringReader(messages);

        var counter = 0;
        while ((line = lineReader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            UsedRules.Clear();

            var doesFit = DoesFitLine(0, 0, line);
            if(doesFit != null && doesFit.Any(i => i == line.Length))
            {
                //Console.WriteLine(line);
                counter++;
            }
        }


        return counter;
    }

    private static string testRules = @"
42: 9 14 | 10 1
9: 14 27 | 1 26
10: 23 14 | 28 1
1: a
11: 42 31 | 42 11 31
5: 1 14 | 15 1
19: 14 1 | 14 14
12: 24 14 | 19 1
16: 15 1 | 14 14
31: 14 17 | 1 13
6: 14 14 | 1 14
2: 1 24 | 14 4
0: 8 11
13: 14 3 | 1 12
15: 1 | 14
17: 14 2 | 1 7
23: 25 1 | 22 14
28: 16 1
4: 1 1
20: 14 14 | 1 15
3: 5 14 | 16 1
27: 1 6 | 14 18
14: b
21: 14 1 | 1 14
25: 1 1 | 1 14
22: 14 14
8: 42 | 42 8
26: 14 22 | 1 20
18: 15 15
7: 14 5 | 1 21
24: 14 1";

private static string testMessages = @"
abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa
bbabbbbaabaabba
babbbbaabbbbbabbbbbbaabaaabaaa
aaabbbbbbaaaabaababaabababbabaaabbababababaaa
bbbbbbbaaaabbbbaaabbabaaa
bbbababbbbaaaaaaaabbababaaababaabab
ababaaaaaabaaab
ababaaaaabbbaba
baabbaaaabbaaaababbaababb
abbbbabbbbaaaababbbbbbaaaababb
aaaaabbaabaaaaababaa
aaaabbaaaabbaaa
aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
babaaabbbaaabaababbaabababaaab
aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba
";

    private static string rules = @"
107: 18 | 47
116: 1 18 | 111 47
21: 45 47 | 110 18
20: 2 47 | 76 18
44: 47 47 | 18 107
2: 47 18 | 107 47
113: 51 47 | 35 18
80: 18 22 | 47 44
110: 18 18 | 47 107
1: 22 18 | 112 47
77: 47 7 | 18 113
16: 121 47 | 2 18
66: 91 18 | 58 47
86: 12 18 | 78 47
72: 18 76 | 47 121
46: 47 68 | 18 82
131: 66 18 | 75 47
108: 84 18 | 24 47
27: 22 47 | 110 18
53: 108 18 | 122 47
90: 77 47 | 37 18
6: 18 29 | 47 52
10: 76 18 | 44 47
43: 47 39 | 18 130
106: 18 112 | 47 110
89: 97 47 | 51 18
84: 112 18
91: 74 47 | 110 18
114: 18 47 | 107 18
105: 26 18 | 118 47
18: b
19: 2 47 | 22 18
15: 34 47 | 2 18
111: 110 47 | 22 18
94: 47 95 | 18 57
34: 107 107
60: 47 114 | 18 121
62: 44 47 | 45 18
50: 38 47 | 103 18
61: 18 44 | 47 110
87: 112 107
8: 42 | 42 8
31: 18 49 | 47 25
57: 18 20 | 47 60
103: 18 67 | 47 40
39: 18 45 | 47 22
128: 112 18 | 13 47
5: 45 47 | 44 18
13: 18 18
3: 47 10 | 18 80
11: 42 31 | 42 11 31
124: 22 18
88: 18 18 | 47 18
54: 32 18 | 129 47
63: 81 18 | 86 47
49: 18 132 | 47 105
102: 10 18 | 120 47
93: 18 44 | 47 34
65: 107 2
132: 83 47 | 14 18
125: 18 43 | 47 71
109: 47 22 | 18 76
117: 18 112 | 47 22
59: 47 22 | 18 13
75: 18 109 | 47 27
4: 47 90 | 18 69
48: 18 65 | 47 115
24: 18 2 | 47 44
42: 47 50 | 18 4
96: 106 47 | 16 18
101: 111 47 | 91 18
58: 47 76 | 18 73
79: 47 73 | 18 22
12: 47 34 | 18 88
25: 33 47 | 55 18
130: 18 114 | 47 44
83: 126 18 | 102 47
45: 47 18
9: 41 18 | 62 47
78: 112 18 | 45 47
14: 3 47 | 85 18
97: 47 45 | 18 22
22: 18 47
41: 18 22 | 47 112
112: 18 47 | 18 18
68: 21 18 | 19 47
121: 47 47 | 18 18
119: 22 47
129: 47 5 | 18 39
30: 73 18 | 2 47
98: 34 18 | 45 47
40: 18 70 | 47 9
7: 64 47 | 79 18
115: 121 47 | 44 18
32: 19 47 | 30 18
73: 18 47 | 47 18
35: 18 76 | 47 45
95: 93 18 | 72 47
52: 28 47 | 124 18
126: 59 18 | 92 47
26: 104 18 | 89 47
81: 47 98 | 18 127
0: 8 11
64: 112 47
71: 47 119 | 18 117
74: 47 47
82: 47 120 | 18 17
56: 74 47 | 88 18
123: 88 47 | 76 18
33: 6 18 | 63 47
104: 80 18 | 128 47
99: 98 18 | 23 47
38: 94 18 | 53 47
36: 47 110 | 18 74
37: 47 48 | 18 101
70: 61 47 | 87 18
55: 47 54 | 18 131
23: 2 47 | 112 18
122: 10 47 | 64 18
120: 88 18 | 112 47
28: 18 112 | 47 73
51: 47 74 | 18 110
17: 47 88 | 18 22
127: 18 73 | 47 45
69: 125 18 | 46 47
92: 2 47 | 114 18
29: 117 18 | 56 47
67: 18 96 | 47 116
85: 18 36 | 47 15
100: 18 123 | 47 12
47: a
118: 100 47 | 99 18
76: 47 47 | 47 18
";

    private static string messages = @"bbbbbbbbbaaaabbaababbabbaaabbbabbbbaaabb
babaabbaabbbbaababbaabbabaababba
baabbaaabbabbbbaaabababb
bbbbababbaabbbabbabaaaaaaabbbbbbabbabbbaabbbbbbbbbaaaaab
bbbbbabbbbbbaaabbaaaaaba
baabbabbbaabbbabbaabbabbababaaab
bbabbbbaabbabaaababbbbbbbbbabaab
aaababbaabbaaabbbbbbbabbbaaaabbaabbabbabaababbaa
aabbbabbaaaaaaabbbabbaba
baabaaaabbbababbbbaabbbb
bbbbabababbbbabbbbbbbbba
babbaababbbbababaabaababbbabbbbbaabaaabb
baabbbabaabbabbbaaaaabba
baabbabbbbaabbbabbbaaabb
aaaaabbaababaabaabbabbbbbabbabaabbaaabbb
babbaabaaabbbbbbbbbbbabbaabbaaabbaaaaabbabbaabaabaaabbbaaabaaabb
aaabaaabbaaaaabbabaaaaababbabaaababbaabaabbaaaabaaaabaaaaaaabbaaababbbbb
baaababbbaabaaaaaaaaaaaabbababaababaaaaabaaabaaa
abaaaaabbbaababababaaaba
abbaabbabaaabbabaaaaaabaababbabbbbbbabbaabaabbbb
baabbabaaaaabbabbaaaaaabaababaab
abbaabbabbaabaaaabbaaabbabbaaababaaaaaaa
abaaaaabbbbaabbabbbbbbaaaaaaababbbbbbaaa
aabbaaabaaaaaabababaabab
bbbabbaaaaabaaaaaabbbbaa
abbaaababbaaaabbaabaaabbbaaaabbbbbbbaaaaabbbbabaabaabbab
aaaaaaaaaababbabbbabbaba
abbaaaaabbbabbbaabbaaabbabbbbaabbabbabbbabaababbbbabaaba
babaaaaabaabbaaabbaabaaababababa
bababaabbaaaabbabbbabbbabaabbaaaabbaaababbbbbbaaaaababaa
aabaabbababbbabaaaaaaaabababaaaababbabaabbaaaaabbbbbbaab
baaabbbbaaabbaabaaaaabaaababbaabaaababbabaababbb
abaabbbaaabbabbabaaababbbbabbaaaabaaaabb
abbaababbaaaaabbbbaaabbb
aabbabbabbaaaaaabbbaaabb
baabbbabbabbaababbabbbbabaabbbabbbbabbbaabbabbaababbbbbaabbababbaababaab
abbaabaabaaabbbbbaaabbabababbabbbaabbabbaaabbaaababbbababbbbbaaa
aabbbaabbbbababbbabaabaa
baabbaaaabbbaababaabbabbabbbbbaabababaaabaabbbbaaababbbb
abbabababbbbbabbababbbaaaaaaabbbaabababb
baaabbabbbbaababbbaaaabb
aabaaaaaaaaaaabbbabbbabbaaabbbab
abbaabbababbbbaabaaaabbaababbabbaaabbbbabbbaaabbbbaaaabb
baaabbabbbaabaabbbabbaba
abbbbbaaabababbbbbbabbbb
aaabbabbbbaabbbabbaaabbb
babbbbaaabaaabbaabaaaaabbabbaaab
aaabbabaabbaababaabbbbbbbabaaaba
baaaaabbababbbbabbbbabbbababaaaa
abbaaaabaabbbaabbabbbbab
bbbaabbaabaaaaaaababbbbaabbaaaaabbaaabbabbabbbaa
baabbbabaabaabbaababbabbbbaaaaaaaabbbaabbbabbbaa
babaabbabbbbaabaabababbbaabaababbbbaabbb
aabbbabbbaaaaaababbaaaaaaabbaaabbabbabbaaabbaaba
aaabababababbbabbbaabaabaaababbaabbbaabbbbaaaaba
aaabaaababbaababbbbabbab
baaabbabbaabaaabaaaababbabbbabba
abbbabaaaabbbaaaabaabbabbbaabbbbaaabaabb
abbbbabbabbaababbbbbbbaabaaabbbbabaabababbbaaaabaabbaaba
baabbababbbabaaaaabbbaab
aaaabbaabbaabbbbaabbaaaabbabbabaabaaababbaabaabababbaabbbabbbaabaabbbabbbabaaabb
abaaaaabbabbaaaabbabbbaa
baaabbbbababbbaaaababbabbabbaababbabbbabbbbbaaaa
aaaababbabaaaaabbbaabaabbababbbbaabbaaabbbbaaabbbabbbaaaababbaba
abbaaabbbbbbaaabaaaaaababbbbbaaa
aaaaabbababbaabaabaaaaaababbbaabaaaabbaa
bababaabbbbbaaababbaaaaaaabbbbabaaaaabbbaabbbaaa
bbaabbaaaabaaabbaabbaaaabbabaaaabaabbbab
aabbaaabbaabaaaaaaaababbbabaabbaaaababbb
abbbaabaabbaabbbaaaababa
abbaaabbaabaaaabaababbbb
baabababbbbaaabaaabbabababbbabba
abaaabbaaaabbabaabbaaabbaaaaaaaaabbbaababaababbaaabaaaaaabbbabaa
abbbbbabaabbaaabaaaaaabb
abbaabbaabbbbaabbbaabaababbbbbaaababbbbb
aaabbababaaabbabbbaaabbb
baaababaababaabbbbbaabaa
bbbaababbbaaabbaaaaabbabbabbbbab
bbaabaaaaaababbabbaaaaab
aaabaaabbabbabbabbaaaaba
babbabababbbbabbaabbbaba
ababaabbbaabbaababbaabababbbabbbabbbbaabbababaaababbbabbbbbabbbb
abbbbabbaababbababbbbbaaabababbbaabaaababbbaaabb
babaabbbababaabaabaaabbbbaabbbaa
bbbbabbbbbababaabbbbaaabbaabababbbbbabaabaaaaaaa
aabbabaaaaabaaabaaaaabab
abababaaabbabaaabbaaabbababbbbaaaaaababbbabbbaaa
abbaaaaabbabbbbaaabaabbabbaabaababbaaabaaababaaa
aabbbabbababaabbababaabbaaabbbab
baaaaabbbbbbbbaaabbbbbabaabaababaaaabaab
abaaaaaaaaabbababbabababaabbbabbbbaabbababbbaaab
baabaaaaabbaaaabbbbabaaabbababababbbbbba
baabaaababababaaaabbabaaaabaabababaaaabb
bbaaabbaaaabaaababbbbabbaababaaa
babaabbaabbbbaabbbaaabbabbaabbbbbabbbaaa
bbaaaaaaabbaaaaaababbaaaababbbabbbbaaaabbaabababababbabbbbbbaabbaaababbbbbbbbbab
aabaababbabbaaaaabaaaaabbababbaa
aaabbaabaabaabaabbbaababaabaabaabbaabaabbaaabbaa
abaaabbabbbabbaabbbbbabbabaababbababbaaa
bbabaaaaaababababbababba
aaabbabbbabbbbbbabbaabbbbbbaababbaabaaabababbbbbabbabbaa
aabbbbabbabbabaabababbaa
aabaaabaaaabaaaabbabbbbaaaabbbbb
aaaabbabbbabbbbbbaaababaaaaabbabaaaaabab
baabbbabaaabbabbbbababba
bbbabbbaabbaabaabbabababbbbababbabaaabbababbabbbabbbaaaabbaaababbaababaa
abbaabbbbaaabbabbaaaaaaa
aabaaabaaaaaaaaaaaaaaabb
babbaaaabbabaaaaaaaaabbaabbbbaabbbabbbbbbbbaaaaa
aabbaabbbaaaaabbabbbbbba
baaaaabbaaabaaaaabaabbaa
bbaabaabbaabbbabbabbbaabbbabbbaababaabab
aaabbabababaabbabaaababaaaabbabbbbabbaabbabbbbba
bbaaabbabaaababbabaaaaaabaabbbaa
bbbbbabbabbbbbabbbbababa
abaabbbabaabbabababaabaa
babbabaaaaaababbbaaaaabbababbabbbbbbbabbbbbbabaa
aaaababbabbbbbaabbbbaaabbaaabbbbaabbbbbababbaabababbbaaa
babaaaaaabbbabbbaabaabbabaaabbbbbabababbbaabbbaabbaabbaa
ababbaabbbbbbabbbabaaabbabaabaabbababbbbabbbbaab
abaaaaaaaaaaaabaabbbbaabbababbbbbaaabbaa
bbbaabbabbbabbaaaaabbabbbaaaabbaaabaabbb
aaaaaaabaaabaaabaabbaaabbbabbbabaaaabaaababbbaaa
aabbbbababbaaababababaabaababbba
bbabbbbbbabaabbbbabbababaaaaabaabbaaabaabbbabbabababbaaa
babaabbaabbabbabbabbabaaabbabaab
bbbabbbaabbaaabbaabaaabaaababbaa
abbbabbbababababbaaabbab
abbaaaaababbababaaaaaabaaabaaabb
aaaaaabaabbaaabaaabaabbabbabbaab
aabababaaabaaababaabaabbaababaabbaabaaba
bbababaabaabababaabaaaabbaaabaab
aabbbbabbaaabababbabaabb
bbbbbbaaaabbbbabbabaaaababbbbbba
ababbbbabaabaaaaababbbbaabbaaaabbbabbaaa
abbaaabbaabbaabbaaaababbbaabaabbaabbbbbabababaaabbaaababbbabaaab
aaaaaaabbaaaaabbabaabaabbbbbbbabbbbbaaaabbbabbbb
babbabbaaaaaaaaabbbaaabb
aaabaaaaaaaaaabaabbabaaabaaababbbbbabaaaabbbaaba
aabbaaabbaabbabbbbabbaaa
aabbbaabaabbbbbaabbaaababbaabbbabbbabbbb
bbbabbbabbababababababbbabbababb
aaaaaaabaabbabaaababbbbb
bbabababbbbabbaabaaabbababbaaaaabbaaabaa
bbabbbbaaabbaabbabaabbaa
aaabbaabaabaabbaabbaababaabbbaabaabaabbababbabababaaabbbbbabaababaaaababbbbbbbbabababbba
abababaaabbaabbabbbbaaaa
abaabbbaaabbbabaabababbbabaaaaabbaaaaabaaabbabbbbaaaaaabbabbabab
baabababaabbaabbbbababba
abbaaabbabbaabaabbabbaab
baabbaababaabaababaaabab
abaabaaaaaababbabbabbaba
baaabbbbbbbbbbababaabbab
babaaabbaabaabbaabbabababababbba
aaababbaaabaabaabbbabbbababbbaabaabbbaaa
aabbaaaaababbbbaababbaaa
babaaaabbbabbbbababbbabb
bbbaabbabbbaabbabbabbbaa
babaaaaaabbaabbabbaabaabbbbbbabbbbaaabbabababaaaaaabbbbb
babbaaaabbbbababbaabbbabbaaabaabbaabbbba
bbbbbbaabaaababaababbaabababbbab
abbabbbbaabbbbaaabbbaabbbbabbabb
bbbabaaaaabbaaabbabababb
abbaabababbbbaababbabbabbabbaaaaaaaaabaaaaaabaab
aabbabaabbababaaaaabbababbaabaaabaaaaaaa
bbbbaabbbbbababaabbbabaaaabababb
baabbabbaabaaabaaabaaabababaabab
baabaaaaaabbaaabbbabababbabbbbbbbbbbaaaa
aaaaaaababaabbbabbaabbbaaaaabaababbbaaababbbaabaabbbbbbbababbababaabbaab
abbabbbbbbbbbabbababaaaa
babbbaabaabbbbabbaabbaabbbbabbaabbabbaab
ababaabbbbaabaabaaabbabbbaaaaaaa
baaaaaabaabbaaababbbbabbabaaababbbaaaaba
bbbbabbaaabbbaabbaaaabab
ababbbaaabbabbabbbbabbab
abababaaaaabbabbbbabaaba
aaaaabbabbabbbababbbabba
aabbbbbabbabababaabbaaabaaaaabbbbbbaaaab
bbbabaaabbbabbbaaaabbababbbbbaba
aaabaaababbbaababaabbbaa
aaaababbbbbbbbabaabbaabbabbbabbbbababaaabbbaabbbbabbbbab
aabaabbabaabababbabaabbaaaaaabab
abbaabababaabaaababbbbaaaabbaaba
bbabababbaabaabbababbaaa
babaaaabbaabababbbbaabbaaaabaaabbababbaaaaaabbaabbbaaaaa
bbbabbbababbbbbbbaaaabaa
bbbbabbaababbbaabbabbbbabbabaaaaaabaababababaababbaababbbbbbaaaa
baabbbbaaaabbbabababbaba
aabbbbabbbbbababbbababbbababbaaaabbbbaaa
aabbaaabbbabababbbbabbbabaabbaaababbaaababababba
baabaabbbaabaaababbbbaabbaaaaababababbbabaaabbbbbaababababaaaaaa
bababbbbaabaaaababbaaabbbabaabab
babaabbbaaaabbbaaaaabbbbabbbaaaabbabbaba
abaaaaabbbbabbaabbbbbbabaaabaabb
ababaababbaabaabababbaba
baabaaaabaabbabbbbabaaaababbaaab
aaaabbbabbbbbbabbbabbabb
bbbbbbabbabbaaaaabaaabbabaaababbaabbbaaa
bbbbbabbbbbaababbabababb
aaabbaababababaababaaabbabaaabbb
baabaabbaaaaabbabaabaaaaabaaabab
bbbbbbababbbbaababbabbabbbbaabaa
abaabbabbabaababbbbbbbbabbbbbbaaabbbaabaabbbbbbbbbbbaaab
abaabababaabbabbbbbbabbabaaabaab
bbabbbababbaabbaababbbbaabbaabbbbbaabaabbabaababbbababbbbaababaa
abbaaaaaaaaabbbaabbabaaaabbaaaaaababbabaaaaabaabaabababb
aaaaabbaaaabbbbaaaabbbbaaaababbababbbaabbaababbababaaabaaabaaabbbbaaaaab
aabaabbaaabaaabaaabababb
aaabbabbabaaaaabbbbbababaaaabaaa
baababababbbbabbabbabaaaaabaaabbaaababbb
ababbbbaabbaabbaabbbbaaa
aaaabbabbbabaaaaaabaabbb
bbbbabbaaaaaabbabbbababa
aaaaaabbbabaaababaaabbaaabaabbbabaaababbbbabaabaaababaabaaaaaaaaabbbaaaababaabaa
baabbbababbbbbaabaabaaaaaaabaabb
abbabbabbbbbabbbaaaaabbabaaabbaa
baabaaababaabaabbaabbabbaaaaaaababbabaaaaababbba
baaabbabaaaaabaaabbabbbaaababbba
aabbaaababaabaaababbaabaaababbabbabbbbabbbaaaabb
baabbaaabaabbaababbbbaba
aaaaabaabbaaabbaaabaaaabbaabbaaaaabaaabb
bbaababaabaaabbababababb
bbabbabbbbaaaaaaaaaabaabbbaaabaabbabbabbbbbbabbbabbbaaaabaababbbbabbbbabababbaab
aaabbbbabaabaaaaaaabbbab
baabbbabbbbabbaaabaaabab
bbaaabbabaabababaabbabbabbaaabaa
ababbbaaababaabbbabaaaabbababbbbbaaaaaba
baabbaaaabaababaaabababb
aababbabbbababababbbbbaabaabbaaaaaaabaaabbaaaaba
aaaaaaabbaabaaaaababababbabbaaabaaabaaba
abbaabbbaabbbabbaaabbaaa
baabbaabbbbababbbaaaaabbbbabbabb
aaaaabbababbaaaaabaaabbaabaabbbabaababaa
aabbabbbbbbbabbbbbbbbbba
abababbbaaaaaaabaabbabbaabbbbaaa
abababbbaabaaababbaababaabbabbaa
baabbaabaaaaaaaaaaabaaba
ababbbaabbbbababbbaaaaab
baaabbabaaabaaabaaaabbbbbaaaaaba
bbaaabbabbabaaaabbaaaaaaababaabbbabaaaba
abaabbbaabaaabaaaabbbbbbbaaabaabaababbba
bbbbabaabbbbbaababaaaaabaaabbabbbabababbbaabbaaa
abaabaabaaaaabaababaabbbbbaabaaaaaababbb
aabbbbbaaabbbbabaabaaababbbbbbabbbbaabbbbaabbbaa
abaabbbaabaaaaaaaaaabaaa
baabbbababbbaababbabaaba
babbbaaaaabbbababbababbabbababab
baaabbababbbabbbabbbbbba
abbaaaaaabaabaabaaabbbbaabbababbababbaaa
abbaaaabbaaabbababbaababaaaababbbabbaabb
baabaaabaaabaaabbaabababbaabaaba
ababbbbaabbaaabbbbaababaaaaabbbbabbababb
baabbabbbbabbbbabaabbababbbbababbaaaaaababababba
abaabaaaabbaaaababbaaaabababbbaaababbabaabaabababbbbbaab
abbbbbabbaaaaaabbabaaaaabaaabbbbbaabababaaababaaabbbabab
baabaaabbabbabababbaaabbbabbabbabbbbbabbbabbbaaabaaaabab
babbabaaaaaabbbbabaaaaabaabbbabbabbaaaaaaababbba
babbbabaabbbaaaababaabaa
aaabbabbbabbabaaabbbbabbabbababbabbbabba
abbaaaabaaabaaabbaabbabaaaababbabaaabbbbbababbaaabbabaab
aabbabaabaaaaabbaabbbaabbabaabab
baaaaabbbbaaaaaaabbbbbabbbbbbbbbaababbbbbbbaabaa
bbababaaabbaabaabbabababbabaabbbabbaaaaaabbbabbb
aaaabbabbbaabaabaaaaaaaaabbbbbaaaaabbbababbbaaaa
abbabbbbbbbbabababaabbab
babaabbabbbbabbabaaabbbbbbababbaaaabbbabbabaaabaabbabaab
bbababaaaabbbabbbbababbb
baabbbababbaabaabbbbababbababbbbabbbabab
baabaaaabbbbbbababbabbaa
babaaaaaaabbbaabbbbbbbaabaabaaabaaaaabbbabaaabbb
aaababbaaaaabbbbbbabbbba
aabbbaabbbabaaaaaabababaabbbabbbbbaaabaabbbaaaba
abbbabbbbabaaabbbbbbababababbbbb
abbbbaabaaaabbbaaaabbbbaaaaaabab
ababbbbaaabaaabaababaaab
aaaaaaabbbbaababbaabababaabababaabbbbabb
aabaaaabbaabaaabbaaaaaabbababbbaababaaaa
aabbabbabaaaaabbaaabbbab
bbbbbabbbbbbbbabbbabbaba
aabaababaaaabbabbbbaabbaabaaabaabbabababbbaabbbbbaaaabaa
babbabbabbbaababaabbbbbbabbaaabaabbabbbaabbbbbbb
aabbaaabaababbababaaabab
abaaabaaaababbabbbbbbbabbbaaaaaaabbabbaaaaabbbbb
babbbbbbbbaabaaabbbbbaba
aababbaaaaabababbababbbaabbbabaa
abbbbbaaabbbbaabaaaabbbbababbbabbbaabbaa
aabbabbaaabaaabaaaaabbabaabbaabbabbbbbabbaabbbba
baabbbabbabbaababbbbabbb
aaaaaaabaabaaaaabbbbbbbbaaabbabbbabaabababababbb
bbabbbabbabaaaabbaabbbabaaaabbbabaabbabbbbabbabbabbbaabbbaabaabaaababaaa
bbaabaabaabbaaaaabaabaaaaabaabbbbababbaa
bbbababbabaaaaabbbbabbab
abbbabbbaabbbaabbbbbabbababaaabbabbbbbaabbaabbbbabbababbabbbbbbb
babbabbaaababbabbbbabbbaaaabaaababbaabaaabababab
bbabaaaabbbabbbaababbabbaababbaa
abbbbabbabbaaaaaaaababaa
bbbababbabbaaabbaabbabbbbbbabbbbaaaabbaa
abbabbbbbbbaababbaabaabbbbabababbaaaaaaabbbbbababbabaabb
aaaaaababaaabbaaabaaaabb
babbababbabbbaaababbbabbbbbabaab
aaabaaabbabaabbbbababaabaabbbbbaaabababb
aabbbbbaabbbbabbbbbbababaaaaabbaabbabbabbaaabbbbbbabbabb
abbabaaababaabbaaabbaaabbaaaababbbbbbbba
bbbbbbabababababbbabbbabbaaaaabbabaaaabaabaaaababababbaa
abbaabaaaabbabbbabaababaababaabbbbbbbaba
abbabbbbbabbaababbbaabaa
abbaababbababaabbbaaabbaababbbaaabaaabaabbaaabbb
bababaabaaaababbbbaaaaaaaaaaabab
bbbbabbbbaabbabababbabbb
abbaaaabaaaaaabaabaaabaaabbbbaaabbabaaba
bababbbbaaaaaababaabbabbbbaabbab
abbaaabbababaabaabaabbab
aabaaaabaabbabbabbbababa
aaaaabbabbbbbbaaaaabbabaaaaaaaaaabbbbabbaabbabbabbabbbaaaaabaabb
bbbbbaaaabababbababbbaabbabbbbaaaabaabaabaabaaababbaabababbabbbb
abaabaabaabbbbbbaaaaabab
aabbabaaababababbbbaabbaabbbbbaabbabbaabaabaaabbbaabbbaa
abaaaaaaabbaababbbbbbabbbbaabbab
babbabaabaaabbbbabbabbaa
aabaaaabababaabaaabbaabbabbbabababbbbbbb
aabbaaaaaaabaaaaabbbbabbbaabbbababbaaababbaaababbbaabbbb
aaabbbbabbbbbbbbbbaaabab
aabbbabbbaabaabbaaaaaababbabbbbbbbbaabbb
bbbababbbababaabaabababb
aabaabaaaaaabbabbaabbaabaaababab
babbbaababaaabbaabbaaabbbbbababa
baabbaabbbaaaaaaaaaabbabbabbbaabbabaaabbbbaabbbbbbbaabaaaabaaabbabbabbaa
abbaababbabaaaaababbbaba
bbabababaaaaabaababaaaba
aaaaaababaaaaabbaabbaaabaaaaababababbaba
babbbbbbaabbaaaabbbbaabb
ababaabbbbaabbbaababbbbb
bbbabbbaaaabbaabbabbbbaa
aabababaabbaaabbabaaaaba
baabaaabaabbabbabbbbbaaa
bbbbbbbbbbbabbaabbabbbbabaabbaab
babbaabaaabaabaaabaabbbabbabbbaa
ababbbbaaaaaabbaabbaaaabbbaababaabbabbabbbbabbabaaaaababbaaabaab
aaaabbabbaaabbabababababbaababaa
aaabaaabbabbaaaaaaabaaaabbbbbbbaaaabaabababbbabbabaaaababbbbabaa
babaaaaaabbaabbbbbababaababababb
bbabbbabbbaabaaaabaaabbabbbbbbba
abaabaaaaabaabaaababaabaababbabbbbababbb
bbaabababbbaababaaabbbbaabaaaaaaabaaabbb
aabaabbaabbbbbaabaaaaaabbaabbababbaaabbaaabbabab
baabaabbaaaaaaabbaaaabab
baaabbababbabbbbaabbabab
aabbbbbbbaabbababaabababbabababbababbaaa
bbabbbbbbaabbabbabaaaaaabbbbaabb
aabbaaaaaabbaaaaaababbbb
bbbbabbababbabaaababbbbaabaaaaba
bbbbbbaaababaabaabbbbaababbbbbabbababbaa
aaaabbbaaababababbbaaabaabaaabbbbabaaaabaabbbaaa
bbabbbabbbbbbbbbababaabaabbbabaabbaababb
aababababbabaaaaababaaaa
babbaabbbaaaababbbaabaaababbabaaaabaabaaabbaabbabbabbbbbabbbbbab
aaabbabbaaaaabbaaabbbaabaaabaabb
abbbaababbbababbaabbbbbbababbbab
bbbabaabbaaaaaabbbbbbaababbaaabaabbabbbbaaaabbaa
aaabaaabababbaabababaaab
bbbbaabaaabbbaabaababbabaabaaaabaaaabbaaaabaaabb
aaaaaaaababaaaaababbbaabaaaaabbababbbbba
abbababbbbbaaaababbaaababaaabaababbabaaabbbbbabaababbabbaaabbabaabaabaab
aaabaaaabbabaaaaababaabaabbaaaaaaababbabbababaaa
baabbabbabababababbabbbbbabbabbb
abaaabbaabbbaababbbbaaabbbbbbabbaabaababbbbaaaba
ababbaabaabbabbababaaaabbaaaabbabbaabaababbbaabbbabbbbabbbbbaabb
baabbbabaabaabbaabbbbaaa
abababaaaaabaaaabbbbbabbbbaababb
ababababbaaabbabababaabbabbaabbbbaaabbababbbbbbb
abaaabbabbaabbbabbaabababbaaabbb
babbabaabaaababababbbbaabaaaaaabababbbaabaabaaba
babaaaaaababaabbabaabaabababaabbbbbbabbbaabbaababaababbaaababbbbbbbababa
baabaabbaabaaabaaabbaabbabbabbabbbaaaaaaabbbabab
bbbaabbababbaaaabbaaabbb
aabaabaabbbbaaababbabaaaababbabbbbbabbbaabaaabbbbbaaabbbaababaab
aaababbbbaababbbbabbaabbabbabaababaababb
bbababaabbbbabbaaabbabbbababababaabbaaabababaaaa
bababaababbaaabbaaaabbaa
abbbaabaaabaabbbaabbbababbbbabbaabaabaaaaabbbabaaaaaabbb
aabaabbabbabbbbbbbaaaaab
bbbabbabbbbaabbbabbbabab
bbaaabbaabaaaaaabbaaabbb
aaaaabaabbabaaaababbbabb
aaaaaaaaabbabbbbbbabbbbbaababbaa
bbbbbbabbbbbabbbabaaabab
abaabbbaabaababaaaaaaaababaaaababbbbaaaa
babbbaabbabaaabbbaabbaabbbbabaaaaaabaaaabbbbababbbaabbbbbbbbbaab
aabbbaabaaaabbbbaaababbabababaabbbbbbbbbbbbababbbbbbaaaaabaaabbbaababaaabbaaaaabbbbaabaa
aaaababbaabbabaaabbabbbaaabbaabbbbabbbbaabbabbaa
bbaabbaabbbaaaaaaabaaabb
babaabbbababaabaabaaaaaaababbabbababaabbabbbbaba
aaabbabababaaaaabbabaaaaaaaaaaabbbbbaabaaaabbbabbbbbbaab
aaaaaaaaababbaabaababbaa
bbabaaaabbaaaaaaaabbbbbbaabaaaaa
abbabaaaabababaabaabababbaaabaab
abababbbaababbabbbbbbbbbabbbbabbaababbabbbaabbabbbabbbaabababbab
babbbbaaabaabbbaaaabbaaa
bbbbbbaabbaabaaaabbaaabbbbbabbbbbbbabaab
babbaabaabbabbabbaababaa
abbaabbabaabbaaaabaaaabb
babaaaabbabbbbbbaaaabbaabbbabbababbbbaba";
};