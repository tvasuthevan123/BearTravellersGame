using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Items/Consumable")]
public class Consumable : Item
{
    public ConsumableType type;
    public GameObject prefab;
    public int itemCount;
}

public enum ConsumableType { Medkit, Ammo }
