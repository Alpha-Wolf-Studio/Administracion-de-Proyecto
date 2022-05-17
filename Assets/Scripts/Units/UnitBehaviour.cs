using UnityEngine;

[RequireComponent(typeof(Unit))]
public abstract class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private int priority = 0;

    public System.Action<bool> OnMoved;
    public System.Action OnAttacked;

    public int Priority => priority; 

    public abstract bool IsBehaviourExecutable();

    public abstract void Execute();

}
