using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



namespace QS.API.Data.Model
{
    public interface IItem 
    {
        string UUID { get; set; }
        string Name { get; set; }
        ItemType Type { get; set; }
        
    }
}
