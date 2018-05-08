using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

class OverrideNetworkManager : NetworkManager
{
	[SerializeField] GameObject beastPrefab;
	[SerializeField] GameObject hunterPrefab;


    public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
    {
    	Transform playerSpawnPos = GetStartPosition();
    	GameObject player;
    	if (numPlayers == 0)
        	player = GameObject.Instantiate(hunterPrefab, playerSpawnPos.position, Quaternion.identity);
    	else if (numPlayers == 1)
        	player = GameObject.Instantiate(beastPrefab, playerSpawnPos.position, Quaternion.identity);
        else {
        	player = null;
        	Debug.Log("There's more than 2 players in the game! Not spawning anyone!!");
        }

    	NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}