using System;
using Unity.Mathematics;
using UnityEngine;

public class AstroidRock : MonoBehaviour {
    [SerializeField] private float speed = 5f;
    [SerializeField] private string rockName = "";
    [SerializeField] private int rockHealth = 1;
    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private float maxSpreadAngle = 15f;
    [SerializeField] private AstroidSize astroidSize;

    public enum AstroidSize {
        Small,
        Medium,
        Large,
    }

    public event EventHandler<OnDestroyedEventArgs> OnDestroyed;
    public class OnDestroyedEventArgs : EventArgs {
        public string rockName;
        public AstroidSize astroidSize;
        public Vector3 location;
        public Quaternion quaternion;
    }


    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        GameManager.Instance.RegisterAsteroid(this);
        CreateRandomDirection();
        rotationSpeed = UnityEngine.Random.Range(-90f, 90f);
    }

    private void CreateRandomDirection() {
        float randomAngle = UnityEngine.Random.Range(-maxSpreadAngle, maxSpreadAngle);
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);
        Vector2 randomizedDirection = randomRotation * transform.up;
        rb.linearVelocity = randomizedDirection * speed;
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

    public AstroidSize GetAstroidSize() {
        return astroidSize;
    }

    public void DestroySelf() {
        OnDestroyed?.Invoke(this, new OnDestroyedEventArgs {
            rockName = GetRockName(),
            astroidSize = GetAstroidSize(),
            location = transform.position,
            quaternion = quaternion.identity,
        });
        GameManager.Instance.UnRegisterAsteroid(this);
        SoundManager.Instance.PlayAstroidDestroyedSound();
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
        if (rockHealth <= 0) {
            DestroySelf();
        }
    }
}
