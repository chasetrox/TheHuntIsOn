using UnityEngine;
using UnityEngine.UI;

/* Alex Pantuck
 * Comp50
 * 5/8/18
 * inGameMenu.cs
 * 
 * This script is attached to the player canvas,
 * assign a menu in the canvas that the player
 * needs to be able to toggle on/off with escape
 * 
 */

public class inGameMenu : MonoBehaviour {

    public GameObject menu;

    private void Start()
    {
        if (menu == null)
        {
            print("Unassigned menu object, destroying self");
            Destroy(this);
        }
    }

    void Update () {
        // If escape (or controller equiv) is pressed, toggle the state of the menu
		if (Input.GetButtonDown("Cancel"))
        {
            toggle(!menu.activeSelf);
        }
            

	}

    public void toggle(bool active)
    {
        // swap active state
        menu.SetActive(active);

        // adjust cursor lock state
        if (active)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
