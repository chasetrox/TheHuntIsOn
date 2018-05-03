using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {
    [SerializeField] int maxHealth = 3;

    [SyncVar (hook = "OnHealthChange")] int health;


    Player player;
    ImpactReceiver playerImpact;

	// Use this for initialization
	void Awake () {
        player = GetComponent<Player>();
        playerImpact = GetComponent<ImpactReceiver>();
	}

    // When player is enabled, give max health (only run on server)
    [ServerCallback]
    void OnEnable()
    {
        health = maxHealth;
    }

    // Receive damage from other player
    [Server]
    public bool TakeDamage(int dmg, Vector3 hitDirection)
    {
        bool died = false;

        if (health <= 0)
            return died;

        // decrement health
        health -= dmg;
        died = health <= 0;

        RpcTakeDamage(died);

        return died;
    }

    [ClientRpc]
    void RpcTakeDamage(bool died)
    {
        if (isLocalPlayer)
            PlayerCanvas.canvas.FlashDamageEffect ();
        
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
