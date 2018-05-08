using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

class OverrideNetworkManager : NetworkManager
{
	[SerializeField] GameObject beastPrefab;
	[SerializeField] GameObject hunterPrefab;

    // This override lets people connect from different continents. By default, unity MM
    // connects people to the nearest server, which means if youre in different continents
    // you wont see the other persons server
    private void Start()
    {
        NetworkManager networkManager = FindObjectOfType<NetworkManager>();
        networkManager.SetMatchHost("us1-mm.unet.unity3d.com", networkManager.matchPort, true);
    }

    public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
    {
    	Transform playerSpawnPos = GetStartPosition();
    	GameObject player;
    	if (numPlayers == 0)
        	player = GameObject.Instantiate(beastPrefab, playerSpawnPos.position, Quaternion.identity);
    	else if (numPlayers == 1)
        	player = GameObject.Instantiate(hunterPrefab, playerSpawnPos.position, Quaternion.identity);
        else {
        	player = null;
        	Debug.Log("There's more than 2 players in the game! Not spawning anyone!!");
        }

    	NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}