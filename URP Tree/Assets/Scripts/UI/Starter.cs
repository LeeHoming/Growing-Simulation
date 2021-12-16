using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    public Button StartButton;

    public KeyInput Keyinputer;

    public CameraMovement CamMover;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartButton.onClick.AddListener (starterClick);
    }

    private void starterClick()
    {
        Keyinputer.onControl = false;
        CamMover.canControl = true;
    }
}
