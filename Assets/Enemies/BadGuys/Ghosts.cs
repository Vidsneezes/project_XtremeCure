using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghosts : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GhostStates ghostStates;
    public Transform rootTransform;

    float timer;

    public enum GhostStates
    {
        Idle,
        Charging
    }

    void Start()
    {
        ghostStates = GhostStates.Idle;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(ghostStates)
        {
            case GhostStates.Idle:
                if(BadGuy.LineToPlayer(transform.position,35f) && Time.time - timer > 4)
                {
                    BadGuy.FollowPlayer(navMeshAgent);
                    ghostStates = GhostStates.Charging;
                }
                break;
            case GhostStates.Charging:
                if(navMeshAgent.remainingDistance < 1)
                {
                    timer = Time.time;
                    ghostStates = GhostStates.Idle;
                }

                BadGuy.HitPlayer(transform.position, navMeshAgent.velocity.normalized);
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
}
