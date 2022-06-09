using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    GameManager reftoManager;
    public bool holdingCard, doorOpen;
    private Bounds offset;

    // Start is called before the first frame update
    void Start()
    {
        holdingCard = false;
        doorOpen = false;
        reftoManager = FindObjectOfType<GameManager>();
        offset = this.GetComponent<SpriteRenderer>().bounds;
        offset.Expand(0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(offset) && Input.GetKeyDown(KeyCode.Return) && holdingCard == true)
        {
            doorOpen = true;
            this.gameObject.SetActive(false);
        }
    }
}
