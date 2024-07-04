using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0, 90, 0);
        }
    }
}
