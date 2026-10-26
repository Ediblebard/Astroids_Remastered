using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawnerManager : MonoBehaviour {
    [SerializeField] private List<AstroidRock> astroidRocks;
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private int astroidType = 0, astroidPositionRotation = 0;

    private void Start() {
        //SpawnAllSides();

        Instantiate(astroidRocks[1], spawnPositions[0].position, spawnPositions[0].rotation);
    }

    public void SpawnAstroidAtLocation(int astroidRockType, int spawnPosition) {               
        Instantiate(astroidRocks[astroidRockType], spawnPositions[spawnPosition].position, spawnPositions[spawnPosition].rotation);
    }

    public void SpawnAstroidAtPosition(int astroidRockType, Vector3 spawnPosition, Quaternion quaternion) { 
        Instantiate(astroidRocks[astroidRockType], spawnPosition, quaternion);               
        //Instantiate(astroidRocks[astroidRockType], spawnPositions[spawnPosition].position, spawnPositions[spawnPosition].rotation);
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
