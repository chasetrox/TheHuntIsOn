using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Prototype.NetworkLobby;


public class HuntIsOnLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager,
                                                         GameObject lobbyPlayer,
                                                         GameObject gamePlayer)
    {
        PlayerShooting player = gamePlayer.GetComponent<PlayerShooting>();
        LobbyPlayerOverride lPlayer = gamePlayer.GetComponent<LobbyPlayerOverride>();

        // Allow in-game player to access IncrementScore()
        //PlayerShooting.ScoreDelegate scoreDel = new PlayerShooting.ScoreDelegate(lPlayer.IncrementScore);
        //Debug.Log(scoreDel);
        //player.lPlayer = lPlayer;
    }

}
