using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float gravity;
    public float speedModifier;
    public BoxCollider boxCollider;
    public Transform root;
    Vector3 velocity;
    public FpsControllerCore fpsControllerCore;

    bool alive;

    private void Awake()
    {
        alive = true;
        velocity = new Vector3();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Launch(Vector3 direction, float speed)
    {
        velocity = direction * speed * speedModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(!alive)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        velocity.y += gravity * Time.deltaTime;
      

        transform.position += velocity * Time.deltaTime;


        HitEnemy();

        if (alive)
        {
            HitTerrain();
        }

    }

    private void LateUpdate()
    {
        if (fpsControllerCore != null)
        {
            root.transform.rotation = Quaternion.LookRotation(root.transform.position - fpsControllerCore.fpsCamera.transform.position);
        }
    }

    void HitEnemy()
    {
        if (alive)
        {
            RaycastHit rayHit;

            if(PlayerProjectile.HitRayDirection(transform.position,velocity.normalized, speedModifier,1 << 10,out rayHit))
            {
                EnemyWeakPoint ewp = rayHit.collider.GetComponent<EnemyWeakPoint>();
                if (ewp != null)
                {
                    fpsControllerCore.HitParticle(rayHit.point + rayHit.normal * 0.05f, rayHit.normal, true);
                    alive = false;
                  //  fpsControllerCore.EmitExplosion(rayHit.collider.transform.position);
                    ewp.CallPoint();
                }
            }
        }
    }

    void HitTerrain()
    {
        RaycastHit rayHit;

        if(PlayerProjectile.HitRayDirection(transform.position,velocity.normalized,speedModifier,1<<0,out rayHit))
        {
            fpsControllerCore.HitParticle(rayHit.point + rayHit.normal * 0.05f, rayHit.normal);
            GameObject.Destroy(gameObject);
        }
        else if(transform.position.y < -16)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void Kill()
    {

    }

    public static bool HitRayDirection(Vector3 center, Vector3 direction, float rayLength, int layerMask, out RaycastHit rayHit)
    {
        if (Physics.Raycast(center - direction * 0.5f * rayLength, direction, out rayHit, 0.5f * rayLength, layerMask))
        {
            return true;
        }
        return false;
    }
}
