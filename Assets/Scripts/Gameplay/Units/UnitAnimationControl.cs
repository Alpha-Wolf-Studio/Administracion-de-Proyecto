using UnityEngine;

public class UnitAnimationControl : MonoBehaviour
{

    [SerializeField] private UnitBehaviour[] possibleBehaviours = default;

    [SerializeField] private UnitShootBehaviour rangeAttackBehaviour = default;

    private Animator animator = default;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        foreach (var behaviour in possibleBehaviours)
        {
            behaviour.OnMoving += OnMovementChange;
            behaviour.OnAttacking += OnAttack;
        }
    }

    private void OnDisable()
    {
        foreach (var behaviour in possibleBehaviours)
        {
            behaviour.OnMoving -= OnMovementChange;
            behaviour.OnAttacking -= OnAttack;
        }
    }

    private void OnMovementChange(bool move)  => animator.SetBool("Moving", move);

    private void OnAttack(bool attack) => animator.SetBool("Attacking", attack);

    private void RangeAttack() => rangeAttackBehaviour.SpawnProjectile();

}
