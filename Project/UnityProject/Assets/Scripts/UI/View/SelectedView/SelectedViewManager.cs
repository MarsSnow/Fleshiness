using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class SelectedViewManager : Singleton<SelectedViewManager>
{
    public List<List<GameObject>> gridList = new List<List<GameObject>>() 
    { 
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
    };
}
