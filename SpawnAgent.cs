using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnAgent : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform spawnPointBoss;

    public GameObject agent;
    public GameObject bossAgent;

    //public static bool respawn = false;
    public static bool increaseForVague = false;

    public TextMeshProUGUI vagueUi;

    public int nextVague = 0;

    public int count = 3;

    public int vague = 1;

    void Start(){
        Vector3[] pos = new Vector3[spawnPoint.childCount];
        for (int i = 0; i < pos.Length; i++){
            pos[i] = spawnPoint.GetChild(i).position;
        }

        for (int i = 0; i < pos.Length; i++){
            Instantiate (agent, pos[i], Quaternion.identity);
        }

        vagueUi.SetText("Vague : 1");
    }

    void Update(){
        /*if (respawn){
            StartCoroutine(TimeRes());
            respawn = false;
        }*/

        if (increaseForVague){
            Count();
            increaseForVague = false;
        }
    }

    /*public void Respawn(int nbres){
        print("res");
        Vector3[] pos = new Vector3[spawnPoint.childCount];
        for (int i = 0; i < pos.Length; i++){
            pos[i] = spawnPoint.GetChild(i).position;
        }
        for (int i = 0; i < nbres; i++){
            Instantiate (agent, pos[Random.Range(0, 3)], Quaternion.identity);
        }
    }

    IEnumerator TimeRes(){
        yield return new WaitForSeconds(2f);
        Respawn(1);
    }*/

    public void Count(){
        nextVague += 1;

        if (nextVague == count){
            Vague(nextVague);
            count += 1;
            nextVague = 0;
            vague += 1;
            /*if (vague == 2){
                SpawnBoss();
            }*/
        }
    }

    void Vague(int agentSpawn){
        Vector3[] pos = new Vector3[spawnPoint.childCount];
        for (int i = 0; i < pos.Length; i++){
            pos[i] = spawnPoint.GetChild(i).position;
        }
        for (int i = 0; i < agentSpawn + 1; i++){
            Instantiate (agent, pos[Random.Range(0, 3)], Quaternion.identity);
        }

        vagueUi.SetText("Vague : " + (vague + 1).ToString());
    }

    /*void SpawnBoss(){
        if (vague == 2){
            Vector3 spawnBoss = new Vector3(spawnPointBoss.position.x, 10, spawnPointBoss.position.y);
            Instantiate (bossAgent, spawnBoss, Quaternion.identity);
        }
    }*/
}