using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScript : MonoBehaviour
{
    public Vector2 startPos;

    public Transform player;
    public GameObject curButton;
    public GameObject curMaze;

    // Start is called before the first frame update
    void Start()
    {
        startPos = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("penis");
        if (other.gameObject.tag == "Player")
        {
            TurnOff();
            //Debug.Log("penis");
        }
    }

    public void TurnOff()
    {
        player.GetComponent<MouseControlledObject>().moving = false;
        player.position = startPos;
        curButton.SetActive(true);
        Cursor.visible = true;
    }
}
