using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.UnitTests.IEnumerable
{
    public class FlattenExtensionsTests
    {
        [Fact]
        public void Flatten_Flattens_Tree_Structure()
        {
            var child1 = new ItemWithChildren("c1", new List<ItemWithChildren>());
            var child2 = new ItemWithChildren("c2", new List<ItemWithChildren>());
            var child3 = new ItemWithChildren("c3", new List<ItemWithChildren>());
            var child4 = new ItemWithChildren("c4", new List<ItemWithChildren>());

            var parent1 = new ItemWithChildren("p1", new List<ItemWithChildren> { child1, child2 });
            var parent2 = new ItemWithChildren("p2", new List<ItemWithChildren> { child3, child4 });
            var parents = new List<ItemWithChildren> { parent1, parent2 };

            var actual = parents.Flatten(parent => parent.Children);

            var exepected = new List<ItemWithChildren> { parent2, child4, child3, parent1, child2, child1 };
            Assert.Equal(exepected, actual);
        }

        private class ItemWithChildren
        {
            public ItemWithChildren(string id, IEnumerable<ItemWithChildren> children)
            {
                Id = id;
                Children = new List<ItemWithChildren>(children);
            }

            private string Id { get; }

            public IReadOnlyList<ItemWithChildren> Children { get; }

            public override string ToString()
            {
                return Id;
            }
        }
    }
}
