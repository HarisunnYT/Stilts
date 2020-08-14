using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody LegPivot1;
    public Rigidbody LegPivot2;

    private float rotationSpeed = 75;

    public bool InputEnabled { get; set; } = true;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!InputEnabled)
            return;

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
