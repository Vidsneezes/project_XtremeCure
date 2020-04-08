using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBehavior : MonoBehaviour
{
    public Monster monster;
    public bool IsAware
    {
        get
        {
            return isAware;
        }
    }
    protected bool isAware = false;

    public abstract void BeginBehavior();
    public abstract void RunBehavior();
    public abstract void RecieveSensor(int sensorTag);
    public abstract void BecomeAware();
    
}
