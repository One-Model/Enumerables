using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OneModel.Enumerables.Tests.IEnumerable
{
    public class ProjectExtensionsTests
    {
        [Fact]
        public void Project_Filters_Out_Default_Values()
        {
            var enumerable = new List<int> { 1, 2 };

            var actual = enumerable.Project(item => item == 2 ? null : item.ToString());

            var expected = new List<string> { "1" };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProjectMany_Filters_Out_Default_Values()
        {
            var enumerable = new List<int[]>
            {
                new[] { 1, 2 },
                new [] { 3, 4 }
            };

            var actual = enumerable.ProjectMany(itemArray =>
                itemArray.Select(item => item == 2 ? null : item.ToString()));

            var expected = new List<string> { "1", "3", "4" };
            Assert.Equal(expected, actual);
        }
    }
}
