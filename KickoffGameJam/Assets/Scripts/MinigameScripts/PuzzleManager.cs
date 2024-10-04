using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [System.Serializable]
    public class PuzzleList
    {
        public List<DragDrop> puzzlePieces = new List<DragDrop>();
    }

    public List<PuzzleList> puzzles = new List<PuzzleList>();
    public List<PuzzleSlot> slots = new List<PuzzleSlot>();
    public List<RectTransform> puzzlePieceSpots;
    public List<RectTransform> BadCloudSpotsOG;
    List<Vector3> BadCloudSpots;

    public GameObject BadCloudPrefab;
    public int BadCloudCount;
    public float BadCloudSpawn;
    public int BadCloudLimit;
    public Slider cloudMeter;

     int curPuzzle = -1;
    public bool notSolved = true;

    public GameObject CloudParent;
    public GameObject GameOver;
    public GameObject WinScreen;
    public GameObject StartButtonn;

    // Start is called before the first frame update
    public void StartGame()
    {
        NextPuzzle();
        StartButtonn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(slots.All(x => x.solved))
        {
            notSolved = false;
            NextPuzzle();
        }

        if(BadCloudCount > BadCloudLimit)
        {
            notSolved = false;
            GameOver.SetActive(true);
            for (int i = 0; i < puzzles[curPuzzle].puzzlePieces.Count; i++)
            {
                puzzles[curPuzzle].puzzlePieces[i].gameObject.SetActive(false);
                slots[i].solved = false;
            }
        }
    }

    void NextPuzzle() {
        if (curPuzzle > -1)
        {
            foreach(DragDrop drag in puzzles[curPuzzle].puzzlePieces)
            {
                drag.gameObject.SetActive(false);
            }
            if (curPuzzle == puzzles.Count - 1)
            {
                WinScreen.SetActive(true);
                GameManager.Instance.BigBro_DONE = true;
                Debug.Log("end");
                return;
            }
        }

        curPuzzle++;
        BadCloudSpots = BadCloudSpotsOG.ConvertAll(x => x.position);
        for (int i = puzzlePieceSpots.Count - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            RectTransform temp = puzzlePieceSpots[i];

            // Swap the new and old values
            puzzlePieceSpots[i] = puzzlePieceSpots[rnd];
            puzzlePieceSpots[rnd] = temp;
        }

        for (int i = 0; i < puzzles[curPuzzle].puzzlePieces.Count; i++)
        {
            puzzles[curPuzzle].puzzlePieces[i].gameObject.SetActive(true);
            puzzles[curPuzzle].puzzlePieces[i].GetComponent<RectTransform>().position = puzzlePieceSpots[i].position;
            slots[i].solved = false;
            slots[i].puzzlePiece = puzzles[curPuzzle].puzzlePieces[i].gameObject;
        }
        notSolved = true;
        StartCoroutine(RepeatingCloud());
    }

    public void cloudButton(GameObject button)
    {
        BadCloudCount--;
        cloudMeter.value = (float) BadCloudCount / BadCloudLimit;
        BadCloudSpots.Add(button.transform.position);
        Destroy(button);
    }

    IEnumerator RepeatingCloud()
    {
        
        while(notSolved || BadCloudSpots.Count < 1)
        {
            yield return new WaitForSeconds(BadCloudSpawn);
            BadCloudCount++;
            int rand = Random.Range(0, BadCloudSpots.Count);
            Vector3 trans = BadCloudSpots[rand];
            GameObject newCloud = Instantiate(BadCloudPrefab, CloudParent.transform);
            newCloud.transform.position = trans;
            newCloud.GetComponent<Button>().onClick.AddListener(() => cloudButton(newCloud));
            cloudMeter.value = (float) BadCloudCount / BadCloudLimit;
            
            BadCloudSpots.Remove(trans);
        }

        
    }

    public void BackButton(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
