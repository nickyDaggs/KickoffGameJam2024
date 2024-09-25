using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PipePuzzleManager : MonoBehaviour
{
    [System.Serializable]
    public class Pipe
    {
        public Transform pipeObject;
        public int turns = 0;
        public int turnLimit = 0;
    }

    public List<Pipe> pipesStored;
    public List<int> correctPipesValues;

    Sprite pipeFlat;

    public float RotationSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < pipesStored.Count; i++)
        {
            int number = i;

            pipeRandPress(number);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pipePress(int pipeNum)
    {
        //anim.SetTrigger("Turn");
        //Quaternion.Lerp(transform.rotation, transform.rotation + 90, Time.time * RotationSpeed);
        Pipe change = pipesStored[pipeNum];

        change.turns++;
        
        if (change.turns <= change.turnLimit)
        {
            change.pipeObject.Rotate(0, 0, 90);
        } else
        {
            change.turns = 0;
            change.pipeObject.rotation = Quaternion.identity;
        }


        CheckPipes();
    }

    void pipeRandPress(int pipeNum)
    {
        //anim.SetTrigger("Turn");
        //Quaternion.Lerp(transform.rotation, transform.rotation + 90, Time.time * RotationSpeed);
        Pipe change = pipesStored[pipeNum];

        change.turns = Random.Range(0, pipesStored[pipeNum].turnLimit + 1);

        for(int i = 0; i < change.turns; i++)
        {
            change.pipeObject.Rotate(0, 0, 90);
        }

        
    }

    void CheckPipes()
    {
        int sum = 0;
        for(int i = 0; i < correctPipesValues.Count; i++)
        {
            if (pipesStored[i].turns >= correctPipesValues[i])
            {
                sum++;
            }
            else
            {
                return;
            }
        }

        if(sum == correctPipesValues.Count)
        {
            foreach(Pipe pipeObj in pipesStored)
            {
                //pipeObj.pipeObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(1);
    }
}
