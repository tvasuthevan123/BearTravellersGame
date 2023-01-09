using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Items/Weapon")]
public class Weapon : Item
{
    public GameObject prefab;
    public int magSize;
    public int magCount;
    public float range;
}
