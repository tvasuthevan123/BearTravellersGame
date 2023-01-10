using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendChoiceTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        int friendshipMeter = ((Ink.Runtime.IntValue)StoryManager.instance.GetVariableState("friendship_meter")).value;
        if (other.gameObject.tag == "Player")
        {
            if (friendshipMeter >= 0)
            {
                Destroy(GameObject.FindWithTag("friendchoicepos"));
            }
            else if (friendshipMeter < 0)
            {
                Destroy(GameObject.FindWithTag("friendchoiceneg"));
            }
        }
    }
}
