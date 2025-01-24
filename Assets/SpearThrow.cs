using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrow : MonoBehaviour
{
    public GameObject spear;
    public BoxCollider2D boxCollider2D;
    public Transform spearThrowPos;
    public Transform transform;
    public Transform spearTransform;
    public bool groundIn;
    private Vector2 size;
    void Start() {
        boxCollider2D = spear.GetComponent<BoxCollider2D>();
        transform = GetComponent<Transform>();
        spearTransform = spear.GetComponent<Transform>();
        groundIn = false;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.E))
        {
            groundIn = false;
            Vector2 overlapBoxSize = new Vector2(boxCollider2D.size.x*spearTransform.lossyScale.x - 1f, boxCollider2D.size.y*spearTransform.lossyScale.y);
            Collider2D[] hitInfo = Physics2D.OverlapBoxAll(spearThrowPos.position, overlapBoxSize, transform.eulerAngles.z);
            foreach (Collider2D hit in hitInfo)
            {
                if (hit.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    groundIn = true;
                }
            }
            if (groundIn != true)
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
                Vector3 scale = newObj.transform.localScale;
                scale.x = transform.localScale.x;
                newObj.transform.localScale = scale;
            }
        }
    }
    void OnDrawGizmos()
    {
        Vector2 overlapBoxSize = new Vector2(boxCollider2D.size.x*spearTransform.lossyScale.x -1f, boxCollider2D.size.y*spearTransform.lossyScale.y);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spearThrowPos.position, overlapBoxSize);
    }
}
