using System;
using UnityEngine;

public class AstroidRock : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private string rockName = "";
    [SerializeField] private int rockHealth = 1;
    [SerializeField] private float rotationSpeed = 45f;

    private Rigidbody2D rb;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        rb.linearVelocity = transform.up * speed;
    }

    private void Update() {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);  
    }

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
