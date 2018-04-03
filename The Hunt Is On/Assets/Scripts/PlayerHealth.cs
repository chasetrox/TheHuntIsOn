using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {
    [SerializeField] int maxHealth = 100;

    Player player;
    int health;

	// Use this for initialization
	void Awake () {
        player = GetComponent<Player>();
	}

    // When player is enabled, give max health (only run on server)
    [ServerCallback]
    void OnEnable()
    {
        health = maxHealth;
    }

    // Receive damage from other player
    [Server]
    public bool TakeDamage()
    {
        bool died = false;

        if (health <= 0)
            return died;

        //
        health--;
        died = health <= 0;

        RpcTakeDamage(died);

        return died;
    }

    [ClientRpc]
    void RpcTakeDamage(bool died)
    {
        if (died)
            player.Die();
    }
}
