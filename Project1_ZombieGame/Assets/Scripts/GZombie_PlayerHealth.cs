using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GZombie_PlayerHealth : GZombie_LivingEntity
{
    public Slider healthSlider;
    public AudioClip deathClip;
    public AudioClip hitClip;

    private AudioSource playerAudioPlayer;

    private GZombie_PlayerMove GZombie_playerMove;
    private GZombie_PlayerShoot GZombie_playerShoot;

    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        GZombie_playerMove = GetComponent<GZombie_PlayerMove>();
        GZombie_playerShoot = GetComponent<GZombie_PlayerShoot>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;

        GZombie_playerMove.enabled = true;
        GZombie_playerShoot.enabled = true;
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);

        healthSlider.value = health;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            playerAudioPlayer.PlayOneShot(hitClip);
        }
        base.OnDamage(damage, hitPoint, hitNormal);

        healthSlider.value = health;
    }

    public override void Die()
    {
        base.Die();

        healthSlider.gameObject.SetActive(false);

        playerAudioPlayer.PlayOneShot(deathClip);

        GZombie_playerMove.enabled = false;
        GZombie_playerShoot.enabled = false;
    }

}// end of class
