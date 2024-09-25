using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class TwinsMinigameManager : MonoBehaviour
{
    
    public List<PuzzleSlot> slots;
    public int curButtons;
    public List<GameObject> pills;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slots.All(x => x.solved))
        {
            GameManager.Instance.Twins_DONE = true;
        }
    }

    public void cloudButton(GameObject button)
    {
        //BadCloudCount--;
        //BadCloudSpots.Add(button.transform.position);
        curButtons--;
        Destroy(button);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(1);
    }
}
