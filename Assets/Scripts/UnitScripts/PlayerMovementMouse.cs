using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMouse : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<IMovePosition>().SetMovePosition(getMWP());
        }
    }

    public Vector3 getMWP()       //gets mouse position in reference to world
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
