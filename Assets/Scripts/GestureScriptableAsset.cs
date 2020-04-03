using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GestureData", menuName = "ScriptableObjects/GestureScriptableAsset")]
public class GestureScriptableAsset : ScriptableObject
{
    public GestureScriptableAsset(string name, Handiness handiness, List<Vector3> fingerPositions)
    {
        Name = name;
        Hand = handiness;
        FingerPositions = fingerPositions;
    }

    public enum Handiness { Right, Left }

    public string Name;
    public Sprite Image;
    public Handiness Hand; 
    public List<Vector3> FingerPositions;
}
