using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadGuy : MonoBehaviour
{
    public enum BadGuyStates
    {
        Idle,
        SeekingPlayer,
        ShotPlayer
    }

    public NavMeshAgent navMeshAgent;
    public Transform eyeSight;
    public BadGuyProjectile prefab_projectile;
    public Transform rootTransform;
    public float fireRate;
    public float followPrecision;
    public float coolDown;

    public BadGuyStates badGuyStates;
    Vector3 lastPosition;
    float coolTimer;
    float fireRateTimer;
    float followTimer;

    // Start is called before the first frame update
    void Start()
    {
        badGuyStates = BadGuyStates.Idle;
        lastPosition = transform.position;
        coolTimer = 0;
        followTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(badGuyStates)
        {
            case BadGuyStates.Idle:
                if(BadGuy.LineToPlayer(eyeSight.position))
                {
                    badGuyStates = BadGuyStates.SeekingPlayer;
                    lastPosition = transform.position;
                }
                break;
            case BadGuyStates.SeekingPlayer:

                if (Time.time - followTimer > followPrecision)
                {
                    followTimer = Time.time;
                    BadGuy.FollowPlayerShell(navMeshAgent, 3, Random.Range(0, 360));
                }
                if (Time.time - fireRateTimer > fireRateTimer)
                {
                    if (BadGuy.LineToPlayer(eyeSight.position, 10))
                    {
                        navMeshAgent.isStopped = true;
                        badGuyStates = BadGuyStates.ShotPlayer;
                        coolTimer = Time.time;
                        BadGuy.LaunchProjectile2D(prefab_projectile, eyeSight.position);
                        fireRateTimer = Time.time;
                    }
                }
                break;
            case BadGuyStates.ShotPlayer:
                if(Time.time - coolTimer < coolDown)
                {
                    navMeshAgent.isStopped = false;

                    navMeshAgent.SetDestination(lastPosition + Vector3.right * Random.Range(-3,3));
                }else
                {
                    badGuyStates = BadGuyStates.Idle;
                }
                break;
        }
    }

    void LateUpdate()
    {
        rootTransform.rotation = Quaternion.LookRotation(rootTransform.position - FpsControllerCore.instance.fpsCamera.transform.position);
    }

    public void Kill()
    {
        GameObject.Destroy(gameObject);
    }

    public static bool LineToPlayer(Vector3 position, float distance = 30)
    {
        Vector3 direction = FpsControllerCore.instance.transform.position - position;
        direction.y = 0;

        return Physics.Raycast(position,direction,distance,1 << 9);
    }

    public static void FollowPlayer(NavMeshAgent agent)
    {
        agent.SetDestination(FpsControllerCore.instance.transform.position);
    }

    public static void FollowPlayerShell(NavMeshAgent agent, float radius, float degAngle)
    {
        agent.SetDestination(BadGuy.CircleShellPlayer(radius,degAngle));
    }

    public static Vector3 DirectionToPlayer2D(Vector3 position)
    {
        Vector3 direction = FpsControllerCore.instance.transform.position - position;
        direction.y = 0;
        return direction.normalized;
    }

    public static Vector3 DirectionToPlayer(Vector3 position)
    {
        Vector3 direction = FpsControllerCore.instance.transform.position - position;
        return direction.normalized;
    }

    public static Vector3 DirectionUpCircleShellPlayer(Vector3 startPosition, float radius)
    {
        Vector3 endPosition = UpCirlceShellPlayer(radius);

        return (endPosition - startPosition).normalized;

    }

    public static Vector3 DirectionUpCircleShellPlayer(Vector3 startPosition, float radius, float angle)
    {
        Vector3 endPosition = UpCirlceShellPlayerFixed(radius, angle);

        return (endPosition - startPosition).normalized;

    }

    public static Vector3 UpCirlceShellPlayer(float radius)
    {
        float radAngle = Mathf.Deg2Rad * Random.Range(0f, 360f);
        Vector3 position = FpsControllerCore.instance.transform.position;

        position.x += radius * Mathf.Cos(radAngle);
        position.y += radius * Mathf.Sin(radAngle);

        return position;

    }

    public static Vector3 UpCirlceShellPlayerFixed(float radius, float angle = 90)
    {
        float radAngle = Mathf.Deg2Rad * angle;
        Vector3 position = FpsControllerCore.instance.transform.position;

        position.x += radius * Mathf.Cos(radAngle);
        position.y += radius * Mathf.Sin(radAngle);

        return position;

    }

    public static Vector3 CircleShellPlayer(float radius, float degAngle)
    {
        float radAngle = degAngle * Mathf.Deg2Rad;

        Vector3 shellPoint = FpsControllerCore.instance.transform.position;

        shellPoint.x += Mathf.Cos(radAngle) * radius;
        shellPoint.z += Mathf.Sin(radAngle) * radius;

        return shellPoint;
    }

    public static Vector3 CircleShellPlayerRelative(float radius, float degAngle)
    {

        float radAngle = degAngle * Mathf.Deg2Rad;

        Vector3 shellPoint = FpsControllerCore.instance.transform.position;

        float playerAngle = Mathf.Acos(shellPoint.x / radius);

        float finalAngle = playerAngle + radAngle;

        shellPoint.x += Mathf.Cos(finalAngle) * radius;
        shellPoint.z += Mathf.Sin(finalAngle) * radius;

        return shellPoint;
    }

    public static void HitPlayer(Vector3 position, Vector3 direction, float damage = 2, float length = 1)
    {
        RaycastHit rayHit;

        if (PlayerProjectile.HitRayDirection(position, direction, length, 1 << 9, out rayHit))
        {
            FpsControllerCore.instance.HitParticle(rayHit.point + rayHit.normal * 0.05f, rayHit.normal);
            Debug.Log("hit player");
            FpsControllerCore.instance.TakeDamage((int)damage);

        }
    }

    public static void LaunchProjectile2D(BadGuyProjectile prefab, Vector3 launchPosition)
    {
        BadGuyProjectile bgp = GameObject.Instantiate(prefab, launchPosition, Quaternion.identity);
        bgp.Launch(BadGuy.DirectionToPlayer2D(launchPosition), 10);
    }

    public static void LaunchProjectile(BadGuyProjectile prefab, Vector3 launchPosition)
    {
        BadGuyProjectile bgp = GameObject.Instantiate(prefab, launchPosition, Quaternion.identity);
        bgp.Launch(BadGuy.DirectionToPlayer(launchPosition), 10);
    }
}
