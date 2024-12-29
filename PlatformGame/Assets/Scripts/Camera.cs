using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    public Vector3 cameraOffset;
    public Vector3 targetPosition;
    public Vector3 velocity = Vector3.zero;
    public float worldBorderLeft;
    public float worldBorderRight;

    public bool controll;

    public float smoothTime;

    private void Update()
    {
        if (target.transform.position.x <= worldBorderLeft || target.transform.position.x >= worldBorderRight) controll = false;
        else controll = true;
        if (controll)
        {
            targetPosition = target.transform.position + cameraOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            targetPosition.y = target.transform.position.y;
            targetPosition.z = target.transform.position.z + cameraOffset.z;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
