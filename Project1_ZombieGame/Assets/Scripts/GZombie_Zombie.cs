using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class GZombie_Zombie : GZombie_LivingEntity
{
    public LayerMask whatIsTarget;

    private GZombie_LivingEntity targetEntity;
    private NavMeshAgent navMeshAgent;

    public AudioClip deathSound;
    public AudioClip hitSound;

    private AudioSource zombieAudioPlayer;
    private Renderer zombieRenderer;

    public float damage = 20f;
    public float timeBetAttack = 0.5f;
    public float lastAttackTime;

    private bool hasTarget
    {
        get
        {
            if (targetEntity != null && !targetEntity.dead)
            {
                return true;
            }
            return false;
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAudioPlayer = GetComponent<AudioSource>();
        zombieRenderer = GetComponentInChildren<Renderer>();
    }

    public void Setup(GZombie_ZombieData GZombie_zombieData)
    {
        startingHealth = GZombie_zombieData.health;
        health = GZombie_zombieData.damage;
        damage = GZombie_zombieData.damage;
        navMeshAgent.speed = GZombie_zombieData.speed;
        zombieRenderer.material.color = GZombie_zombieData.skinColor;
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if (hasTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                navMeshAgent.isStopped = true;

                Collider[] colliders = Physics.OverlapSphere(transform.position, 100f, whatIsTarget);

                for (int i = 0; i < colliders.Length; ++i)
                {
                    GZombie_LivingEntity GZombie_livingEntity = colliders[i].GetComponent<GZombie_LivingEntity>();

                    if (GZombie_livingEntity != null && GZombie_livingEntity.dead)
                    {
                        targetEntity = GZombie_livingEntity;

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            zombieAudioPlayer.PlayOneShot(hitSound);
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        Collider[] zombieColliders = GetComponents<Collider>();
        for (int i = 0; i < zombieColliders.Length; ++i)
        {
            zombieColliders[i].enabled = false;
        }
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        zombieAudioPlayer.PlayOneShot(deathSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            GZombie_LivingEntity attackTarget = other.GetComponent<GZombie_LivingEntity>();

            if (attackTarget != null && attackTarget == targetEntity)
            {
                lastAttackTime = Time.time;

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }

}// end of class
