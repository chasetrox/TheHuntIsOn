using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackEffectsManager : MonoBehaviour {

    public abstract void Initialize();

    //Play muzzle flash and audio
    public abstract void PlayShotEffects();

    //Play impact effect and target position
    public abstract void PlayImpactEffect(Vector3 impactPosition);
}
