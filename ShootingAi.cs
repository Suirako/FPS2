using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAi : MonoBehaviour
{
    double health = 100;

    public void TakeDamage(int dmg){
        health -= dmg;
        if (health <= 0) Invoke("DestroyObject", 0f);
    }

    public void DestroyObject(){
        Destroy(gameObject);
    }
}
