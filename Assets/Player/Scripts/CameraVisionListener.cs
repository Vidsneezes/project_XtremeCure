using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraVisionListener : MonoBehaviour
{
    public Image detectImage;
    public Camera mainCamera;

    public Transform whipRopePoint;
    public float maxWhipDistance;
    public LineRenderer lineRenderer;
    public PlayerMovement playerMovement;
    private RaycastHit hitTarget;
    private Vector3 whipGrabPoint;
    private bool lastFrameHadTarget;
    public bool inWhip;
    private bool hitWhipPoint;

    // Start is called before the first frame update
    void Start()
    {
        hitWhipPoint = false;
        detectImage.rectTransform.position = Vector3.one * -1000;
    }

    public void ReleaseWhip()
    {
        lineRenderer.SetPosition(0, Vector3.one * -1000f);
        lineRenderer.SetPosition(1, Vector3.one * -1000f);
        inWhip = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inWhip)
        {
            lineRenderer.SetPosition(0, whipRopePoint.position);
            lineRenderer.SetPosition(1, whipGrabPoint);

            return;
        }

        if (hitTarget.collider != null && hitWhipPoint)
        {
            lastFrameHadTarget = true;
            if (hitTarget.distance < maxWhipDistance)
            {
                Vector3 localPosition = mainCamera.WorldToScreenPoint(hitTarget.point);
                detectImage.rectTransform.localPosition = Vector3.zero;
                whipGrabPoint = hitTarget.point;
                if(Input.GetMouseButtonDown(0) && playerMovement.playerState == PlayerMovement.PlayerState.FreeMove)
                {
                    playerMovement.TeleportToWhipPoint(whipGrabPoint + Vector3.down * 0.4f);

                    inWhip = true;
                }
            }
        }
        else
        {
            if (lastFrameHadTarget)
            {
                detectImage.rectTransform.position = Vector3.one * -1000;
                lastFrameHadTarget = false;
            }
        }
    }

    private void FixedUpdate()
    {
        hitWhipPoint = false;

        Physics.Raycast(transform.position, transform.forward,out hitTarget, 20, 1 << 0);
        if(hitTarget.collider != null)
        {
            if(hitTarget.collider.CompareTag("WhipPoint"))
            {
                hitWhipPoint = true;
            }
        }
    }
}
