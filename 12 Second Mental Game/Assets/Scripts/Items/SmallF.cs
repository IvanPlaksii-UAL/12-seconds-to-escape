using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallF : MonoBehaviour
{
    GameManager reftoManager;
    public string currentState;//Set, Holding, Thrown, Collected
    private float itemValue = 1.5f, randomX, randomY;
    private int itemWeight = 4;
    public int pickupOrder;
    private int throwTime;
    private string throwDir;
    private Bounds offset;
    bool invSet, carrying;
    // Start is called before the first frame update
    void Start()
    {
        reftoManager = FindObjectOfType<GameManager>();
        currentState = "Set";
        offset = this.GetComponent<SpriteRenderer>().bounds;
        offset.Expand(0.1f);
        invSet = false;
        carrying = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        else GetComponent<SpriteRenderer>().sortingOrder = 4;

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
            throwDir = reftoManager.facing;
            this.transform.position = new Vector3(reftoManager.Player.transform.position.x, reftoManager.Player.transform.position.y, reftoManager.Player.transform.position.z);
            currentState = "Thrown";
        }

        //Item Throwing
        if (currentState == "Thrown" && throwTime > 0)
        {
            throwTime--;
            if (throwDir == "N") this.transform.position += new Vector3(0, 0.3f, 0);
            if (throwDir == "E") this.transform.position += new Vector3(0.3f, 0, 0);
            if (throwDir == "S") this.transform.position -= new Vector3(0, 0.3f, 0);
            if (throwDir == "W") this.transform.position -= new Vector3(0.3f, 0, 0);
            if (throwDir == "NE") this.transform.position += new Vector3(0.3f, 0.3f, 0);
            if (throwDir == "SE") this.transform.position += new Vector3(0.3f, -0.3f, 0);
            if (throwDir == "SW") this.transform.position += new Vector3(-0.3f, -0.3f, 0);
            if (throwDir == "NW") this.transform.position += new Vector3(-0.3f, 0.3f, 0);
        }
        if (currentState == "Thrown" && throwTime == 0)
        {
            currentState = "Set";

            Start();
        }

        if (this.GetComponent<SpriteRenderer>().bounds.Intersects(reftoManager.exitZone.GetComponent<SpriteRenderer>().bounds) && currentState == "Thrown")
        {
            if (carrying == true) reftoManager.carryWeight = reftoManager.carryWeight -= itemWeight;
            reftoManager.Food = reftoManager.Food + itemValue;
            currentState = "Collected";
        }

        if (currentState == "Collected") Destroy(this.gameObject);
    }
}
