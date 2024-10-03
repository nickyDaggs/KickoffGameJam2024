using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool Sis_DONE;
    public bool LilBro_DONE;
    public bool BigBro_DONE;
    public bool Cousin_DONE;
    public bool Twins_DONE;
    public bool FULLYDONE;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Sis_DONE && LilBro_DONE && BigBro_DONE && Cousin_DONE && Twins_DONE)
        {
            FULLYDONE = true; ;
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
}
