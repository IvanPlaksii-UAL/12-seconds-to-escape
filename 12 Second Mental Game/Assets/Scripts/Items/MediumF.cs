using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumF : MonoBehaviour
{
    GameManager reftoManager;
    private string currentState;//Set, Holding, Collected
    private float foodValue = 1.5f, randomX, randomY;
    private int itemWeight = 7;
    private Bounds offset;
    bool invset;
    // Start is called before the first frame update
    void Start()
    {
        reftoManager = FindObjectOfType<GameManager>();
        currentState = "Set";
        offset = this.GetComponent<SpriteRenderer>().bounds;
        offset.Expand(1);
        invset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == "Set" && this.transform.position == new Vector3(0, 0, 0)) Destroy(this.gameObject);

        if (currentState == "Set")
        {
            if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(offset) && Input.GetKeyDown(KeyCode.Space))
            {
                print("near");
                currentState = "Holding";
            }
        }

        if (currentState == "Holding") GetComponent<SpriteRenderer>().sortingOrder = 8;
        else GetComponent<SpriteRenderer>().sortingOrder = 0;

        if (currentState == "Holding")
        {
            if (invset == false)
            {
                randomX = Random.Range(8, 12);
                randomY = Random.Range(-2, -7);
                invset = true;
            }
            this.transform.position = new Vector3(reftoManager.myCamera.transform.position.x + randomX, reftoManager.myCamera.transform.position.y + randomY, 0);
        }
    }
}
