using UnityEngine;

/* Alex Pantuck
 * Comp50
 * 5/8/18
 * initialCursorLock.cs
 * 
 * Ok, so this might seem silly, but let me explain:
 * We were having weird problems with the cursor lock state anyway,
 * so I made a custom function for an in game menu that handles the cursor
 * lock state. Thus, I removed the code from other scripts which affected
 * cursor lock state, but now cursor lock state is free by default. So
 * this script sets the lock state when the player spawns in. If that
 * doesn't make sense, it's because it's 2:00 AM (the best time to code)
 * 
 */

public class initialCursorLock : MonoBehaviour {

	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
}
