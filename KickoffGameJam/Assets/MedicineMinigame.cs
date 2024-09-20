using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    void Start()
    {
        GenerateArticles();
        StartArticle(10f);
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
