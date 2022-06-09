using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    GameManager reftoManager;
    DoorInteraction reftoDoor;
    public string currentState;//Set, Holding, Thrown, Collected
    private int itemWeight = 2, selfdestruct = 10;
    private float randomX, randomY;
    private Bounds offset;
    GameObject infoBox;
    bool invSet, carrying;
    // Start is called before the first frame update
    void Start()
    {
        reftoManager = FindObjectOfType<GameManager>();
        reftoDoor = FindObjectOfType<DoorInteraction>();
        currentState = "Set";
        offset = this.GetComponent<SpriteRenderer>().bounds;
        offset.Expand(0.1f);
        invSet = false;
        InfoGeneration();
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

        if (currentState == "Holding")
        {
            reftoDoor.holdingCard = true;

            if (invSet == false)
            {
                randomX = Random.Range(8, 12);
                randomY = Random.Range(-2, -7);
                invSet = true;
            }
            this.transform.position = new Vector3(reftoManager.myCamera.transform.position.x + randomX, reftoManager.myCamera.transform.position.y + randomY, 0);
        }

        //Item usage
        if (currentState == "Used")
        {
            if (selfdestruct == 0) Destroy(this.gameObject);
        }

        if(reftoManager.GameState == "Reset" || reftoDoor.doorOpen == true)  
        {
            reftoManager.carryWeight = reftoManager.carryWeight -= itemWeight;
            Destroy(this.gameObject);
        }
    }

    private void InfoGeneration()
    {
        infoBox = new GameObject("InfoBox");
        infoBox.AddComponent<Transform>();
        infoBox.AddComponent<MeshRenderer>();
        infoBox.AddComponent<TextMesh>();
        infoBox.transform.parent = this.transform;
        infoBox.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1f, -1);
        infoBox.GetComponent<TextMesh>().characterSize = 0.1f;
        infoBox.GetComponent<TextMesh>().fontSize = 40;
        infoBox.GetComponent<MeshRenderer>().sortingOrder = 8;
        infoBox.GetComponent<TextMesh>().color = Color.gray;
        infoBox.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
        infoBox.GetComponent<TextMesh>().alignment = TextAlignment.Center;
        infoBox.GetComponent<TextMesh>().fontStyle = FontStyle.Bold;
        infoBox.GetComponent<TextMesh>().text = ($"Value: N/A\nWeight: {itemWeight}");
    }
}
