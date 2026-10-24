using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Collider2D shipCollider;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (gameCamera == null)
            gameCamera = Camera.main;

        if (shipCollider == null)
            shipCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        WorldBounds boundary = other.GetComponent<WorldBounds>();

        if (boundary == null)
            return;

        // Calculate the visible camera boundaries.
        float halfHeight = gameCamera.orthographicSize;
        float halfWidth = halfHeight * gameCamera.aspect;

        float left = gameCamera.transform.position.x - halfWidth;
        float right = gameCamera.transform.position.x + halfWidth;
        float bottom = gameCamera.transform.position.y - halfHeight;
        float top = gameCamera.transform.position.y + halfHeight;

        // Account for the size of the ship's collider.
        float shipHalfWidth = shipCollider.bounds.extents.x;
        float shipHalfHeight = shipCollider.bounds.extents.y;

        Vector3 position = transform.position;

        WorldBounds.Direction direction = boundary.GetDirection();

        switch (direction)
        {
            case WorldBounds.Direction.North:
                position.y = bottom + shipHalfHeight;
                break;

            case WorldBounds.Direction.South:
                position.y = top - shipHalfHeight;
                break;

            case WorldBounds.Direction.East:
                position.x = left + shipHalfWidth;
                break;

            case WorldBounds.Direction.West:
                position.x = right - shipHalfWidth;
                break;
        }

        transform.position = position;

        // Intentionally leave Rigidbody2D velocity unchanged!
    }
}
