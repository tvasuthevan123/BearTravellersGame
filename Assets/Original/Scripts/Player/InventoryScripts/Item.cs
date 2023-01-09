using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    public TextAsset dialogue;

    public virtual void Use(){
        Debug.Log(name + " was used");
    }
}
