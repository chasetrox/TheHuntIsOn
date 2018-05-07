using UnityEngine;
using System.Collections;
 
public class LoadGame : MonoBehaviour 
{	
    public void NextLevelButton(string levelName)
    {
        Application.LoadLevel(levelName);
    }
}