using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    private Rigidbody rigidbody;

    private Vector3 startPosition;

    private Vector3 leg1StartPosition;
    private Quaternion leg1StartRotation;

    private Vector3 leg2StartPosition;
    private Quaternion leg2StartRotation;

    private MovementController movementController;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementController = GetComponent<MovementController>();

        startPosition = transform.position;

        leg1StartPosition = movementController.LegPivot1.transform.position;
        leg1StartRotation = movementController.LegPivot1.transform.rotation;

        leg2StartPosition = movementController.LegPivot2.transform.position;
        leg2StartRotation = movementController.LegPivot2.transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();
    }

    private void Respawn()
    {
        rigidbody.velocity = Vector3.zero;
        transform.position = startPosition;

        movementController.LegPivot1.velocity = Vector3.zero;
        movementController.LegPivot1.angularVelocity = Vector3.zero;
        movementController.LegPivot1.position = leg1StartPosition;
        movementController.LegPivot1.rotation = leg1StartRotation;
        movementController.LegPivot1.transform.localScale = Vector3.one;

        movementController.LegPivot2.velocity = Vector3.zero;
        movementController.LegPivot2.angularVelocity = Vector3.zero;
        movementController.LegPivot2.position = leg2StartPosition;
        movementController.LegPivot2.rotation = leg2StartRotation;
        movementController.LegPivot2.transform.localScale = Vector3.one;

        //movementController.LegPivot1.transform.rotation = Quaternion.Euler(0, 0, -movementController.RespawnRotation);
        //movementController.LegPivot2.transform.rotation = Quaternion.Euler(0, 0, movementController.RespawnRotation);
    }
}
