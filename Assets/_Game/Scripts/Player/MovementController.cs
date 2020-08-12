using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody LegPivot1;
    public Rigidbody LegPivot2;

    [SerializeField]
    private float rotationSpeed = 10;

    [SerializeField]
    private float scaleSpeed = 2f;

    [SerializeField]
    private float minScale = 0.5f;

    [SerializeField]
    private float maxScale = 1.2f;

    public float RespawnRotation = 10;

    [Space()]
    [SerializeField]
    private float startingLeg1Force;

    [SerializeField]
    private float startingLeg2Force;

    private Rigidbody body;

    private void Awake()
    {
        LegPivot1.AddTorque(new Vector3(0, 0, startingLeg1Force), ForceMode.Impulse);
        LegPivot2.AddTorque(new Vector3(0, 0, startingLeg2Force), ForceMode.Impulse);

        body = GetComponent<Rigidbody>();

        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Leg1") != 0)
        {
            if (Input.GetAxisRaw("Leg1") > 0)
                LegPivot1.AddTorque(new Vector3(0, 0, -rotationSpeed * Time.deltaTime), ForceMode.Impulse);
            if (Input.GetAxisRaw("Leg1") < 0)
                LegPivot1.AddTorque(new Vector3(0, 0, rotationSpeed * Time.deltaTime), ForceMode.Impulse);
        }

        if (Input.GetAxisRaw("Leg2") != 0)
        {
            if (Input.GetAxisRaw("Leg2") > 0)
                LegPivot2.AddTorque(new Vector3(0, 0, -rotationSpeed * Time.deltaTime), ForceMode.Impulse);
            if (Input.GetAxisRaw("Leg2") < 0)
                LegPivot2.AddTorque(new Vector3(0, 0, rotationSpeed * Time.deltaTime), ForceMode.Impulse);
        }
    }
}
