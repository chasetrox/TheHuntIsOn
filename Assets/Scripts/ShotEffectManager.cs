using UnityEngine;

public class ShotEffectManager : AttackEffectsManager
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource gunAudio;
    [SerializeField] GameObject impactPrefab;
    [SerializeField] GameObject tracerPrefab;
    [SerializeField] GameObject gunMuzzle;

    ParticleSystem impactEffect;

    //Create the impact effect for our shots
    public override void Initialize()
    {
        impactEffect = Instantiate(impactPrefab).GetComponent<ParticleSystem>();
    }

    //Play muzzle flash and audio
    public override void PlayShotEffects()
    {
        muzzleFlash.Stop(true);
        muzzleFlash.Play(true);

        gunAudio.Stop();
        gunAudio.Play();
    }

    //Play impact effect and target position
    public override void PlayImpactEffect(Vector3 impactPosition)
    {
        BulletTracer bulletTracer = Instantiate(tracerPrefab, gunMuzzle.transform.position, Quaternion.identity).GetComponent<BulletTracer>();
        bulletTracer.shoot(gunMuzzle.transform.position, impactPosition);
    }
}
