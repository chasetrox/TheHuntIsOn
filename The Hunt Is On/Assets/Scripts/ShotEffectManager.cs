using UnityEngine;

public class ShotEffectManager : AttackEffectsManager
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource gunAudio;
    [SerializeField] GameObject impactPrefab;

    ParticleSystem impactEffect;

    //Create the impact effect for our shots
    public override void Initialize()
    {
        impactEffect = Instantiate(impactPrefab).GetComponent<ParticleSystem>();
    }

    //Play muzzle flash and audio
    public override void PlayShotEffects()
    {
        Debug.Log("Playing shot effects!");
        muzzleFlash.Stop(true);
        muzzleFlash.Play(true);

        gunAudio.Stop();
        gunAudio.Play();
    }

    //Play impact effect and target position
    public override void PlayImpactEffect(Vector3 impactPosition)
    {
        impactEffect.transform.position = impactPosition;
        impactEffect.Stop();
        impactEffect.Play();
    }
}
