using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool canControl = false;

    float speed;

    float zoomSpeed;

    float rotateSpeed;

    float maxHeight = 40f;

    float minHeight = 4f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 0.06f;
                zoomSpeed = 20f;
            }
            else
            {
                speed = 0.035f;
                zoomSpeed = 10f;
            }

            float hsp =
                transform.position.y * speed * Input.GetAxis("Horizontal");
            float vsp =
                transform.position.y * speed * Input.GetAxis("Vertical");
            float scrollSp = -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

            Vector3 VerticalMove = new Vector3(0, scrollSp, 0);
            Vector3 lateralMove = hsp * transform.right;
            Vector3 forwardMove = transform.forward;

            forwardMove.y = 0;
            forwardMove.Normalize();
            forwardMove *= vsp;

            Vector3 move = VerticalMove + lateralMove + forwardMove;

            transform.position += move;
        }
    }
}
