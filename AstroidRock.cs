using System;
using UnityEngine;

public class AstroidRock : MonoBehaviour
{
    [SerializeField] private string rockName = "";
    [SerializeField] private int rockHealth = 1;

    public string GetRockName() {
        return rockName;
    }

    public int GetRockHealth() {
        return rockHealth;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }    

    private void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.gameObject.TryGetComponent(out Projectile projectile)) {
            projectile.DestroySelf();
            rockHealth--;
            CheckRockStatus();        
        }
    }

    private void CheckRockStatus() {
        if(rockHealth <= 0) {
            DestroySelf();
        }
    }
}
