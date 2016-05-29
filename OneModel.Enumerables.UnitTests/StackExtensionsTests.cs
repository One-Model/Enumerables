using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.UnitTests
{
    public class StackExtensionsTests
    {
        [Fact]
        public void PushContext_Item_Is_Removed_From_Stack_At_End_Of_Using_Statement()
        {
            var stack = new Stack<int>();

            using (stack.PushContext(1))
            {
                Assert.Equal(1, stack.Count);
            }

            Assert.Empty(stack);
        }
    }
}
