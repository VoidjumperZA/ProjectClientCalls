using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttonOnArray;
    [SerializeField]
    private GameObject[] buttonOffArray;
    [SerializeField]
    private GameObject[] subMenu;
    [SerializeField]
    private Image bar;

    private bool xAxisInUse = false;
    private bool yAxisInUse = false;
    private bool selectionAxisInUse = false;
    private bool returnAxisInUse = false;

    private int activeMenuOption = 0;
    private int menuDepth = 0;
    private int maxMenuOptions = 2;
    private int maxMenuDepth = 3;

    void Start()
    {
        toggleMenuGroup(buttonOnArray, false);
    }
    void Update()
    {
        //check all axes with a generic command checking method
        checkAxisCommands("Horizontal", ref xAxisInUse);
        checkAxisCommands("Vertical", ref yAxisInUse);
        checkAxisCommands("Jump", ref selectionAxisInUse);
        checkAxisCommands("Fire1", ref returnAxisInUse);

        //set all our buttons to false bar the one our keys have selected
        toggleMenuGroup(buttonOnArray, false);
        buttonOnArray[activeMenuOption].SetActive(true);
    }

    public void LoadScene(int pSceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(pSceneIndex);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    //generic command check list, scanning all command axes at once
    //feed it the axis and bool that controls it's "KeyDown" status
    private void checkAxisCommands(string pAxisName, ref bool pAxisToggle)
    {
        if (Input.GetAxisRaw(pAxisName) != 0) //if we've pressed button
        {
            if (pAxisToggle == false) //if the key is not pressed down
            {
                switch (pAxisName)
                {
                    case "Horizontal":
                        xAxisCommands();
                        break;
                    case "Vertical":
                        yAxisCommands();
                        break;
                    case "Jump":
                        selectionAxisCommands();
                        break;
                    case "Fire1":
                        returnAxisCommands();
                        break;
                }
                pAxisToggle = true; //key is now 'down'
            }
        }
        //key has now been lifted up
        if (Input.GetAxisRaw(pAxisName) == 0)
        {
            pAxisToggle = false;
        }
    }

    //input manager commands using horizontal keys
    private void xAxisCommands()
    {

       
    }

    //input manager commands using vertical keys
    private void yAxisCommands()
    {
        //set menu option to a detection of a button press
        activeMenuOption += (int)Input.GetAxisRaw("Vertical") * -1;
        //make sure value does not exceed ranges
        //clamp and range were not working as intended
        if (activeMenuOption >= maxMenuOptions)
        { activeMenuOption = maxMenuOptions; }
        if (activeMenuOption <= 0)
        { activeMenuOption = 0; }
        Debug.Log("activeMenuOption: " + activeMenuOption);
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        menuDepth++;
        if (menuDepth >= maxMenuDepth)
        { menuDepth = maxMenuDepth; }
    }

    //
    private void returnAxisCommands()
    {
        menuDepth--;
        if (menuDepth <= 0)
        { menuDepth = 0; }
    }

    //assign visibility to whole gameobject clusters at once
    private void toggleMenuGroup(GameObject[] pGroupToToggle, bool pState)
    {
        for (int i = 0; i < pGroupToToggle.Length; i++)
        {
            pGroupToToggle[i].SetActive(pState);
        }
    }
}
