using System;
using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.UnitTests.IList
{
    public class ForEachPairMutableExtensionsTests
    {
        [Fact]
        public void Throws_Exception_If_Source_Is_Null()
        {
            List<string> input = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                input.ForEachPairMutable(ctx =>
                {
                    Assert.NotEqual(ctx.LhsIndex, ctx.RhsIndex);
                    Assert.NotEqual(ctx.Lhs, ctx.Rhs);
                });
            });
        }

        [Fact]
        public void Doesnt_Throw_Exception_If_Source_Is_Empty()
        {
            var input = new List<string>{ "a" };
            input.ForEachPairMutable(ctx =>
            {
                throw new Exception("Code should be unreachable");
            });
        }

        [Fact]
        public void Throws_Exception_If_Callback_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new List<int>{ 1, 2, 3 }.ForEachPairMutable(null);
            });
        }

        [Fact]
        public void Items_Are_Not_Paired_With_Themselves()
        {
            var input = new List<string> { "a", "b", "c" };
            input.ForEachPairMutable(ctx =>
            {
                Assert.NotEqual(ctx.LhsIndex, ctx.RhsIndex);
                Assert.NotEqual(ctx.Lhs, ctx.Rhs);
            });
        }

        [Fact]
        public void Pairs_Are_Enumerated_In_Forward_Order()
        {
            var input = new List<string> { "a", "b", "c" };
            var pairs = new List<Tuple<string, string>>();
            input.ForEachPairMutable(ctx =>
            {
                pairs.Add(new Tuple<string, string>(ctx.Lhs, ctx.Rhs));
            });

            var expected = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("a", "b"),
                new Tuple<string, string>("a", "c"),
                new Tuple<string, string>("b", "a"),
                new Tuple<string, string>("b", "c"),
                new Tuple<string, string>("c", "a"),
                new Tuple<string, string>("c", "b"),
            };

            Assert.Collection(pairs,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i),
                i => Assert.Equal(expected[3], i),
                i => Assert.Equal(expected[4], i),
                i => Assert.Equal(expected[5], i)
            );
        }

        [Fact]
        public void Rhs_Of_Pair_Can_Be_Removed()
        {
            var input = new List<string> { "a", "b", "c" };
            var pairs = new List<Tuple<string, string>>();
            input.ForEachPairMutable(ctx =>
            {
                pairs.Add(new Tuple<string, string>(ctx.Lhs, ctx.Rhs));
                if (ctx.Rhs == "c")
                {
                    ctx.RemoveRhs();
                }
            });

            var expected = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("a", "b"),
                new Tuple<string, string>("a", "c"), // "c" will be removed here.
                new Tuple<string, string>("b", "a")
                // "b" + "c" not generated because "c" was removed.
                // "c" + "a" not generated because "c" was removed.
                // "c" + "b" not generated because "c" was removed.
            };

            Assert.Collection(pairs,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i)
            );
        }

        [Fact]
        public void Lhs_Of_Pair_Can_Be_Removed()
        {
            var input = new List<string> { "a", "b", "c" };
            var pairs = new List<Tuple<string, string>>();
            input.ForEachPairMutable(ctx =>
            {
                pairs.Add(new Tuple<string, string>(ctx.Lhs, ctx.Rhs));
                if (ctx.Lhs == "a")
                {
                    ctx.RemoveLhs();
                }
            });

            var expected = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("a", "b"), // "a" will be removed here.
                // "a" + "c" not generated because "a" was removed.
                // "b" + "a" not generated because "a" was removed.
                new Tuple<string, string>("b", "c"),
                // "c" + "a" not generated because "a" was removed.
                new Tuple<string, string>("c", "b"),
            };

            Assert.Collection(pairs,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i)
            );
        }

        [Fact]
        public void Lhs_And_Rhs_Of_Pair_Can_Be_Removed()
        {
            var input = new List<string> { "a", "b", "c", "d", "e" };
            var pairs = new List<Tuple<string, string>>();
            input.ForEachPairMutable(ctx =>
            {
                pairs.Add(new Tuple<string, string>(ctx.Lhs, ctx.Rhs));
                if (ctx.Lhs == "b" && ctx.Rhs == "d")
                {
                    ctx.RemoveBoth();
                }
            });

            var expected = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("a", "b"),
                new Tuple<string, string>("a", "c"),
                new Tuple<string, string>("a", "d"),
                new Tuple<string, string>("a", "e"),
                new Tuple<string, string>("b", "a"),
                new Tuple<string, string>("b", "c"),
                new Tuple<string, string>("b", "d"), // "b" and "d" will be removed here
                // "b" + "e" skipped because "b" was removed
                new Tuple<string, string>("c", "a"),
                new Tuple<string, string>("c", "e"),
                // all "d" pairs skipped because "d" was removed.
                new Tuple<string, string>("e", "a"),
                new Tuple<string, string>("e", "c"),
            };

            Assert.Collection(pairs,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i),
                i => Assert.Equal(expected[3], i),
                i => Assert.Equal(expected[4], i),
                i => Assert.Equal(expected[5], i),
                i => Assert.Equal(expected[6], i),
                i => Assert.Equal(expected[7], i),
                i => Assert.Equal(expected[8], i),
                i => Assert.Equal(expected[9], i),
                i => Assert.Equal(expected[10], i)
            );
        }

        [Fact]
        public void Lhs_Can_Be_Replaced()
        {
            var input = new List<string> { "a", "b", "c", "d" };
            var pairs = new List<Tuple<string, string>>();
            input.ForEachPairMutable(ctx =>
            {
                pairs.Add(new Tuple<string, string>(ctx.Lhs, ctx.Rhs));
                if (ctx.Lhs == "b")
                {
                    ctx.ReplaceLhs("x");
                }
            });

            var expected = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("a", "b"),
                new Tuple<string, string>("a", "c"),
                new Tuple<string, string>("a", "d"),
                new Tuple<string, string>("b", "a"), // "b" replaced with "x" here
                // Skip remaining "b" pairs, because "b" was replaced
                new Tuple<string, string>("c", "a"),
                new Tuple<string, string>("c", "x"),
                new Tuple<string, string>("c", "d"),
                new Tuple<string, string>("d", "a"),
                new Tuple<string, string>("d", "x"),
                new Tuple<string, string>("d", "c"),
                new Tuple<string, string>("x", "a"),
                new Tuple<string, string>("x", "c"),
                new Tuple<string, string>("x", "d"),
            };

            Assert.Collection(pairs,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i),
                i => Assert.Equal(expected[3], i),
                i => Assert.Equal(expected[4], i),
                i => Assert.Equal(expected[5], i),
                i => Assert.Equal(expected[6], i),
                i => Assert.Equal(expected[7], i),
                i => Assert.Equal(expected[8], i),
                i => Assert.Equal(expected[9], i),
                i => Assert.Equal(expected[10], i),
                i => Assert.Equal(expected[11], i),
                i => Assert.Equal(expected[12], i)
            );
        }

        [Fact]
        public void Rhs_Can_Be_Replaced()
        {
            var input = new List<string> { "a", "b", "c", "d" };
            var pairs = new List<Tuple<string, string>>();
            input.ForEachPairMutable(ctx =>
            {
                pairs.Add(new Tuple<string, string>(ctx.Lhs, ctx.Rhs));
                if (ctx.Lhs == "b" && ctx.Rhs == "c")
                {
                    ctx.ReplaceRhs("x");
                }
            });

            var expected = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("a", "b"),
                new Tuple<string, string>("a", "c"),
                new Tuple<string, string>("a", "d"),
                new Tuple<string, string>("b", "a"),
                new Tuple<string, string>("b", "c"), // "c" replaced with "x" here
                new Tuple<string, string>("b", "d"),
                new Tuple<string, string>("x", "a"),
                new Tuple<string, string>("x", "b"),
                new Tuple<string, string>("x", "d"),
                new Tuple<string, string>("d", "a"),
                new Tuple<string, string>("d", "b"),
                new Tuple<string, string>("d", "x")
            };

            Assert.Collection(pairs,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i),
                i => Assert.Equal(expected[3], i),
                i => Assert.Equal(expected[4], i),
                i => Assert.Equal(expected[5], i),
                i => Assert.Equal(expected[6], i),
                i => Assert.Equal(expected[7], i),
                i => Assert.Equal(expected[8], i),
                i => Assert.Equal(expected[9], i),
                i => Assert.Equal(expected[10], i),
                i => Assert.Equal(expected[11], i)
            );
        }

        [Fact]
        public void Complex_Test()
        {
            var input = new List<int> { 3, -5, 97, 44, 99, 105, 1, 12 };

            input.ForEachPairMutable(ctx =>
            {
                if ((ctx.Lhs + ctx.Rhs) % 100 == 0)
                {
                    if (ctx.Lhs > ctx.Rhs)
                    {
                        ctx.ReplaceLhs(100);
                        ctx.RemoveRhs();
                    }
                    else
                    {
                        ctx.ReplaceRhs(100);
                        ctx.RemoveLhs();
                    }
                }
            });

            var expected = new List<int>
            {
                44, 100, 12
            };

            Assert.Collection(input,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i)
            );
        }

        [Fact]
        public void Calling_RemoveLhs_Multiple_Times_Only_Removes_The_Item_Once()
        {
            var input = new List<string> { "a", "b", "c" };
            input.ForEachPairMutable(ctx =>
            {
                if (ctx.Lhs == "b")
                {
                    ctx.RemoveLhs();
                    ctx.RemoveLhs();
                }
            });
            
            Assert.Collection(input,
                i => Assert.Equal("a", i),
                i => Assert.Equal("c", i)
            );
        }

        [Fact]
        public void Calling_RemoveRhs_Multiple_Times_Only_Removes_The_Item_Once()
        {
            var input = new List<string> { "a", "b", "c" };
            input.ForEachPairMutable(ctx =>
            {
                if (ctx.Rhs == "b")
                {
                    ctx.RemoveRhs();
                    ctx.RemoveRhs();
                }
            });

            Assert.Collection(input,
                i => Assert.Equal("a", i),
                i => Assert.Equal("c", i)
            );
        }

        [Fact]
        public void Calling_ReplaceLhs_Multiple_Times_Replaces_The_Item_With_The_Last_Value()
        {
            var input = new List<string> { "a", "b", "c" };
            input.ForEachPairMutable(ctx =>
            {
                if (ctx.Lhs == "b")
                {
                    ctx.ReplaceLhs("x");
                    ctx.ReplaceLhs("y");
                }
            });

            Assert.Collection(input,
                i => Assert.Equal("a", i),
                i => Assert.Equal("y", i),
                i => Assert.Equal("c", i)
            );
        }

        [Fact]
        public void Calling_ReplaceRhs_Multiple_Times_Replaces_The_Item_With_The_Last_Value()
        {
            var input = new List<string> { "a", "b", "c" };
            input.ForEachPairMutable(ctx =>
            {
                if (ctx.Rhs == "b")
                {
                    ctx.ReplaceRhs("x");
                    ctx.ReplaceRhs("y");
                }
            });

            Assert.Collection(input,
                i => Assert.Equal("a", i),
                i => Assert.Equal("y", i),
                i => Assert.Equal("c", i)
            );
        }

        [Fact]
        public void Calling_RemoveLhs_Then_ReplaceLhs_Throws_An_Exception()
        {
            var input = new List<string> { "a", "b", "c" };

            Assert.Throws<InvalidOperationException>(() =>
            {
                input.ForEachPairMutable(ctx =>
                {
                    if (ctx.Lhs == "b")
                    {
                        ctx.RemoveLhs();
                        ctx.ReplaceLhs("x");
                    }
                });
            });
        }

        [Fact]
        public void Calling_RemoveRhs_Then_ReplaceRhs_Throws_An_Exception()
        {
            var input = new List<string> { "a", "b", "c" };

            Assert.Throws<InvalidOperationException>(() =>
            {
                input.ForEachPairMutable(ctx =>
                {
                    if (ctx.Rhs == "b")
                    {
                        ctx.RemoveRhs();
                        ctx.ReplaceRhs("x");
                    }
                });
            });
        }

        [Fact]
        public void Calling_Remove_Twice_Removes_The_Correct_Items()
        {
            var input = new List<string> { "a", "b", "c" };
            
            input.ForEachPairMutable(ctx =>
            {
                if (ctx.Lhs == "a" && ctx.Rhs == "b")
                {
                    ctx.RemoveLhs();
                    ctx.RemoveRhs();
                }
            });

            Assert.Collection(input,
                i => Assert.Equal("c", i)
            );
        }
        [Fact]
        public void The_Last_Item_Can_Be_Removed()
        {
            var input = new List<string> { "a", "b", "c" };

            input.ForEachPairMutable(ctx =>
            {
                if (ctx.Lhs == "a" && ctx.Rhs == "c")
                {
                    ctx.RemoveRhs();
                }
            });

            Assert.Collection(input,
                i => Assert.Equal("a", i),
                i => Assert.Equal("b", i)
            );
        }
    }
}