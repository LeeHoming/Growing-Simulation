using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainUnit : MonoBehaviour
{
    public bool staticUnit;

    public bool colorUnit = false;

    public float offsetValue = 1;

    public float height = 0f;

    public Color shoreColor = Color.black;

    public Color halfColor = Color.gray;

    public Color topColor = Color.white;

    public float threhold1 = 0.1f;

    public float threhold2 = 0.3f;

    public float threhold3 = 0.9f;

    private Vector3 origin = Vector3.zero;

    private void Awake()
    {
        origin = transform.localPosition;
    }

    void Update()
    {
        if (!staticUnit)
        {
            RendererUpdate();
            GetComponent<Transform>().localPosition =
                new Vector3(origin.x, height, origin.z);
        }
    }

    void RendererUpdate()
    {
        if (offsetValue < threhold1)
        {
            GetComponentInChildren<Renderer>().enabled = false;

            height = -0.5f;
        }
        else if (offsetValue >= threhold1 && offsetValue < threhold2)
        {
            GetComponentInChildren<Renderer>().enabled = true;
            if (colorUnit)
            {
                GetComponentInChildren<Renderer>().material.color = shoreColor;
            }
            else
            {
                GetComponentInChildren<Renderer>().material.color = Color.gray;
            }
            height = (offsetValue - threhold1);
        }

        if (offsetValue >= threhold2)
        {
            GetComponentInChildren<Renderer>().enabled = true;
            if (colorUnit)
            {
                float offsetColor = 0;
                offsetColor = offsetValue.Remap(threhold2, threhold3, 0f, 1f);
                GetComponentInChildren<Renderer>().material.color =
                    Color.Lerp(halfColor, topColor, offsetColor);
            }
            else
            {
                GetComponentInChildren<Renderer>().material.color = Color.gray;
            }

            height = 2 * (offsetValue - threhold1);
        }
    }
}
