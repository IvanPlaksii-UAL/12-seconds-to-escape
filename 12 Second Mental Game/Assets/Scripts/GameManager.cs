using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string GameState, //Reset, PlayAgain, Menu, Items, HowTo, Playable, Result, Score
        SortState;
    public float Food, Water, MoveSpeed;
    public int DaysSurvived, Highscore, FrameCount, Timer, SlotSelected;
    public int FoodSpawnS, FoodSpawnM, FoodSpawnL, WaterSpawnS, WaterSpawnM, WaterSpawnL, CardSpawn, NoteSpawn, Roll, ItemLocation;
    private bool SpecialSpawnW, SpecialSpawnF, CanSpawnSF1, CanSpawnSF2, CanSpawnSF3, CanSpawnSF4, CanSpawnSF5, CanSpawnSF6, CanSpawnMF1, CanSpawnMF2, CanSpawnMF3, CanSpawnMF4, CanSpawnLF1, CanSpawnLF2, CanSpawnSW1, CanSpawnSW2, CanSpawnSW3, CanSpawnSW4, CanSpawnSW5, CanSpawnSW6, CanSpawnMW1, CanSpawnMW2, CanSpawnMW3, CanSpawnLW1, CanSpawnLW2;
    public GameObject PlayAreaMain, PlayAreaHead, Player, CameraObject, ItemSpawn;
    Vector3 FoodS1, FoodS2, FoodS3, FoodS4, FoodS5, FoodS6, FoodM1, FoodM2, FoodM3, FoodM4, FoodL1, FoodL2, WaterS1, WaterS2, WaterS3, WaterS4, WaterS5, WaterS6, WaterM1, WaterM2, WaterM3, WaterL1, WaterL2;
    public Camera myCamera;
    public Sprite Default;
    public TextMesh Countdown;
    void Start()
    {
        Application.targetFrameRate = 60;
        GameState = "Reset";
        Highscore = 0; 
        MoveSpeed = 0.4f;
        SpawnPositionSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState == "Menu")
        {

        }

        if (GameState == "Reset")
        {
            SetUp();
        }

        if (GameState == "Spawns")
        {
            Spawns();
            //add load screen
            if (FoodSpawnS == 0 && FoodSpawnM == 0 && FoodSpawnL == 0 && WaterSpawnS == 0 && WaterSpawnM == 0 && WaterSpawnL == 0 && CardSpawn == 0 && NoteSpawn == 0 && SpecialSpawnF == true && SpecialSpawnW == true) GameState = "Playable";
        }

        if (GameState == "Playable")
        {
            TimeKeeper();
            InvManager();
            PlayerControls();
            PlayerCollision();
        }
    }

    private void TimeKeeper()
    {
        FrameCount++;
        if (FrameCount == 60)
        {
            Timer--;
            FrameCount = 0;
        }

        Countdown.text = ($"Time:{Timer}");
    }

    private void Spawns()
    {
        //Small Food (66% Chance)
        if (FoodSpawnS > 0)
        {
            FoodSpawnS--;
            Roll = Random.Range(1, 101);
            if (Roll < 67)
            {
                ItemSpawn = new GameObject("SmallFood");
                ItemSpawn.AddComponent<SpriteRenderer>();
                ItemSpawn.AddComponent<SmallF>();
                ItemSpawn.GetComponent<SpriteRenderer>().sprite = Default; //Change sprite
                ItemSpawn.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
                ItemLocation = Random.Range(1, 7);
                if (ItemLocation == 1)
                {
                    if (CanSpawnSF1 == true)
                    {
                        ItemSpawn.transform.position = FoodS1;
                        CanSpawnSF1 = false;
                    }
                    else if (CanSpawnSF1 == false) FoodSpawnS++;
                }
                if (ItemLocation == 2)
                {
                    if (CanSpawnSF2 == true)
                    {
                        ItemSpawn.transform.position = FoodS2;
                        CanSpawnSF2 = false;
                    }
                    else if (CanSpawnSF2 == false) FoodSpawnS++;
                }
                if (ItemLocation == 3)
                {
                    if (CanSpawnSF3 == true)
                    {
                        ItemSpawn.transform.position = FoodS3;
                        CanSpawnSF3 = false;
                    }
                    else if (CanSpawnSF3 == false) FoodSpawnS++;
                }
                if (ItemLocation == 4)
                {
                    if (CanSpawnSF4 == true)
                    {
                        ItemSpawn.transform.position = FoodS4;
                        CanSpawnSF4 = false;
                    }
                    else if (CanSpawnSF4 == false) FoodSpawnS++;
                }
                if (ItemLocation == 5)
                {
                    if (CanSpawnSF5 == true)
                    {
                        ItemSpawn.transform.position = FoodS5;
                        CanSpawnSF5 = false;
                    }
                    else if (CanSpawnSF5 == false) FoodSpawnS++;
                }
                if (ItemLocation == 6)
                {
                    if (CanSpawnSF6 == true)
                    {
                        ItemSpawn.transform.position = FoodS6;
                        CanSpawnSF6 = false;
                    }
                    else if (CanSpawnSF6 == false) FoodSpawnS++;
                }
            }
        }


        //Medium Food (30% Chance)
        if (FoodSpawnM > 0)
        {
            FoodSpawnM--;
            Roll = Random.Range(1, 101);
            if (Roll < 31)
            {
                ItemSpawn = new GameObject("MediumFood");
                ItemSpawn.AddComponent<SpriteRenderer>();
                ItemSpawn.AddComponent<MediumF>();
                ItemSpawn.GetComponent<SpriteRenderer>().sprite = Default; //Change sprite
                ItemSpawn.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0, 1);
                ItemLocation = Random.Range(1, 5);
                if (ItemLocation == 1)
                {
                    if (CanSpawnMF1 == true)
                    {
                        ItemSpawn.transform.position = FoodM1;
                        CanSpawnMF1 = false;
                    }
                    else if (CanSpawnMF1 == false) FoodSpawnM++;
                }
                if (ItemLocation == 2)
                {
                    if (CanSpawnMF2 == true)
                    {
                        ItemSpawn.transform.position = FoodM2;
                        CanSpawnMF2 = false;
                    }
                    else if (CanSpawnMF2 == false) FoodSpawnM++;
                }
                if (ItemLocation == 3)
                {
                    if (CanSpawnMF3 == true)
                    {
                        ItemSpawn.transform.position = FoodM3;
                        CanSpawnMF3 = false;
                    }
                    else if (CanSpawnMF3 == false) FoodSpawnM++;
                }
                if (ItemLocation == 4)
                {
                    if (CanSpawnMF4 == true)
                    {
                        ItemSpawn.transform.position = FoodM4;
                        CanSpawnMF4 = false;
                    }
                    else if (CanSpawnMF4 == false) FoodSpawnM++;
                }
            }
        }

        //Large Food (80% Chance)
        if (FoodSpawnL > 0)
        {
            FoodSpawnL--;
            Roll = Random.Range(1, 101);
            if (Roll < 81)
            {
                ItemSpawn = new GameObject("LargeFood");
                ItemSpawn.AddComponent<SpriteRenderer>();
                ItemSpawn.AddComponent<LargeF>();
                ItemSpawn.GetComponent<SpriteRenderer>().sprite = Default; //Change sprite
                ItemSpawn.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.7f, 0, 1);
                ItemLocation = Random.Range(1, 3);
                if (ItemLocation == 1)
                {
                    if (CanSpawnLF1 == true)
                    {
                        ItemSpawn.transform.position = FoodL1;
                        CanSpawnLF1 = false;
                    }
                    else if (CanSpawnLF1 == false) FoodSpawnL++;
                }
                if (ItemLocation == 2)
                {
                    if (CanSpawnLF2 == true)
                    {
                        ItemSpawn.transform.position = FoodL2;
                        CanSpawnLF2 = false;
                    }
                    else if (CanSpawnLF2 == false) FoodSpawnL++;
                }
            }
        }

        //Small Water (75% Chance)
        if (WaterSpawnS > 0)
        {
            WaterSpawnS--;
            Roll = Random.Range(1, 101);
            if (Roll < 76)
            {
                ItemSpawn = new GameObject("SmallWater");
                ItemSpawn.AddComponent<SpriteRenderer>();
                ItemSpawn.AddComponent<SmallW>();
                ItemSpawn.GetComponent<SpriteRenderer>().sprite = Default; //Change sprite
                ItemSpawn.GetComponent<SpriteRenderer>().color = new Color(0, 0.7f, 1, 1);
                ItemLocation = Random.Range(1, 7);
                if (ItemLocation == 1)
                {
                    if (CanSpawnSW1 == true)
                    {
                        ItemSpawn.transform.position = WaterS1;
                        CanSpawnSW1 = false;
                    }
                    else if (CanSpawnSW1 == false) WaterSpawnS++;
                }
                if (ItemLocation == 2)
                {
                    if (CanSpawnSW2 == true)
                    {
                        ItemSpawn.transform.position = WaterS2;
                        CanSpawnSW2 = false;
                    }
                    else if (CanSpawnSW2 == false) WaterSpawnS++;
                }
                if (ItemLocation == 3)
                {
                    if (CanSpawnSW3 == true)
                    {
                        ItemSpawn.transform.position = WaterS3;
                        CanSpawnSW3 = false;
                    }
                    else if (CanSpawnSW3 == false) WaterSpawnS++;
                }
                if (ItemLocation == 4)
                {
                    if (CanSpawnSW4 == true)
                    {
                        ItemSpawn.transform.position = WaterS4;
                        CanSpawnSW4 = false;
                    }
                    else if (CanSpawnSW4 == false) WaterSpawnS++;
                }
                if (ItemLocation == 5)
                {
                    if (CanSpawnSW5 == true)
                    {
                        ItemSpawn.transform.position = WaterS5;
                        CanSpawnSW5 = false;
                    }
                    else if (CanSpawnSW5 == false) WaterSpawnS++;
                }
                if (ItemLocation == 6)
                {
                    if (CanSpawnSW6 == true)
                    {
                        ItemSpawn.transform.position = WaterS6;
                        CanSpawnSW6 = false;
                    }
                    else if (CanSpawnSW6 == false) WaterSpawnS++;
                }
            }
        }
        //Medium Water (50% Chance)
        if (WaterSpawnM > 0)
        {
            WaterSpawnM--;
            Roll = Random.Range(1, 101);
            if (Roll < 51)
            {
                ItemSpawn = new GameObject("MediumWater");
                ItemSpawn.AddComponent<SpriteRenderer>();
                ItemSpawn.AddComponent<MediumW>();
                ItemSpawn.GetComponent<SpriteRenderer>().sprite = Default; //Change sprite
                ItemSpawn.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
                ItemLocation = Random.Range(1, 5);

                /*
                SpawnLocation(1, CanSpawnMW1, WaterM1, WaterSpawnM);
                SpawnLocation(2, CanSpawnMW2, WaterM2, WaterSpawnM);
                SpawnLocation(3, CanSpawnMW3, WaterM3, WaterSpawnM);
                */

                if (ItemLocation == 1)
                {
                    if (CanSpawnMW1 == true)
                    {
                        ItemSpawn.transform.position = WaterM1;
                        CanSpawnMW1 = false;
                    }
                    else if (CanSpawnMW1 == false) WaterSpawnM++;
                }
                if (ItemLocation == 2)
                {
                    if (CanSpawnMW2 == true)
                    {
                        ItemSpawn.transform.position = WaterM2;
                        CanSpawnMW2 = false;
                    }
                    else if (CanSpawnMW2 == false) WaterSpawnM++;
                }
                if (ItemLocation == 3)
                {
                    if (CanSpawnMW3 == true)
                    {
                        ItemSpawn.transform.position = WaterM3;
                        CanSpawnMW3 = false;
                    }
                    else if (CanSpawnMW3 == false) WaterSpawnM++;
                }
            }
        }
        /*
        //Large Water (90% Chance)
        if (WaterSpawnL > 0)
        {
            WaterSpawnL--;
            Roll = Random.Range(1, 101);
            if (Roll < 91)
            {
                ItemSpawn = new GameObject("LargeWater");
                ItemSpawn.AddComponent<SpriteRenderer>();
                ItemSpawn.AddComponent<LargeW>();
                ItemSpawn.GetComponent<SpriteRenderer>().sprite = Default; //Change sprite
                ItemSpawn.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0, 1f, 1);
                ItemLocation = Random.Range(1, 3);

                /*
                SpawnLocation(1, CanSpawnLW1, WaterL1, WaterSpawnL);
                SpawnLocation(2, CanSpawnLW2, WaterL2, WaterSpawnL);
                

                if (ItemLocation == 1)
                {
                    if (CanSpawnLW1 == true)
                    {
                        ItemSpawn.transform.position = WaterL1;
                        CanSpawnLW1 = false;
                    }
                    else if (CanSpawnLW1 == false) WaterSpawnL++;
                }
                if (ItemLocation == 2)
                {
                    if (CanSpawnLW2 == true)
                    {
                        ItemSpawn.transform.position = WaterL2;
                        CanSpawnLW2 = false;
                    }
                    else if (CanSpawnLW2 == false) WaterSpawnL++;
                }
            }
    
            //Notes (33% Chance)

            //Keycards (20% Chance - 50% Red 50% Green)

            //Special

        }*/
    }
    
    private void SetUp()
    {
        Food = 0;
        Water = 0;
        FoodSpawnS = 6;
        FoodSpawnM = 4;
        FoodSpawnL = 2;
        WaterSpawnS = 6;
        WaterSpawnM = 4;
        WaterSpawnL = 2;
        CardSpawn = 3;
        NoteSpawn = 2;
        SpecialSpawnF = false;
        SpecialSpawnW = false;
        CanSpawnSF1 = true;
        CanSpawnSF2 = true;
        CanSpawnSF3 = true;
        CanSpawnSF4 = true;
        CanSpawnSF5 = true;
        CanSpawnSF6 = true;
        CanSpawnMF1 = true;
        CanSpawnMF2 = true;
        CanSpawnMF3 = true;
        CanSpawnMF4 = true;
        CanSpawnLF1 = true;
        CanSpawnLF2 = true;
        CanSpawnSW1 = true;
        CanSpawnSW2 = true;
        CanSpawnSW3 = true;
        CanSpawnSW4 = true;
        CanSpawnSW5 = true;
        CanSpawnSW6 = true;
        CanSpawnMW1 = true;
        CanSpawnMW2 = true;
        CanSpawnMW3 = true;
        DaysSurvived = 0;
        Timer = 12;
        FrameCount = 0;
        GameState = "Spawns";
    }

    private void SpawnLocation(int _randomLocation, bool _canSpawn, Vector3 _spawnLocation, int spawnType)
    {
        if (ItemLocation == _randomLocation)
        {
            if (_canSpawn == true)
            {
                print("spawned");
                ItemSpawn.transform.position = _spawnLocation;
                _canSpawn = false;
            }
            else if (_canSpawn == false) spawnType++;
        }
    }

    private void ItemGeneration(string _itemName, Vector4 _spriteColor) //figure out how to add script
    {
        ItemSpawn = new GameObject("MediumWater");
        ItemSpawn.AddComponent<SpriteRenderer>();
        ItemSpawn.AddComponent<MediumW>();
        ItemSpawn.GetComponent<SpriteRenderer>().sprite = Default; //Change sprite
        ItemSpawn.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
        ItemLocation = Random.Range(1, 5);
    }
    private void InvManager()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SlotSelected = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SlotSelected = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SlotSelected = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SlotSelected = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SlotSelected = 5;
        }


        /*if (SlotSelected == 1)
        {
            InvSelector.transform.position = new Vector3(myCamera.transform.position.x - 8, myCamera.transform.position.y + 4, 1);
        }
        if (SlotSelected == 2)
        {
            InvSelector.transform.position = new Vector3(myCamera.transform.position.x - 7, myCamera.transform.position.y + 4, 1);
        }
        if (SlotSelected == 3)
        {
            InvSelector.transform.position = new Vector3(myCamera.transform.position.x - 6, myCamera.transform.position.y + 4, 1);
        }
        if (SlotSelected == 4)
        {
            InvSelector.transform.position = new Vector3(myCamera.transform.position.x - 5, myCamera.transform.position.y + 4, 1);
        }*/


        //Inventory Manager
        if (SortState == "Idle")
        {

        }
        /*if (SortState == "Pickup")
        {
            //Checking if slot is free
            if (inv1.CurrentState == "Empty")
            {
                inv1.CurrentState = "Set";
                SortState = "Sort";
            }
            else if (inv2.CurrentState == "Empty")
            {
                inv2.CurrentState = "Set";
                SortState = "Sort";
            }
            else if (inv3.CurrentState == "Empty")
            {
                inv3.CurrentState = "Set";
                SortState = "Sort";
            }
            else if (inv4.CurrentState == "Empty")
            {
                inv4.CurrentState = "Set";
                SortState = "Sort";
            }
            else if (inv5.CurrentState == "Empty")
            {
                inv5.CurrentState = "Set";
                SortState = "Sort";
            }
            else
            {
                print("No Space");
            }
        }*/
    }
    private void PlayerControls()
    {
        //Movement
        if (Player.GetComponent<SpriteRenderer>().bounds.Intersects(PlayAreaMain.GetComponent<SpriteRenderer>().bounds) || Player.GetComponent<SpriteRenderer>().bounds.Intersects(PlayAreaHead.GetComponent<SpriteRenderer>().bounds))
        {
            if (Input.GetKey(KeyCode.W))
            {
                Player.transform.position += new Vector3(0, MoveSpeed, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Player.transform.position -= new Vector3(0, MoveSpeed, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                Player.transform.position += new Vector3(MoveSpeed, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                Player.transform.position -= new Vector3(MoveSpeed, 0, 0);
            }
        }

        //Camera following
        if (Player.transform.position.x > -7 && Player.transform.position.x < 2)
        {
            CameraObject.transform.position = new Vector3(Player.transform.position.x, 0, -10);
        }
    }

    private void PlayerCollision()
    {
        //Left Bound
        if (Player.GetComponent<SpriteRenderer>().bounds.min.x < PlayAreaHead.GetComponent<SpriteRenderer>().bounds.min.x) Player.transform.position += new Vector3(0.4f, 0, 0);
        //Right Bound
        if (Player.GetComponent<SpriteRenderer>().bounds.max.x > PlayAreaMain.GetComponent<SpriteRenderer>().bounds.max.x) Player.transform.position -= new Vector3(0.4f, 0, 0);
        //Top Bound
        if (Player.GetComponent<SpriteRenderer>().bounds.max.y > PlayAreaMain.GetComponent<SpriteRenderer>().bounds.max.y) Player.transform.position -= new Vector3(0, 0.4f, 0);
        //Bottom Bound
        if (Player.GetComponent<SpriteRenderer>().bounds.min.y < PlayAreaMain.GetComponent<SpriteRenderer>().bounds.min.y) Player.transform.position += new Vector3(0, 0.4f, 0);
    }

    private void SpawnPositionSet()
    {
        FoodS1 = new Vector3(-16, -4, 0);
        FoodS2 = new Vector3(-17.5f, 2.5f, 0);
        FoodS3 = new Vector3(-15, 4.5f, 0);
        FoodS4 = new Vector3(7.3f, -3.5f, 0);
        FoodS5 = new Vector3(8.7f, -4, 0);
        FoodS6 = new Vector3(11.7f, -6, 0);
        FoodM1 = new Vector3(-4.5f,6.5f,0);
        FoodM2 = new Vector3(1.5f, 4.2f, 0);
        FoodM3 = new Vector3(9, 6.5f, 0);
        FoodM4 = new Vector3(12, 5.5f, 0);
        FoodL1 = new Vector3(-2.5f, -4.5f, 0);
        FoodL2 = new Vector3(3.5f, -6, 0);
        WaterS1 = new Vector3(-14.5f, -5, 0);
        WaterS2 = new Vector3(-17.5f, -2.5f, 0);
        WaterS3 = new Vector3(-14.5f, 5, 0);
        WaterS4 = new Vector3(7.3f, -5, 0);
        WaterS5 = new Vector3(11.7f, -5, 0);
        WaterS6 = new Vector3(8.7f, -6.5f, 0);
        WaterM1 = new Vector3(1.5f, 6.5f, 0);
        WaterM2 = new Vector3(7, 6.5f, 0);
        WaterM3 = new Vector3(12, 6.5f, 0);
        WaterL1 = new Vector3(-2.5f, -6, 0);
        WaterL2 = new Vector3(3.5f, 4.5f, 0);
    }
}
