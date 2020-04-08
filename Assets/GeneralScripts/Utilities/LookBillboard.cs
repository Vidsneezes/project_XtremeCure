using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBillboard : MonoBehaviour
{
    public Transform rootTransform;

    void LateUpdate()
    {
        rootTransform.rotation = Quaternion.LookRotation(rootTransform.position - FpsControllerCore.instance.fpsCamera.transform.position);
    }

}
