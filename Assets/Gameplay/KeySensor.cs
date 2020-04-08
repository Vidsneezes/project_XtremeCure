using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeySensor : MonoBehaviour
{
    public UnityEvent onHasKey;

    public void CheckKeys()
    {
        if(FpsControllerCore.instance.hasRedKey)
        {
            onHasKey.Invoke();
            GameObject.Destroy(gameObject);
            FpsControllerCore.instance.redKeyUI.enabled = false;
        }
    }
}
