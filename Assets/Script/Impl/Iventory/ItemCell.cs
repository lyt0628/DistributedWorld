
using QS.API;

namespace QS
{
    class ItemCell : IItemCell
    {
        private int _count = 0;

        public IItem Item { get; set; }

        public void Add(int n)
        {
            _count++;
        }

        public int GetCount()
        {
            return _count;
        }
    }
}