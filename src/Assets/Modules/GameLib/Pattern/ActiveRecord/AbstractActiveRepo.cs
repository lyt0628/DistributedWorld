using System;
using System.Collections.Generic;

namespace QS.GameLib.Pattern
{

    /// <summary>
    /// ActiveRepo is the Object for accessing operations on group of record.
    /// </summary>
    /// <typeparam name="T">ActiveRecord</typeparam>
    public abstract class AbstractActiveRepo<T, R>
        : IActiveRepo<T, R>
        where R : IActiveRecord<T>
    {

        /// <summary>
        /// Create a new ActiveRecord
        /// </summary>
        /// <returns></returns>
        public abstract R Create();

        /// <summary>
        /// Destroy all record by condition
        /// </summary>
        /// <param name="condition"></param>
        public abstract void DestroyAll(Predicate<T> condition);

        /// <summary>
        /// Find Record with id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract R Find(Predicate<T> condition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public abstract IList<R> Order(IComparer<T> comparer);

        public abstract ICollection<R> Where(Predicate<T> condition);
    }

}