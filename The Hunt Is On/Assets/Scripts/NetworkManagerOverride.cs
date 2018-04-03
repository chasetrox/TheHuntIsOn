using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerOverride : NetworkManager {
    [SerializeField] GameObject hunterPrefab;
    [SerializeField] GameObject beastPrefab;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        GameObject player;
        Transform playerSpawnPos = base.GetStartPosition();

        // TODO Base this on player settings in pregame lobby
        if (numPlayers == 0)
        { // Hunter
            Debug.Log("Hunter has entered!");
            player = (GameObject)GameObject.Instantiate(hunterPrefab, playerSpawnPos.position, Quaternion.identity);
        }
        else
        { // Beast
            Debug.Log("Beast has entered!");
            player = (GameObject)GameObject.Instantiate(beastPrefab, playerSpawnPos.position, Quaternion.identity);
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
