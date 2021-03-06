﻿/* Copyright 2013 Eduardo Cavazos

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace Tests
{
    class Program
    {
        static void AssertEqual(DoubleFloat a, DoubleFloat b, double tolerance = 0.00000001)
        {
            if (Math.Abs(a.val - b.val) > tolerance)
                Console.WriteLine("{0} and {1} are not equal", a.val, b.val);
        }

        static void AssertEqual(MathObject a, Double b, double tolerance = 0.00000001)
        {
            var x = (DoubleFloat)a;
            var y = new DoubleFloat(b);

            if (Math.Abs(x.val - y.val) > tolerance)
                Console.WriteLine("{0} and {1} are not equal", x.val, y.val);
        }

        static void Main(string[] args)
        {
            Action<Equation> AssertIsTrue = (eq) =>
            {
                if (!eq) Console.WriteLine(eq.ToString());
            };

            Func<MathObject, MathObject> sin = obj => Trig.Sin(obj);
            Func<MathObject, MathObject> cos = obj => Trig.Cos(obj);

            {
                var x = new Symbol("x");
                var y = new Symbol("y");
                var z = new Symbol("z");

                Func<int, Integer> Int = (n) => new Integer(n);

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

                AssertIsTrue(x / x == 1);

                AssertIsTrue(x / y * y / x == 1);

                AssertIsTrue((x ^ 2) * (x ^ 3) == (x ^ 5));

                AssertIsTrue(x + y + x + z + 5 + z == 5 + 2 * x + y + 2 * z);

                AssertIsTrue(((Int(1) / 2) * x + (Int(3) / 4) * x) == Int(5) / 4 * x);

                AssertIsTrue(1.2 * x + 3 * x == 4.2 * x);

                AssertIsTrue(3 * x + 1.2 * x == 4.2 * x);

                AssertIsTrue(1.2 * x * 3 * y == 3.5999999999999996 * x * y);

                AssertIsTrue(3 * x * 1.2 * y == 3.5999999999999996 * x * y);

                AssertIsTrue(3.4 * x * 1.2 * y == 4.08 * x * y);

                // Power.Simplify

                AssertIsTrue((0 ^ x) == 0);
                AssertIsTrue((1 ^ x) == 1);
                AssertIsTrue((x ^ 0) == 1);
                AssertIsTrue((x ^ 1) == x);

                // Product.Simplify

                AssertIsTrue(x * 0 == 0);

                // Difference

                AssertIsTrue(-x == -1 * x);

                AssertIsTrue(x - y == x + -1 * y);


                AssertIsTrue(Int(10).Substitute(Int(10), 20) == 20);
                AssertIsTrue(Int(10).Substitute(Int(15), 20) == 10);

                AssertIsTrue(new DoubleFloat(1.0).Substitute(new DoubleFloat(1.0), 2.0) == 2.0);
                AssertIsTrue(new DoubleFloat(1.0).Substitute(new DoubleFloat(1.5), 2.0) == 1.0);

                AssertIsTrue((Int(1) / 2).Substitute(Int(1) / 2, Int(3) / 4) == Int(3) / 4);
                AssertIsTrue((Int(1) / 2).Substitute(Int(1) / 3, Int(3) / 4) == Int(1) / 2);

                AssertIsTrue(x.Substitute(x, y) == y);
                AssertIsTrue(x.Substitute(y, y) == x);

                AssertIsTrue((x ^ y).Substitute(x, 10) == (10 ^ y));
                AssertIsTrue((x ^ y).Substitute(y, 10) == (x ^ 10));

                AssertIsTrue((x ^ y).Substitute(x ^ y, 10) == 10);

                AssertIsTrue((x * y * z).Substitute(x, y) == ((y ^ 2) * z));
                AssertIsTrue((x * y * z).Substitute(x * y * z, x) == x);

                AssertIsTrue((x + y + z).Substitute(x, y) == ((y * 2) + z));
                AssertIsTrue((x + y + z).Substitute(x + y + z, x) == x);

                AssertIsTrue(
                    ((((x * y) ^ (Int(1) / 2)) * (z ^ 2)) ^ 2)
                        .Substitute(x, 10)
                        .Substitute(y, 20)
                        .Substitute(z, 3)
                        == 16200
                        );

                AssertIsTrue(sin(new DoubleFloat(3.14159 / 2)) == 0.99999999999911982);

                AssertIsTrue(sin(x + y) + sin(x + y) == 2 * sin(x + y));

                AssertIsTrue(sin(x + x) == sin(2 * x));

                AssertIsTrue(sin(x + x).Substitute(x, 1) == sin(Int(2)));

                AssertIsTrue(sin(x + x).Substitute(x, 1.0) == 0.90929742682568171);

                AssertIsTrue(sin(2 * x).Substitute(x, y) == sin(2 * y));

                // Product.RecursiveSimplify

                AssertIsTrue(1 * x == x);

                AssertIsTrue(x * 1 == x);

                AssertIsTrue(x != y);

                AssertIsTrue(x != 10);

                // ==(double a, MathObject b)

                AssertIsTrue(1.0 == new DoubleFloat(3.0) - 2.0);

                // Console.WriteLine((x + x + x + x) / x);
            }

            #region PSE 5E Example 4.3
            {
                var thA = new Symbol("thA"); // angle at point A
                var vA = new Symbol("vA"); // velocity at point A

                var g = new Symbol("g"); // magnitude of gravity

                var _g = new Point(0, -g); // gravity vector

                var objA =
                    new Obj()
                    {
                        position = new Point(0, 0),
                        velocity = Point.FromAngle(thA, vA),
                        acceleration = _g,
                        time = new Integer(0)
                    };

                var objB =
                    new Obj()
                    {
                        velocity = new Point(objA.velocity.x, 0),
                        acceleration = _g
                    };

                var timeB = Calc.Time(objA, objB);
                var timeC = timeB * 2;

                objB = objA.AtTime(timeB);
                var objC = objA.AtTime(timeC);

                //Console.WriteLine("How far does he dump in the horizontal direction?");

                AssertIsTrue(objC.position.x == 2 * Trig.Cos(thA) * Trig.Sin(thA) * (vA ^ 2) / g);

                //Console.WriteLine("What is the maximum height reached?");

                AssertIsTrue(objB.position.y == (Trig.Sin(thA) ^ 2) * (vA ^ 2) / 2 / g);

                // Console.WriteLine("Distance jumped: ");

                var deg = 3.14159 / 180.0;

                AssertIsTrue(
                    objC.position.x
                    // .Substitute(thA, Trig.ToRadians(20))
                    .Substitute(thA, 20 * deg)
                    .Substitute(g, 9.8)
                    .Substitute(Trig.Pi, 3.14159)
                    .Substitute(vA, 11)
                    ==
                    7.9364536850196412);

                //Console.WriteLine("Maximum height reached: ");

                AssertIsTrue(
                    objB.position.y
                    .Substitute(g, 9.8)
                    .Substitute(thA, Trig.ToRadians(20))
                    .Substitute(Trig.Pi, 3.14159)
                    .Substitute(vA, 11)
                    ==
                    0.72215756424454336);
            }
            #endregion

            #region PSE 5E EXAMPLE 4.5
            {
                // A stone is thrown from the top of a building upward at an
                // angle of 30.0° to the horizontal and with an initial speed of
                // 20.0 m/s, as shown in Figure 4.12. If the height of the building 
                // is 45.0 m, (a) how long is it before the stone hits the ground?
                // (b) What is the speed of the stone just before it strikes the
                // ground?

                var thA = new Symbol("thA"); // angle at point A
                var vA = new Symbol("vA"); // velocity at point A

                var g = new Symbol("g"); // magnitude of gravity

                var _g = new Point(0, -g); // gravity vector

                var objA = new Obj()
                {
                    position = new Point(0, 0),
                    velocity = Point.FromAngle(thA, vA),
                    acceleration = _g,
                    time = new Integer(0)
                };

                var objB = new Obj()
                {
                    velocity = new Point(objA.velocity.x, 0),
                    acceleration = _g,
                };

                var timeB = Calc.Time(objA, objB);

                objB = objA.AtTime(timeB);

                var timeC = timeB * 2;

                var objC = objA.AtTime(timeC);

                var yD = new Symbol("yD");

                var objD = new Obj()
                {
                    position = new Point(null, yD),
                    velocity = new Point(objA.velocity.x, null),
                    acceleration = _g
                };

                var timeAD = Calc.Time(objA, objD, 1);

                objD = objA.AtTime(timeAD);

                // "How long is it before the stone hits the ground?".Disp();

                // "Symbolic answer:".Disp();

                AssertIsTrue(
                    timeAD
                    ==
                    -1 * (g ^ -1) * (-1 * Trig.Sin(thA) * vA + -1 * (((Trig.Sin(thA) ^ 2) * (vA ^ 2) + -2 * g * yD) ^ (new Integer(1) / 2))));

                // "Numeric answer:".Disp();

                AssertEqual(
                    (DoubleFloat)
                    timeAD
                        .Substitute(g, 9.8)
                        .Substitute(thA, (30).ToRadians())
                        .Substitute(Trig.Pi, 3.14159)
                        .Substitute(vA, 20)
                        .Substitute(yD, -45),
                    new DoubleFloat(4.21804787012706),
                    0.0001);

                // "What is the speed of the stone just before it strikes the ground?".Disp();

                // "Symbolic answer:".Disp();

                AssertIsTrue(
                    objD.velocity.Norm()
                    ==
                    (((Trig.Cos(thA) ^ 2) * (vA ^ 2) + (Trig.Sin(thA) ^ 2) * (vA ^ 2) + -2 * g * yD) ^ (new Integer(1) / 2)));

                // "Numeric answer:".Disp();

                AssertEqual(
                    (DoubleFloat)
                    objD.velocity.Norm()
                        .Substitute(g, 9.8)
                        .Substitute(thA, (30).ToRadians())
                        .Substitute(Trig.Pi, 3.14159)
                        .Substitute(vA, 20)
                        .Substitute(yD, -45),
                    new DoubleFloat(35.805027579936315),
                    0.1);
            }
            #endregion

            #region PSE 5E EXAMPLE 4.6
            {
                // An Alaskan rescue plane drops a package of emergency rations 
                // to a stranded party of explorers, as shown in Figure
                // 4.13. If the plane is traveling horizontally at 40.0 m/s and is
                // 100 m above the ground, where does the package strike the
                // ground relative to the point at which it was released?

                var xA = new Symbol("xA");      // position.x at point A

                var yA = new Symbol("yA");      // position.y at point A

                var thA = new Symbol("thA");    // angle at point A

                var vA = new Symbol("vA");      // velocity at point A

                var g = new Symbol("g");        // magnitude of gravity

                var _g = new Point(0, -g);      // gravity vector

                var objA = new Obj()            // obj at the initial position
                {
                    position = new Point(xA, yA),
                    velocity = Point.FromAngle(thA, vA),
                    acceleration = _g,
                    time = 0
                };

                var objB = new Obj()            // obj at the final position
                {
                    position = new Point(null, 0),
                    velocity = new Point(objA.velocity.x, null),
                    acceleration = _g
                };

                var timeB = Calc.Time(objA, objB, 1);

                objB = objA.AtTime(timeB);

                //"Where does the package strike the ground relative to the point at which it was released?".Disp(); "".Disp();

                //"symbolic:".Disp();

                //objB.position.x.Disp(); "".Disp();

                AssertIsTrue(
                    objB.position.x
                    ==
                    xA - cos(thA) / g * vA * (-sin(thA) * vA - (((sin(thA) ^ 2) * (vA ^ 2) + 2 * g * yA) ^ new Integer(1) / 2)));

                //"numeric:".Disp();

                //objB.position.x
                //    .Substitute(xA, 0)
                //    .Substitute(yA, 100)
                //    .Substitute(vA, 40)
                //    .Substitute(thA, 0.0)
                //    .Substitute(g, 9.8)
                //    .Disp();

                AssertEqual(
                    objB.position.x
                        .Substitute(xA, 0)
                        .Substitute(yA, 100)
                        .Substitute(vA, 40)
                        .Substitute(thA, 0.0)
                        .Substitute(g, 9.8),
                    180.70158058105025);

                //"".Disp();

                //("What are the horizontal and vertical components " +
                // "of the velocity of the package just before it hits the ground?").Disp(); "".Disp();

                //"symbolic velocity.x:".Disp();

                //objB.velocity.x.Disp(); "".Disp();

                AssertIsTrue(objB.velocity.x == cos(thA) * vA);

                //"symbolic velocity.y:".Disp();

                //objB.velocity.y.Disp(); "".Disp();

                AssertIsTrue(
                    objB.velocity.y
                    ==
                    -1 * (((sin(thA) ^ 2) * (vA ^ 2) + 2 * g * yA) ^ (new Integer(1) / 2)));

                //"numeric velocity.x:".Disp();

                //objB.velocity.x
                //    .Substitute(xA, 0)
                //    .Substitute(yA, 100)
                //    .Substitute(vA, 40)
                //    .Substitute(thA, 0.0)
                //    .Substitute(g, 9.8)
                //    .Disp(); "".Disp();

                AssertEqual(
                    objB.velocity.x
                        .Substitute(xA, 0)
                        .Substitute(yA, 100)
                        .Substitute(vA, 40)
                        .Substitute(thA, 0.0)
                        .Substitute(g, 9.8),
                    40);

                //"numeric velocity.y:".Disp();

                //objB.velocity.y
                //    .Substitute(xA, 0)
                //    .Substitute(yA, 100)
                //    .Substitute(vA, 40)
                //    .Substitute(thA, 0.0)
                //    .Substitute(g, 9.8)
                //    .Disp(); "".Disp();

                AssertEqual(
                    objB.velocity.y
                        .Substitute(xA, 0)
                        .Substitute(yA, 100)
                        .Substitute(vA, 40)
                        .Substitute(thA, 0.0)
                        .Substitute(g, 9.8),
                    -44.271887242357316);
            }
            #endregion

            #region PSE 5E EXAMPLE 4.7

            {
                // A ski jumper leaves the ski track moving in the horizontal 
                // direction with a speed of 25.0 m/s, as shown in Figure 4.14.
                // The landing incline below him falls off with a slope of 35.0°. 
                // Where does he land on the incline?

                var thA = new Symbol("thA");    // angle at point A

                var vA = new Symbol("vA");      // velocity at point A

                var g = new Symbol("g");        // magnitude of gravity

                var _g = new Point(0, -g);      // gravity vector

                var th = new Symbol("th");      // angle of incline

                var objA = new Obj()
                {
                    position = new Point(0, 0),
                    velocity = Point.FromAngle(thA, vA),
                    acceleration = _g,
                    time = 0
                };

                Func<MathObject, MathObject> numeric = obj =>
                    obj
                        .Substitute(vA, 25)
                        .Substitute(thA, 0.0)
                        .Substitute(th, (-35).ToRadians())
                        .Substitute(Trig.Pi, 3.14159)
                        .Substitute(g, 9.8);

                var intersection = objA.ProjectileInclineIntersection(th);

                Action nl = () => "".Disp();

                // "Where does he land on the incline?".Disp(); nl();

                // "x position (symbolic):".Disp();

                // intersection.x.Disp(); nl();

                AssertIsTrue(
                    intersection.x
                    ==
                    -2 * (cos(th) ^ -1) * (cos(thA) ^ 2) * (g ^ -1) * (sin(th) + -1 * cos(th) * (cos(thA) ^ -1) * sin(thA)) * (vA ^ 2));


                //"y position (symbolic):".Disp();

                //intersection.y.Disp(); nl();

                AssertIsTrue(
                    intersection.y
                    ==
                    -2 * (cos(th) ^ -2) * (cos(thA) ^ 2) / g * sin(th) * (sin(th) + -1 * cos(th) * (cos(thA) ^ -1) * sin(thA)) * (vA ^ 2));

                //"x position (numeric):".Disp();

                //numeric(intersection.x).Disp(); nl();

                AssertEqual(numeric(intersection.x), 89.3120879153208);

                //"y position (numeric):".Disp();

                //numeric(intersection.y).Disp(); nl();

                AssertEqual(numeric(intersection.y), -62.536928534704884);

                var objB = new Obj()
                {
                    position = intersection,
                    acceleration = _g
                };

                //"Determine how long the jumper is airborne".Disp(); nl();

                //"symbolic:".Disp();

                var timeB = Calc.Time(objA, objB, 1);

                // timeB.Disp(); nl();

                Func<MathObject, MathObject> sqrt = obj => obj ^ (new Integer(1) / 2);

                AssertIsTrue(
                    timeB
                    ==
                    -1 / g * 
                    (-sin(thA) * vA - 
                        sqrt(
                            (sin(thA) ^ 2) * (vA ^ 2) + 4 * (cos(th) ^ -2) * (cos(thA) ^ 2) * sin(th) * 
                            (sin(th) - cos(th) / cos(thA) * sin(thA)) * 
                            (vA ^ 2))));
                
                //"numeric:".Disp();

                //numeric(timeB).Disp(); nl();

                AssertEqual(numeric(timeB), 3.5724835166128317);

                objB = objA.AtTime(timeB);

                //"Determine his vertical component of velocity just before he lands".Disp();
                //nl();

                //"symbolic:".Disp();

                //objB.velocity.y.Disp(); nl();

                AssertIsTrue(
                    objB.velocity.y
                    ==
                    -sqrt(
                        (sin(thA) ^ 2) * (vA ^ 2) 
                        + 
                        4 * (cos(th) ^ -2) * (cos(thA) ^ 2) * sin(th) * 
                        (sin(th) - cos(th) * (cos(thA) ^ -1) * sin(thA)) * 
                        (vA ^ 2)));

                //"numeric:".Disp();

                //numeric(objB.velocity.y).Disp();

                AssertEqual(
                    numeric(objB.velocity.y),
                    -35.010338462805755);
            }

            #endregion

            #region PSE 5E PROBLEM 4.11

            {
                // One strategy in a snowball fight is to throw a first snowball at a 
                // high angle over level ground. While your opponent is watching the
                // first one, you throw a second one at a low angle and timed to arrive 
                // at your opponent before or at the same time as the first one. Assume 
                // both snowballs are thrown with a speed of 25.0 m/s. The first one is 
                // thrown at an angle of 70.0° with respect to the horizontal.
                // 
                // (a) At what angle should the second (low-angle) snowball be thrown 
                // if it is to land at the same point as the first?
                //
                // (b) How many seconds later should the second snowball be thrown if it 
                // is to land at the same time as the first?

                var xA = new Symbol("xA");      // position.x at point A
                var yA = new Symbol("yA");      // position.y at point A
                var th1A = new Symbol("th1A");  // angle of snowball 1 at point A
                var vA = new Symbol("vA");      // velocity at point A

                var g = new Symbol("g");        // magnitude of gravity
                var _g = new Point(0, -g);      // gravity vector

                //Func<MathObject, MathObject> numeric = obj =>
                //    obj
                //        .Substitute(xA, 0)
                //        .Substitute(xB, 1.4)
                //        .Substitute(yA, 0.86)
                //        .Substitute(g, 9.8)
                //        .Substitute(Trig.Pi, 3.14159);

                var obj1A = new Obj()           // snowball 1 at initial point
                {
                    position = new Point(xA, yA),
                    velocity = Point.FromAngle(th1A, vA),
                    acceleration = _g,
                    time = 0
                };

                var obj1B = new Obj()            // snowball 1 at final point
                {
                    position = new Point(null, 0),
                    velocity = new Point(obj1A.velocity.x, null),
                    acceleration = _g
                };

                var time1B = Calc.Time(obj1A, obj1B, 1);

                obj1B = obj1A.AtTime(time1B);

                var obj2A = new Obj()           // snowball 2 at initial point
                {
                    position = obj1A.position,
                    speed = vA,
                    acceleration = _g
                };

                var obj2B = new Obj()           // snowball 2 at final point
                {
                    position = obj1B.position,
                    acceleration = _g
                };

                //Calc.InitialAngle(obj2A, obj2B, 1, 0)
                //    .Substitute(yA, 0)
                //    .Substitute(th1A, (70).ToRadians())
                //    .Substitute(vA, 25)
                //    .Substitute(Trig.Pi, 3.14159)
                //    .Substitute(g, 9.8)
                //    .ToDegrees()
                //    .Substitute(Trig.Pi, 3.14159)
                //    .Disp();

                var th2 = Calc.InitialAngle(obj2A, obj2B, 0, 0);

                //("At what angle should the second (low-angle) snowball " +
                //"be thrown if it is to land at the same point as the first?").Disp();

                //"".Disp();

                //"symbolic:".Disp();

                //th2.Disp(); "".Disp();

                //"numeric:".Disp();

                AssertEqual(
                    th2
                        .ToDegrees()
                        .Substitute(yA, 0)
                        .Substitute(th1A, (70).ToRadians())
                        .Substitute(vA, 25)
                        .Substitute(g, 9.8)
                        .Substitute(Trig.Pi, Math.PI),
                    20.000000000000007);

                //"".Disp();

                obj2A.velocity = Point.FromAngle(th2, vA);

                var time2B = Calc.Time(obj2A, obj2B, 1);

                //("How many seconds later should the second snowball be thrown if it " +
                //"is to land at the same time as the first?").Disp();

                //"".Disp();

                //"symbolic:".Disp();

                //(time1B - time2B).Disp(); "".Disp();

                //"numeric:".Disp();

                //(time1B - time2B)
                //    .Substitute(yA, 0)
                //    .Substitute(th1A, (70).ToRadians())
                //    .Substitute(vA, 25)
                //    .Substitute(Trig.Pi, 3.14159)
                //    .Substitute(g, 9.8)
                //    .Disp();

                AssertEqual(
                    (time1B - time2B)
                        .Substitute(yA, 0)
                        .Substitute(th1A, (70).ToRadians())
                        .Substitute(vA, 25)
                        .Substitute(Trig.Pi, 3.14159)
                        .Substitute(g, 9.8),
                    3.0493426265020469);

                //Console.ReadLine();
            }

            #endregion

            #region PSE 5E PROBLEM 4.17

            {
                // A cannon with a muzzle speed of 1 000 m/s is used to
                // start an avalanche on a mountain slope. The target is 
                // 2 000 m from the cannon horizontally and 800 m above
                // the cannon. At what angle, above the horizontal, should
                // the cannon be fired?

                var xA = new Symbol("xA");      // position.x at point A
                var yA = new Symbol("yA");      // position.y at point A
                var thA = new Symbol("thA");  // angle of snowball 1 at point A
                var vA = new Symbol("vA");      // velocity at point A

                var xB = new Symbol("xB");      // position.x at point A
                var yB = new Symbol("yB");      // position.y at point A

                var g = new Symbol("g");        // magnitude of gravity
                var _g = new Point(0, -g);      // gravity vector

                var objA = new Obj()
                {
                    position = new Point(xA, yA),
                    speed = vA,
                    acceleration = _g,
                    time = 0
                };

                var objB = new Obj()
                {
                    position = new Point(xB, yB),
                    acceleration = _g
                };

                //"At what angle, above the horizontal, should the cannon be fired?".Disp();

                AssertEqual(
                    Calc.InitialAngle(objA, objB)
                        .ToDegrees()
                        .Substitute(xA, 0)
                        .Substitute(yA, 0)
                        .Substitute(xB, 2000.0)
                        .Substitute(yB, 800)
                        .Substitute(vA, 1000)
                        .Substitute(g, 9.8)
                        .Substitute(Trig.Pi, Math.PI),
                    22.365163229244317);

                //Calc.InitialAngle(objA, objB)
                //    .ToDegrees()
                //    .Substitute(xA, 0)
                //    .Substitute(yA, 0)
                //    .Substitute(xB, 2000.0)
                //    .Substitute(yB, 800)
                //    .Substitute(vA, 1000)
                //    .Substitute(g, 9.8)
                //    .Substitute(Trig.Pi, Math.PI)
                //    .Disp();
            }

            #endregion

            #region PSE 5E PROBLEM 4.24
            {
                // A bag of cement of weight 325 N hangs from three
                // wires as shown in Figure P5.24. Two of the wires make
                // angles th1 = 60.0° and th2 = 25.0° with the horizontal. If
                // the system is in equilibrium, find the tensions
                // T1, T2, and T3 in the wires.

                var F1 = new Symbol("F1");
                var F2 = new Symbol("F2");
                var F3 = new Symbol("F3");

                var th1 = new Symbol("th1");
                var th2 = new Symbol("th2");
                var th3 = new Symbol("th3");

                var _F1 = new Point() { angle = th1 };
                var _F2 = new Point() { angle = th2 };
                var _F3 = new Point() { magnitude = F3, angle = th3 };

                var m = new Symbol("m");

                var obj = new Obj();

                obj.acceleration.x = 0;
                obj.acceleration.y = 0;

                obj.mass = m;

                obj.forces.Add(_F1);
                obj.forces.Add(_F2);
                obj.forces.Add(_F3);

                //"F1 magnitude, symbolic:".Disp(); "".Disp();

                //obj.ForceMagnitude(_F1).Disp(); "".Disp();

                //"F1 magnitude, numeric:".Disp(); "".Disp();

                //obj.ForceMagnitude(_F1)
                //    .Substitute(F3, 325)
                //    .Substitute(th1, (180 - 60).ToRadians())
                //    .Substitute(th2, (25).ToRadians())
                //    .Substitute(th3, (270).ToRadians())
                //    .Substitute(Trig.Pi, Math.PI)
                //    .Disp();

                AssertEqual(
                    obj.ForceMagnitude(_F1)
                        .Substitute(F3, 325)
                        .Substitute(th1, (180 - 60).ToRadians())
                        .Substitute(th2, (25).ToRadians())
                        .Substitute(th3, (270).ToRadians())
                        .Substitute(Trig.Pi, Math.PI),
                    295.67516405290525);

                // "".Disp();

                //"F3 magnitude, numeric:".Disp(); "".Disp();

                //obj.ForceMagnitude(_F2)
                //    .Substitute(F3, 325)
                //    .Substitute(th1, (180 - 60).ToRadians())
                //    .Substitute(th2, (25).ToRadians())
                //    .Substitute(th3, (270).ToRadians())
                //    .Substitute(Trig.Pi, Math.PI)
                //    .Disp();

                AssertEqual(
                    obj.ForceMagnitude(_F2)
                        .Substitute(F3, 325)
                        .Substitute(th1, (180 - 60).ToRadians())
                        .Substitute(th2, (25).ToRadians())
                        .Substitute(th3, (270).ToRadians())
                        .Substitute(Trig.Pi, Math.PI),
                    163.12072360079395);
            }
            #endregion

            #region PSE 5E PROBLEM 5.26
            {
                // You are a judge in a children’s kite-flying contest, and
                // two children will win prizes for the kites that pull most
                // strongly and least strongly on their strings. To measure
                // string tensions, you borrow a weight hanger, some slotted
                // weights, and a protractor from your physics teacher
                // and use the following protocol, illustrated in Figure
                // P5.26: Wait for a child to get her kite well-controlled,
                // hook the hanger onto the kite string about 30 cm from
                // her hand, pile on weights until that section of string is
                // horizontal, record the mass required, and record the
                // angle between the horizontal and the string running up
                // to the kite. (a) Explain how this method works. As you
                // construct your explanation, imagine that the children’s
                // parents ask you about your method, that they might
                // make false assumptions about your ability without concrete
                // evidence, and that your explanation is an opportunity to 
                // give them confidence in your evaluation tech-nique. 
                // (b) Find the string tension if the mass required
                // to make the string horizontal is 132 g and the angle of
                // the kite string is 46.3°.

                var F1 = new Symbol("F1");
                var F2 = new Symbol("F2");
                var F3 = new Symbol("F3");

                var th1 = new Symbol("th1");
                var th2 = new Symbol("th2");
                var th3 = new Symbol("th3");

                var _F1 = new Point() { angle = th1 };
                var _F2 = new Point() { angle = th2 };
                var _F3 = new Point() { magnitude = F3, angle = th3 };

                var m = new Symbol("m");

                var obj = new Obj();

                obj.acceleration.x = 0;
                obj.acceleration.y = 0;

                obj.mass = m;

                obj.forces.Add(_F1);
                obj.forces.Add(_F2);
                obj.forces.Add(_F3);

                //"Tension in line to kite:".Disp(); "".Disp();

                //obj.ForceMagnitude(_F2)
                //    .Substitute(th1, (180).ToRadians())
                //    .Substitute(th2, (46.3 * Math.PI / 180))
                //    .Substitute(th3, (270).ToRadians())
                //    .Substitute(F3, 0.132 * 9.8)
                //    .Substitute(Trig.Pi, Math.PI)
                //    .Disp();

                AssertEqual(
                    obj.ForceMagnitude(_F2)
                        .Substitute(th1, (180).ToRadians())
                        .Substitute(th2, (46.3 * Math.PI / 180))
                        .Substitute(th3, (270).ToRadians())
                        .Substitute(F3, 0.132 * 9.8)
                        .Substitute(Trig.Pi, Math.PI),
                    1.7892929261294661);

                //Console.ReadLine();
            }
            #endregion

            #region PSE 5E PROBLEM 5.28
            {
                // A fire helicopter carries a 620-kg bucket of water at the
                // end of a cable 20.0 m long. As the aircraft flies back
                // from a fire at a constant speed of 40.0 m/s, the cable
                // makes an angle of 40.0° with respect to the vertical. 
                // (a) Determine the force of air resistance on the bucket.
                // (b) After filling the bucket with sea water, the pilot re-
                // turns to the fire at the same speed with the bucket now
                // making an angle of 7.00° with the vertical. What is the
                // mass of the water in the bucket?

                var F1 = new Symbol("F1"); // force of air resistance
                var F2 = new Symbol("F2"); // force of cable
                var F3 = new Symbol("F3"); // force of gravity

                var th1 = new Symbol("th1");
                var th2 = new Symbol("th2");
                var th3 = new Symbol("th3");

                var _F1 = new Point() { angle = th1 };
                var _F2 = new Point() { angle = th2 };
                var _F3 = new Point() { magnitude = F3, angle = th3 };

                var m = new Symbol("m");

                var obj = new Obj();

                obj.acceleration.x = 0;
                obj.acceleration.y = 0;

                obj.mass = m;

                obj.forces.Add(_F1);
                obj.forces.Add(_F2);
                obj.forces.Add(_F3);

                //"Force of air resistance on the bucket:".Disp(); "".Disp();

                var FAir =
                    obj.ForceMagnitude(_F1)
                        .Substitute(F3, 620 * 9.8)
                        .Substitute(th1, (180).ToRadians())
                        .Substitute(th2, (90 - 40).ToRadians())
                        .Substitute(th3, (270).ToRadians())
                        .Substitute(Trig.Pi, Math.PI);

                AssertEqual(
                    obj.ForceMagnitude(_F1)
                        .Substitute(F3, 620 * 9.8)
                        .Substitute(th1, (180).ToRadians())
                        .Substitute(th2, (90 - 40).ToRadians())
                        .Substitute(th3, (270).ToRadians())
                        .Substitute(Trig.Pi, Math.PI),
                    5098.3693590331513);

                //"".Disp();

                _F1.magnitude = FAir;

                _F3.magnitude = null;

                var FBucketWithWater =
                    obj.ForceMagnitude(_F3)
                        .Substitute(th1, (180).ToRadians())
                        .Substitute(th2, (90 - 7).ToRadians())
                        .Substitute(th3, (270).ToRadians())
                        .Substitute(Trig.Pi, Math.PI);

                //"What is the mass of the water in the bucket?".Disp(); "".Disp();

                //(FBucketWithWater / 9.8 - 620).Disp();

                AssertEqual(
                    (FBucketWithWater / 9.8 - 620),
                    3617.0292120139611);
            }
            #endregion

            #region PSE 5E PROBLEM 5.30
            {
                // A simple accelerometer is constructed by suspending a
                // mass m from a string of length L that is tied to the top
                // of a cart. As the cart is accelerated the string-mass 
                // system makes a constant angle th with the vertical. 
                // (a) Assuming that the string mass is negligible compared 
                // with m, derive an expression for the cart’s acceleration
                // in terms of and show that it is independent of
                // the mass mand the length L. 
                // (b) Determine the acceleration of the cart when th = 23.0°.

                var F1 = new Symbol("F1"); // force of string
                var F2 = new Symbol("F2"); // force of gravity

                var th1 = new Symbol("th1");
                var th2 = new Symbol("th2"); ;

                var _F1 = new Point() { angle = th1 };
                var _F2 = new Point() { angle = th2, magnitude = F2 };

                var m = new Symbol("m");

                var g = new Symbol("g");

                var obj = new Obj() { mass = m };

                obj.acceleration.y = 0;

                obj.forces.Add(_F1);
                obj.forces.Add(_F2);

                _F1.magnitude = obj.ForceMagnitude(_F1);

                //("Derive an expression for the cart’s acceleration in terms " +
                //"of and show that it is independent of the mass mand the length L:").Disp();

                //"".Disp();

                //obj.AccelerationX()
                //    .Substitute(F2, m * g)
                //    .Substitute(Trig.Cos(th2), 0)
                //    .Substitute(Trig.Sin(th2), -1)
                //    .Disp();

                //"".Disp();

                //"Determine the acceleration of the cart when th = 23.0°".Disp(); "".Disp();

                //obj.AccelerationX()
                //    .Substitute(F2, m * g)
                //    .Substitute(Trig.Cos(th2), 0)
                //    .Substitute(Trig.Sin(th2), -1)
                //    .Substitute(th1, (90 - 23).ToRadians())
                //    .Substitute(Trig.Pi, Math.PI)
                //    .Substitute(g, 9.8)
                //    .Disp();

                AssertEqual(
                    obj.AccelerationX()
                        .Substitute(F2, m * g)
                        .Substitute(Trig.Cos(th2), 0)
                        .Substitute(Trig.Sin(th2), -1)
                        .Substitute(th1, (90 - 23).ToRadians())
                        .Substitute(Trig.Pi, Math.PI)
                        .Substitute(g, 9.8),
                    4.1598531988541261);
            }
            #endregion

            #region PSE 5E PROBLEM 5.31
            {
                // Two people pull as hard as they can on ropes attached
                // to a boat that has a mass of 200 kg. If they pull in the
                // same direction, the boat has an acceleration of 
                // 1.52 m/s^2 to the right. If they pull in opposite directions, 
                // the boat has an acceleration of 0.518 m/s^2
                // to the left. What is the force exerted by each person on the
                // boat? (Disregard any other forces on the boat.)

                // Trig.Cos(new DoubleFloat(Math.PI)).Disp();

                var m = new Symbol("m");

                var aAx = new Symbol("aAx");
                var aBx = new Symbol("aBx");

                var objA = new Obj() { mass = m };

                objA.acceleration.x = aAx;

                var _F1A = new Point() { angle = 0 };
                var _F2A = new Point() { angle = 0 };

                objA.forces.Add(_F1A);
                objA.forces.Add(_F2A);


                var objB = new Obj() { mass = m };

                objB.acceleration.x = aBx;

                var _F1B = new Point() { angle = 0 };
                var _F2B = new Point() { angle = new DoubleFloat(Math.PI) };

                objB.forces.Add(_F1B);
                objB.forces.Add(_F2B);

                //"force 1 magnitude (symbolic):".Disp(); "".Disp();

                //Calc.ForceMagnitude(objA, objB, _F1A, _F1B)
                //    .Substitute(Trig.Cos(0), 1)
                //    .Disp();

                //"force 1 magnitude (numeric):".Disp(); "".Disp();

                //Calc.ForceMagnitude(objA, objB, _F1A, _F1B)
                //    .Substitute(Trig.Cos(0), 1)
                //    .Substitute(m, 200)
                //    .Substitute(aAx, 1.52)
                //    .Substitute(aBx, -0.518)
                //    .Disp();

                AssertEqual(
                    Calc.ForceMagnitude(objA, objB, _F1A, _F1B)
                        .Substitute(Trig.Cos(0), 1)
                        .Substitute(m, 200)
                        .Substitute(aAx, 1.52)
                        .Substitute(aBx, -0.518),
                    100.19999999999999);

                //"".Disp();

                //"force 2 magnitude (symbolic):".Disp(); "".Disp();

                //Calc.ForceMagnitude(objA, objB, _F2A, _F2B)
                //    .Substitute(Trig.Cos(0), 1)
                //    .Disp();

                //"force 2 magnitude (numeric):".Disp(); "".Disp();

                //Calc.ForceMagnitude(objA, objB, _F2A, _F2B)
                //    .Substitute(Trig.Cos(0), 1)
                //    .Substitute(m, 200)
                //    .Substitute(aAx, 1.52)
                //    .Substitute(aBx, -0.518)
                //    .Disp();

                //"".Disp();

                AssertEqual(
                    Calc.ForceMagnitude(objA, objB, _F2A, _F2B)
                        .Substitute(Trig.Cos(0), 1)
                        .Substitute(m, 200)
                        .Substitute(aAx, 1.52)
                        .Substitute(aBx, -0.518),
                    203.8);

            }
            #endregion

            Console.WriteLine("Testing complete");

            Console.ReadLine();
        }
    }
}
