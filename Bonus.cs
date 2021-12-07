using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public string bonus;

    bool activeBonusThrust;
    bool activeBonusHealth;
    bool activeBonusDamage;

    public GameObject thrust;
    public GameObject health;
    public GameObject damage;


    void Awake(){
        activeBonusThrust = true;
        activeBonusHealth = true;
        activeBonusDamage = true;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider player){
        if (player.CompareTag("Player")){
            BonusEffect(bonus);
        }
    }

    void BonusEffect(string b){
        switch(b){
            case "Thrust":
                if (activeBonusThrust){
                    StartCoroutine(Thrust());
                }
                break;

            case "Health":
                if (activeBonusHealth){
                    StartCoroutine(Health());
                }
                break;

            case "Damage":
                if (activeBonusDamage){
                    StartCoroutine(Damage());
                }
                break;
        }
    }

    IEnumerator Thrust(){
        FindObjectOfType<SpawnPointBonus>().StartCoroutineColor(0);
        PlayerMovement.activeThrust = true;
        activeBonusThrust = false;
        yield return new WaitForSeconds(2);
        activeBonusThrust = true;
        PlayerMovement.activeThrust = false;
        Destroy(thrust);
    }

    IEnumerator Health(){
        PlayerDamage.health += 10;
        activeBonusHealth = false;
        yield return new WaitForSeconds(0f);
        activeBonusHealth = true;
        Destroy(health);
    }

    IEnumerator Damage(){
        FindObjectOfType<SpawnPointBonus>().StartCoroutineColor(1);
        ProjectilesGun.damage += 5;
        activeBonusDamage = false;
        yield return new WaitForSeconds(10f);
        ProjectilesGun.damage -= 5;
        activeBonusDamage = true;
        Destroy(damage);
    }
}
