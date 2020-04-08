using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public enum PlayerState
    {
        FreeMove,
        InRailWhip
    }

    public float fowardSpeed;
    public float strafeSpeed;
    public float jumpSpeed;

    public float mouseXSensitivity;
    public Camera fpsViewCamera;
    public CameraVisionListener cameraVisionListener;

    public PlayerState playerState;

    private CharacterController cc_player;
    private Vector3 velocity;
    private bool playerCrouched;
    private bool canUncrouch;
    private float coyoteTime;
    private bool lastOnGround;
    private Vector3 whipPoint;
    private Vector3 hitNormal;

    // Start is called before the first frame update
    void Start()
    {
        playerCrouched = false;
        cc_player = GetComponent<CharacterController>();
        velocity = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TeleportToWhipPoint(Vector3 whipPoint)
    {
        playerState = PlayerState.InRailWhip;
        this.whipPoint = whipPoint;
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerState)
        {
            case PlayerState.FreeMove:
                FreeMoveUpdate();
                break;
            case PlayerState.InRailWhip:
                InRailWhipUpdate();
                break;
        }
    }

    void InRailWhipUpdate()
    {
        Vector3 dir = (whipPoint - transform.position).normalized;

        cc_player.Move(dir * 15 * Time.deltaTime);

        if(Vector3.Distance(transform.position,whipPoint) < 1f)
        {
            playerState = PlayerState.FreeMove;
            cameraVisionListener.ReleaseWhip();

        }
    }

    void FreeMoveUpdate()
    {
        float fallvalue = velocity.y;

        fallvalue += Physics.gravity.y * Time.deltaTime;
        //Check of grounded
        if (lastOnGround == true && cc_player.isGrounded == false)
        {
            coyoteTime -= Time.deltaTime;
        }
        else if (cc_player.isGrounded)
        {
            coyoteTime = 0.09f;
            fallvalue = 0;
        }
        else if (!cc_player.isGrounded)
        {
            coyoteTime -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && (cc_player.isGrounded || coyoteTime > 0))
        {
            fallvalue = jumpSpeed;
            coyoteTime = -1;
        }

        lastOnGround = cc_player.isGrounded;

        //rotates the player root on the Y axis
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * mouseXSensitivity * Time.deltaTime, 0);


        //rotates the child camera fpsview on the x axis
        float mouseY = Input.GetAxis("Mouse Y");
        float lastCameraRot = fpsViewCamera.transform.localEulerAngles.x;
        lastCameraRot += -mouseY * 180 * Time.deltaTime;
        lastCameraRot = ClampCameraRotation(lastCameraRot);
        fpsViewCamera.transform.localRotation = Quaternion.Euler(lastCameraRot, 0, 0);


        //Gets movement code
        float fowardValue = 0;
        if (Input.GetKey(KeyCode.W))
        {
            fowardValue = fowardSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            fowardValue = -fowardSpeed;
        }

        float horizontalValue = 0;
        if (Input.GetKey(KeyCode.A))
        {
            horizontalValue = -strafeSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalValue = strafeSpeed;
        }

        bool lastCrouchedState = playerCrouched;

        if (Input.GetKey(KeyCode.C))
        {

            playerCrouched = true;
            canUncrouch = false;
        }
        else
        {
            if (canUncrouch)
            {
                playerCrouched = false;
            }
        }

        if (lastCrouchedState == false && playerCrouched == true)
        {
            cc_player.height = 0.85f;
            fpsViewCamera.transform.localPosition = new Vector3(0, 0.1f, -0.02f);
        }

        if (lastCrouchedState == true && playerCrouched == false && canUncrouch == true)
        {
            cc_player.height = 1.85f;
            fpsViewCamera.transform.localPosition = new Vector3(0, 0.55f, -0.02f);
        }


        velocity = (transform.right * horizontalValue) + (transform.forward * fowardValue);
        velocity.y = fallvalue;
    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.FreeMove)
        {
            canUncrouch = true;
            RaycastHit rayHit;
            Ray ceilingCheck = new Ray(transform.position, Vector3.up);

            if (Physics.Raycast(ceilingCheck, out rayHit, 1.85f, 1 << 0))
            {
                canUncrouch = false;
            }

            cc_player.Move(velocity * Time.deltaTime);

        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    //Prevents the camera from looping over angle
    private float ClampCameraRotation(float value)
    {
        if(value < 0)
        {
            return Mathf.Clamp(value, -85, 0);
        }

        if(value > 180)
        {
            return Mathf.Clamp(value, 360-85, 360);
        }

        if(value < 180 && value > 0)
        {
            return Mathf.Clamp(value, 0, 85);
        }

        return value;
    }
}
