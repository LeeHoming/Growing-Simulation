using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRender : MonoBehaviour
{
    public Mesh[] meshes;

    public int numModel;

    private void Awake()
    {
        // GetComponent<Transform>().Rotate(2, 0, 0);
        transform.rotation =
            Quaternion
                .Euler(new Vector3(-90, UnityEngine.Random.Range(0, 360), 0));
        transform.localScale =
            new Vector3(60f, 60f, UnityEngine.Random.Range(45f, 75f));
    }

    void Update()
    {
        GetComponent<MeshFilter>().mesh = meshes[numModel];
    }
}
