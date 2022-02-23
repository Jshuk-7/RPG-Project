using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
  public class Fighter : MonoBehaviour, IAction
  {
    public float weaponRange = 2f;
    public float weaponDamage = 5f;
    public float timeBetweenAttatcks = 1f;

    Transform target;
    Mover mover;
    float timeSinceLastAttack = 0f;

    private void Start()
    {
      mover = GetComponent<Mover>();
    }

    private void Update()
    {
      timeSinceLastAttack += Time.deltaTime;

      if (target == null) return;

      if (!GetIsInRange())
      {
        mover.MoveTo(target.position);
      }
      else
      {
        mover.Cancel();
        AttackBehaviour();
      }
    }

    private void AttackBehaviour()
    {
      if (timeSinceLastAttack > timeBetweenAttatcks)
      {
        // ! This will trigger the Hit() event
        GetComponent<Animator>().SetTrigger("attack");
        timeSinceLastAttack = 0;
      }
    }

    // ! Animation Event
    void Hit()
    {
      Health healthComponent = target.GetComponent<Health>();
      healthComponent.TakeDamage(weaponDamage);
    }

    private bool GetIsInRange()
    {
      return Vector3.Distance(transform.position, target.position) < weaponRange;
    }

    public void Attack(CombatTarget combatTarget)
    {
      GetComponent<ActionScheduler>().StartAction(this);
      target = combatTarget.transform;
    }

    public void Cancel()
    {
      target = null;
    }
  }
}
