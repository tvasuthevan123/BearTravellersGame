using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Weapon gun;
    public Consumable medkit;

    public Consumable medkitTest;

    void Start()
    {
        medkit = ScriptableObject.CreateInstance<Consumable>();
        medkit.name = "Medkit";
    }

    public void Update()
    {
    }

    public void AddItem(Item newItem)
    {
        if (newItem.name == "Gun")
            gun = newItem as Weapon;
        else{
            Consumable consumable = newItem as Consumable;
            if (consumable.type == ConsumableType.Medkit)
                medkit.itemCount++;
            else if(consumable.type == ConsumableType.Ammo && gun != null)
                gun.magCount += consumable.itemCount;

        }
    }

    public void RemoveMedkit()
    {
        if (medkit == null)
            return;

        if (medkit.itemCount > 1)
            medkit.itemCount--;
        else
            medkit.itemCount = 0;
    }
}
