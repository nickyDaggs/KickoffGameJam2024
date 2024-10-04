using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minigameCount : MonoBehaviour
{
    int count = 0;
    Text change;
    // Start is called before the first frame update
    void Start()
    {
        change = GetComponent<Text>();
        count = 0;
        if(GameManager.Instance.BigBro_DONE)
        {
            count++;
        }
        if (GameManager.Instance.Sis_DONE)
        {
            count++;
        }
        if (GameManager.Instance.LilBro_DONE)
        {
            count++;
        }
        if (GameManager.Instance.Cousin_DONE)
        {
            count++;
        }
        if (GameManager.Instance.Twins_DONE)
        {
            count++;
        }
        change.text = "MINIGAMES COMPLETED:\n" + count + " / 5";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
