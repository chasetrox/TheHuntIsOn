using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {
    [SerializeField] int maxHealth = 3;

    [SyncVar (hook = "OnHealthChange")] int health;


    Player player;

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

        // decrement health
        health--;
        died = health <= 0;

        RpcTakeDamage(died);

        return died;
    }

    [ClientRpc]
    void RpcTakeDamage(bool died)
    {
        // Damage flash should go here
        
        if (died)
        {
            player.Die();
        }
    }

    void OnHealthChange(int value)
    {
        health = value;
        if (isLocalPlayer) {
            PlayerCanvas.canvas.SetHealth(value);
        }
    }
}
