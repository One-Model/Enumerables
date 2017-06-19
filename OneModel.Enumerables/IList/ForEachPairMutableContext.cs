using System;
using System.Collections.Generic;

namespace OneModel.Enumerables
{
    public class ForEachPairMutableContext<T> : IForeachPairMutableContext<T>
    {
        private readonly Action<int, T> _replaceCallback;
        private readonly Action<int> _removeCallback;
        private bool _lhsRemoved;
        private bool _rhsRemoved;

        public ForEachPairMutableContext(
            IList<T> source, 
            int lhsIndex, 
            int rhsIndex, 
            Action<int, T> replaceCallback, 
            Action<int> removeCallback)
        {
            Lhs = source[lhsIndex];
            Rhs = source[rhsIndex];
            LhsIndex = lhsIndex;
            RhsIndex = rhsIndex;
            _replaceCallback = replaceCallback;
            _removeCallback = removeCallback;
        }

        public int LhsIndex { get; }

        public int RhsIndex { get; }

        public T Lhs { get; }

        public T Rhs { get; }

        public void ReplaceLhs(T newLhs)
        {
            if (_lhsRemoved)
            {
                throw new InvalidOperationException("Can't replace the LHS of the pair - the item has been removed from the source list.");
            }

            var index = GetAdjustedIndex(LhsIndex);
            _replaceCallback(index, newLhs);
        }

        public void ReplaceRhs(T newRhs)
        {
            if (_rhsRemoved)
            {
                throw new InvalidOperationException("Can't replace the RHS of the pair - the item has been removed from the source list.");
            }

            var index = GetAdjustedIndex(RhsIndex);
            _replaceCallback(index, newRhs);
        }

        public void RemoveLhs()
        {
            if (!_lhsRemoved)
            {
                var index = GetAdjustedIndex(LhsIndex);
                _removeCallback(index);
                _lhsRemoved = true;
            }
        }

        public void RemoveRhs()
        {
            if (!_rhsRemoved)
            {
                var index = GetAdjustedIndex(RhsIndex);
                _removeCallback(index);
                _rhsRemoved = true;
            }
        }

        public void RemoveBoth()
        {
            RemoveLhs();
            RemoveRhs();
        }

        private int GetAdjustedIndex(int i)
        {
            if (_lhsRemoved & i > LhsIndex)
            {
                i--;
            }

            if (_rhsRemoved && i > RhsIndex)
            {
                i--;
            }

            return i;
        }
    }
}