
using UnityEngine;

namespace QS.API
{
     interface IPlayerManager : IGameManager 
    {
        void RegisterCharacter(GameObject character);
        GameObject GetActivedCharacter();
    }
}