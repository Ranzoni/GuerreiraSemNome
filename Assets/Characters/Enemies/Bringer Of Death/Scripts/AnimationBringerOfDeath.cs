public class AnimationBringerOfDeath : CharacterAnimation
{
    public void TriggerMeleeAttack()
    {
        animator.SetTrigger("meleeAttack");
    }

    public void TriggerSpellAttack()
    {
        animator.SetTrigger("spellAttack");
    }
}
