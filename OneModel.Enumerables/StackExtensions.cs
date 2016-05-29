using System;
using System.Collections.Generic;

namespace OneModel.Enumerables
{
    /// <summary>
    /// Extensions to System.Collections.Generic.Stack&lt;T&gt;
    /// </summary>
    public static class StackExtensions
    {
        /// <summary>
        /// Pushes an object onto a stack. When the returned IDisposable is disposed,
        /// the object is popped off the stack. Intended for use with the using()
        /// statement, like so:
        /// 
        /// using (stack.PushContext("SomeValue")){
        ///     // Do stuff.
        /// }
        /// 
        /// </summary>
        public static IDisposable PushContext<T>(this Stack<T> source, T item)
        {
            var disposable = new PushContextDisposable<T>(source);
            source.Push(item);
            return disposable;
        }

        internal class PushContextDisposable<T> : IDisposable
        {
            private readonly Stack<T> _source;

            public PushContextDisposable(Stack<T> source)
            {
                _source = source;
            }

            public void Dispose()
            {
                _source.Pop();
            }
        }
    }
}
