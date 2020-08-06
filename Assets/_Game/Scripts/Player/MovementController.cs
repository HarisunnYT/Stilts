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

    [SerializeField]
    private float startingRotation = 10;

    private void Awake()
    {
        LegPivot1.transform.rotation = Quaternion.Euler(0, 0, -startingRotation);
        LegPivot2.transform.rotation = Quaternion.Euler(0, 0, startingRotation);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
            LegPivot1.AddTorque(new Vector3(0, 0, -rotationSpeed * Time.deltaTime), ForceMode.Impulse);
        if (Input.GetKey(KeyCode.A))
            LegPivot1.AddTorque(new Vector3(0, 0, rotationSpeed * Time.deltaTime), ForceMode.Impulse);
        //if (Input.GetKey(KeyCode.W))
        //{
        //    Vector3 target = new Vector3(1, minScale, 1);
        //    LegPivot1.transform.localScale = Vector3.Lerp(LegPivot1.transform.localScale, target, scaleSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    Vector3 target = new Vector3(1, maxScale, 1);
        //    LegPivot1.transform.localScale = Vector3.Lerp(LegPivot1.transform.localScale, target, scaleSpeed * Time.deltaTime);
        //}

        if (Input.GetKey(KeyCode.RightArrow))
            LegPivot2.AddTorque(new Vector3(0, 0, -rotationSpeed * Time.deltaTime), ForceMode.Impulse);
        if (Input.GetKey(KeyCode.LeftArrow))
            LegPivot2.AddTorque(new Vector3(0, 0, rotationSpeed * Time.deltaTime), ForceMode.Impulse);

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    Vector3 target = new Vector3(1, minScale, 1);
        //    LegPivot2.transform.localScale = Vector3.Lerp(LegPivot2.transform.localScale, target, scaleSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    Vector3 target = new Vector3(1, maxScale, 1);
        //    LegPivot2.transform.localScale = Vector3.Lerp(LegPivot2.transform.localScale, target, scaleSpeed * Time.deltaTime);
        //}
    }
}
