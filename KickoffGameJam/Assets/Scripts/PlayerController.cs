using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float storedSpeed = 0;

    public Rigidbody2D rb;

    Vector2 movement;

    public Dialogue textSystem;

    public bool canSpeak = false;

    // Start is called before the first frame update
    void Start()
    {
        storedSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && canSpeak)
        {
            textSystem.TurnOn();
            canSpeak = false;
            moveSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            textSystem.TurnOn();
            canSpeak = false;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dialogue")
        {
            canSpeak = true;
            textSystem.currentLines = 0;
            textSystem.sign = collision.gameObject.GetComponent<SignDialogue>();
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dialogue")
        {
            canSpeak = false;
            textSystem.sign = null;
        }
    }
}
