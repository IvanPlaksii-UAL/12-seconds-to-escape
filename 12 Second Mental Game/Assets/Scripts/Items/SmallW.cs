using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallW : MonoBehaviour
{
    GameManager reftoManager;
    private string currentState;//Set, Holding, Collected
    private float foodValue = 1.5f, randomX, randomY;
    private int itemWeight = 4;
    private Bounds offset;
    bool invSet, carrying;
    // Start is called before the first frame update
    void Start()
    {
        reftoManager = FindObjectOfType<GameManager>();
        currentState = "Set";
        offset = this.GetComponent<SpriteRenderer>().bounds;
        offset.Expand(1);
        invSet = false;
        carrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == "Set" && this.transform.position == new Vector3(0, 0, 0)) Destroy(this.gameObject);

        if (currentState == "Set")
        {
            if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(offset) && Input.GetKeyDown(KeyCode.Space))
            {
                currentState = "Holding";
            }
        }

        if (currentState == "Holding" && carrying == false)
        {
            reftoManager.carryWeight = reftoManager.carryWeight += itemWeight;
            carrying = true;
        }
        else if (currentState != "Holding" && carrying == true)
        {
            reftoManager.carryWeight = reftoManager.carryWeight -= itemWeight;
            carrying = false;
        }
            if (currentState == "Holding") GetComponent<SpriteRenderer>().sortingOrder = 8;
            else GetComponent<SpriteRenderer>().sortingOrder = 0;

        if (currentState == "Holding")
        {
            if (invSet == false)
            {
                randomX = Random.Range(8, 12);
                randomY = Random.Range(-2, -7);
                invSet = true;
            }
            this.transform.position = new Vector3(reftoManager.myCamera.transform.position.x + randomX, reftoManager.myCamera.transform.position.y + randomY, 0);

             //if (Input.GetKeyDown(KeyCode.Space)) currentState = "Set";
        }
    }
}
