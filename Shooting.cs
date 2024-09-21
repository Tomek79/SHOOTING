using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public int clipSize = 10;
    public int currentAmmo = 0;
    public float bulletSpeed = 10f;
    public float reloadSpeed = 1f;
    public float shootDelay = 0.1f;

    public GameObject bullet;

    public bool canShoot;

    void Start()
    {
        
        currentAmmo = clipSize;
        canShoot = true; 
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        if (Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Reload());
        }
    }

    private void Shoot() { 
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && currentAmmo > 0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 shootDirection = (mousePosition - transform.position).normalized;

            currentAmmo--;
            canShoot = false;
            StartCoroutine(ShootDelay());
            GameObject bullets = Instantiate(bullet, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullets.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection * bulletSpeed;

        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = clipSize;
    }
  
}
