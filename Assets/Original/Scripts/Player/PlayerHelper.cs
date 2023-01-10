using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerHelper : MonoBehaviour
{
    public void ToggleInput(){
        FirstPersonController fpsController = GetComponentInChildren<FirstPersonController>();
        fpsController.disableInput = !fpsController.disableInput;
    }
}
