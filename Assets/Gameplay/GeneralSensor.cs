using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneralSensor : MonoBehaviour
{
    public bool oneTimeSensor;
    public bool eventSensor;

    public UnityEvent onSensorTriggered;

    public void OnSensorTriggered()
    {
        onSensorTriggered.Invoke();

        if(oneTimeSensor)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void EmitParticles()
    {
        FpsControllerCore.instance.EmitExplosion(transform.position + FpsControllerCore.instance.transform.forward );
    }
}
