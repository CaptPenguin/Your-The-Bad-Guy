using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWP : MonoBehaviour
{
    

    private void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        transform.position = mouseWorldPosition;
    }
}
