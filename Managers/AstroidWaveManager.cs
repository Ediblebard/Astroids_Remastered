using UnityEngine;

public class AstroidWaveManager : MonoBehaviour
{
    public static AstroidWaveManager Instance {get; private set;}

    private void Awake() {
        Instance = this;
    }
    
    private void Start() {
        WaveSpawner();
    }

    private void WaveSpawner() {
        //Instantiate(astroidRocks[2], spawnPositions[0].position, spawnPositions[0].rotation);
        AstroidSpawnerManager.Instance.SpawnAstroidAtLocation((int)AstroidRock.AstroidSize.Large,(int)AstroidSpawnerManager.AstroidRockPosition.TopCenter);
    }

    /*
        Level 1 - 1x Small Astroid
        Level 2 - 2x Small Astroid
        Level 3 - 1x Medium Astroid
        Level 4 - 1x Medium Astroid, 1x Small Astroid
        Level 5 - 1x Medium Astroid, 2x Small Astroid
        Level 6 - 1x Large Astroid
        Level 7 - 1x Large Astroid, 1x Small Astroid
        Level 8 - 1x Large Astroid, 2x Small Astroid
        Level 9 - 1x Large Astroid, 1x Medium Astroid
        Level 10 - 1x Large Astroid, 2x Medium Astroid
    */
}
