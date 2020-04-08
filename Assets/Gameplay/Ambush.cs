using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambush : MonoBehaviour
{
    bool opening = false;
    public ProximitySensor3D proxiSensor;
    float waitTimer;
    bool sensorCalled;

    private void Awake()
    {

    }

    public void AmbushTriggered()
    {
        if(!opening)
        {
            sensorCalled = false;
            opening = true;
            waitTimer = Time.time;
        }
    }

    private void Update()
    {
        if(opening)
        {
            transform.position += Vector3.down * 5 * Time.deltaTime;
            if(!sensorCalled && Time.time - waitTimer > 0.8f)
            {
                sensorCalled = true;
                proxiSensor.TriggerSensor();
            }

            if(transform.position.y < -20)
            {
                opening = false;
            }
        }
    }
}
