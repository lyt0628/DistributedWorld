
using System;
using GameLib;
using QS.API;
using UnityEngine;

class PlayerManager : IPlayerManager
{
    public PlayerManager()
    {
        Debug.LogWarning("PlayerManager");
    }
    public ManagerStatus Status => throw new System.NotImplementedException();

    private readonly IMessager _messager = new Messager();
    public IMessager Messager => _messager;

    private GameObject _activedCharacter;
    public GameObject GetActivedCharacter()
    {
        return _activedCharacter;
    }

    public void RegisterCharacter(GameObject character)
    {
        if (character == null)
        {
            throw new Exception("Character cannot be null");
        }
        _activedCharacter = character;
        Debug.Log("Actived Character" + character);
        Messager.Boardcast("ActivedCharacterChanged", null);
    }


    public void Startup()
    {

    }

    public void Update()
    {
    }
}