using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string GameState;//Reset, PlayAgain, Menu, Items, HowTo, Playable, Result, Score
    public string facing; //N, E, S, W, NE, SE, SW, NW
    public float Food, Water, moveSpeed, carryWeight;
    public int daysSurvived, highScore, frameCount, Timer;
    public int foodSpawnS, FoodSpawnM, FoodSpawnL, WaterSpawnS, WaterSpawnM, WaterSpawnL, CardSpawn, Roll, ItemLocation, pickupID;
    [SerializeField] bool specialSpawnW, SpecialSpawnF, CanSpawnSF1, CanSpawnSF2, CanSpawnSF3, CanSpawnSF4, CanSpawnSF5, CanSpawnSF6, CanSpawnMF1, CanSpawnMF2, CanSpawnMF3, CanSpawnMF4, CanSpawnLF1, CanSpawnLF2, CanSpawnSW1, CanSpawnSW2, CanSpawnSW3, CanSpawnSW4, CanSpawnSW5, CanSpawnSW6, CanSpawnMW1, CanSpawnMW2, CanSpawnMW3, CanSpawnLW1, CanSpawnLW2;
    public GameObject PlayAreaMain, PlayAreaHead, Player, CameraObject, itemSpawn, Backpack, weightBar, exitZone;
    private Vector3 FoodS1, FoodS2, FoodS3, FoodS4, FoodS5, FoodS6, FoodM1, FoodM2, FoodM3, FoodM4, FoodL1, FoodL2, WaterS1, WaterS2, WaterS3, WaterS4, WaterS5, WaterS6, WaterM1, WaterM2, WaterM3, WaterL1, WaterL2;
    public Camera myCamera;
    public Sprite Default, smallF, smallW, mediumF, mediumW, largeF, largeW;
    public TextMesh Countdown, ItemsCollected;
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 2;
        GameState = "Reset";
        highScore = 0; 
        moveSpeed = 0.35f;
        SpawnPositionSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) GameState = "Reset";

        if (GameState == "Menu")
        {

        }

        if (GameState == "Reset")
        {
            print("reset state");
            SetUp();
        }

        if (GameState == "Spawns")
        {
            Spawns();
            //add load screen
            if (foodSpawnS == 0 && FoodSpawnM == 0 && FoodSpawnL == 0 && WaterSpawnS == 0 && WaterSpawnM == 0 && WaterSpawnL == 0 /*&& CardSpawn == 0 && SpecialSpawnF == true && SpecialSpawnW == true*/) GameState = "Playable";
        }

        if (GameState == "Playable")
        {
            UserInterface();
            InvManager();
            PlayerControls();
            PlayerCollision();
        }

        if (GameState == "Result")
        {

        }
    }

    private void UserInterface()
    {
        frameCount++;
        if (frameCount == 60)
        {
            Timer--;
            frameCount = 0;
        }

        Countdown.text = ($"Time:{Timer}");

        ItemsCollected.text = ($"Food Collected: {Food} \nWater Collected: {Water}");
    }

    private void GameEnd()
    {
        if (Timer == 0)
        {
            if (Food >= 8 && Water >= 10) GameState = "Win";
            else GameState = "Loss"; 
        }
    }

    private void Spawns()
    {
        //Small Food
        if (foodSpawnS > 0)
        {
            foodSpawnS--;
            {
                itemSpawn = new GameObject("SmallFood");
                itemSpawn.AddComponent<SpriteRenderer>();
                itemSpawn.AddComponent<SmallF>();
                itemSpawn.GetComponent<SpriteRenderer>().sprite = smallF;
                ItemLocation = Random.Range(1, 7);

                CanSpawnSF1 = SpawnLocation(1, CanSpawnSF1, FoodS1, foodSpawnS);
                CanSpawnSF2 = SpawnLocation(2, CanSpawnSF2, FoodS2, foodSpawnS);
                CanSpawnSF3 = SpawnLocation(3, CanSpawnSF3, FoodS3, foodSpawnS);
                CanSpawnSF4 = SpawnLocation(4, CanSpawnSF4, FoodS4, foodSpawnS);
                CanSpawnSF5 = SpawnLocation(5, CanSpawnSF5, FoodS5, foodSpawnS);
                CanSpawnSF6 = SpawnLocation(6, CanSpawnSF6, FoodS6, foodSpawnS);
            }
        }

        //Medium Food
        if (FoodSpawnM > 0)
        {
            FoodSpawnM--;
            {
                itemSpawn = new GameObject("MediumFood");
                itemSpawn.AddComponent<SpriteRenderer>();
                itemSpawn.AddComponent<MediumF>();
                itemSpawn.GetComponent<SpriteRenderer>().sprite = mediumF;
                ItemLocation = Random.Range(1, 5);

                CanSpawnMF1 = SpawnLocation(1, CanSpawnMF1, FoodM1, FoodSpawnM);
                CanSpawnMF2 = SpawnLocation(2, CanSpawnMF2, FoodM2, FoodSpawnM);
                CanSpawnMF3 = SpawnLocation(3, CanSpawnMF3, FoodM3, FoodSpawnM);
                CanSpawnMF4 = SpawnLocation(4, CanSpawnMF4, FoodM4, FoodSpawnM);
            }
        }

        //Large Food
        if (FoodSpawnL > 0)
        {
            FoodSpawnL--;
            {
                itemSpawn = new GameObject("LargeFood");
                itemSpawn.AddComponent<SpriteRenderer>();
                itemSpawn.AddComponent<LargeF>();
                itemSpawn.GetComponent<SpriteRenderer>().sprite = largeF;
                ItemLocation = Random.Range(1, 3);

                CanSpawnLF1 = SpawnLocation(1, CanSpawnLF1, FoodL1, FoodSpawnL);
                CanSpawnLF2 = SpawnLocation(2, CanSpawnLF2, FoodL2, FoodSpawnL);
            }
        }

        //Small Water
        if (WaterSpawnS > 0)
        {
            WaterSpawnS--;
            {
                itemSpawn = new GameObject("SmallWater");
                itemSpawn.AddComponent<SpriteRenderer>();
                itemSpawn.AddComponent<SmallW>();
                itemSpawn.GetComponent<SpriteRenderer>().sprite = smallW;
                ItemLocation = Random.Range(1, 7);

                CanSpawnSW1 = SpawnLocation(1, CanSpawnSW1, WaterS1, WaterSpawnS);
                CanSpawnSW2 = SpawnLocation(2, CanSpawnSW2, WaterS2, WaterSpawnS);
                CanSpawnSW3 = SpawnLocation(3, CanSpawnSW3, WaterS3, WaterSpawnS);
                CanSpawnSW4 = SpawnLocation(4, CanSpawnSW4, WaterS4, WaterSpawnS);
                CanSpawnSW5 = SpawnLocation(5, CanSpawnSW5, WaterS5, WaterSpawnS);
                CanSpawnSW6 = SpawnLocation(6, CanSpawnSW6, WaterS6, WaterSpawnS);
            }
        }

        //Medium Water
        if (WaterSpawnM > 0)
        {
            WaterSpawnM--;
            {
                itemSpawn = new GameObject("MediumWater");
                itemSpawn.AddComponent<SpriteRenderer>();
                itemSpawn.AddComponent<MediumW>();
                itemSpawn.GetComponent<SpriteRenderer>().sprite = mediumW;
                ItemLocation = Random.Range(1, 4);


                CanSpawnMW1 = SpawnLocation(1, CanSpawnMW1, WaterM1, WaterSpawnM);
                CanSpawnMW2 = SpawnLocation(2, CanSpawnMW2, WaterM2, WaterSpawnM);
                CanSpawnMW3 = SpawnLocation(3, CanSpawnMW3, WaterM3, WaterSpawnM);
            }
        }
        
        //Large Water
        if (WaterSpawnL > 0)
        {
            WaterSpawnL--;
            {
                print("waterL");
                itemSpawn = new GameObject("LargeWater");
                itemSpawn.AddComponent<SpriteRenderer>();
                itemSpawn.AddComponent<LargeW>();
                itemSpawn.GetComponent<SpriteRenderer>().sprite = largeW; //Change sprite
                ItemLocation = Random.Range(1, 3);

                CanSpawnLW1 = SpawnLocation(1, CanSpawnLW1, WaterL1, WaterSpawnL);
                CanSpawnLW2 = SpawnLocation(2, CanSpawnLW2, WaterL2, WaterSpawnL);
            }
    
            //Notes (33% Chance)

            //Keycards (20% Chance - 50% Red 50% Green)

            //Special

        }
    }
    
    private void SetUp()
    {
        Food = 0;
        Water = 0;
        foodSpawnS = 4;
        FoodSpawnM = 3;
        FoodSpawnL = 2;
        WaterSpawnS = 4;
        WaterSpawnM = 3;
        WaterSpawnL = 2;
        CardSpawn = 1;
        SpecialSpawnF = false;
        specialSpawnW = false;
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
        CanSpawnLW1 = true;
        CanSpawnLW2 = true;
        daysSurvived = 0;
        Timer = 12;
        frameCount = 0;
        carryWeight = 0;
        pickupID = 2;
        GameState = "Spawns";
    }

    private void GameEnd()
    {

    }

    private bool SpawnLocation(int _randomLocation, bool _canSpawn, Vector3 _spawnLocation, int spawnType)
    {
        if (ItemLocation == _randomLocation)
        {
            if (_canSpawn == true)
            {
                itemSpawn.transform.position = _spawnLocation;
                _canSpawn = false;
            }
            else
            {
                spawnType++; 
                Destroy(itemSpawn.gameObject);
            }
        }
        return _canSpawn;
    }
    private void InvManager()
    {
        weightBar.transform.localScale = new Vector3((5 * (carryWeight / 20)), 0.5f, 1);

        Backpack.transform.position = new Vector3(myCamera.transform.position.x + 9.5f, myCamera.transform.position.y - 4, myCamera.transform.position.z + 10);
    }
    private void PlayerControls()
    {
        //Movement
        if (Player.GetComponent<SpriteRenderer>().bounds.Intersects(PlayAreaMain.GetComponent<SpriteRenderer>().bounds) || Player.GetComponent<SpriteRenderer>().bounds.Intersects(PlayAreaHead.GetComponent<SpriteRenderer>().bounds))
        {
            if (Input.GetKey(KeyCode.W))
            {
                Player.transform.position += new Vector3(0, moveSpeed, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Player.transform.position -= new Vector3(0, moveSpeed, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                Player.transform.position += new Vector3(moveSpeed, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                Player.transform.position -= new Vector3(moveSpeed, 0, 0);
            }
        }

        //Camera following
        if (Player.transform.position.x > -7 && Player.transform.position.x < 2)
        {
            CameraObject.transform.position = new Vector3(Player.transform.position.x, 0, -10);
        }

        //Direction Facing
        if (Input.GetKey(KeyCode.W)) facing = "N";
        if (Input.GetKey(KeyCode.D)) facing = "E";
        if (Input.GetKey(KeyCode.S)) facing = "S";
        if (Input.GetKey(KeyCode.A)) facing = "W";
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) facing = "NE";
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) facing = "SE";
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) facing = "SW";
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) facing = "NW";
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
