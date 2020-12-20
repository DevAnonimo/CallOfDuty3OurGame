using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    //Gun generals
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    //Gun Reloading
    public int maxAmmo = 7;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator animator;

    //Gun effects
    public Camera fpsCam;
    public ParticleSystem fireFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if(currentAmmo <= 0f || Input.GetKeyDown((KeyCode.R)))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime + 1f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        fireFlash.Play();

        currentAmmo--;

        //Uses raycasting
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.takeDamage(damage);
            }

            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            /* Para destruir um objeto de particula, depois de 2 segundos. 
             * Nesse caso, não precisa pois a particula já tem um tempo de vida na Unity
            
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            */
        }
    }

}
