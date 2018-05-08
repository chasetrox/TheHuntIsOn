using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {
    [SerializeField] float shotCooldown = .3f; // time between shots
    [SerializeField] float range = 35; // range of attack
    [SerializeField] Transform firePosition; // Position outside player
    [SerializeField] AttackEffectsManager attackFX;
    [SerializeField] Transform gunShotPosition;
    [SerializeField] GameObject tracerPrefab;

    [SyncVar] public int numBullets = 3;
    public int dmgPerShot = 3;
    public float forcePerHit = 100f;
    public float hitRadius = 5f;

    float elapsedTime;
    bool canShoot;

    Player player;


	// Use this for initialization
	void Start () {
        // only allow local player to shoot
        if (isLocalPlayer) {
            canShoot = true;
            player = GetComponent<Player>();
            if (player.isHunter)
                PlayerCanvas.canvas.SetAmmo(numBullets);
        }


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
            CmdFireShot(firePosition.position, firePosition.forward, gameObject.layer);
        }
	}

    // Client asks the server to fire a shot;
    [Command]
    public virtual void CmdFireShot(Vector3 origin, Vector3 direction, int layer)
    {
        RaycastHit hit;

        Ray ray = new Ray (origin, direction);
        //Debug.DrawRay(ray.origin, ray.direction*3f, Color.red, 1f);

        // Only shoot at layers that the shooter isn't on
        int mask = 1 << layer;
        mask = ~mask;

        bool result = Physics.SphereCast(ray, hitRadius, out hit, range, mask, QueryTriggerInteraction.UseGlobal);

        // Only show tracer when hunter hits something
        if (player.isHunter && result)
        {
            GameObject bulletTracer = Instantiate(tracerPrefab, gunShotPosition.position, Quaternion.identity);
            NetworkServer.Spawn(bulletTracer);  
            bulletTracer.GetComponent<BulletTracer>().shoot(gunShotPosition.position, hit.point);
        }

        if (result) {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 5.0f);
            // Check if the thing hit was a player
            PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth>();
            
            // If so, player takes damage
            if (enemy != null)
            {
                Debug.Log("Hit an enemy!");
                // Generates impact on enemy, kickback
                ImpactReceiver enemyImpactRec = hit.transform.GetComponent<ImpactReceiver>();
                enemyImpactRec.AddImpact(direction, forcePerHit);

                bool died = enemy.TakeDamage(dmgPerShot, direction);
                if (died) {
                    player.Won();
                }
            }
        }

        RpcProcessShotEffects(result, hit.point);
    }

    [ClientRpc]
    public void RpcProcessShotEffects(bool hitSomething, Vector3 point)
    {
        attackFX.PlayShotEffects ();
        if (player.isHunter)
            PlayerCanvas.canvas.SetAmmo(numBullets);

        if (hitSomething) {
            Debug.Log("Hit Something!");
            attackFX.PlayImpactEffect(point);
        }
    }
	
    // Add bullets
    public void addBullets(int num)
    {
	    numBullets += num;
        if (player.isHunter)
            PlayerCanvas.canvas.SetAmmo(numBullets);
    }
}
