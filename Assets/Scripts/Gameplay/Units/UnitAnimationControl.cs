using UnityEngine;

public class UnitAnimationControl : MonoBehaviour
{

    [SerializeField] private UnitBehaviour[] possibleBehaviours = default;

    [SerializeField] private UnitBehaviour rangeAttackBehaviour = default;

    private IShootBehaviour shootBehaviour = default;
    private Animator animator = default;
    private Unit unit = default;
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int ReAttackTrigger = Animator.StringToHash("ReAttack");
    private static readonly int Moving = Animator.StringToHash("Moving");

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (rangeAttackBehaviour)
        {
            shootBehaviour = (IShootBehaviour)rangeAttackBehaviour;
            shootBehaviour.OnReAttack += ReAttack;
        }
        
        foreach (var behaviour in possibleBehaviours)
        {
            behaviour.OnMoving += OnMovementChange;
            behaviour.OnAttacking += OnAttack;
        }

        unit = possibleBehaviours[0].GetComponent<Unit>();
        unit.OnDie += OnDie;
    }

    private void OnDisable()
    {
        if (rangeAttackBehaviour)
            shootBehaviour.OnReAttack -= ReAttack;
        
        foreach (var behaviour in possibleBehaviours)
        {
            behaviour.OnMoving -= OnMovementChange;
            behaviour.OnAttacking -= OnAttack;
        }
    }

    private void OnMovementChange(bool move)  => animator.SetBool(Moving, move);
    private void OnAttack(bool attack) => animator.SetBool(Attacking, attack);

    private void OnDie()
    {
        unit.OnDie -= OnDie;
        animator.SetTrigger(Die);   
    }

    private void ReAttack() => animator.SetTrigger(ReAttackTrigger);
    
    private void RangeAttack() => shootBehaviour.SpawnProjectile();

    private void CheckReAttack()
    {
        shootBehaviour.CheckReAttack = true;
    }

}
