

using GameLib;
using QS.API;
using UnityEngine;

class PlayerManager : IPlayerManager
{
    public ManagerStatus Status => throw new System.NotImplementedException();

    private IMessager _messager = new Messager();
    public IMessager Messager => _messager;

    private GameObject _activedCharacter;
    public GameObject GetActivedCharacter()
    {
        return _activedCharacter;
    }

    public void RegisterCharacter(GameObject character)
    {
        _activedCharacter = character;
        Messager.Boardcast("ActivedCharacterChanged", null);
    }


    public void Startup()
    {

    }

    public void Update()
    {
    }
}