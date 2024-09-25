using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib
{
    public  interface Spawner<T> 
    {
        public T Spawn();
    }

    public class SpawnFor<T,U> : Spawner<T> where U : T, new()
    {
        public T Spawn()
        {
            return new U();
        }

    }
}
