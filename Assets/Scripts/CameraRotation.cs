using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    private Touch touch;

    private Vector2 touchPosition;

    private Quaternion rotationY;

    private float rotateSpeedModifier = 0.1f;

    float minY = -6f, maxY = 3f;

    bool rotate = true;
    
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(
                    0f,
                    -touch.deltaPosition.x * rotateSpeedModifier,
                    0f
                    );

                if (rotate)
                {
                    transform.rotation *= rotationY;
                }
                //transform.eulerAngles.y = Mathf.Clamp(transform.eulerAngles.y, minY, maxY);
            }
        }

        if (transform.eulerAngles.y < -5f)
        {
            rotate = false;
        }
        else if(transform.eulerAngles.y > 2f)
        {
            rotate = false;
        }
        else
        {
            rotate = true;
        }


    }


}
