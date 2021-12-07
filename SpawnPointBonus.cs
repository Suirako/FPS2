using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpawnPointBonus : MonoBehaviour
{
    public float range;

    public GameObject spawnPointThrust;
    public GameObject spawnPointDamage;
    public GameObject spawnPointHealth;

    public RawImage thrustUI;
    public RawImage damageUI;

    public int numberPoint;

    void Start()
    {
        //Spawn(numberPoint);

        thrustUI.GetComponent<RawImage>().color = new Color32(255, 0, 0, 100);
        damageUI.GetComponent<RawImage>().color = new Color32(255, 0, 0, 100);
    }

    public void Spawn(int point){
        for (int b = 0; b < 1; b++){
            int i = 0;
            int random = Random.Range(0, 3);

            switch(random){
                case 0:
                    for (i = 0; i < point; i++){
                        float randomZ = Random.Range(-range, range);
                        float randomX = Random.Range(-range, range);

                        Vector3 rangeSpawn = new Vector3(randomX, 100, randomZ);

                        Instantiate(spawnPointThrust, rangeSpawn, Quaternion.identity);
                    }
                    break;

                case 1:
                    for (i = 0; i < point; i++){
                        float randomZ = Random.Range(-range, range);
                        float randomX = Random.Range(-range, range);

                        Vector3 rangeSpawn = new Vector3(randomX, 100, randomZ);

                        Instantiate(spawnPointDamage, rangeSpawn, Quaternion.identity);
                    }
                    break;

                case 2:
                    for (i = 0; i < point; i++){
                        float randomZ = Random.Range(-range, range);
                        float randomX = Random.Range(-range, range);

                        Vector3 rangeSpawn = new Vector3(randomX, 100, randomZ);

                        Instantiate(spawnPointHealth, rangeSpawn, Quaternion.identity);
                    }
                    break;
            }
        }
    }

    public void StartCoroutineColor(int b){
        StartCoroutine(ChangeColor(b));
    }

    IEnumerator ChangeColor(int b){
        switch(b){
            case 0:
                thrustUI.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
                yield return new WaitForSeconds(2f);
                thrustUI.GetComponent<RawImage>().color = new Color32(255, 0, 0, 100);
                break;
            
            case 1:
                damageUI.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
                yield return new WaitForSeconds(10f);
                damageUI.GetComponent<RawImage>().color = new Color32(255, 0, 0, 100);
                break;
        }
    }
}
