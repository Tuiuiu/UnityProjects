using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Reference Type, so it can be altered when managing debuff dictionary
    private class EffectTimer
    {
        public int value;
        public float timer;
        public float duration;

        public EffectTimer(int val, float tmr, float dur)
        {
            value = val;
            timer = tmr;
            duration = dur;
        }
    }

    public int life = 10;
    private float speed = 5.0f;
    private float speedModifier = 100;

    private int score = 100;
    private int rewardTier = 1;
    private bool stunned = false;

    public GameObject target;
    public LevelControllers level;
    private Rigidbody2D body;
    public HealthBar healthbar;

    private Dictionary<EffectType, EffectTimer> debuffs = new Dictionary<EffectType, EffectTimer>();

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        healthbar.SetMaxHealth(life);
    }

    // Update is called once per frame
    void Update()
    {
        CountDebuffs();
        if (stunned)
        {
            body.velocity = Vector3.zero;
        }
        else
        {

            Vector3 direction = target.transform.position - transform.position;
            body.velocity = direction.normalized * speed * (speedModifier/100);
        }
    }

    public void takeDamage(int damage)
    {
        life -= damage;
        healthbar.SetHealth(life);
        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        int[] rewards = { score, rewardTier };
        level.EnemyKilled(transform.position, rewards);
        Destroy(gameObject);
    }
    
    public void setTarget(GameObject targetReference)
    {
        target = targetReference;
    }
    
    public void setLevel(LevelControllers levelReference)
    {
        level = levelReference;
    }


    // Resolve an effect caused by a projectile when hit
    // When damage, take damage. When debuff, add to debuff list
    public void resolveEffect(ShotEffect effect)
    {
        Debug.Log("Dealing with effect " + effect.type + " of value " + effect.value + " and duration " + effect.duration);
        switch (effect.type)
        {
            case EffectType.PhysicalDamage:
            case EffectType.ExplosionDamage:
                takeDamage(effect.value);
                break;
            case EffectType.Burn:
                if (!debuffs.ContainsKey(effect.type))
                {
                    debuffs.Add(effect.type, new EffectTimer(effect.value, 1.0f, effect.duration));
                }
                else
                {
                    // ShotEffect existingEffect = debuffs[effect.type].effect;
                    EffectTimer effTmr = debuffs[EffectType.Burn];

                    if (effect.value > effTmr.value)
                        effTmr.value = effect.value;
                    if (effect.duration > effTmr.timer)
                        effTmr.timer = effect.duration;
                }
                break;
            case EffectType.Freeze:
                if (!debuffs.ContainsKey(effect.type))
                {
                    debuffs.Add(effect.type, new EffectTimer(effect.value, effect.duration, effect.duration));
                    speedModifier -= effect.value;
                }
                else
                {
                    EffectTimer effTmr = debuffs[EffectType.Freeze];
                    if (effect.value > effTmr.value)
                    {
                        speedModifier += effTmr.value;
                        effTmr.value = effect.value;
                        speedModifier -= effTmr.value;

                    }
                    if (effect.duration > effTmr.timer)
                    {
                        effTmr.timer = effect.duration;
                        effTmr.duration = effect.duration;
                    }
                }
                break;
            case EffectType.Stun:
                if (!debuffs.ContainsKey(effect.type))
                {
                    debuffs.Add(effect.type, new EffectTimer(effect.value, effect.duration, effect.duration));
                    stunned = true;
                }
                break;
            default:
                Debug.Log("Default case! Your effect went wrong");
                break;
        }
    }

    // Reduce Debuff timers after each tick, dealing with them when the timer reaches 0
    private void CountDebuffs()
    {
        List<EffectType> toBeRemoved = new List<EffectType>(); 
        foreach (KeyValuePair<EffectType, EffectTimer> entry in debuffs)
        {
            // Get the current effect timer reference
            EffectTimer effectTimer = entry.Value;
            effectTimer.timer -= Time.deltaTime;
            // If this effect timer reaches 0
            if (effectTimer.timer <= 0)
            {
                switch (entry.Key)
                {
                    // Decrement burn total duration, and deal fire damage
                    // Reset the 2 seconds timer
                    // When duration expires, burning effect goes off
                    case EffectType.Burn:
                        effectTimer.timer = 1;
                        effectTimer.duration -= 1;
                        takeDamage(effectTimer.value);
                        if (effectTimer.duration <= 0)
                        {
                            toBeRemoved.Add(entry.Key);
                        }
                        break;
                    // Timer equals it's duration on freeze, so when timer goes off, 
                    // the debuff expires
                    case EffectType.Freeze:
                        speedModifier += effectTimer.value;
                        toBeRemoved.Add(entry.Key);
                        break;
                    case EffectType.Stun:
                        stunned = false;
                        toBeRemoved.Add(entry.Key);
                        break;
                }
            }
        }

        // Remove the entries after iterating through dictionary
        foreach (EffectType type in toBeRemoved)
        {
            debuffs.Remove(type);
        }

    }
}
