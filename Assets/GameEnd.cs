using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private bool endOpen;
    void Start()
    {
        endOpen = false;
    }
    void Update()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] campfires = GameObject.FindGameObjectsWithTag("CampFire");
        if (player.Length == 0)
        {
            SceneManager.LoadScene("Restart", LoadSceneMode.Single);
        } 
        endOpen = true;
        foreach (GameObject campFire in campfires)
        {
            if (campFire.GetComponent<Fire>().open != true)
            {
                endOpen = false;
            }
        }
        if (endOpen == true)
        {
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
        }
    }
}
