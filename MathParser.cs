using System;
using System.Collections.Generic;
using System.Linq;

public class Day18
{
    public static long Part2()
    {
        long total = 0;
        foreach (var expressionString in Homework)
        {
            var expression = new Expression(expressionString);
            var result = expression.Calculate2();
            //Console.WriteLine($"{expressionString} = {result}");
            total += result;
        }

        return total;
    }

    public static long Part1()
    {
        //var testString = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
        //var exp = new Expression(testString);
        //var result = exp.Calculate();
        
        long total = 0;
        foreach (var expressionString in Homework)
        {
            var expression = new Expression(expressionString);
            var result = expression.Calculate();
            Console.WriteLine($"{expressionString} = {result}");
            total += result;
        }

        return total;
    }

    private enum Operation
    {
        SUM,
        PRODUCT
    }

    private class Expression
    {
        private Expression first;
        private List<(Operation, Expression)> _operations = new List<(Operation, Expression)>();
        private long? value;

        public Expression(long val)
        {
            value = val;
        }

        public Expression(string stringToParse)
        {
            var trimmed = stringToParse.Replace(" ", string.Empty);
            int pos = 0;

            Expression getExpression()
            {
                int deepness = 0;
                int tempPos = pos;
                do
                {
                    if(trimmed[tempPos] =='(') deepness++;
                    if(trimmed[tempPos] ==')') deepness--;
                    tempPos++;
                }
                while(deepness > 0);

                if(tempPos == pos + 1)
                    return new Expression(getLongVal());

                var subExpressionString = trimmed.Substring(pos + 1, tempPos - pos - 2);
                pos = tempPos;

                return new Expression(subExpressionString);
            }

            long getLongVal()
            {
                var start = pos;
                while (pos < trimmed.Length && Char.IsDigit(trimmed[pos]))
                    pos++;
            
                var valToParse = trimmed.Substring(start, pos - start);
                return long.Parse(valToParse);
            }

            Operation getOperation()
            {
                var operatorToParse = trimmed.Substring(pos, 1);
                if(operatorToParse == "+") return Operation.SUM;
                if(operatorToParse == "*") return Operation.PRODUCT;

                throw new Exception("Operator was not defined");
            }
            
            first = getExpression();

            while(pos < trimmed.Length)
            {
                var operation = getOperation();
                pos++;
                var exp = getExpression();
                _operations.Add((operation, exp));
            }
        }

        public long Calculate() 
        {
            if(value is not null)
                return value.Value;

            var result = first.Calculate();

            foreach (var (operation, operand) in _operations)
            {
                switch (operation)
                {
                    case Operation.SUM: result += operand.Calculate(); break;
                    case Operation.PRODUCT: result *= operand.Calculate(); break;
                }
            }

            return result;
        }

        public long Calculate2() //with product as a priority
        {
            if(value is not null)
                return value.Value;

            var products = new List<long>();
            var prevProduct = first.Calculate2();
            foreach (var (operation, operand) in _operations)
            {
                if(operation == Operation.PRODUCT)
                {
                    products.Add(prevProduct);
                    prevProduct = operand.Calculate2();
                }
                else if(operation == Operation.SUM)
                {
                    prevProduct += operand.Calculate2();
                }
            }
            products.Add(prevProduct);

            long result = 1;
            foreach (var prod in products)
                result *= prod;
            
            return result;
        }
    }

