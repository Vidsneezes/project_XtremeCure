using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionSensor : MonoBehaviour
{
    public UnityEvent onInteracted;
    public bool oneTime;


    bool beenInteractedWith;

    int frameSkip = -1;

    public void OnInteractionTriggered()
    {

        if(beenInteractedWith && oneTime)
        {
            return;
        }

        if(frameSkip < 0)
        {
            beenInteractedWith = true;
            onInteracted.Invoke();
            frameSkip = 3;
        }
    }

    private void FixedUpdate()
    {
        if(frameSkip >= 0)
        {
            frameSkip -= 1;
        }
    }

    public void PushX(float amount)
    {
        transform.position += Vector3.right * amount;
    }
}
