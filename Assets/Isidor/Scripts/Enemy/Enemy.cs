using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public abstract class Thing : MonoBehaviour
{

}

public class Enemy : Thing
{
    public Animator animator;

    public Movement movement;

    [System.Serializable]
    public class EnemySounds
    {
        public AudioClip passiveSound;
        public AudioClip hurtSound;
        public AudioClip dieSound;
        public AudioClip attackSound;
    }

    [SerializeField] protected EnemySounds sounds;

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
        if (movement != null) movement.StartMovement();
    }

    [SerializeField] private float maxHP;
    [HideInInspector] public float HP;

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

    protected void Update()
    {
        TryAttack();
    }
    
    protected virtual void TryAttack()
    {
        
    }

    protected virtual void OnDamage()
    {
        animator.SetTrigger("Damage");

        if (sounds.hurtSound)
        {
            
        }

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

class EnemyStates
{

}

[System.Serializable]
class EnemyState
{
    public float hpToActive;
    public Movement movement;
}

public abstract class Movement : MonoBehaviour
{
    [HideInInspector] public Thing parent;
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

    Coroutine coroutine;

    public virtual void StartMovement()
    {
        coroutine = StartCoroutine(Move());
    }

    public virtual void StopMovement()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator Move()
    {
        float time = 0;
        while (true)
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


