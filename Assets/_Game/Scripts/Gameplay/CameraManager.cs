using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float moveSpeed = 10;

    [SerializeField]
    private Vector3 offset;

    [Header("Zoom")]
    [SerializeField]
    private bool doZoom = true;

    [SerializeField]
    private float zoomoutVelocityThreshold = 10;

    [SerializeField]
    private float maxZoomOutDistance;

    [SerializeField]
    private float zoomOutSpeed = 2.5f;

    [SerializeField]
    private float zoomInSpeed = 2.5f;

    [SerializeField]
    private float zoomTimeDelay = 1.5f;

    private float startingDistance;
    private float zoomTimer = -1;

    private bool following = false;

    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        startingDistance = camera.orthographicSize;
    }

    public void StartFollowing()
    {
        following = true;
    }

    private void LateUpdate()
    {
        if (following || FTU.Instance == null)
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z), moveSpeed);

        if (doZoom)
        {
            if (Mathf.Abs(MovementController.Instance.Body.velocity.y) > zoomoutVelocityThreshold)
            {
                if (zoomTimer == -1)
                    zoomTimer = Time.time + zoomTimeDelay;

                if (Time.time > zoomTimer)
                    camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, maxZoomOutDistance, Time.deltaTime * zoomOutSpeed);
            }
            else
            {
                zoomTimer = -1;
                camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, startingDistance, Time.deltaTime * zoomInSpeed);
            }

        }
    }
}
