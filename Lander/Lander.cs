using System;
using System.Collections;
using UnityEngine;

public class Lander : MonoBehaviour {

    private const float GRAVITY_NORMAL = 0f; //Normally 0.7f

    public static Lander Instance { get; private set; }

    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler OnFuelPickup;
    public event EventHandler OnFireShoot;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    public enum State {
        WaitingToStart,
        Normal,
        GameOver,
    }

    private Rigidbody2D landerRigidBody2D;
    [SerializeField] private float fuelAmount;
    private float fuelAmountMax = 10f;
    private State state;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private void Awake() {
        Instance = this;

        fuelAmount = fuelAmountMax;
        state = State.WaitingToStart;

        landerRigidBody2D = GetComponent<Rigidbody2D>();
        landerRigidBody2D.gravityScale = 0f;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        switch (state) {
            default:
            case State.WaitingToStart:
                if (GameInput.Instance.IsUpActionPressed() ||
                    GameInput.Instance.IsLeftActionPressed() ||
                    GameInput.Instance.IsRightActionPressed() ||
                    GameInput.Instance.IsFireActionPressed()) {
                    // Pressing any input
                    landerRigidBody2D.gravityScale = GRAVITY_NORMAL;
                    SetState(State.Normal);
                }
                break;
            case State.Normal:
                if (fuelAmount <= 0f) {
                    // No fuel
                    return;
                }

                if (GameInput.Instance.IsUpActionPressed()) {
                    float force = 700f;
                    landerRigidBody2D.AddForce(force * transform.up * Time.deltaTime);
                    ConsumeFuel();
                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.Instance.IsLeftActionPressed()) {
                    float turnSpeed = +100f;
                    landerRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
                    ConsumeFuel();
                    OnLeftForce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.Instance.IsRightActionPressed()) {
                    float turnSpeed = -100f;
                    landerRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
                    ConsumeFuel();
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.Instance.IsFireActionPressed()) {
                    Shoot();
                }
                break;
            case State.GameOver:
                break;
        }

    }

    bool canShoot = true;
    private void Shoot() {
        if (canShoot) {
            OnFireShoot?.Invoke(this, EventArgs.Empty);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            canShoot = false;
            StartCoroutine(ShootTimer());
        }
    }

    private IEnumerator ShootTimer() {
        float timeToWait = .5f;
        yield return new WaitForSeconds(timeToWait);

        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.gameObject.TryGetComponent(out FuelPickup fuelPickup)) {
            float addFuelAmount = 10f;
            fuelAmount += addFuelAmount;
            if (fuelAmount > fuelAmountMax) {
                fuelAmount = fuelAmountMax;
            }
            OnFuelPickup?.Invoke(this, EventArgs.Empty);
            fuelPickup.DestroySelf();
        }

        if (collider2D.gameObject.TryGetComponent(out CoinPickup coinPickup)) {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroySelf();
        }

        if (collider2D.gameObject.TryGetComponent(out WorldBounds worldBounds)) {
            Debug.Log("We collided with " + worldBounds.GetDirection() + " world bounds!");
        }
    }

    private void SetState(State state) {
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            state = state,
        });
    }

    private void ConsumeFuel() {
        float fuelConsumptionAmount = 1f;
        fuelAmount -= fuelConsumptionAmount * Time.deltaTime;
    }

    public float GetFuelAmount() {
        return fuelAmount;
    }

    public float GetFuelAmountNormalized() {
        return fuelAmount / fuelAmountMax;
    }

    public float GetSpeedX() {
        return landerRigidBody2D.linearVelocityX;
    }

    public float GetSpeedY() {
        return landerRigidBody2D.linearVelocityY;
    }
}