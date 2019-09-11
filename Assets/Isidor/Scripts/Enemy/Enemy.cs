using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public EnemyMovement movement;

    private void Start()
    {
        movement.parent = this;
        HP = maxHP;
        if (onDamage == null)
        {
            onDamage = new UnityEvent();
        }
        if (onDeath == null)
        {
            onDeath = new UnityEvent();
        }
    }

    [SerializeField] private float maxHP;
    public float HP;

    public bool dead => HP <= 0;

    public void Damage(float amount)
    {
        if (!dead)
        {
            HP -= amount;
            if (HP <= 0)
            {
                HP = 0;
                OnDeath();
            }
            else
            {
                OnDamage();
            }
        }
    }

    protected virtual void OnDamage()
    {
        animator.SetTrigger("Damage");
        onDamage.Invoke();
    }

    protected virtual void OnDeath()
    {
        animator.SetTrigger("Die");
        onDeath.Invoke();
    }

    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onDeath;
}

public abstract class EnemyMovement : MonoBehaviour
{
    public Enemy parent;
    public float timeScale = 1;
    protected Vector2 lastPosition { get; private set; }

    /// <summary>
    /// Get position at the given time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public abstract Vector2 GetPosition(float time);

    /// <summary>
    /// Get current position
    /// </summary>
    /// <returns></returns>
    public abstract Vector2 GetPosition();

    public abstract void SetPosition(Vector2 pos);

    public abstract void OnTick();

    protected void Start()
    {
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        float time = 0;
        while (!parent.dead)
        {
            lastPosition = GetPosition();
            SetPosition(GetPosition(time));
            OnTick();
            yield return null;
            time += Time.deltaTime * timeScale;
            if (time > 100000) time = 0;
        }
    }
}


