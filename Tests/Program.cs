﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var x = new Symbol("x");
                var y = new Symbol("y");
                var z = new Symbol("z");

                Func<int, Integer> Int = (n) => new Integer(n);

                Action<Equation> AssertIsTrue = (eq) =>
                {
                    if (!eq) Console.WriteLine(eq.ToString());
                };

                AssertIsTrue(x + x == 2 * x);

                AssertIsTrue(x + x == 2 * x);

                AssertIsTrue(x + x + x == 3 * x);

                AssertIsTrue(5 + x + 2 == 7 + x);

                AssertIsTrue(3 + x + 5 + x == 8 + 2 * x);

                AssertIsTrue(4 * x + 3 * x == 7 * x);

                AssertIsTrue(x + y + z + x + y + z == 2 * x + 2 * y + 2 * z);

                AssertIsTrue(10 - x == 10 + x * -1);

                AssertIsTrue(x * y / 3 == Int(1) / 3 * x * y);

                AssertIsTrue(x / y == x * (y ^ -1));

                AssertIsTrue(x / 3 == x * (Int(1) / 3));

                AssertIsTrue(6 * x * y / 3 == 2 * x * y);

                AssertIsTrue((((x ^ Int(1) / 2) ^ Int(1) / 2) ^ 8) == (x ^ 2));

                AssertIsTrue(((((x * y) ^ (Int(1) / 2)) * (z ^ 2)) ^ 2) == (x * y * (z ^ 4)));

                AssertIsTrue(x / x == Int(1));

                AssertIsTrue(x / y * y / x == Int(1));

                AssertIsTrue((x ^ 2) * (x ^ 3) == (x ^ 5));

                AssertIsTrue(x + y + x + z + 5 + z == 5 + 2 * x + y + 2 * z);

                AssertIsTrue(((Int(1) / 2) * x + (Int(3) / 4) * x) == Int(5) / 4 * x);

                AssertIsTrue(1.2 * x + 3 * x == 4.2 * x);

                AssertIsTrue(3 * x + 1.2 * x == 4.2 * x);

                AssertIsTrue(1.2 * x * 3 * y == 3.5999999999999996 * x * y);

                AssertIsTrue(3 * x * 1.2 * y == 3.5999999999999996 * x * y);

                AssertIsTrue(3.4 * x * 1.2 * y == 4.08 * x * y);

                // Power.Simplify

                AssertIsTrue((0 ^ x) == Int(0));
                AssertIsTrue((1 ^ x) == Int(1));
                AssertIsTrue((x ^ 0) == Int(1));
                AssertIsTrue((x ^ 1) == x);

                // Product.Simplify

                AssertIsTrue(x * 0 == new Integer(0));

                // Difference

                AssertIsTrue(-x == -1 * x);

                AssertIsTrue(x - y == x + -1 * y);


                AssertIsTrue(Int(10).Substitute(Int(10), Int(20)) == Int(20));
                AssertIsTrue(Int(10).Substitute(Int(15), Int(20)) == Int(10));

                AssertIsTrue(new DoubleFloat(1.0).Substitute(new DoubleFloat(1.0), new DoubleFloat(2.0)) == new DoubleFloat(2.0));
                AssertIsTrue(new DoubleFloat(1.0).Substitute(new DoubleFloat(1.5), new DoubleFloat(2.0)) == new DoubleFloat(1.0));

                AssertIsTrue((Int(1) / 2).Substitute(Int(1) / 2, Int(3) / 4) == Int(3) / 4);
                AssertIsTrue((Int(1) / 2).Substitute(Int(1) / 3, Int(3) / 4) == Int(1) / 2);

                AssertIsTrue(x.Substitute(x, y) == y);
                AssertIsTrue(x.Substitute(y, y) == x);

                AssertIsTrue((x ^ y).Substitute(x, Int(10)) == (10 ^ y));
                AssertIsTrue((x ^ y).Substitute(y, Int(10)) == (x ^ 10));

                AssertIsTrue((x ^ y).Substitute(x ^ y, Int(10)) == Int(10));

                AssertIsTrue((x * y * z).Substitute(x, y) == ((y ^ 2) * z));
                AssertIsTrue((x * y * z).Substitute(x * y * z, x) == x);

                AssertIsTrue((x + y + z).Substitute(x, y) == ((y * 2) + z));
                AssertIsTrue((x + y + z).Substitute(x + y + z, x) == x);

                AssertIsTrue(
                    ((((x * y) ^ (Int(1) / 2)) * (z ^ 2)) ^ 2)
                        .Substitute(x, Int(10))
                        .Substitute(y, Int(20))
                        .Substitute(z, Int(3))
                        == Int(16200)
                        );

                Func<MathObject, MathObject> sin = arg => new Sin(arg).Simplify();

                AssertIsTrue(sin(new DoubleFloat(3.14159 / 2)) == new DoubleFloat(0.99999999999911982));

                AssertIsTrue(sin(x + y) + sin(x + y) == 2 * sin(x + y));

                AssertIsTrue(sin(x + x) == sin(2 * x));

                AssertIsTrue(sin(x + x).Substitute(x, Int(1)) == sin(Int(2)));

                AssertIsTrue(sin(x + x).Substitute(x, new DoubleFloat(1.0)) == new DoubleFloat(0.90929742682568171));

                AssertIsTrue(sin(2 * x).Substitute(x, y) == sin(2 * y));

                // Product.RecursiveSimplify

                AssertIsTrue(1 * x == x);

                AssertIsTrue(x * 1 == x);
            }

            Console.ReadLine();

        }
    }
}