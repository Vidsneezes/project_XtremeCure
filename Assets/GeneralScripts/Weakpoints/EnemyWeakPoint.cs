using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWeakPoint : MonoBehaviour
{
    public UnityEvent onCallPoint;

   public void CallPoint()
    {
        onCallPoint.Invoke();
    }
}
