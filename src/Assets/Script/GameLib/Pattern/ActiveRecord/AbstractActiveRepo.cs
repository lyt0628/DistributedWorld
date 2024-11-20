


using GameLib.DI.Pattern;
using System;
using System.Collections.Generic;

namespace GameLib.Pattern
{
  
   public abstract class AbstractActiveRepo<T> : IActiveRepo<T> where T : IActiveRecord
   {
               
       public abstract T  Create();

       public abstract void DestroyAll(Predicate<T> condition);

       public abstract T Find(int id);

       public abstract IList<T>  Order(IComparer<T> comparer);

       public abstract ICollection<T> Where(Predicate<T> condition);
            
   }

}