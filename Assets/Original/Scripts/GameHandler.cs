using System.IO;
using UnityEngine;

public class GameHandler : MonoBehaviour{
    private const string SAVE_SEPERATOR = "#SAVE-VALUE";

    void Awake(){
        SaveObject saveObject = new SaveObject();
        string json = JsonUtility.ToJson(saveObject);


    }

    public void Save(){
        
    }

    private class SaveObject{
        public Vector3 position;
        public Vector3 rotation;
        public int ammo;
        public float health;
        public float medkitNumber;
        public bool hasGun;

    }

} 