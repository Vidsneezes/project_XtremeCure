using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObstacle : MonoBehaviour
{
    bool opening;

    public void OpenDoor()
    {
        opening = true;
    }
    // Start is called before the first frame update
    void Awake()
    {
        opening = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(opening)
        {
            transform.position += Vector3.down * 3 * Time.deltaTime;
            if(transform.position.y < -30)
            {
                opening = false;
            }
        }
    }
}
