using UnityEngine;

public class UnitAnimationControl : MonoBehaviour
{

    [SerializeField] private UnitBehaviour[] possibleBehaviours = default;

    [SerializeField] private UnitShootBehaviour rangeAttackBehaviour = default;

    private Animator animator;

    private bool movement = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        foreach (var behaviour in possibleBehaviours)
        {
            behaviour.OnMoved += OnMovementChange;
            behaviour.OnAttacked += OnAttack;
        }
    }

    private void OnDisable()
    {
        foreach (var behaviour in possibleBehaviours)
        {
            behaviour.OnMoved -= OnMovementChange;
            behaviour.OnAttacked -= OnAttack;
        }
    }

    private void OnMovementChange(bool move)  => animator.SetBool("Moving", move);

    private void OnAttack() => animator.SetTrigger("Attack");

    private void RangeAttack() => rangeAttackBehaviour.SpawnProjectile();

}
