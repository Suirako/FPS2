using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ProjectilesGun : MonoBehaviour
{
    public static int _damage;

    //Mega bullet
    public RawImage megaBulletUI;

    public static int unlockMegaBullet;
    public int nextMegaBullet;

    public bool mgBullet = false;

    //Bullet
    public GameObject bullet;
    public GameObject megaBullet;

    //Bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public static int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //Recoil
    public Rigidbody playerRb;
    public float recoilForce;

    //Bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI ammunationDisplay;
    public TextMeshProUGUI realodingDisplay;
    
    //Bug fixing
    public bool allowInvoke = true;

    void Awake(){
        damage = 0;
        spread = 0f;
        magazineSize = 0;
        bulletsPerTap = 0;
        reloadTime = 0f;
        timeBetweenShots = 0f;
        timeBetweenShooting = 0f;

        damage = PlayerPrefs.GetInt("damage");
        spread = PlayerPrefs.GetFloat("spread");
        magazineSize = PlayerPrefs.GetInt("magazineSize");
        bulletsPerTap = PlayerPrefs.GetInt("bulletsPerTap");
        reloadTime = PlayerPrefs.GetFloat("reloadTime");
        timeBetweenShots = PlayerPrefs.GetFloat("timeBetweenShots");
        timeBetweenShooting = PlayerPrefs.GetFloat("timeBetweenShooting");

        _damage = damage;

        //Mega bullet
        megaBulletUI.GetComponent<RawImage>().color = new Color32(255, 0, 0, 100);
        nextMegaBullet = 10;

        //Make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;

        realodingDisplay.SetText("");
    }

    void Update(){
        MyInput();

        print(damage);

        //Set ammo display, if it exist
        if (ammunationDisplay != null){
            ammunationDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
    }

    private void MyInput()
    {
        if (unlockMegaBullet >= nextMegaBullet){
            megaBulletUI.GetComponent<RawImage>().color = new Color32(0, 91, 255, 100);
            if (Input.GetKeyDown(KeyCode.V)){
                megaBulletUI.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
                nextMegaBullet += 10;
                mgBullet = true;
            }
        }

        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading 
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    void Shoot(){
        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //Check if ray hits something
        if (Physics.Raycast(ray, out hit, range, whatIsEnemy)){
            if (rayHit.collider.CompareTag("Enemy")){
                rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
            }
        }

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //Instantiate bullet/projectile
        if (!mgBullet){
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);  

            //Rotate bullet to shoot direction
            currentBullet.transform.forward = direction.normalized;

            //Add force to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);
            //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * shootForce, ForceMode.Impulse);
        }

        else if (mgBullet){
            megaBulletUI.GetComponent<RawImage>().color = new Color32(255, 0, 0, 100);

            GameObject currentBullet = Instantiate(megaBullet, attackPoint.position, Quaternion.identity);  

            //Rotate bullet to shoot direction
            currentBullet.transform.forward = direction.normalized;

            //Add force to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);
            //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * shootForce, ForceMode.Impulse);

            mgBullet = false;
        }

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null){
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0){
            Invoke("Shoot", timeBetweenShots);
        }
    }

    void ResetShot(){
        //Allow shooting and invoked again
        readyToShoot = true;
        allowInvoke = true;
    }

    void Reload(){
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        realodingDisplay.SetText("Reloding");
    }

    void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;
        realodingDisplay.SetText("");
    }

    public void ChooseGun(string gun){
        switch(gun){
            case "Shotgun":
                damage = 10;
                spread = 0.05f;
                magazineSize = 6;
                bulletsPerTap = 3;
                reloadTime = 3f;
                timeBetweenShots = 0.1f;
                timeBetweenShooting = 1.5f;
                
                PlayerPrefs.SetInt("damage", damage);
                PlayerPrefs.SetFloat("spread", spread);
                PlayerPrefs.SetInt("magazineSize", magazineSize);
                PlayerPrefs.SetInt("bulletsPerTap", bulletsPerTap);
                PlayerPrefs.SetFloat("reloadTime", reloadTime);
                PlayerPrefs.SetFloat("timeBetweenShots", timeBetweenShots); 
                PlayerPrefs.SetFloat("timeBetweenShooting", timeBetweenShooting);
                break;
            
            case "MachineGun":
                damage = 13;
                spread = 0f;
                magazineSize = 30;
                bulletsPerTap = 1;
                reloadTime = 1.5f;
                timeBetweenShots = 0f;
                timeBetweenShooting = 0.2f;

                PlayerPrefs.SetInt("damage", damage);
                PlayerPrefs.SetFloat("spread", spread);
                PlayerPrefs.SetInt("magazineSize", magazineSize);
                PlayerPrefs.SetInt("bulletsPerTap", bulletsPerTap);
                PlayerPrefs.SetFloat("reloadTime", reloadTime);
                PlayerPrefs.SetFloat("timeBetweenShots", timeBetweenShots); 
                PlayerPrefs.SetFloat("timeBetweenShooting", timeBetweenShooting);
                break;
        }
    }
}