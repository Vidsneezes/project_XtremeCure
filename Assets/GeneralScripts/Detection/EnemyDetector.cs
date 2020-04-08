using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetector : MonoBehaviour
{
    public UnityEvent onDetection;
    public BoxCollider boxCollider;
    public float tickTime;

    float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - counter > tickTime)
        {
            if (Physics.CheckBox(transform.position, boxCollider.bounds.extents, Quaternion.identity, 1 << 9))
            {
                onDetection.Invoke();
                counter = Time.time;
            }
        }
    }
}
