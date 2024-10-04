using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Dialogue : MonoBehaviour
{
    List<string> Lines = new List<string>();
    int current = 0;
    public Text textBox1;
    public Text textBox2;
    public Text textHeader;
    public Image panel;
    public bool on;
    public SignDialogue sign;
    float textDelay = 0.1f;
    IEnumerator coroutine;
    public static Dialogue instance;
    bool skip = false;
    public PlayerController player;
    public bool pets;
    public bool noTurn;
    //public musicController music;
    public Animator winScreen;
    //public bossScript boss;
    public Sprite lastHeader;
    public Sprite currentHeader;
    public GameObject HeaderPanel;

    public arrowManager arrows;


    public int currentLines = 0;

    // Start is called before the first frame 
    void Start()
    {
        instance = this;
        //currentHeader = sign.animator.gameObject;
        //TurnOn();
        panel.enabled = false;
        current = 0;
        textBox1.text = "";
        textBox1.enabled = false;
        textBox2.text = "";
        textBox2.enabled = false;
        textHeader.enabled = false;
        HeaderPanel.SetActive(false);
    }

    public void Restart()
    {
        currentHeader = null;
        TurnOn();
    }

    private void Update()
    {
        /*if(lastHeader !=  null)
        {
            AnimatorClipInfo[] checker = lastHeader.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            if(checker[0].clip.name.Contains("Talking"))
            {
                lastHeader.GetComponent<Animator>().SetTrigger("Off");
            }
        }
        if (currentHeader != null)
        {
            AnimatorClipInfo[] checker = currentHeader.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            if (checker[0].clip.name.Contains("Off"))
            {
                currentHeader.GetComponent<Animator>().SetTrigger("On");
            }
            
        }*/
        if (textBox1 != null && Lines.Count > 0)
        {
            if (textBox1.text == Lines[current] || textBox2.text == Lines[current])
            {
                if (Input.GetKeyDown(KeyCode.Space) && current < Lines.Count - 1)
                {
                    current++;
                    textBox1.text = "";
                    textBox2.text = "";
                    skip = false;
                    textBox1.enabled = true;
                    textBox2.enabled = false;
                    StartCoroutine("Text");
                }
                else if (Input.GetKeyDown(KeyCode.Space) && current >= Lines.Count - 1)
                {
                    Stop();
                    if(pets == true)
                    {
                        //player.BackPetSelect();
                    }
                }
            }
            else if(textBox1.text != Lines[current] && textBox1.text.Length > 3)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopCoroutine("Text");
                    textBox1.enabled = false;
                    textBox2.enabled = true;
                    textBox2.text = "";
                    skip = true;
                    textBox2.text = Lines[current];
                }
            }
            if (skip == true)
            {
                textBox1.text = "";
            } else
            {

            }
        }
    }


    public void TurnOn()
    {
        if (lastHeader != null)
        {
            Debug.Log("gfgffggffg");
            //lastHeader.GetComponent<Animator>().SetTrigger("Off");

        }
        
        
        //currentHeader.GetComponent<Animator>().SetTrigger("On");
        //AnimatorClipInfo[] please = currentHeader.GetComponent<Animator>().GetCurrentAnimatorClipInfo
        /*if (currentHeader)
            OtherPanel.SetActive(false);*/
        panel.enabled = true;
        textBox1.enabled = true;
        textBox1.text = "";
        textBox2.enabled = false;
        
        textHeader.enabled = true;
        if(currentLines == 0)
        {
            Lines = sign.Lines;
        } else
        {
            Lines = sign.nextLines[currentLines - 1].lines;
        }

        
        
        StartCoroutine("Text");
        HeaderPanel.SetActive(true);
        HeaderPanel.transform.GetChild(0).GetComponent<Image>().sprite = sign.headers[currentLines];
        currentHeader = sign.headers[currentLines];
        textHeader.text = currentHeader.name + ":";


        //player.enabled = false;

        //player.animatorOff = true;

    }
    // Update is called once per frame
    public IEnumerator Text()
    {
        foreach (char letter in Lines[current])
        {
            textBox1.text += letter;
            yield return new WaitForSecondsRealtime(textDelay);
        }
        textDelay = 0.1f;
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(textDelay);
        on = true;
    }

    public void Stop()
    {
        panel.enabled = false;
        current = 0;
        textBox1.text = "";
        textBox1.enabled = false;
        textBox2.text = "";
        textBox2.enabled = false;
        textHeader.enabled = false;
        HeaderPanel.SetActive(false);
        //
        currentLines++;
        Action t = sign.actionToDo;
        DoAction();
        //sign = null;
        //player.controller.enabled = true;
        //player.animatorOff = false;
        Lines = new List<string>();
        skip = false;
        if (sign.nextLines.Count - 1 >= currentLines - 1)
        {
            //sign.Lines = sign.nextLines[0].lines;
            if (sign.nextLines[currentLines - 1].act != Action.None)
            {
                sign.action = true;
                sign.actionToDo = sign.nextLines[currentLines - 1].act;
            }
            
            //sign.headers[0].GetComponent<Animator>().SetTrigger("On");
            //lastHeader = currentHeader;
            currentHeader = sign.headers[currentLines];
            //sign.nextLines.Remove(sign.nextLines[currentLines - 1]);
            //sign.headers.Remove(sign.headers[currentLines - 1]);
            textHeader.text = currentHeader.name + ":";
            TurnOn();
        } else
        {
            
            player.moveSpeed = player.storedSpeed;
            StartCoroutine(WaitToEnable());
        }
    }

    IEnumerator WaitToEnable()
    {
        yield return new WaitForSeconds(0.2f);
        player.canSpeak = true;
    }

    public void DoAction()
    {
        switch(sign.actionToDo)
        {
            case Action.None:
                break;
            case Action.EnableObj:
                Debug.Log("workje");
                foreach (GameObject obj in sign.objs)
                {
                    obj.SetActive(true);
                }
                textHeader.enabled = false;
                HeaderPanel.SetActive(true);
                break;
            case Action.Damage:
                //boss.Damage();
                break;

            case Action.EndBattle:
                //currentHeader.GetComponent<Animator>().SetTrigger("End");
                //GameManager.Instance.OverworldTransition();
                break;

            case Action.Sound:
                
                break;
            case Action.Activity:
                
                break;
            case Action.Cutscene:
                //Debug.Log("Work");
                sign.animator.SetTrigger("Next");
                sign.action = false;

                break;
            case Action.Fight:
                //GameManager.Instance.curEnemySpawn = new Vector3();
                //GameManager.Instance.enemySpawn = sign.enemySpawn;
                //GameManager.Instance.BattleTransition();
                break;
            case Action.ActivatePortal:
                //sign.Portal.GetComponent<levelPortal>().enabled = true;
                Color c = sign.Portal.GetComponent<SpriteRenderer>().color;
                c.a = 1;
                sign.Portal.GetComponent<SpriteRenderer>().color = c;
                break;
            case Action.Heal:
                
                break;
            case Action.AddMemberFight:
                
                break;
            case Action.Check:
                //Debug.Log(sign.items.Where(x => GameManager.Instance.keyInventory.Contains(x)).Count());
                
                break;
            case Action.EndChapter:
                SceneManager.LoadScene(sign.ending); 
                //GameManager.Instance.sceneLoad = 1;
                //SceneManager.LoadScene(1);
                break;
        }
    }
}
    
