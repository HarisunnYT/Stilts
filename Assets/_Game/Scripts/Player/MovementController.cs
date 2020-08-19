using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static MovementController Instance;

    public Rigidbody LegPivot1;
    public Rigidbody LegPivot2;

    [SerializeField]
    private float rotationSpeed = 75;

    public bool InputEnabled { get; set; } = true;
    public float ExpressionMetre { get; private set; }

    public float TimePlayed { get { return Time.time - timeStarted; } }
    private float timeStarted;

    private Rigidbody body;

    private void Awake()
    {
        Instance = this;
        Cursor.visible = false;
        timeStarted = Time.time;

        body = GetComponent<Rigidbody>();

#if !UNITY_EDITOR
        if (SaveManager.Instance.HasSavedData(SaveManager.Instance.CurrentMap))
            transform.position = SaveManager.Instance.LoadCheckpoint();
#endif
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

        if (body.velocity.magnitude > 50)
            ExpressionMetre = 1;
        else
        {
            float leg1Vel = Mathf.Abs(LegPivot1.angularVelocity.z);
            float leg2Vel = Mathf.Abs(LegPivot2.angularVelocity.z);

            if ((leg1Vel > 0.1f && leg1Vel < 0.6f && leg2Vel < 0.3f) ||
                (leg2Vel > 0.1f && leg2Vel < 0.6f && leg1Vel < 0.3f))
                ExpressionMetre = 0;
            else
                ExpressionMetre = 0.5f;
        }
    }

    public void SetInputEnable(bool enabled)
    {
        InputEnabled = enabled;
    }

    private void OnApplicationQuit()
    {
        if (SaveManager.Instance)
            SaveManager.Instance.SaveTime(TimePlayed);
    }
}
