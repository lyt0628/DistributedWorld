using System;
using System.Collections.Generic;

namespace QS.GameLib.Pattern
{
    /// <summary>
    /// A resources that are accessed by activeRecord Pattern.
    /// </summary>
    /// <typeparam name="T">The actual type of the Record</typeparam>
    interface IActiveRepo<T, R>
        where R : IActiveRecord<T>
    {

        /// <summary>
        /// Find a record by unique id and return it.
        /// </summary>
        /// <param name="id"></param>
        R Find(Predicate<T> condition);

        ICollection<R> Where(Predicate<T> condition);

        IList<R> Order(IComparer<T> comparer);

        R Create();

        void DestroyAll(Predicate<T> condition);

    }
}