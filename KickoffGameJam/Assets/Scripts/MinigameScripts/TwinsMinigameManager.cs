using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class TwinsMinigameManager : MonoBehaviour
{
    
    public List<PuzzleSlot> slots;
    public GameObject winScreen;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slots.All(x => x.solved) && !done)
        {
            done = true;
            winScreen.SetActive(true);
            GameManager.Instance.Twins_DONE = true;
        }
    }

    public void BackButton(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
