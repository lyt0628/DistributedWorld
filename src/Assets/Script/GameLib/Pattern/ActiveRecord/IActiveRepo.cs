


using GameLib.Pattern;
using System;
using System.Collections.Generic;

namespace GameLib.DI.Pattern
{
    /// <summary>
    /// A resources that are accessed by activeRecord Pattern.
    /// </summary>
    /// <typeparam name="T">The actual type of the Record</typeparam>
    interface IActiveRepo< T> where T : IActiveRecord
    {

        /// <summary>
        /// Find a record by unique id and return it.
        /// </summary>
        /// <param name="id"></param>
        T Find(int id);

        ICollection<T> Where(Predicate<T> condition);

        IList<T> Order(IComparer<T> comparer);

        T Create();

        void DestroyAll(Predicate<T> condition);

    }
}