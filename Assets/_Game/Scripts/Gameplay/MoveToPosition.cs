using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    [SerializeField]
    private Vector3 position;

    public void MovePosition()
    {
        transform.position = position;
    }
}
