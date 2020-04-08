using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FpsControllerCore : MonoBehaviour
{
    public static FpsControllerCore instance;


    const float weaponRayRange = 30;

    public enum WeaponMode
    {
        Manual,
        Automatic
    }

    //Audio
    public AudioSource playerShootAudio;
    public AudioSource gloryKillAudio;
    public AudioSource playerHurtAudio;
    public AudioSource playerRecoverAudio;
    public AudioSource playerTeleportAudio;
    public AudioSource explosionsAudio;
    public AudioSource playerJump;
    public AudioSource playerLand;



    //Rest
    public int life;
    public TextMeshProUGUI lifeText;

    public int points;
    public TextMeshProUGUI pointsText;

    public Canvas GameOverCanvas;
    public TextMeshProUGUI firePlayAgainText;

    public TextMeshProUGUI enterTextMesh;
    public TextMeshProUGUI needRedTextMesh;

    public Image handImageUI;
    public Image punchImageUI;
    public Image redKeyUI;


    public Camera fpsCamera;
    public CharacterController characterController;

    public ParticleSystem ps_weaponBlaster;
    public ParticleSystem ps_hitparticle;

    public ParticleSystem ps_explode;

    public PlayerProjectile prefab_projectile;
    public PlayerProjectile prefab_rapidProjectile;
    public PlayerProjectile prefab_chargeShotP;


    public WeaponMode weaponMode;
    public Transform weaponBlasterPoint;

    public float cameraSensitivity;
    public float normalSpeed;
    public float runSpeed;
    public float jumpSpeed;

    public float acceleration;
    public float friction;

    public Transform weaponHold;

    public bool hasRedKey;

    [HideInInspector]
    public bool isStepping;
    public AudioSource steps;
    public float stepsPeriod;
    public int stepFrameSkip = -1;

    private PlayerInput playerInput;
    float xRotation = 0f;
    Vector3 velocity;
    Vector3 directionVelocity;
    bool touchingGround;
    bool lastGrounded;
    bool isOutsideSlope;
    float jumpShoot;
    Vector3 hitNormal;
    bool isWalking;
    float slideFriction = 0.3f;
    float fireRate;
    float hurtTimer;

    bool inGameOver;
    float gameOverTimer;
    bool inGloryMode;
    GloryKillable gloryKillable;
    float gloryDelayTimer;
    bool killedTarget;

    int skipInteractionFrame = 0;
    float gloryKillRelease;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.04f);

    private void Awake()
    {
        FpsControllerCore.instance = this;
        //Screen.SetResolution(1280, 720, true);

        GameOverCanvas.enabled = false;
        firePlayAgainText.enabled = false;
        weaponMode = WeaponMode.Manual;
        hasRedKey = false;
    }

    // Start is called before the first frame update
    void Start()
    {

        playerInput = new PlayerInput();
        playerInput.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        jumpShoot = 1;

        directionVelocity = new Vector3();
        fireRate = Time.time;

        life = 100;
        lifeText.text = $"{life}%";

        gameOverTimer = Time.time;
        hurtTimer = Time.time;
        inGloryMode = false;
        killedTarget = false;
        lastGrounded = false;

        isStepping = false;


        redKeyUI.enabled = false;
    }

    public void GetRedKey()
    {
        hasRedKey = true;
    }

    public void TakeDamage(int damage)
    {
        if (Time.time - hurtTimer > 0.12f && !inGameOver)
        {
            hurtTimer = Time.time;
            Debug.Log("damage " + Time.time);
            life -= damage;
            playerHurtAudio.PlayOneShot(playerHurtAudio.clip);
            if (life <= 0)
            {
                inGameOver = true;
                gameOverTimer = Time.time;
                GameOverCanvas.enabled = true;
            }
            else
            {
                lifeText.text = $"{life.ToString("000")}%";
            }
        }
    }

    public void TeleportPlayer(Vector3 position, Vector3 facingDirection)
    {
        characterController.enabled = false;
        transform.position = position;
        transform.forward = facingDirection;
        characterController.enabled = true;
        playerTeleportAudio.PlayOneShot(playerTeleportAudio.clip);
    }

    public void EmitExplosion(Vector3 position)
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = position;
        ps_explode.Emit(emitParams, 1);
    }

    void LaunchProjectile(PlayerProjectile prefab_p)
    {
        PlayerProjectile p = GameObject.Instantiate(prefab_p, weaponBlasterPoint.position, Quaternion.identity);
        p.fpsControllerCore = this;
        p.Launch(fpsCamera.transform.forward, runSpeed + 4);
    }

    void ScanGloryKill()
    {
        RaycastHit gloryRayHit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out gloryRayHit, 5f, 1 << 10))
        {
            gloryKillable = gloryRayHit.collider.GetComponent<GloryKillable>();
            if(playerInput.inputActions.Melee.triggered)
            {
                if(gloryKillable.CanBeGloryKilled())
                {
                    handImageUI.enabled = false;
                    punchImageUI.enabled = true;
                    inGloryMode = true;
                    gloryDelayTimer = 1;
                    gloryKillRelease = Time.time;
                }
            }
        }
    }

    void FpsCamera_Rotation()
    {
        //Debug.Log(playerInput.Keyboard.Mouse.ReadValue<Vector2>());

        Vector2 lookVector = playerInput.inputActions.Look.ReadValue<Vector2>() * cameraSensitivity * Time.deltaTime;
        
        xRotation -= lookVector.y;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        fpsCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * lookVector.x);
    }

    void FpsMove()
    {
        //Animation and bobs
        isWalking = false;
        touchingGround = false;

       //Input poll
        float jumped = playerInput.inputActions.Jump.ReadValue<float>();
        Vector2 move = playerInput.inputActions.Move.ReadValue<Vector2>();

      
        if (Physics.CheckSphere(transform.position + Vector3.down * characterController.height * 0.53f, characterController.radius * 0.2f, 1 << 0) && velocity.y < 0.1f)
        {
            if(lastGrounded == false)
            {
                // land sound only plays if player jumps
                lastGrounded = true;
                playerLand.transform.position = transform.position;
                playerLand.PlayOneShot(playerLand.clip, 0.5f);
            }

            touchingGround = true;
            jumpShoot = 1;
        }


        //if grounded
        if (touchingGround)
        {
            velocity.y = 0;
            if(jumped > 0)
            {
                playerJump.transform.position = transform.position - Vector3.up;
                playerJump.PlayOneShot(playerJump.clip, 0.5f);
                lastGrounded = false;
                //jump 
                velocity.y = jumpSpeed;
                touchingGround = false;
                jumpShoot = 2;
            }
        }

        if(jumpShoot > 1)
        {
            jumpShoot -= Time.deltaTime;
        }

        if(jumped < 0.3f)
        {
            jumpShoot = 1;
        }

        if(move.magnitude > 0)
        {
            isWalking = true;
        }

        //apply friction
        if (directionVelocity.magnitude > 0)
        {
            directionVelocity -= directionVelocity * friction * Time.deltaTime;
        }

        //move via acceleration
        directionVelocity += (transform.right * move.x * jumpShoot * 0.7f + transform.forward * move.y * jumpShoot) * acceleration * Time.deltaTime;

        float currentSpeed = normalSpeed;
        if(playerInput.inputActions.Run.ReadValue<float>() > 0.3f)
        {
            currentSpeed = runSpeed;
        }

        directionVelocity.x = Mathf.Clamp(directionVelocity.x, -currentSpeed, currentSpeed);
        directionVelocity.z = Mathf.Clamp(directionVelocity.z, -currentSpeed, currentSpeed);


        velocity.y += Physics.gravity.y * Time.deltaTime;

        directionVelocity.y = velocity.y;


        if (!isOutsideSlope)
        {
            directionVelocity += -transform.forward * 0.09f;
        }

        characterController.Move(directionVelocity * currentSpeed * Time.deltaTime);

        isStepping = false;

        if (directionVelocity.y < 0 && (Mathf.Abs(directionVelocity.x) > 0.4f || Mathf.Abs(directionVelocity.z) > 0.4f))
        {
            isStepping = true;
            stepsPeriod = Mathf.Abs(Mathf.Sin(10*Time.time));

            if (stepFrameSkip < 0 && stepsPeriod > 0.8f)
            {
                stepFrameSkip = 50;
                steps.PlayOneShot(steps.clip);
            }

            if(stepsPeriod < 0.5f)
            {
                stepFrameSkip = -1;
            }
        }

        isOutsideSlope = (Vector3.Angle(Vector3.up, hitNormal) <= characterController.slopeLimit);
    }

    void FpsInteract()
    {
        enterTextMesh.enabled = false;
        needRedTextMesh.enabled = false;

        if (skipInteractionFrame > 0)
        {
            skipInteractionFrame -= 1;
            return;
        }

        bool interactedTriggeres = playerInput.inputActions.Interact.triggered;

        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hitInfo, 0.8f, 1 << 10))
        {
            Portal portal = hitInfo.collider.GetComponent<Portal>();
            if(portal != null)
            {
                enterTextMesh.enabled = true;

                if (interactedTriggeres)
                {
                    Debug.Log($"hit {portal.name}");
                    skipInteractionFrame = 8;
                    portal.EnterPortal();
                }
            }

            DoorObstacle doorObs = hitInfo.collider.GetComponentInParent<DoorObstacle>();
            if(doorObs != null)
            {
                needRedTextMesh.enabled = true;
            }

            InteractionSensor inter_sensor = hitInfo.collider.GetComponent<InteractionSensor>();
            if(inter_sensor != null)
            {
                enterTextMesh.enabled = true;
                if(interactedTriggeres)
                {
                    inter_sensor.OnInteractionTriggered();
                }
            }
        }
    }

    void FpsWeapon()
    {
        //bool changeWeaponMode = playerInput.inputActions.WeaponMode.triggered;
        //if(changeWeaponMode)
        //{
        //    if(weaponMode == WeaponMode.Automatic)
        //    {
        //        weaponMode = WeaponMode.Manual;
        //    }else if(weaponMode == WeaponMode.Manual)
        //    {
        //        weaponMode = WeaponMode.Automatic;
        //    }
        //}

        bool fired = playerInput.inputActions.Fire.triggered;

        if (weaponMode == WeaponMode.Manual)
        {
            fired = playerInput.inputActions.Fire.triggered;
            //bool superShot = playerInput.inputActions.ChargeShot.triggered;
            //if(superShot)
            //{
            //    LaunchProjectile(prefab_chargeShotP);

            //    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            //    emitParams.position = weaponBlasterPoint.position - transform.forward * 0.5f;
            //    emitParams.startSize = 0.04f;
            //    //ps_weaponBlaster.Emit(emitParams, 1);
            //    fireRate = Time.time;
            //    //    EnemyWeakPoint enemyWeakPoint = hit.collider.GetComponent<EnemyWeakPoint>();
            //    //    if(enemyWeakPoint != null)
            //    //    {
            //    //        enemyWeakPoint.CallPoint();
            //    //    }
            //}

            if (fired && Time.time - fireRate > 0.15f)
            {
                LaunchProjectile(prefab_projectile);

                ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
                emitParams.position = weaponBlasterPoint.position - transform.forward * 0.5f;
                emitParams.startSize = 0.04f;
                //ps_weaponBlaster.Emit(emitParams, 1);
                fireRate = Time.time;
                playerShootAudio.PlayOneShot(playerShootAudio.clip);
                //    EnemyWeakPoint enemyWeakPoint = hit.collider.GetComponent<EnemyWeakPoint>();
                //    if(enemyWeakPoint != null)
                //    {
                //        enemyWeakPoint.CallPoint();
                //    }
            }
        }
        else if(weaponMode == WeaponMode.Automatic)
        {
            fired = playerInput.inputActions.RapidFire.ReadValue<float>() > 0.2f;

            if (fired && Time.time - fireRate > 0.18f)
            {
                LaunchProjectile(prefab_rapidProjectile);

                ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
                emitParams.position = weaponBlasterPoint.position - transform.forward * 0.5f;
                emitParams.startSize = 0.04f;
                //ps_weaponBlaster.Emit(emitParams, 1);
                fireRate = Time.time;
                //    EnemyWeakPoint enemyWeakPoint = hit.collider.GetComponent<EnemyWeakPoint>();
                //    if(enemyWeakPoint != null)
                //    {
                //        enemyWeakPoint.CallPoint();
                //    }
            }
        }

    }

    public void HitParticle(Vector3 point, Vector3 normal, bool monster = false)
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = point;
        Vector3 particleRotEuler = Quaternion.LookRotation(-normal).eulerAngles;
        particleRotEuler.z = Random.Range(0, 360);

        emitParams.rotation3D = particleRotEuler;
        if (monster)
        {
            ps_hitparticle.Emit(emitParams, 1);
        }
        emitParams = new ParticleSystem.EmitParams();
        emitParams.position = point;
        ps_weaponBlaster.Emit(emitParams, 1);
        explosionsAudio.transform.position = point;
        explosionsAudio.PlayOneShot(explosionsAudio.clip);

    }

    // Update is called once per frame
    void Update()
    {
        if(inGameOver)
        {
            if(Time.time - gameOverTimer > 1)
            {
                firePlayAgainText.enabled = true;

                if(playerInput.inputActions.Fire.triggered)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                }
            }

            return;
        }

        if(inGloryMode)
        {

           

            if(Time.time - gloryKillRelease > 2f)
            {
                inGloryMode = false;
                killedTarget = false;
                handImageUI.enabled = true;
                punchImageUI.enabled = false;
                return;
            }

            if (!killedTarget)
            {
                Vector3 dir = fpsCamera.transform.forward;

                characterController.Move(dir * 15 * Time.deltaTime);
                if (AIBlackboard.SqrtDistanceVector3(transform.position, gloryKillable.transform.position) < 1)
                {
                    EmitExplosion(fpsCamera.transform.position + fpsCamera.transform.forward * 0.5f);
                    gloryKillable.GloryKillIt();
                    killedTarget = true;
                    gloryDelayTimer = Time.time;
                    gloryKillAudio.PlayOneShot(gloryKillAudio.clip);

                }
            }

            if (gloryKillable.monster.healthyState != Monster.HealthyState.Staggered && !killedTarget)
            {
                inGloryMode = false;
                killedTarget = false;
                handImageUI.enabled = true;
                punchImageUI.enabled = false;
                return;
            }

            if (killedTarget && Time.time - gloryDelayTimer > 0.3f)
            {
                killedTarget = false;
                inGloryMode = false;
                handImageUI.enabled = true;
                punchImageUI.enabled = false;
            }
            else
            {
                return;
            }
        }

        FpsCamera_Rotation();

        FpsMove();
        FpsInteract();

        FpsWeapon();

        BobWeapon();

        ScanGloryKill();

        if(UnityEngine.InputSystem.Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            AIBlackboard.instance.Sensor_PlayerNear2D(AIBlackboard.playerPosition3D);
        }
    }

    void BobWeapon()
    {
        if(isWalking)
        {
            Vector3 position = handImageUI.rectTransform.localPosition;
            position.y = -240 + Mathf.Sin(Time.time * 15) * 8;
            handImageUI.rectTransform.localPosition = position;
        }
        else
        {
            Vector3 position = handImageUI.rectTransform.localPosition;
            position.y = -237;
            handImageUI.rectTransform.localPosition = Vector3.MoveTowards(handImageUI.rectTransform.localPosition, position,Time.deltaTime * 1000f);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;

       
    }

    private void OnTriggerEnter(Collider other)
    {
        GeneralSensor gs = other.GetComponent<GeneralSensor>();
        if (gs != null)
        {
            if (!gs.eventSensor)
            {
                gs.OnSensorTriggered();
            }
        }

        KeySensor ks = other.GetComponent<KeySensor>();
        if(ks != null)
        {
            ks.CheckKeys();
        }

        LifeRestoreConsumable lrc = other.GetComponent<LifeRestoreConsumable>();
        if (lrc != null)
        {
            if (!lrc.usedUp)
            {
                playerRecoverAudio.PlayOneShot(playerRecoverAudio.clip);
                life += 6;
                lifeText.text = $"{life.ToString("000")}%";
                lrc.OnUsedUp();
            }
        }
    }

}
