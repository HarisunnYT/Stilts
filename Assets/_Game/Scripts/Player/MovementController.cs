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

    [SerializeField]
    private bool mainController = true;

    public bool InputEnabled { get; set; } = true;
    private float expressionMetre;

    public float TimePlayed { get { return Time.time - timeStarted; } }
    private float timeStarted;

    public Rigidbody Body { get; private set; }

    private Animator animator;

    private void Awake()
    {
        if (mainController)
        {
            Instance = this;
            Cursor.visible = false;
            timeStarted = Time.time;
        }

        Body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

#if !UNITY_EDITOR
        if (mainController && SaveManager.Instance.HasSavedData(SaveManager.Instance.CurrentMap))
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

        if (Body.velocity.magnitude > 25)
            expressionMetre = 1;
        else
        {
            float leg1Vel = Mathf.Abs(LegPivot1.angularVelocity.z);
            float leg2Vel = Mathf.Abs(LegPivot2.angularVelocity.z);

            if ((leg1Vel > 0.2f && leg1Vel < 1f && leg2Vel < 0.3f) ||
                (leg2Vel > 0.2f && leg2Vel < 1f && leg1Vel < 0.3f))
                expressionMetre = 0;
            else
                expressionMetre = 0.5f;
        }

        animator.SetFloat("Expression", expressionMetre);
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
