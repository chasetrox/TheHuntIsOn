using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastAttackFX : AttackEffectsManager {
	[SerializeField] Animator armAnim;

    public override void Initialize() {
    	// do nothing?
    }

    //Play muzzle flash and audio
    public override void PlayShotEffects() {
    	armAnim.SetTrigger("Attack");
    	// maybe audio for attack?
    }

    //Play impact effect and target position
    public override void PlayImpactEffect(Vector3 impactPosition) {
    	// do nothing
    }
}