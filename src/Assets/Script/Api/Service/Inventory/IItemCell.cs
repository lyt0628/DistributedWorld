



namespace QS.API
{
    interface IItemCell
    {
        IItem Item { get; set; }
        int GetCount();
        void Add(int n);
        void Inc()
        {
            Add(1);
        }
        void Dec()
        {
            Add(-1);
        }

    }
}