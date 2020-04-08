using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbius : MonoBehaviour
{
    public Transform rootTransform;
    public Path path;
    public BadGuyProjectile prefab_projectile;
    public float completeEdgeTime;

    Vector3 velocity;
    float timer = 0;

    int currentIndex;
    int nextIndex;
    float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
        timer = 0;
        currentIndex = 0;
        nextIndex = 1;
        shootTimer = Time.time;
    }

    private void Update()
    {
        float normalizeElapse = (Time.time - timer) / completeEdgeTime;

        transform.position = path.LerpNext(currentIndex, nextIndex, normalizeElapse);

        if (normalizeElapse > 1)
        {
            timer = Time.time;

            currentIndex += 1;
            if (currentIndex >= path.points.Count)
            {
                currentIndex = 0;
            }

            nextIndex = currentIndex + 1;
            if (nextIndex >= path.points.Count)
            {
                nextIndex = 0;
            }
        }


        if(Time.time - shootTimer > 1.2f)
        {
            BadGuy.LaunchProjectile(prefab_projectile, transform.position);
            shootTimer = Time.time;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rootTransform.rotation = Quaternion.LookRotation(rootTransform.position - FpsControllerCore.instance.fpsCamera.transform.position);
    }

    public void Kill()
    {
        GameObject.Destroy(gameObject);
    }
}
