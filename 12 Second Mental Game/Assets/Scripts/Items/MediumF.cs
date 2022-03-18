﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumF : MonoBehaviour
{
    GameManager reftoManager;
    private string CurrentState;//Set, Holding, Collected
    private float FoodValue;
    private Bounds offset;
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = "Set";
        offset = this.GetComponent<SpriteRenderer>().bounds;
        offset.Expand(1.5f);
        FoodValue = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == "Set" && this.transform.position == new Vector3(0, 0, 0)) Destroy(this.gameObject);

        if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(offset))
        {
            //Sort Inv. State
            CurrentState = "Holding";
        }
    }
}