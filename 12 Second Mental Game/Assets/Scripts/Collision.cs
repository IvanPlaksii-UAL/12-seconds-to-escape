using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    float offset = 0.35f;
    GameManager reftoManager;
    // Start is called before the first frame update
    void Start()
    {
        reftoManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //Left
        if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.max.x < (this.GetComponent<SpriteRenderer>().bounds.min.x + offset))
        {
            if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
            {
                reftoManager.Player.transform.position -= new Vector3(0.35f, 0, 0);
                print("left");
            }
        }

        //Right
        if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.min.x > (this.GetComponent<SpriteRenderer>().bounds.max.x - offset))
        {
            if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
            {
                reftoManager.Player.transform.position += new Vector3(0.35f, 0, 0);
                print("right");
            }
        }
        if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.max.y < this.GetComponent<SpriteRenderer>().bounds.min.y + offset)
        {
            print("yactive");

            //Top
            if ((reftoManager.Player.GetComponent<SpriteRenderer>().bounds.min.y < this.GetComponent<SpriteRenderer>().bounds.max.y + offset))
            {
                if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
                {
                    print("top");
                    reftoManager.Player.transform.position -= new Vector3(0, 0.35f, 0);
                }
            }
        }
        if(reftoManager.Player.GetComponent<SpriteRenderer>().bounds.min.y > this.GetComponent<SpriteRenderer>().bounds.max.y - offset)
        {    
            //Bottom
            if ((reftoManager.Player.GetComponent<SpriteRenderer>().bounds.max.y > this.GetComponent<SpriteRenderer>().bounds.min.y - offset))
            {
                if (reftoManager.Player.GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
                {
                    print("bottom");
                    reftoManager.Player.transform.position += new Vector3(0, 0.35f, 0);
                }
            }
        }
    }
}
