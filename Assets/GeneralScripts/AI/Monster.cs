using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum HealthyState
    {
        Healthy,
        Staggered,
        Stunned,
        LastStand,
        Dead
    }

    public MonsterBehavior monsterBehavior;
    public HealthyState healthyState;
    public Transform rootTransform;

    public Texture healthyTexture;
    public Texture staggeredTexture;

    public GameObject deathPile;

    public MeshRenderer billboardRender;

    public Vector3 CenterPosition
    {
        get
        {
            return rootTransform.position;
        }
    }

    [Range(0,10)]
    public float defense;

    public float Defense
    {
        get
        {
            return 1 - (defense / 10f);
        }
    }

    public float bleedDPS = 0.03f;
    public float bleedRecover = 0.3f;

    public float staggerRecovery = 3;
    public float staggerShotCap = 1.2f;

    float life = 1;
    float bleedTimer = 0;
    float staggeredLife = 1;
    float staggerTime = 0;

    private void Awake()
    {
        AIBlackboard.instance.monsters.Add(this);
        if(monsterBehavior != null)
        {
            monsterBehavior.BeginBehavior();
        }

        billboardRender.material.SetTexture("_mainTexture", healthyTexture);
    }

    private void OnDestroy()
    {
        AIBlackboard.instance.monsters.Remove(this);
    }

    public void OnHurt(float damage)
    {
        if(healthyState == HealthyState.LastStand)
        {
            ToDeadState();
        }
        else if (healthyState == HealthyState.Healthy)
        {
            if(monsterBehavior != null)
            {
                if(!monsterBehavior.IsAware)
                {
                    monsterBehavior.BecomeAware();
                }
            }

            life -= damage * Defense;
            Debug.Log($"Total damage done {damage * Defense}");
            bleedTimer = Time.time;

            if(life < 0)
            {
                ToStaggerState();
            }

        }else if(healthyState == HealthyState.Staggered)
        {
            staggeredLife += Time.deltaTime;
            if(staggeredLife > staggerShotCap)
            {
                ToDeadState();
            }
        }
    }

    public void GloryKill()
    {
        ToDeadState();
    }

    public void ToLastStand()
    {
        healthyState = HealthyState.LastStand;
        billboardRender.material.SetFloat("_outline", 0);
        billboardRender.material.SetTexture("_mainTexture", healthyTexture);
    }

    public void ToDeadState()
    {
        healthyState = HealthyState.Dead;
        Debug.Log("Monster Died");
        AIBlackboard.instance.monsterDead.transform.position = transform.position;
        AIBlackboard.instance.monsterDead.PlayOneShot(AIBlackboard.instance.monsterDead.clip);

        if (deathPile != null)
        {
            GameObject.Instantiate(deathPile, transform.position, Quaternion.identity);   
        }
    }

    public void ToStaggerState()
    {
        Debug.Log("to staggered");
        staggerTime = Time.time;
        staggeredLife = 0;
        healthyState = HealthyState.Staggered;
        billboardRender.material.SetFloat("_outline", 1);
        billboardRender.material.SetTexture("_mainTexture", staggeredTexture);

    }

    private void Update()
    {
        if(healthyState == HealthyState.Healthy)
        {
            if(Time.time - bleedTimer < bleedRecover)
            {
                life -= bleedDPS;
                if(life < 0)
                {
                    ToStaggerState();
                }
            }

            if(monsterBehavior != null)
            {
                monsterBehavior.RunBehavior();
            }
        }
        else if(healthyState == HealthyState.Staggered)
        {
            if(Time.time - staggerTime > staggerRecovery)
            {
                ToLastStand();
            }
        }else if(healthyState == HealthyState.LastStand)
        {
            if (monsterBehavior != null)
            {
                monsterBehavior.RunBehavior();
            }
        }

        if(healthyState == HealthyState.Dead)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        rootTransform.rotation = Quaternion.LookRotation(rootTransform.position - FpsControllerCore.instance.fpsCamera.transform.position);
    }

    #region

    public void BecomeAware()
    {
        if (monsterBehavior != null)
        {
            monsterBehavior.BecomeAware();
        }
    }

    public void RecieveSensor(int sensorTag)
    {
        if(monsterBehavior != null)
        {
            monsterBehavior.RecieveSensor(sensorTag);
        }
    }

    #endregion


}
