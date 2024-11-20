using QS.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTranslationDTO : ICharacterTranslationDTO
{

    public float Speed { get; set; }

    public Vector3 Displacement { get; set; }

    public bool Jumping {  get; set; }
}
