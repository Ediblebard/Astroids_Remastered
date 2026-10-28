using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawnerManager : MonoBehaviour {
    [SerializeField] private List<AstroidRock> astroidRocks;
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private int astroidType = 0; 

    public static AstroidSpawnerManager Instance {get; private set;}

    private void Awake() {
        Instance = this;
    }

    public enum AstroidRockPosition {
        BottomCenter,
        BottomRight,
        BottomRightCorner,
        Right,
        TopRightCorner,
        TopRight,
        TopCenter,
        TopLeft,
        TopLeftCorner,
        Left,
        BottomLeftCorner,
        BottomLeft
    }

    public void SpawnAstroidAtLocation(int astroidRockType, int spawnPosition) {
        Instantiate(astroidRocks[astroidRockType], spawnPositions[spawnPosition].position, spawnPositions[spawnPosition].rotation);
    }

    public void SpawnAstroidAtPosition(int astroidRockType, Vector3 spawnPosition, Quaternion quaternion) {
        Instantiate(astroidRocks[astroidRockType], spawnPosition, quaternion);        
    }

    private void SpawnAllSides() {
        for (int i = 0; i < spawnPositions.Count; i++) {
            Instantiate(astroidRocks[astroidType], spawnPositions[i].position, spawnPositions[i].rotation);
        }
    }

    public void SpawnAstroid(AstroidRock AstroidRockType, Vector3 SpawnPosition, Quaternion SpawnRotation) {
        Instantiate(AstroidRockType, SpawnPosition, SpawnRotation);
    }

}
