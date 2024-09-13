using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("penis");
        }
    }
}
