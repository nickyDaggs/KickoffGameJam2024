using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledObject : MonoBehaviour
{
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    public bool moving;

    public float xOffset;
    public float yOffset;
    // Start is called before the first frame update
    public void StartMove()
    {
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
        }
    }
}
