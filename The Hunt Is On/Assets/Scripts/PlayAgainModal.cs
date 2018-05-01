using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class PlayAgainModal : MonoBehaviour {
    public Button yesButton;
    public Button noButton;
    public GameObject modalPanelObject;
    

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void Choice (UnityAction yesEvent, UnityAction noEvent) {
        modalPanelObject.SetActive (true);
        
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener (yesEvent);
        yesButton.onClick.AddListener (ClosePanel);
        
        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener (noEvent);
        noButton.onClick.AddListener (ClosePanel);

        yesButton.gameObject.SetActive (true);
        noButton.gameObject.SetActive (true);
    }

    void ClosePanel () {
        modalPanelObject.SetActive (false);
    }
}