    private static List<string> Homework = new List<string>()
    {
"6 * ((5 * 3 * 2 + 9 * 4) * (8 * 8 + 2 * 3) * 5 * 8) * 2 + (4 + 9 * 5 * 5 + 8) * 4",
"2 + (3 + 3 + (9 + 3 * 4 * 9) + 2 + 5 * 7) * 7 * (3 * 6 * 5 * 9 + 6) + 6",
"3 * (7 * 7 + 5 * 2) + 7 * 8 * 9 * 6",
"9 + 3 * (3 + 3 * 2 + 4) * 2 * (5 + 9 * 9 * (2 + 5 * 2 * 4) * 6)",
"2 * 3 * (2 + 7 + 3 + 7) + 3 + 7",
"(4 + (4 + 7 * 6 * 5) + 6) + (3 * 2 + 2) + 3 + (8 * 5 * 6) + 9",
"(2 + 3 + (8 + 9 + 4 * 8) * (5 * 3 * 7 + 9 * 5 * 8) + 6) * 9 + 6",
"(4 + (6 * 5) + 4 + 7 + 7) + (2 + 3) * 2",
"2 + 9 + 7 + (8 + 7 + 2 * 4 * 8 + 2) + (3 + 5 * 5 * (7 * 7 * 2 * 7)) + ((6 * 6 + 7 + 9 * 9 * 7) + 4 + 3)",
"7 + ((8 + 4) + (4 + 9 + 9) + 3 + 3) + 7",
"9 + 9 * 6 + 6",
"(7 * 7 * 7 * 3 + 2) * 5 * 7",
"3 + 6 + ((9 * 6 * 7 + 9 + 8 * 6) * 5 * 5 * (8 * 6 * 9 + 6)) * 8 * 9 + 8",
"4 + (7 * 7 + 9 * (5 + 4 * 3 * 4 + 6 + 7) * 6 * 2)",
"3 + 5 * (4 * 8) * 9 * (2 * 5 * (5 + 9 + 5 * 8 + 6 + 6)) * 5",
"3 + (8 + 2 * 6 * 7 + 5) + (2 * 2 + 5 * 8) + 6 * 9",
"((9 * 5 + 9 + 5 + 8 * 2) * 5) + (7 + 6 * 2 + (5 * 9 * 9) * 3) * 5 * 4 * 9",
"9 * 9 * (3 + 6 + 4 * (3 + 3 + 7 * 6 + 4 * 6)) + 2",
"4 + 5",
"8 * (3 + 3) * 9 * ((9 + 2 * 2) * 3 * 8 + 7) + 7 * 3",
"(5 * 7 + 5 * 9 + 7) * 5 + 5 * 3 * 8 * 6",
"(6 + 7 + 6 + 8 * (6 * 8)) * 8",
"4 + 3 + (7 * 7 * 2)",
"(8 * 7 * (7 * 2 * 2 * 8)) + 4",
"4 * (4 + 6 + 7 * 2 * (6 + 6 + 3 + 6 * 2 * 5)) * 9 + 7 + 6 + 8",
"9 * 8 + (7 * (8 * 7 * 4 + 8 * 6 + 2) + 7 * 9) + 8 + 2",
"(9 + 7 * 8 * 8 * (7 * 4 * 7 * 2 * 6 + 2) + 6) * (2 + (2 + 2 + 2 * 5 + 5 * 9) + 5 + 8 + 6)",
"4 + 2 * 7 * (9 + 8 + 3 + 9 * 4 + (8 + 7 * 6 + 7)) + 4",
"7 * 9 * 8 * (5 * 4 + 8 + 2 + 2 + 4) * 5",
"4 * 4 * (5 * (6 * 2 + 9)) + 5 * 3",
"2 + 2 * (5 + (9 * 7 * 8 * 9 * 8 * 2) * 6 * (4 + 6 + 6 + 6)) + (3 + 9 + 6 * 2)",
"2 + ((3 * 4) + 2) * 9 * 4 + 4 * 3",
"(4 + 2 + (5 * 6 * 2 + 7 + 8 * 2)) * 6 * 8 * (2 + 3 * 2 * 8)",
"9 * 2 + (5 * 7 + (3 * 9 + 2 + 2 + 5)) + 8",
"4 * 9 * 7 + ((5 * 4 + 6 * 3 + 3) * (8 * 6 * 3 + 9 * 8 + 7))",
"4 + (9 * 7 + 4 + 6 + 5 * 2)",
"7 + 4 * 9 * ((8 + 8 + 8 * 6) * (5 + 2 * 2 * 5 + 8 * 5) + 5 + (8 + 4 * 2 * 5 + 6) + 6)",
"(7 + (2 + 7 + 8 + 4 + 9 + 4) + 9 * 4 + 5 * 5) + 6 + ((7 + 7 * 5 + 3 * 3 + 3) * 6)",
"((4 + 7 * 9 + 5 * 4 + 8) * (7 * 9 * 3) + 7) + (5 * 2)",
"9 * (8 * 8 * 2) * 2 * 5 * 4 + 6",
"9 + ((9 + 5 + 9) + 2 + 4 + 8) * 3",
"3 + (2 + 5)",
"9 + (2 + 4 + 4 * 9 + (7 * 2 + 3 * 8 + 3 * 2) + (4 * 6 * 9 + 7)) + 7",
"(7 + 6) * (4 * 5 * 4)",
"(9 * 2 * 7 + (7 * 3 + 4 + 6 * 3 + 9) + (9 + 6)) + 4 + 8",
"5 * ((6 * 2) * (9 + 7 + 7 + 2)) * (3 + 9 * 6 + 2 * 3) + 4 + 7",
"6 + (9 + (8 + 6 * 4 + 7)) + 2 * 8 * 7",
"4 + (8 + 4 * 5 * (2 + 2 * 4) + 7) * 2",
"3 * 5 * 2 + 4 * 6 + 9",
"(7 + (2 * 7 * 4 + 5)) * 4",
"6 + 5 + ((7 + 6 * 4 + 7 + 5 * 5) * 5) * 8",
"7 * 3 + 4 + 9 * (7 + (9 * 5 * 7 * 3 + 8 * 2)) + 6",
"(6 * (2 + 8 * 2 + 2) * 2 * (5 * 7)) + 8 + (4 + 4 * 7 + 8 + 8 + 9) * (6 + 7)",
"6 * (2 * 6 + 6 * 3 + 5 + 8) + 6 + 3",
"8 * (8 * 3 + 9 * 7) * 7",
"6 * 7 + 8 * (8 * 9 + (5 + 8 * 5 + 6) * 6 * 2) + 7 * 3",
"((9 * 4 * 4) + 4 * 2) + 3 + (9 + 3 + 8) * 3 * 2",
"7 + (2 * 4 * (2 * 6 + 8) * 8) + 8 * 5",
"6 * 4 * 5 * 8 * (9 * 8)",
"6 * (6 * 3 * 9 * (2 + 3 + 2))",
"3 * 9 + 6 + 5",
"2 * (7 + 8 + 9 * 9 * 7) + 4 * 6 * ((8 * 3 * 2 * 3 + 9 + 9) + 3 * (8 * 5 + 3 * 5 + 7 + 9))",
"6 + 9 * 7 * 8 + (9 + 5 + 8 * 4 + 4 * 8)",
"6 * 5 + 2 + (4 + (8 + 7 * 6 * 9 * 2 * 3) * (8 * 6 * 4) + 5 * (7 * 8) * 2) * 9 * 2",
"4 + (3 + 8 * (2 * 8 + 9 * 6) * 6)",
"((3 + 7) * 9 * 9 + 9) * 7",
"2 * 9 * (4 * 9 * (9 * 8 + 6 * 5) + 3)",
"7 + 4 + 3 + 7 + (8 + 4 * 8 + 8 + 8) * 8",
"6 * (7 * 6 * (2 + 8 + 9 + 4) * (8 * 2) * 4 + (7 * 9 * 3 * 3 + 2 * 6)) * 6 + 8 + 5",
"6 * 6 + 8 + 7",
"3 * ((8 + 6) * 4 * (8 + 7 * 7 * 7) + 8 * 7 * 5) * ((9 + 9 + 3 + 7 + 4) * 7 + 4)",
"4 * 4 + 5 + 5 + 7",
"4 + (6 + 4 + 9)",
"7 * 3 + (5 * 6 + (5 * 3 + 7 + 2)) * 9",
"4 * ((2 * 4) * 6 + 8) + 6 * 5 + (8 * 9)",
"2 * (3 * 4) + 2 + 2",
"(2 * 4 * 5 + 2 + 4 * 5) + 5 + 3 * 6",
"9 * 2 * 6 + 3 + (3 + 8) * (6 * 4 + 2)",
"9 * 6 * 4 + (9 * 8 * 5 * 6 + (5 * 6)) + 9 * 7",
"9 + 2 * 6 * (9 + 3 + (5 + 2 + 3 + 8 * 2 * 6) * 9) * 7 + 9",
"8 + (7 * 2 + (3 + 6) * (6 + 6 + 8)) + 8 * 5 + 4",
"3 * (3 * 2 + 5 * 4 + (8 * 3 * 5) + (6 * 5 * 8 + 7 * 4 * 5)) + 6 * 6",
"6 + 7 + 7 + 2 + (2 + (4 * 3) + 3 + 8)",
"((2 + 3 + 2) * 9 + 2 * 4 * 7 + 9) * 8 + 7 * 4 + 3 + 2",
"(5 + (7 + 9 * 3 * 6 + 5 + 8) * 2) * 7",
"3 + 3 * 3 + (6 + 7 * 3 * 2 * 5 * 7) + (4 * 6 * 5 + 3 * (3 + 6 + 5 + 5) + 8)",
"4 * 5 + 8 + (5 + 6) + (4 + 2 * 2 + 4)",
"5 * 6 * 8 * (3 * 2 + 7 + (4 * 8 * 3) * 3 + 3)",
"5 + 8 * 6 * 4 * (2 * 4 + (3 + 6 + 9 * 6 * 2 * 6) * 9 + 3 + 9) + 9",
"(6 + (4 + 5 * 8) + 8 * 9) + 7 * 2",
"(5 + 2 * 3 * 3 * 2 * 8) + 3 + 2 + (7 + 4) + 4",
"(9 * 8 * 2) * 2 + 2",
"8 * 6 + 3",
"6 + 9 * (9 + 2) + (7 + 7 + (5 * 9 * 4) + (2 * 6)) + 5",
"5 + 7 * 7 * (7 + 8 * (7 * 9 + 2 * 6 + 9) + 3) * 6",
"2 + (5 * 3 + 7 * 5 * 3 * 3) + 6",
"4 + 9 * 8 * (4 * 8) + (3 + 5) * (8 * 5 + 6 + 9)",
"(5 * 6 + (7 + 3)) + 3 + 9 + 7 + 7",
"(8 * 5 + (3 + 6 * 2 + 3) * 8 + (7 * 3 + 7 * 4 + 9) + 9) + 7 + (8 + 2 + (7 * 7 * 3) * (4 * 5 + 4 * 9 * 8)) * 5 * (3 + 8 * 4)",
"2 * 5 + (6 + 8 + 8 * 7 * 7 + (7 + 3 + 9 * 6 + 7))",
"2 * 3 * ((7 + 6) + 8 + 6 * (4 * 3 * 7) * 7) + 3 * 9 * (8 + (2 + 9 + 9))",
"8 * 8 * (3 + 8 * 3 * 5 * 5 + 3)",
"(7 * 4 + 7 * 6 * 6 * (2 * 6 + 7)) * 3",
"8 * (3 * (5 + 8 * 8 + 7 + 2)) + 8",
"4 + (7 * (9 * 2 + 2 * 7) * 7 + 4 + 9 * 4) + 3",
"5 + 3 + 5 + 2 * ((9 * 9 + 2 * 3) + 2 * (3 * 8 + 8 * 7 * 6 + 7) + 9 + 5)",
"5 * 5 + 9 * (4 * 9 * 9 * 3) + 9 + (7 + 9 * 8 * 2)",
"7 * 7 + (7 + 2 * (9 + 6 * 3 + 2 + 3) + (2 * 6) + 6) * 2 * 5",
"(7 * (2 + 6 + 2 + 9) * 7) + (8 * 2 + 4 + 6 * 2 + 3) + 3",
"3 + 3 * 6 * ((3 * 5) * (8 * 6 + 4)) + ((9 + 2 + 5) + (4 + 2 + 2 * 3 + 5 + 6) + 7 + 5 + 6) + 5",
"5 * 9 + 9",
"(5 + 3 + (4 + 4) + 7 + 7 * 9) * ((7 + 7) * 5 * 7)",
"7 * ((7 + 5) * 2 * 7) * 7",
"9 * ((6 + 4) * (6 * 5 * 8 + 6 * 2 * 3)) * 8 + 5",
"3 + 2 + 5 * 5 + (8 + (8 * 2 + 9 * 7 * 4)) * 2",
"(6 + 2 * 5 + 3) + 9 * (5 * 6 + (7 * 6 + 8 + 4 + 8) + 8 + 8)",
"(3 + (3 * 4 * 8 * 3 + 6 + 8) + 9 + 8 + (3 * 9 * 9 + 4 * 8 + 9) + (7 + 6 + 9 + 6)) + 8 + 5 + 2 * 7",
"6 * (7 + (2 + 5 + 5 * 6 + 4) * 6 * (2 * 5 * 7 * 4 * 7 * 4)) * 9 * 2",
"3 * (7 * 4 + 7 + 9) + ((4 * 4 * 2 * 8) + 9 + 6 * 9 * (9 + 9 + 2 * 2) * 5) + 5 * 6 + 9",
"6 + (4 * 5 + 8 * 8 * 9)",
"((9 * 2) * 2 * (4 * 8 + 4 * 8) + 6 + 6) * 2",
"(5 * (3 * 2 * 2 + 4 + 4) + 6 * (8 + 6 * 3 + 6)) * (3 * 7 * (5 * 9 * 7 + 5 * 6 * 2) + 6) + 2 + 8 * ((9 + 5 + 4 + 8 * 5 * 4) + 3) * 5",
"8 * 4 + 8 + 6 * ((4 * 3 + 5 * 6 + 5) * 2) * 3",
"3 * (2 * 8 * 8 * 7 * (2 + 7) + 8)",
"(9 * 8 * (4 + 8 * 3 * 7 * 6 + 8)) + 7",
"9 * ((6 * 5 + 6 * 5 * 2 * 2) + (8 + 6 + 3 + 4) + 6)",
"2 * (8 * (5 + 9 * 4 * 5) + 9 * (5 + 5 + 8 * 4) + 8 * 5) + 4 * 3 + 3",
"(9 + 3 + 9) * (7 + 9 + 7 + 4 + (3 + 4)) * 2",
"(9 + 7 + 8 * 5) + 3 * 4 * 2 + 3 * 6",
"(7 + 3 + 3 + 2 * 9) + (6 + 6 + (7 + 4 * 5) + 6 + 3) * 8",
"5 * 7 * 9 + (6 * 9 * 7 * (3 + 4) + 4 + 6)",
"(3 + 4 + (8 + 5 * 6) * 9 * 6) * 6 * (8 + 7 * 8) * 3 + 2 * 5",
"7 + (8 + (3 * 4) + (4 + 9 + 9 * 7 + 4) + (2 * 2 + 7) * (6 * 9)) + (9 * (7 * 4 + 5 + 2 * 3) + 3 + 7) + 8 + 2 + 7",
"(6 + 8 + 4 * 8 + 9 + 2) * 3 + 4",
"3 + 7 * (4 * 3 + 2 + 3)",
"(5 + 8 * (8 + 2 + 3 * 9) + 9 * 7 + 2) * 8 * 7 + 3 + 5",
"(6 * 2 + 4) + 4",
"6 * (2 + (5 + 2 + 9 + 8 * 7 + 6) + 7) + 4 * 7 * 4",
"4 * 6 * ((9 * 2 * 6 * 4 * 2) * 5) + 9",
"(9 * (3 * 2 + 5 + 9 + 3 * 6) + 5 * 5) + 5 * 3 * 6",
"(9 * (3 + 6 + 2 * 4 + 8 + 2) * 8) * 9 * 2 * 3",
"2 + 6 + (7 + 9 + 7 + (6 + 5 * 8 + 3 + 7) + 8) + 3",
"(4 * 6 * 3) + (6 * 9 + 3 + (3 * 8 + 9 * 9 + 5 + 8) * 8 * 7) + 4",
"7 * 9 + ((8 + 8 * 2) * 7 + 5 * 2 * 8) + 8 + 9 * 6",
"3 + ((6 + 9 + 9 * 8 * 5 + 6) * 3) + 4 + (3 + 5 * (6 * 8 * 7 + 5 * 3) * 2 * 7) + (9 * 5)",
"3 + 6 + (5 + 2) * 6 * 8",
"(4 + 4 + (8 * 6 * 5 + 5 + 9 * 9) + (7 * 3) + 9) * 7",
"3 * 5 * 8 * 9 + 4",
"7 * ((3 + 2) * 9 * (4 * 4 + 5 + 7) * 7)",
"7 * 5 + 8 * ((8 + 3 + 5 + 3 + 4) + (6 * 7 + 6) + (3 * 9 * 7 + 6 * 6) * (7 * 7 + 9 + 9 * 9)) * 7 + ((3 * 9 + 7 + 6) * 5)",
"(9 * 5) * 6 + (7 * (3 * 6) * (8 + 6) * 8 + 6)",
"3 * 5 * 2 * 8 * (5 + 6 + 6) + (9 + 2 * 5 + 4 + 4 + 9)",
"(2 * 8 * 5 + 5) * 4",
"7 * (9 * (4 + 4 * 6) * 5) * 5 * 4 + 5 * 7",
"(6 + 2 + 2 + 4) + 4 * 9 * (6 + 3 * 4 * 9) + 2 * 8",
"6 + 9 + 9 + (5 + (5 * 3 + 7 * 9 * 3) * 7 + 8 + 3) + 4",
"(5 * 9 * 7 + 8) + 2 * 7 * 2 * 8",
"4 + 2 * 5 * 2 * 3 + 7",
"5 + 6 * 2 + (3 + 2 + 8 * (3 * 6 * 5 * 2 * 2)) + ((8 * 3) + (2 * 9 + 4 + 9 + 7 * 5) + 4 * 9 + (6 + 7 + 5 + 4) + (9 * 8 + 2 + 9 + 2))",
"((8 * 5 + 6 + 9 * 4 * 6) + 2 + (8 + 9) + 3) + 2 * 2 + (4 + 6 * 9 * 4) + 2 * (3 + 3)",
"2 + (3 + 3) * (2 * (4 + 4 + 2 * 9) + 9 + 9 + 8 * 9)",
"3 + (7 * 4 + 5 * (4 * 9) + (7 * 5 * 4 + 7 * 7 + 6)) + 8",
"5 * 5 * (6 * 3 * 9) * 9 + 6 * 7",
"9 + 5 * (7 * (2 + 8) + 6) + 8",
"3 + 3 * (8 + 2) * 2",
"5 * (8 * 7 + (9 + 4 * 7 + 7 + 2) + (4 + 3 + 5 * 2) * 5 * 5) * 2 * 6",
"4 + (5 * 5 * 2 * (3 * 5 + 5 * 5) * 5) * 7",
"5 + (7 * (9 * 3 + 9 * 3 + 5 * 8) * 9 * 3 * 5 * 7) * 7 + (6 + 4 * 8) + 9 * 2",
"6 * 7 + 4 + (2 + 2 * 4 + 9 * 4 + (5 * 5 + 4 * 5 * 6))",
"2 + (4 + 3 * (6 * 4 + 2 * 4) * 5 + 8 * 6) + (6 * (9 * 4) + 5 + 4 * 7)",
"4 + 6 + 6 + (5 + 3 * (3 + 4 * 8) + 9)",
"7 + 7 + 5 + 8 + ((2 + 2 + 5 + 2 * 5) * 3 * 8 * 9)",
"3 * (5 + 4 + 3) * (4 + 2 * 5 + 8) * 6 + (4 * 9 + 2 + 6) * 5",
"(9 + 7 * 9 * 2 * 7) * 7 + 2 + 4 + 8 * (7 * 9 + 9 + 8)",
"(7 * 7 * 5 + 4 + 3) + 9 * 8 + 9 + 4",
"(2 * 3 * 8 + 6 * 6 * 7) * (7 * 5 * (8 + 6 * 3 * 4 * 7 * 8)) * 3 * (7 * 4 * 7 * 8)",
"((4 * 5 + 4) + 8 * 3 + (9 * 9 * 4)) * (7 * 9)",
"6 * (4 * 9 * 9) * (5 + (5 * 8 * 3 + 3) * 9) * 8 * 3 + 6",
"(4 + 8 + 9 * 7 * 7) * 5 + (2 + 9 + 5 + 5) + 3 * 7",
"2 * 6 + 8 * (2 * 6 * 8)",
"5 + 7 * 2 + 7 + (3 * (7 * 9 + 7) + (9 + 3 * 5 * 9 * 5 + 7) + 8 * 5)",
"8 + 3 * 9 * 6 + 9 * 2",
"5 + (6 + (7 + 5 * 4 * 4) + 6)",
"(3 + 9 + 9 * (6 * 3 * 8 * 9 + 4)) + 2 + 9 * 3 * 8",
"(2 * (9 + 3 * 8 + 9 + 7) + 4 * 6 * 7) + (9 + 4 * (2 * 8 * 2 + 3 + 6) * (5 * 2) + (6 + 4 + 6 + 5 * 3 * 9) + 8) * (8 * 2 + 9 * 6 * 8) + 6",
"4 + (4 + (7 + 9) * (7 * 7 * 6 + 3 * 8) + 6 + 9) * 5 * (5 + 9 + (6 * 8 * 8 * 2 * 2 + 6) + 3 + 6 * (4 + 7 * 7))",
"4 + 8 + 9 + 3",
"9 * 7 + 9 * 3 * ((8 * 7 * 4 * 5) * 6 * (6 + 6 + 4 + 8 + 4 + 6) * 9 * 6 * (2 * 5 * 8)) + 6",
"(3 + 8 * 6 + 8 * 9) + 2",
"7 + 8 + 5 + 3 + 6",
"2 + 4 + 4 * 4",
"((8 * 8 * 9 * 4 * 8) + (4 + 6 + 7 * 2 * 4 + 6) + 9) * 4 * 6 * 7",
"9 * (6 + (3 + 5 + 9 * 6) + 9 * (2 * 2 * 9) * (8 + 5 * 4 + 4 * 2)) * 9 * 6 * 2",
"4 + 9 + 2 + 6 + 5 * (7 * 3 + 7 * 6 * 7 * 4)",
"5 + 9 * 6 + (6 * 6 + 8) * (6 * 3 * 3 * 7 * 5)",
"8 + 9 + (7 + 3 + 4 * 9) + 7",
"2 + ((5 * 9 + 7 + 4 * 6) * 5 * 3) + 5 * 9 * ((5 * 7 * 2) + 4 + 5 + 4) * 5",
"8 + (2 * (4 * 3 + 2 * 7 * 6) + 5 * 6) + (7 + 3 + 8 * 9)",
"3 + (7 * 3 + 6) + (3 * (7 * 5 + 5 * 3 * 7)) + 9 + 6",
"8 * 9",
"3 * 3",
"6 * 7 + 6 + 2 + 6 + (5 + (3 + 4 * 9 * 2 + 9) + 9 + 9 * 3 + 7)",
"4 * ((4 * 3 + 8 * 3 * 7 + 9) + 3) * (5 + 9 * 6 + (6 + 7)) + 2 + 6 * 7",
"(2 * 8 + 9 + 2 + 4 + 6) + ((3 + 3 * 7 + 7 + 2 * 9) * 4 + 9)",
"3 + 4 * 8 + 7 * (3 * 5 + (5 * 6) * 3)",
"5 * ((2 + 9 * 5 + 3 + 8 * 9) * 8 * 2 + 7 + 5 * 5) + 7 + (7 * 6 * 4 * 2 + 5) * 2",
"2 * 3 + 2 * 4 * 6 + 2",
"3 * 4 * 6 + 7 * (4 * (5 * 7 + 8 * 2) * 4 + 5 * 6 + (2 * 2 * 2))",
"(6 + 5 + (8 + 4 + 2 * 5 + 5 + 4)) + ((8 + 8) + 6 + (4 + 6)) + 7 * (3 + 2 + 9 + 2 * 9) + 8",
"2 * 6 * 3 + 6 * (4 + (4 + 4 + 7 + 8 + 9) * 8 * (4 + 8 * 4 + 5 + 9) * 7) * 3",
"8 * 8 + 8 + 3 * 7",
"4 + 9 * ((9 * 3 + 7 * 2) * 8) * (7 + 5 * 9)",
"6 + 2 * (7 + (9 + 3 * 4 + 8 * 2) + 7)",
"4 * 5 + 6 + 6 + 6 * (4 * 8 + 7)",
"(8 * 2) * (4 * 4 * (8 + 3 * 3 + 8 * 8) * 6 * 4)",
"2 + (9 + 4 * 7 + 9 * 2 + 8) + 8 + 5 + (4 * 2)",
"((5 * 3) + 3 * 8 + 9) * (9 * 8) * 3",
"((9 + 4 * 5 + 8 + 5) * (7 * 4 * 9 + 4 + 7 + 8) * 4 + 9) + 4",
"3 * (2 * 2 + 9 * (2 + 5 + 6 + 4) * 4) + 6 + ((8 * 3 + 6 + 9) * 5 + 4) + 6 * (9 + (2 + 3 + 8) + (3 + 7 * 3 * 3 * 4 + 8) + 4)",
"((9 * 2 + 3 * 6) + 2 + 7) + 7 + ((5 + 3 * 4 * 2 + 7 * 9) * 9 + 7 * 2) + (3 + 9 + 5)",
"2 * 3 + 2 * (2 + 7 * 2 + 3) * 6 * 7",
"(9 * (4 * 7 * 8 + 9 * 5 + 6) + 6 * 5 + 9) * (6 + 8 + 6 * 9 + 8 + 9) * 9 + 2 + 2",
"7 + (2 * 4 + 4) * 3 + 8",
"2 * 6 + ((8 * 7 * 2 * 3 * 5 * 3) + 5 + 5 + 4 * (6 * 7 * 9 + 5) * 7) + 2",
"8 + (8 + 6 + 8 * 2) * (2 * 9 + 5 + 2)",
"8 + (8 + (4 + 3 * 6)) * 6 * 6",
"5 + 5 + (4 + 2 * 8) + 9 * 5",
"(8 + 6 * 9 * 7 * 9) * (7 * 3) * 8 + 4 + 6",
"((5 + 4 + 3) * (5 * 4 + 9 * 2 + 9) * 4 * 2 * 3) + 3 + 6",
"(7 + 9 * 5 + 5) + (8 * 3 * 3 * (6 * 3 + 8 * 6 * 9 + 5)) * 5 + (2 * 8 * 2 * 8)",
"6 + (9 * 6 + 7 * (3 * 3 + 2) + 4 * 4)",
"(9 + 4 * (2 * 4) * 5) + 9 * 7 * 7",
"(7 + 6 + 4 + 6 * 8) * 7 * 3 + (4 * 9) + 3 + 5",
"6 + 7 + 4 + 7 * 6 * (3 + 5 * (5 + 4 * 9 + 5 + 6) * 7 + 9)",
"((8 * 5 * 3) + 5 + 3 * 8 + 3) + (7 + 6 * 7 + 6 * (7 * 8 + 4)) + 4 + 8 + 4",
"8 * 5 + (7 * 4 * 2 + 5 + (3 * 6 + 8 + 3) + (5 * 3))",
"7 * 9",
"5 + 6 * 4 * 5 * (2 + 3 + 5 + (4 + 9 + 8 * 8 + 7) * 5 * (7 * 7 * 6 + 5))",
"5 + 7 * (3 + 6) * ((3 + 6 + 6) * (8 + 6 * 6 * 4 + 7 + 2) + (2 + 9 * 2 * 6 * 3 + 9) + 7 + 7) + ((7 + 3 * 3 * 7 * 4) + 2 * 2) * 7",
"6 * 3 * ((8 * 8 + 7 * 8 * 4 + 5) + 9)",
"2 + (7 + (6 + 7 + 9 + 2 * 9 + 2)) + 4 + (4 * 6 * 3) + 3",
"5 * 6 + (2 + 8 + 9 + (6 + 8 + 3) * 2) + 8",
"5 + (4 + 8 + 7 * 7 + 3 + 8) * (4 + 4 * 3 + (9 * 5 * 4 * 9) * 9) * 5 * ((6 * 4) * (4 * 6 + 9 + 9 + 8) * (8 + 3 + 2 + 7 * 9) + 6) + 2",
"3 + (4 + 4 * 6 * 4 + 6 + 3) * (5 + (7 * 6 * 4 * 6 + 9 * 8)) * (9 * 7 + 5 * 2 * 4 + 8) + 9",
"6 + 3 * (3 + 3 * (9 * 4 * 3 * 9 * 7) * 3 + 4) * 3",
"4 * 3 * 3 * 4 * 2 + (2 * 3 * 9 * (9 * 9 * 6) * 6)",
"9 + 4 + (7 + 6 + (3 + 4 * 7 * 3) + (5 * 8 + 9 + 6) * (4 + 8 * 4 + 6) * 8) + 8 * (2 + (5 + 5))",
"7 + 6 + 5 + 2 + 2 + 8",
"(6 * 2 + 6 + 5 * (9 * 4)) * 8 + 5",
"5 + 2 + (6 * 4 + 3 + 3 * 3 + 9) * (9 + 2 * 2) + 9",
"6 + (9 + 7 * 7 + 3 * 7 * 2) + (8 * 5)",
"9 * (3 * 6 * 2 + 5) * 4 + 6 * (3 * 8 + 9 * 2 + 8 + 9) + 2",
"6 + (9 * 2 * 3 * (3 * 8 + 7 * 4))",
"8 + (6 + 6 + (2 + 8 * 9) + 7 + 6 * (6 + 8 * 2)) * (9 * 7) * 3 + 3",
"(4 * (6 * 8 + 3 * 8 * 5) * 8 + 3 + (8 * 9 * 3 * 3)) + 5 * 7 * 9 + 9",
"7 * 5 * (5 * 9 * (6 * 2 * 4) + 8 + 9) * 7 + (8 * 7 + 7 + (7 * 4) + 7) + (7 + 3)",
"8 * (5 * (5 + 2)) + 5 + 6",
"8 + 9 * (3 + (3 * 7 * 2))",
"(5 + 3 * 8 * 7) * 2 + 5 * 9",
"7 * 4 * 2 * 5 + (7 + 8 + 4 + 9 * 8 * 8) * 3",
"7 * 8 + 7 * 8 * 2",
"(9 + (3 + 5 + 3 * 8 + 8 + 3)) + 8 + 6 * 5 + (5 * 2 * 3 + 3 * (2 * 7 * 4 * 4 + 7 + 4)) + (7 + (8 + 4 * 4 + 9 + 8 * 9) * 7)",
"4 + 4 + 8 + ((5 * 3) * 8 * 3 * 4 * 7) + 5",
"2 + 9 * (5 * 6 * (7 * 5 + 4 + 7) * (9 * 2 * 7 * 7 * 2 + 7) * 4)",
"9 + (6 * 6) * 2 + 2 * 8 * 2",
"4 + 2 * (2 * 9 * 4 * 2 + 9 + 4) + (7 + 4 + 8 + 9) * 8 + 5",
"7 * ((5 * 9 * 8) * 8) + 6 + (7 * 5 * 4 * 6 * 5) + 6 * 3",
"7 * 8 * (7 + 3) * 2 * 9 * 2",
"9 + ((7 * 9 * 7 + 5 * 8 * 9) * 2 + 6 * 9 + (4 * 3 + 8)) + 2 * 9 * 3 + 5",
"6 * 2 + (7 + (4 + 8 + 7 + 6) * 2 + 7 * (7 + 3 * 8 * 5)) * 6",
"(9 * 8 + 3 * 9 * 5) * 9 + 3 * 5",
"8 + (7 + 2) + (9 + 9 * 7 + 3 * 4 + 6)",
"7 + 8 * 4 + 8 * (8 * 4) * 8",
"(3 * 4 * 2 * (2 + 2) + 9) + 8 + 9",
"5 * (9 * 6) + 7",
"2 + ((9 + 4 * 7) + 5 + 2 + (4 + 4 + 7 + 4 * 4) + 7 * 4) + 6",
"((6 + 3 + 6 * 8 + 8 * 6) + 5 + 6 + 2 + 7 + 5) * ((9 * 6) + 2 * 3 * 8)",
"(5 + 7 + 3 + 6 + (9 + 2 + 3) + 4) * 6 * (9 + 5) * 2 + 6",
"4 * 6 + 4 * (9 * 4 * (3 + 5) * 8 + (7 + 8 + 9 + 3)) + 4 * 4",
"9 * 4 + 8 + 9 * (4 + 6 + 9 + 2 + 4)",
"(8 * 5) * 6 * 6 + 8 + 5",
"(7 + 4 + 7 * 3 + 7 + 7) * 2",
"9 + 5 * (4 * 2 + 4 * 9 * 3)",
"5 * 6 * 2 * ((6 + 8 * 5 * 7) + (2 + 7 * 5 + 7) * 7 * 2 * (3 * 2 + 6 * 7 * 3) + 5) + 3 * 3",
"6 * 2 + 7 + 5 + 5 * ((7 + 2 * 7 + 6 + 2) * 4)",
"((8 * 3 * 4 * 7 * 8) * 9 * 8 * 2) + 8 * 9 * (3 * 9) * 3 * (2 * 6 + 7 + 9 * 4)",
"2 + (4 * 4 * 8 * (4 + 8 * 3 * 2 * 9 + 4) * 5 * (7 * 7 + 3 + 2 + 3)) * 5 + 6",
"5 + 5 + 9 * (8 * 9 + 9 * 9 + (2 + 9) + 9) * 2",
"(5 * 6 + 8) + 9 + (2 * 6 + 5 + 5 * (3 + 9 + 2) + 2) + 3 * ((3 * 7) + 8 * 5) + (3 + 2)",
"(9 * 7 + 4 + 2 * (2 * 2 + 3 + 8)) + 6",
"4 * 5 * ((7 * 7 * 2 + 5 + 9 * 4) * (9 + 4 * 7 + 8) * 8 * 3)",
"9 * 4 + (8 + (4 + 8 + 3) * 6 * 9 + 3 * 7) * 5 * 4",
"8 + (7 + 3 * (8 * 6 * 8 + 7 * 8) + (6 * 2 * 3 + 7 * 9))",
"(6 * 4 * 4 * 8 + (6 + 6)) * 8 + 2 + 4 + 9 * 7",
"7 + (9 + 5 * (2 + 5 + 8 + 2 * 2 * 4) * 2 + 6) + 5 + 6 + 3 * (6 * (9 + 3))",
"2 * (8 * 4) * 7 + 3 * 9",
"3 + 6 + 2 + 6 + 3 * (3 + (2 * 6 + 6 * 3) * 6 + 2 * (7 * 7 + 7))",
"6 + 2 * 7 * (8 * 5 * 8 * 9 + 8)",
"2 * 8 + 3 * (5 * 6 + 3 * 2 * 5 * 8)",
"(7 + (5 * 8 + 5 + 4 + 9) + 4 + 3 * 3 * (9 + 5 * 3 * 7 * 6)) + 3 + (6 * 2 * 3 * 4) + ((2 * 2) + 9 + (9 + 3 + 6 * 9 * 4 + 4) + 8)",
"(8 * 5 + (3 + 3 * 5 + 9 * 2) + 4 + (5 + 5 + 7 * 3) + 6) * ((2 + 2 * 4) * 3 + 5)",
"9 * 9 * (3 + 2 + 7 * 2 + 6 * 5) + 2 + 4 * (2 + 3 * (8 * 4) + 2)",
"(6 * 8 + (5 * 7 * 6 + 3 + 4 * 4) + (6 + 7) + 9) * (7 * 7 * 5 + 4) + 2 + 9 * 9",
"4 * 9 * 2 * (7 * 3 * (6 + 8 * 5))",
"8 + 2 + 2 * (7 * 9 + 4 + (4 + 5 + 3 * 7 + 4 + 8)) + 4 * 2",
"(4 + (7 * 3) * 8 * 2) * 5 * 9 + 9 * 4 + 3",
"(6 * 6 + (8 * 4 + 3 + 2 * 6) * 6) * 5 * 2 + 7 * (6 + (7 * 3)) + (6 + 6 * (6 * 9 * 8 + 3 * 3) * 3)",
"(8 + 8 + 5 * 6) + 7 + 4 + ((9 + 7) * (2 * 9 * 2) * 8) + 5",
"5 * 9 * (8 * (9 * 8 * 4) * 9 * 8)",
"7 + (3 * 6 + (6 * 2 * 9) * 2)",
"7 + 9 + 6 + (6 + 4 + 7) + 4",
"3 + 6 * 3",
"2 * 7 * 7 + (2 + 2 + 7 + (5 * 9 * 2 * 7 * 9)) + 5 * 9",
"2 * (2 + 7 * 2 * (5 + 7 + 7) * 7) + 8 + 5",
"9 + 7 + 5",
"8 + 8 + 3 * 2 * 5 * (5 * (6 + 9) * (4 * 5 * 8 * 2 + 4))",
"7 + 8 * (6 * 8) * 8 * (3 * 2 + 8 + 5 + 8 + 8)",
"7 * 8 * (2 * 3 + 9 * 6) * 8 + ((9 * 3 + 9 + 3 * 9 + 6) + 7 * 2 + 9 + (7 + 3 * 9 * 6) * (5 * 6 + 4 * 2 + 4 + 3))",
"3 * 9 + (4 * 6 + 3 * 3 + 4 * (5 * 5))",
"((5 * 9 + 7 + 9) + 4 + 9 + 8 + 8 + 5) * 9 + (8 + 7 * (4 * 5 + 5 * 7 + 7)) + 4 * 2 * ((4 + 9 * 6 + 2) * 3 * 2 * 8 * 9)",
"4 + 6 + 3 * (8 + 9 + 9) * 7 + 5",
"((5 * 8 * 9 * 9 + 5) + (5 * 8 * 5 * 3 * 8) * 5 * 3 + (6 * 6 * 8 + 2 + 5 + 6)) + 2 * 6 + 3 + 3 + 2",
"7 * 6 * 3 + (6 + 7 * 3 + (7 * 8 * 7 + 9 * 7) * 7) * 7 + 4",
"(7 * 4 + 8 + 8) * (3 * 9 + 9 + 9 + 9 + 7) * (6 * 6 * 3 + 8 * (6 + 3 + 8 + 7 + 3)) * 2 + 5",
"(9 + 3) * 5 * 9",
"((2 + 2 + 7 * 2 * 4) + 5 + 2 + 5) * 5 + 2 * 7 + (6 * (6 + 8 * 5 + 6) * 5 * 6 * 3) + 8",
"(8 * 4 * 9 + 7) + (8 + 4)",
"8 * (4 + 5 + (9 * 2 + 8 + 4 * 8) * 2) + 8",
"(9 + 9) + 5",
"7 * (7 + 3 + 7) + 5 + 8 * 3",
"(6 * 6 * (3 + 4 + 6 + 7 + 5 * 2) * 4 * 2 * 7) + 2 + (4 * 8 + 8) + 2 + 7",
"2 + 5 * 5 + ((3 * 7 + 5 + 2 * 3) + 5 * 2 * 6 * 8)",
"9 + 9 * (2 + (9 + 4) + 9) * 8 + 3",
"7 * 5",
"7 + 5 * 7 * (9 * 8 * 7 * 5) * 8",
"(8 + (5 + 2 + 8 * 7 + 8)) + 3 + 2 + 9 + (8 + (9 * 4 + 4 * 4 * 3) + 4 * 4) + 9",
"(7 * 8) + (6 + 4 * 9 + 8 + 4 + (6 + 6 * 8)) * (2 + 6 * 3 + (3 * 2 * 7 + 2) * 5 + (4 * 4 + 7 + 4)) * 9",
"3 * 9 * ((3 * 7 * 8 * 9) + 7 * (7 + 5 * 9 * 7) * 8 + 4 + 2) + 9 * 5",
"(8 * 6 * 3 + (9 + 5)) * 4 + 9 * 7 + ((6 * 7) + 9 + 8 + 9) + 3",
"8 * ((4 + 8) * 7 + 8 + 2 * 8) * 5 * 7 + (6 + 3 * 6) * 9",
"((6 + 2 * 7 + 2 + 9 * 5) * 5 * 9 + (7 * 7)) * 9 * 2 * 6 + 4",
"8 + (7 * 8 + 6 * 8 * 5) + 2 * (4 + 4 * 9 + 9 + 9) * 3 * 8",
"9 * ((9 + 3) * 3 * 3 * 5 * 2 * 5) * 5 * (8 * 3 * (6 * 4 * 6 * 7 * 9) + 2)",
"9 + 6 + 7 + 6 * ((2 + 8 * 6 + 7 + 6) + 6 + (9 + 9 * 9 + 5)) * 8",
"(7 * 4 + (4 + 5 + 2 + 3)) + 7 * 7 * 3 + 3 * (5 + 4 + 2 + 3)",
"(4 * 6) + 6 * 8 + 3 + 8 + (5 * 7 * 5)",
"(9 + 2 * (6 + 4 * 9 + 7) * 3) * 4",
"(8 + 2 * 4 * 7 * (9 * 4) + 4) * (3 + 4 * 5) + (4 * 2 * 8 * (6 + 6 + 9) + 3 * (6 + 6 + 7 + 3)) * 3",
"3 * 5 + 6",
"5 + 2 + 8 * 6 + (4 + (9 * 3) * (6 + 9 * 6 + 3 * 7) + 4 * 5 + 8)",
"(3 + 5 * (6 * 2 * 5 + 3 + 2)) * 8 * 7 * 7",
"7 * 2 + 8 * ((6 + 2 * 6 + 8 + 9 + 3) + 6 * 4)",
"7 + 7 + 3 * 6 + ((2 * 9 * 2 * 5 + 6) + 6 + 6)",
"(9 + 4) + 3 + 5 * ((9 + 4 * 8 * 5) + 7 + 3 + 6 * 6 + (5 * 5 + 6))",
"(8 + 4 + 2) + (7 + 2)",
"6 * (4 * 9 + 7 + 8 * (8 + 8)) + 5 + 6 + 5 * 9",
"3 * (4 + 8 + 2 + 2) * 9 + 5 * 4",
"(5 * 8 * (9 * 4 * 7 + 3 * 3 * 8) + 5) * (6 + (3 * 3 * 3 * 4)) + 4 * ((7 * 3 * 4 + 3) * 4 * 8 * (9 + 3) + 4)",
"4 * ((8 * 8 + 6 + 7 * 6) * 9 * 5 * 5 + 4)",
"(3 + 7 * 8 + (7 * 7 * 7 * 7)) * 6 + 8 + (8 + 5) + 8 + 5",
"(7 * (8 * 3 + 3 + 8) + 2 * 8 + 6) * 8 + 6",
"4 + (3 * 6 + 5) * (6 + 3 + (6 + 8 + 6 * 9 * 8) * (3 * 6) + 6 * 3)",
"6 + (9 * (7 + 9 * 6 + 9 + 8)) * 9 * 8 * 5 * (9 + (3 + 4 + 2 + 9 + 6) * (3 * 5 * 2 + 6 * 8 * 6) + (8 + 7 * 6) + (7 * 4 * 8 * 7 + 3 * 9) + (3 + 7 + 9))",
"3 + 9 * 7 + ((9 + 8 + 3 * 2) + 6 + 5 + (4 + 8 * 5 * 4 * 7) + (9 * 3 * 8))",
"4 * 2 * 2 + (6 + 8) + 6",
"3 * 7 * (8 * 3)",
"5 + 5 + 6 + (2 * 5 * 5) + (6 + 6)",
"9 * 6 * (3 + (6 * 2 + 4 + 8 * 3) * 6 + (3 * 9 * 9 + 9)) * 9",
"(3 * 3) * 8 * 2 * 3 * 2 * 8",
"((4 * 5 * 3) * 8 * (9 * 2 + 4 * 3 * 7 * 6) * 9) * (6 + 6 + 6 * 8) + 8 + 5 * 2",
"7 * (5 * 7 + 4 + 8) * 2 * 4",
"(6 + 3 * (2 * 8 + 4 + 2 + 9 + 6) + 7) * 3 * 2 * 9",
"9 * (8 + 9 + (9 + 8) * 5) + (4 + 3 * 7 + 5 + (6 * 2 + 5)) * 2",
"(5 * 7 + 6 + (3 + 3) + 9) + 6",
"8 + (6 * (6 + 7 * 3))",
"(7 + 7) * 3 + 5 * (9 + 7 + (5 + 9 + 2) * 3 * 7)",
"4 * (3 + 8 + 2 + 3) * 8 + (7 * 6 * 9 + (4 * 7 * 5) * 3 * 3) * 5 + 7",
"4 + 7 * (9 + 5 * 2 * 2 * 2 + 7) * 2",
"4 + 3 + 5 + (6 * (5 * 7 + 2 * 7)) + 4 + (2 * 8 + (3 + 2 + 3 * 2) * 5 * (2 * 2))",
"6 + 6 + 2 + 6 + (9 * 2 * 9)",
    };
}