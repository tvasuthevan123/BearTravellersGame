using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedItem = 0;

    public void Start(){
        SelectWeapon();
    }

    public void Update(){

        int currentSelected = selectedItem;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f){
            if(selectedItem >= transform.childCount-1)
                selectedItem = 0;
            else
                selectedItem++;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            if(selectedItem <= 0)
                selectedItem = transform.childCount-1;
            else
                selectedItem--;
        }

        if(selectedItem != currentSelected){
            SelectWeapon();
        }
    }

    void SelectWeapon(){
        int i = 0;
        foreach(Transform weapon in transform){
            if(i == selectedItem)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
