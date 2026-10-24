using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawnerManager : MonoBehaviour
{
    [SerializeField] private List<AstroidRock> astroidRocks;
    [SerializeField] private List<Transform> spawnPositions;
    

    private void Start() {
        Instantiate(astroidRocks[0], spawnPositions[0].position, spawnPositions[0].rotation);
        Instantiate(astroidRocks[1], spawnPositions[1].position, spawnPositions[1].rotation);
        Instantiate(astroidRocks[0], spawnPositions[2].position, spawnPositions[2].rotation);
    }

}
