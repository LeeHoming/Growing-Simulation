using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public bool isReset;

    public float

            rotateStep = 0.1f,
            movingStep = 0.1f;

    public Quaternion OriginalRotation;

    public Vector3 rotating = new Vector3(0, 0, 0);

    public Transform CamZ;

    public float Zoffset;

    void Start()
    {
        OriginalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
