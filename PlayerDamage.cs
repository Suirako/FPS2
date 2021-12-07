using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public static float health = 100;

    public float damage = 10;

    public GameObject player;
    public GameObject camDeath;

    public LayerMask wall;

    public TextMeshProUGUI healthPlayer;

    void Awake(){
        healthPlayer.SetText("100");
    }

    void Update(){
        healthPlayer.SetText(health.ToString("F2"));

        if (health <= 0){
            DestroyPlayer();
        }
    }

    void OnTriggerEnter(Collider bullet){
        if (bullet.CompareTag("BulletAi")){
            TakeDamage(damage);
        }
    }

    void TakeDamage(float _damage){
        health -= _damage;
    }

    void DestroyPlayer(){
        healthPlayer.SetText("Dead");
        camDeath.SetActive(true);
        DestroyImmediate(player, true);
    }
}
