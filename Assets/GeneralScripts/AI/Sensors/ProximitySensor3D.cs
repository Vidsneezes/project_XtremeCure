using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor3D : MonoBehaviour
{
    public float width;
    public float length;
    public float maxRelativeHeight;
    public bool eventSensor;

    float coolOff = 0;

    private void Awake()
    {
        if (!eventSensor)
        {
            AIBlackboard.instance.sensors.Add(this);
        }
    }

    public void TriggerSensor()
    {
        if (!eventSensor)
        {
            if (Time.time - coolOff > 4)
            {
                Debug.Log("sensor triggered");
                coolOff = Time.time;
                AIBlackboard.instance.Sensor_Scan3DCube(transform.position, width, length, maxRelativeHeight);
            }
        }
        else
        {
            Debug.Log("event sensor triggered");
            AIBlackboard.instance.Sensor_Scan3DCube(transform.position, width, length, maxRelativeHeight);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * maxRelativeHeight * 0.5f);
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * maxRelativeHeight * 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(width, 0.2f, length));
    }
}
