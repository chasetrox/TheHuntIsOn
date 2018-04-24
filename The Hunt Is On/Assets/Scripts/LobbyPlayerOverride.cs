using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prototype.NetworkLobby;


//Player entry in the lobby. Handle selecting color/setting name & getting ready for the game
//Any LobbyHook can then grab it and pass those value to the game player prefab (see the Pong Example in the Samples Scenes)
public class LobbyPlayerOverride : LobbyPlayer
{
    [SyncVar (hook = "OnPlayerRoleChange")]
    public bool playerIsHunter = true;
    [SyncVar (hook = "OnPlayerScoreChanged")]
    private int playerScore = 0;

    public Button playerSelectionToggle;
    public Text playerScoreText;

    // Called when a player returns to the lobby
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        SetupLocalPlayer();
    }

    // Called the first time a player enters the lobby
    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();
        if (isLocalPlayer)
        {
            SetupLocalPlayer();
        }
        else
        {
            //SetupOtherPlayer();
        }

        OnPlayerRoleChange(true);
    }

    void SetupLocalPlayer()
    {
        playerSelectionToggle.transform.GetChild(0).GetComponent<Text>().text = "HUNTER";

        playerSelectionToggle.onClick.RemoveAllListeners();
        playerSelectionToggle.onClick.AddListener(OnPlayerToggledClicked);

        playerScoreText.transform.GetChild(0).GetComponent<Text>().text = playerScore.ToString();
    }

    // TODO Fix this so both players' toggles aren't set to interactable false
    void SetupOtherPlayer()
    {
        playerSelectionToggle.interactable = false;
        OnPlayerRoleChange(true);
    }

    // Listener for player Select button toggle
    public void OnPlayerToggledClicked()
    {
        CmdPlayerSelectionChange();
    }
    //
    void OnPlayerRoleChange(bool newRole)
    {
        playerIsHunter = newRole;
        string buttonText = (playerIsHunter ? "HUNTER" : "WOLF");
        playerSelectionToggle.transform.GetChild(0).GetComponent<Text>().text = buttonText;
    }
    void OnPlayerScoreChanged(int newScore)
    {
        playerScore = newScore;
        playerScoreText.transform.GetChild(0).GetComponent<Text>().text = playerScore.ToString();
    }

    public void IncrementScore()
    {
        playerScore += 1;
    }

    //===== UI Handler

    [Command]
    public void CmdPlayerSelectionChange()
    {
        string buttonText = (!playerIsHunter ? "HUNTER" : "WOLF");
        playerSelectionToggle.transform.GetChild(0).GetComponent<Text>().text = buttonText;
        playerIsHunter = !playerIsHunter;
    }
}
