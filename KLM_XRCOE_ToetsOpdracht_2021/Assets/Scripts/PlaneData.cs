using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaneData")] // scriptable object containing plane data
public class PlaneData : ScriptableObject
{ 
    public string brand;
    public string type;
    public Material material;
}
