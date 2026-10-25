using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawnerManager : MonoBehaviour {
    [SerializeField] private List<AstroidRock> astroidRocks;
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private int astroidType = 0, astroidPositionRotation = 0;



    private void Start() {

        for (int i = 0; i < spawnPositions.Count; i++) {
            Instantiate(astroidRocks[astroidType], spawnPositions[i].position, spawnPositions[i].rotation);
        }

        //Instantiate(astroidRocks[astroidType], spawnPositions[astroidPositionRotation].position, spawnPositions[astroidPositionRotation].rotation);
        //Instantiate(astroidRocks[1], spawnPositions[1].position, spawnPositions[1].rotation);
        //Instantiate(astroidRocks[0], spawnPositions[3].position, spawnPositions[3].rotation);

        //SpawnAstroid(astroidRocks[0], spawnPositions[0].position, spawnPositions[0].rotation);
    }


    // Repetative, no reason to implement.
    /* private void SpawnAstroid(AstroidRock AstroidRockType, Vector3 SpawnPosition, Quaternion SpawnRotation) {
         Instantiate(AstroidRockType, SpawnPosition, SpawnRotation);
     } */
}
