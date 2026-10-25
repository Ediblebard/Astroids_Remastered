using System;
using UnityEngine;

public class AstroidRock : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private string rockName = "";
    [SerializeField] private int rockHealth = 1;
    [SerializeField] private float rotationSpeed = 45f;

    public event EventHandler OnDestroyed;

    private Rigidbody2D rb;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        GameManager.Instance.RegisterAsteroid(this);
        rb.linearVelocity = transform.up * speed;
        rotationSpeed = UnityEngine.Random.Range(-90f, 90f);
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
        OnDestroyed?.Invoke(this,EventArgs.Empty);

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
