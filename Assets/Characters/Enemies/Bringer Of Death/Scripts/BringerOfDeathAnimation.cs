public class BringerOfDeathAnimation : CharacterAnimation
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
