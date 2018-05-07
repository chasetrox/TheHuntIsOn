using UnityEngine;
using System.Collections;
 
public class LoadGame : MonoBehaviour 
{	
    public void NextLevelButton()
    {
        Application.LoadLevel("ForestLevel_B");
    }
}