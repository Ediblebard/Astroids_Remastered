using UnityEngine;

public class WorldBounds : MonoBehaviour
{
    // Just for reference instead of using strings

    public enum Direction {
        North,
        East,
        South,
        West,
    }

    [SerializeField] private Direction direction;

    public Direction GetDirection() {
        return direction;
    }

}
