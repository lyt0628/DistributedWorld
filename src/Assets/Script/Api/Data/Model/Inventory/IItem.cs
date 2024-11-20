using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace QS.API.Data
{
    public interface IItem 
    {
        string UUID { get; set; }
        string Name { get; set; }
    }
}
