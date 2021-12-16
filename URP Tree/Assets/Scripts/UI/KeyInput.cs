using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    public float step = 3f;

    public bool onControl = true;

    public TerrainFormer TerrainFormer;

    void Update()
    {
        if (onControl)
        {
            TerrainFormer.offsetY =
                TerrainFormer.offsetY + 0.3f * Input.GetAxis("Vertical") * step;

            TerrainFormer.offsetX =
                TerrainFormer.offsetX +
                0.3f * Input.GetAxis("Horizontal") * step;
        }
    }
}
