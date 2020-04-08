using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyProjectile : MonoBehaviour
{
    public Transform root;
    Vector3 velocity;
    float baseDamage = 11;
    float damageModifier = 1;

    float aliveTime;

    bool alive;

    private void Awake()
    {
        alive = true;
        velocity = new Vector3();
    }

    public void Launch(Vector3 direction, float speed)
    {
        velocity = direction * speed;

        aliveTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        transform.position += velocity * Time.deltaTime;


        HitPlayer();

        if (alive)
        {
            HitTerrain();
        }

    }

    private void LateUpdate()
    {
        root.transform.rotation = Quaternion.LookRotation(root.transform.position - FpsControllerCore.instance.fpsCamera.transform.position);
    }

    void HitPlayer()
    {
        RaycastHit rayHit;


        float elapseAlive = Time.time - aliveTime;

        if(elapseAlive < 1.2f)
        {
            damageModifier = 1.4f;
        }else if(elapseAlive < 5)
        {
            damageModifier = 1;
        }else
        {
            damageModifier = 0.2f;
        }


        if (PlayerProjectile.HitRayDirection(transform.position, velocity.normalized, 0.3f, 1 << 9, out rayHit))
        {
            FpsControllerCore.instance.HitParticle(rayHit.point + rayHit.normal * 0.05f, rayHit.normal);
            alive = false;
            FpsControllerCore.instance.TakeDamage(Mathf.FloorToInt(baseDamage * damageModifier));

        }
    }

    void HitTerrain()
    {
        RaycastHit rayHit;

        if (PlayerProjectile.HitRayDirection(transform.position, velocity.normalized, 2, 1 << 0, out rayHit))
        {
            FpsControllerCore.instance.HitParticle(rayHit.point + rayHit.normal * 0.05f, rayHit.normal);
            GameObject.Destroy(gameObject);
        }
        else if (transform.position.y < -16)
        {
            GameObject.Destroy(gameObject);
        }
    }



}
