using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal destination;
    public Transform localEntrance;

    [HideInInspector]
    public int frameCool = -1;

    public void EnterPortal()
    {
        if (frameCool < 0)
        {
            FpsControllerCore.instance.TeleportPlayer(destination.localEntrance.position, destination.localEntrance.forward);
            frameCool = 4;
            destination.frameCool = 4;
        }
    }

    private void Update()
    {
        if(frameCool >= 0)
        {
            frameCool -= 1;
        }
    }

    private void OnDrawGizmos()
    {
        if (destination != null)
        {
            Gizmos.DrawLine(transform.position, destination.transform.position);
        }
    }
}
