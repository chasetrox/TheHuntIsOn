using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {
    [SerializeField] float shotCooldown = .3f; // time between shots
    [SerializeField] float range = 35; // range of attack
    [SerializeField] Transform firePosition; // Position outside player
    [SerializeField] AttackEffectsManager attackFX;
    [SyncVar] public int numBullets = 3;

    float elapsedTime;
    bool canShoot;

	// Use this for initialization
	void Start () {
        // only allow local player to shoot
        if (isLocalPlayer)
            canShoot = true;

	}

	// Update is called once per frame
	void Update () {
        // Objects that can't shoot don't need this
        if (!canShoot)
            return;

        elapsedTime += Time.deltaTime;

        // when the local player tries to shoot, ask server to shoot for them
        if (Input.GetButtonDown("Fire1") && elapsedTime > shotCooldown && numBullets > 0) {
            elapsedTime = 0;
	    	numBullets--;
            CmdFireShot(firePosition.position, firePosition.forward);
            // SHOOT
        }
	}

    // Client asks the server to fire a shot;
    [Command]
    public virtual void CmdFireShot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        Ray ray = new Ray (origin, direction);
        Debug.DrawRay(ray.origin, ray.direction*3f, Color.red, 1f);

        bool result = Physics.Raycast(ray, out hit, range);

        if (result) {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 5.0f);
            // Check if the thing hit was a player
            PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth>();
                
            // If so, player takes damage
            if (enemy != null)
            {
                Debug.Log("Hit an enemy!");
                enemy.TakeDamage();
            }
        }

        RpcProcessShotEffects(result, hit.point);
    }

    [ClientRpc]
    public void RpcProcessShotEffects(bool hitSomething, Vector3 point)
    {
        attackFX.PlayShotEffects ();

        if (hitSomething) {
            Debug.Log("Hit Something!");
            //attackFX.PlayImpactEffect(point);
        }
    }
}
