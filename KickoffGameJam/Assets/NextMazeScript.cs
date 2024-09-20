using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMazeScript : MonoBehaviour
{
    public GameObject mazeNext;
    public GameObject buttonNext;

    public MazeScript mazeManager;

    public Vector2 positionNext;

    public GameObject player;

    int curMazeI = 0;

    public bool lastOne;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerMain")
        {
            if(lastOne)
            {
                Debug.Log("Win");
                mazeManager.TurnOff();
                return;
            }
            mazeManager.curMaze.SetActive(false);
            mazeNext.SetActive(true);
            mazeManager.curMaze = mazeNext;
            mazeManager.curButton.SetActive(false);
            mazeManager.curButton = buttonNext;
            mazeManager.startPos = positionNext;

            mazeManager.TurnOff();
            gameObject.SetActive(false);
        }
    }
}
