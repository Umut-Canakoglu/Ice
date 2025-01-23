using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrow : MonoBehaviour
{
    public GameObject spear;
    public Transform spearThrowPos;
    public Transform transform;
    void Start() {
        transform = GetComponent<Transform>();
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject[] spears = GameObject.FindGameObjectsWithTag("Spear");
            int length = spears.Length;
            if (length == 2)
            {
                foreach (GameObject obj in spears)
                {
                    if (obj.name == "Spear1")
                    {
                        Destroy(obj);
                        length -= 1;
                    } else
                    {
                        obj.name = "Spear" + length.ToString();
                    }
                }
            }
            GameObject newObj = Instantiate(spear, spearThrowPos.position, transform.rotation);
            newObj.name = "Spear" + (length + 1).ToString();
        }
    }
}
