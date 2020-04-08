using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRestoreConsumable : MonoBehaviour
{
    public float lifePercentRecover;

    [HideInInspector]
    public bool usedUp = false;

    public void OnUsedUp()
    {
        usedUp = true;
    }

    void Update()
    {
        if(usedUp)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
