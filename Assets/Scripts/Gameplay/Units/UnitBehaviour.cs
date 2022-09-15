using UnityEngine;

[RequireComponent(typeof(Unit))]
public abstract class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private int priority = 0;

    public System.Action<bool> OnMoving;
    public System.Action<bool> OnAttacking;

    public int Priority => priority; 

    public abstract bool IsBehaviourExecutable();

    public abstract void Execute();

}
