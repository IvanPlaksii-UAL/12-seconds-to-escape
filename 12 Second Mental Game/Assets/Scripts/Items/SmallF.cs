using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallF : MonoBehaviour
{
    GameManager reftoManager;
    public string currentState;//Set, Holding, Thrown, Collected
    private float itemValue = 1.5f, randomX, randomY;
    private int itemWeight = 4;
    private int pickupOrder;
    private int throwTime;
    private Bounds offset;
    bool invSet, carrying;
    // Start is called before the first frame update
    void Start()
    {
        reftoManager = FindObjectOfType<GameManager>();
        currentState = "Set";
        offset = this.GetComponent<SpriteRenderer>().bounds;
        offset.Expand(0.3f);
        invSet = false;
        carrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == "Set" && this.transform.position == new Vector3(0, 0, 0)) Destroy(this.gameObject);

        //colission detection
        if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(offset)) print("touching");

        if (currentState == "Set")
        {
            if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(offset) && (reftoManager.carryWeight + itemWeight < 20) && Input.GetKeyDown(KeyCode.Space))
            {
                pickupOrder = reftoManager.pickupID;
                reftoManager.pickupID++;
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

        //Carrying Items
        if (currentState == "Holding")
        {
            if (invSet == false)
            {
                randomX = Random.Range(8, 12);
                randomY = Random.Range(-2, -7);
                invSet = true;
            }
            this.transform.position = new Vector3(reftoManager.myCamera.transform.position.x + randomX, reftoManager.myCamera.transform.position.y + randomY, 0);
        }

        //Item Dropping
        if (Input.GetKeyDown(KeyCode.Return) && pickupOrder == (reftoManager.pickupID - 1))
        {
            //State Reset
            invSet = false;
            reftoManager.pickupID--;
            throwTime = 15;
            this.transform.position = new Vector3(reftoManager.Player.transform.position.x, reftoManager.Player.transform.position.y, reftoManager.Player.transform.position.z);
            currentState = "Thrown";
        }

        //Item Throwing
        if (currentState == "Thrown" && throwTime > 0)
        {
            throwTime--;
            if (reftoManager.facing == "N") this.transform.position += new Vector3(0, 0.3f, 0);
            if (reftoManager.facing == "E") this.transform.position += new Vector3(0.3f, 0, 0);
            if (reftoManager.facing == "S") this.transform.position -= new Vector3(0, 0.3f, 0);
            if (reftoManager.facing == "W") this.transform.position -= new Vector3(0.3f, 0, 0);
            if (reftoManager.facing == "NE") this.transform.position += new Vector3(0.3f, 0.3f, 0);
            if (reftoManager.facing == "SE") this.transform.position += new Vector3(0.3f, -0.3f, 0);
            if (reftoManager.facing == "SW") this.transform.position += new Vector3(-0.3f, -0.3f, 0);
            if (reftoManager.facing == "NW") this.transform.position += new Vector3(-0.3f, 0.3f, 0);
        }
        if (currentState == "Thrown" && throwTime == 0) currentState = "Set";

        if (this.GetComponent<SpriteRenderer>().bounds.Intersects(reftoManager.exitZone.GetComponent<SpriteRenderer>().bounds) && currentState == "Thrown")
        {
            reftoManager.Food = reftoManager.Food + itemValue;
            currentState = "Collected";
        }

        if (currentState == "Collected") Destroy(this.gameObject);
    }
}
