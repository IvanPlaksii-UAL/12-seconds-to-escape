using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject topButton, bottomButton, backButton, titleText, howText, mouse;
    public TextMesh topText, bottomText;
    public string menuState;
    // Start is called before the first frame update
    void Start()
    {
        menuState = "Main";
    }

    // Update is called once per frame
    void Update()
    {
        mouse.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        Cursor.visible = false;
        if (menuState == "Main")
        {
            if (topButton.GetComponent<SpriteRenderer>().bounds.Intersects(mouse.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene("Main");
            }
            if (bottomButton.GetComponent<SpriteRenderer>().bounds.Intersects(mouse.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                menuState = "HowTo";
            }
        }
        else if (menuState == "HowTo")
        {
            if (topButton.GetComponent<SpriteRenderer>().bounds.Intersects(mouse.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                menuState = "Controls";
            }
            if (bottomButton.GetComponent<SpriteRenderer>().bounds.Intersects(mouse.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                menuState = "Goal";
            }

            if (backButton.GetComponent<SpriteRenderer>().bounds.Intersects(mouse.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0)) menuState = "Main";
        }
        else if(menuState =="Goal")
        {
            howText.transform.position = new Vector3(0, 0, 1);

            if (backButton.GetComponent<SpriteRenderer>().bounds.Intersects(mouse.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0)) menuState = "HowTo";
        }
        else if(menuState == "Controls")
        {


            if (backButton.GetComponent<SpriteRenderer>().bounds.Intersects(mouse.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0)) menuState = "HowTo";
        }

        if (menuState != "Controls" && menuState != "Goal")
        {
            titleText.transform.position = new Vector3(0, 2.5f, 0);
            topButton.transform.position = new Vector3(0, -1, 0);
            bottomButton.transform.position = new Vector3(0, -3, 0);
        }
        else
        {
            titleText.transform.position = new Vector3(-20, 2.5f, 0);
            topButton.transform.position = new Vector3(-20, -1, 0);
            bottomButton.transform.position = new Vector3(-20, -3, 0);
        }

        if(menuState != "Main") backButton.transform.position = new Vector3(-7.5f, 4, 0);
        if (menuState != "Goal") howText.transform.position = new Vector3(-20, 0, 0);
        MenuStates();
        ColorChange(topButton, new Vector4(0.14f, 0.14f, 0.14f, 1), new Vector4(1, 0.5f, 0, 1));
        ColorChange(bottomButton, new Vector4(0.14f, 0.14f, 0.14f, 1), new Vector4(1, 0.5f, 0, 1));
        ColorChange(backButton, new Vector4(0.14f, 0.14f, 0.14f, 1), new Vector4(1, 0.5f, 0, 1));
    }

    public void ColorChange(GameObject _changeThis, Color _originalC, Color _changeTo)
    {
        if (_changeThis.GetComponent<SpriteRenderer>().bounds.Intersects(mouse.GetComponent<SpriteRenderer>().bounds))
        {
            _changeThis.GetComponent<SpriteRenderer>().color = _changeTo;
        }
        else _changeThis.GetComponent<SpriteRenderer>().color = _originalC;
    }

    private void MenuStates()
    {
        if (menuState == "Main")
        {
            topText.text = "P L A Y";
            bottomText.text = "How to play";
        }

        if (menuState == "HowTo")
        {
            topText.text = "Controls";
            bottomText.text = "Goal";
        }
    }
}
