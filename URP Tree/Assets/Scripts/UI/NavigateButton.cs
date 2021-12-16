using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigateButton : MonoBehaviour
{
    public Button

            Up,
            Down,
            Left,
            Right;

    public float step = 3f;

    public TerrainFormer TerrainFormer;

    void Start()
    {
        Up.onClick.AddListener (upClick);
        Down.onClick.AddListener (downClick);
        Left.onClick.AddListener (leftClick);
        Right.onClick.AddListener (rightClick);
    }

    private void upClick()
    {
        TerrainFormer.offsetY += step;
    }

    private void downClick()
    {
        TerrainFormer.offsetY -= step;
    }

    private void leftClick()
    {
        TerrainFormer.offsetX -= step;
    }

    private void rightClick()
    {
        TerrainFormer.offsetX += step;
    }
}
