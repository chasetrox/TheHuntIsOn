using UnityEngine;

// extends from parent AttackFXManager class
public class MeleeEffectsManager : AttackEffectsManager
{
    [SerializeField] GameObject arm;
    private Animator anim;

    public override void Initialize()
    {
        anim = arm.GetComponent<Animator>();
    }

    // Override the shot effect play function to use the arm's
    // Hit animation trigger for the Beast
    public override void PlayShotEffects()
    {
        anim.SetTrigger("Hit");
    }

    public override void PlayImpactEffect(Vector3 _x)
    {
        // No impact
    }
}
