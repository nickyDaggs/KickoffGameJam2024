using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action {None, EnableObj, Cutscene, Damage, Sound, Activity, EndBattle, Fight, ActivatePortal, Heal, AddMemberFight, Check, EndChapter };

[System.Serializable]
public class newLines
{
    public List<string> lines;
    public Action act;
}

public class SignDialogue : MonoBehaviour
{
    

    public GameObject player;
    public List<string> Lines = new List<string>();
    public List<newLines> nextLines = new List<newLines>();
    public List<Sprite> headers = new List<Sprite>();
    public bool action;

    public Action actionToDo;
    public List<GameObject> objs = new List<GameObject>();

    public Animator animator;
    //public List<baseCharacter> enemySpawn;
    public GameObject Portal;
    //public baseCharacter member;
    public List<string> items;
    public int ending;

    //public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Interact()
    {
        
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("Change");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("Change");
        }
    }*/
}
