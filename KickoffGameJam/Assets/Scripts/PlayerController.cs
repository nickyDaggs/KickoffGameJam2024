using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float storedSpeed = 0;

    public Rigidbody2D rb;

    public Vector2 movement;

    public Dialogue textSystem;

    public bool canSpeak = false;

    int sceneChange;

    public Animator playerAnim;
    public Animator popText;

    public string textToPop;
    public string textToPopTwo;

    SignDialogue storedSign;

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
        if(Input.GetAxis("Horizontal") != 0)
        {
            playerAnim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        }
        

        if(Input.GetKeyDown(KeyCode.Space) && canSpeak)
        {
            textSystem.currentLines = 0;
            textSystem.sign = storedSign;
            textSystem.TurnOn();
            canSpeak = false;
            moveSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z) && canSpeak && sceneChange != 0)
        {
            if (sceneChange == 4 && !GameManager.Instance.Sis_DONE)
            {
                popText.GetComponent<Text>().text = textToPop;
                popText.SetTrigger("Pop");
            } else if(sceneChange == 7 && !GameManager.Instance.FULLYDONE)
            {
                popText.GetComponent<Text>().text = textToPopTwo;
                popText.SetTrigger("Pop");
            }
            else
            {
                SceneManager.LoadScene(sceneChange);
            }
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
            //textSystem.sign = collision.gameObject.GetComponent<SignDialogue>();
            storedSign = collision.gameObject.GetComponent<SignDialogue>();
            sceneChange = storedSign.ending;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dialogue")
        {
            storedSign = null;
            canSpeak = false;
            textSystem.sign = null;
        }
    }
}
