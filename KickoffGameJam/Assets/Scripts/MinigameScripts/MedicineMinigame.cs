using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum ArticleType { Short = 8, Middle = 10, Long = 12, Longest = 15 }

[System.Serializable]
public class ArticleOption
{
    public ArticleType time;
    public string title;
}



public class MedicineMinigame : MonoBehaviour
{
    public List<ArticleOption> articles = new List<ArticleOption>();

    public List<Button> articleButtons = new List<Button>();

    public Slider timer;
    //float sliderTime;
    public int curButtons;
    int curPill = 0;
    public List<GameObject> pills;
    public List<GameObject> pillsInFood;
    public List<int> pillsTargets;
    public GameObject StartButton;
    //public GameObject Question;
    public GameObject GameOver;
    public GameObject WinScreen;
    bool done = false;

    public Text articlesText;

    public Animator nerdAnim;

    public void StartGame()
    {
        //GenerateArticles();
        StartArticle(10f);
        pills[curPill].SetActive(true);
        curButtons = pillsTargets[curPill];
        StartButton.SetActive(false);
    }

    // Start is called before the first frame update
    public void StartArticle(float time)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateSliderOverTime(time));
        if(articles.Count > 0)
        {
            GenerateArticles();
        } else
        {
            foreach(Button dead in articleButtons)
            {
                dead.interactable = false;
            }
        }
        nerdAnim.SetTrigger("newArticle");
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.value == 1 && !done)
        {
            GameOver.SetActive(true);
            nerdAnim.SetTrigger("lookUp");
            done = true;
        }
    }

    void GenerateArticles()
    {
        for(int i = 0; i < 3; i++)
        {
            ArticleOption random = articles[Random.Range(0, articles.Count)];

            articleButtons[i].onClick.RemoveAllListeners();
            articleButtons[i].onClick.AddListener(() => StartArticle((float)random.time));

            articleButtons[i].GetComponentInChildren<Text>().text = random.title;

            articles.Remove(random);
        }
        articlesText.text = "ARTICLES\nLEFT:\n" + articles.Count;
    }

    IEnumerator AnimateSliderOverTime(float seconds)
    {
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            timer.value = Mathf.Lerp(0.00f, 1.15f, lerpValue);
            
            yield return null;
        }
    }

    public void targetButton(GameObject button)
    {
        //BadCloudCount--;
        //BadCloudSpots.Add(button.transform.position);
        curButtons--;
        if(curButtons == 0 && curPill < pills.Count - 1)
        {
            NextPill();
        } else if (curButtons == 0 && curPill == pills.Count - 1)
        {
            pillsInFood[curPill].SetActive(true);
            WinScreen.SetActive(true);
            done = true;
            GameManager.Instance.Cousin_DONE = true;
            //Debug.Log("you win");
        }
        Destroy(button);
    }

    void NextPill()
    {
        pillsInFood[curPill].SetActive(true);
        pills[curPill].SetActive(false);
        curPill++;
        pills[curPill].SetActive(true);
        curButtons = pillsTargets[curPill];
        
    }


    public void BackButton(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
