using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GZombie_Gun : MonoBehaviour
{
    public enum State { Ready, Empty, Reloading}

    public State state { get; private set; }

    private LineRenderer bulletLineRenderer;

    public Transform fireTransform;

    public ParticleSystem muzzleFlashEffect;
    public ParticleSystem shellEjectEffect;

    //public AudioClip shotClip;
    //public AudioClip reloadClip;
    private AudioSource gunAudioPlayer;

    public GZombie_GunData GZombie_gunData;

    //public float damage = 25;

    //public int startAmmoRemain = 100;
    //public int magCapacity = 25;

    //public float timeBetFire = 0.12f;
    //public float reloadTime = 1.8f;

    private float fireDistance = 60f;

    public int ammoRemain = 100;
    public int magAmmo;

    private float lastFireTime;


    private void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        ammoRemain = GZombie_gunData.startAmmoRemain;
        magAmmo = GZombie_gunData.magCapacity;

        state = State.Ready;
        lastFireTime = 0;
    }

    public void Fire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + GZombie_gunData.timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    private void Shot()
    {
        RaycastHit hit;

        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            GZombie_IDamageable target = hit.collider.GetComponent<GZombie_IDamageable>();

            if (target != null)
            {
                target.OnDamage(GZombie_gunData.damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        
        StartCoroutine(ShotEffect(hitPosition));

        magAmmo--;
        if(magAmmo <= 0)
        {
           state = State.Empty;
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();

        gunAudioPlayer.PlayOneShot(GZombie_gunData.shotClip);

        bulletLineRenderer.SetPosition(0,fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }

    public bool Reload()
    {
        if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= GZombie_gunData.magCapacity)
        {
            return false;
        }

        StartCoroutine(ReloadRoutine());
        return true;
    }

    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;

        gunAudioPlayer.PlayOneShot(GZombie_gunData.reloadClip);

        yield return new WaitForSeconds(GZombie_gunData.reloadTime);

        int ammoToFill = GZombie_gunData.magCapacity - magAmmo;

        if (ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        magAmmo += ammoToFill;
        magAmmo -= ammoToFill;

        state = State.Ready;
    }

}// end of class
