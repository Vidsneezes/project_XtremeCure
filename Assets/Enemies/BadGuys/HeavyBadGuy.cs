using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBadGuy : MonoBehaviour
{
    public Transform rootTransform;
    public BadGuyProjectile prefab_projectile;

    public List<Transform> eyeSights;

    public float shootTime;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timer > shootTime)
        {

            timer = Time.time;
            ShootProjectile(eyeSights[0].position, 90);
            ShootProjectile(eyeSights[0].position, 210);
            ShootProjectile(eyeSights[0].position, 300);
        }
    }

    void ShootProjectile(Vector3 position, float angle)
    {
        BadGuyProjectile bgp = GameObject.Instantiate(prefab_projectile, position, Quaternion.identity);
        bgp.Launch(BadGuy.DirectionUpCircleShellPlayer(transform.position,0.5f,angle), 10);
    }

    void LateUpdate()
    {
        rootTransform.rotation = Quaternion.LookRotation(rootTransform.position - FpsControllerCore.instance.fpsCamera.transform.position);
    }
}
