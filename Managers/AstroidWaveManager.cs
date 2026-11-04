using System;
using UnityEngine;

public class AstroidWaveManager : MonoBehaviour {
    public static AstroidWaveManager Instance { get; private set; }
    [SerializeField] private float waveStartTimer = 0, waveDelay = 5f;
    private bool canSpawn = true;
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        waveStartTimer = GameManager.Instance.GetTime();
    }

    private void Update() {
        CheckTimer();
    }

    private void CheckTimer() {
        if ((GameManager.Instance.GetTime() - waveStartTimer) >= waveDelay && canSpawn) {
            canSpawn = false;
            WaveSpawner(GameManager.Instance.GetLevelNumber());
        }
    }

    private void WaveOne() {
        AstroidSpawnerManager.Instance.SpawnAstroidAtLocation(
            (int)AstroidRock.AstroidSize.Small,
            (int)AstroidSpawnerManager.AstroidRockPosition.Left);
        waveStartTimer = GameManager.Instance.GetTime();
        canSpawn = true;
    }

    public void WaveSpawner(int currentLevel) {
        switch (currentLevel) {
            case 1:
                spawnAstroid(AstroidRock.AstroidSize.Small, AstroidSpawnerManager.AstroidRockPosition.Left);
                break;
            case 2:
                spawnAstroid(AstroidRock.AstroidSize.Small, AstroidSpawnerManager.AstroidRockPosition.Right);
                spawnAstroid(AstroidRock.AstroidSize.Small, AstroidSpawnerManager.AstroidRockPosition.TopCenter);
                break;
            case 3:
                spawnAstroid(AstroidRock.AstroidSize.Medium, AstroidSpawnerManager.AstroidRockPosition.BottomCenter);
                break;
            case 4:
                spawnAstroid(AstroidRock.AstroidSize.Medium, AstroidSpawnerManager.AstroidRockPosition.TopCenter);
                spawnAstroid(AstroidRock.AstroidSize.Small, AstroidSpawnerManager.AstroidRockPosition.Right);
                break;
            case 5:
                spawnAstroid(AstroidRock.AstroidSize.Medium, AstroidSpawnerManager.AstroidRockPosition.TopCenter);
                spawnAstroid(AstroidRock.AstroidSize.Small, AstroidSpawnerManager.AstroidRockPosition.Right);
                spawnAstroid(AstroidRock.AstroidSize.Small, AstroidSpawnerManager.AstroidRockPosition.Left);
                break;
            case 6:
                spawnAstroid(AstroidRock.AstroidSize.Large, AstroidSpawnerManager.AstroidRockPosition.TopCenter);
                break;
            case 7:
                spawnAstroid(AstroidRock.AstroidSize.Large, AstroidSpawnerManager.AstroidRockPosition.TopCenter);
                spawnAstroid(AstroidRock.AstroidSize.Small, AstroidSpawnerManager.AstroidRockPosition.Right);
                break;
            case 8:
                spawnAstroid(AstroidRock.AstroidSize.Large, AstroidSpawnerManager.AstroidRockPosition.TopCenter);
                spawnAstroid(AstroidRock.AstroidSize.Small, AstroidSpawnerManager.AstroidRockPosition.Right);
                spawnAstroid(AstroidRock.AstroidSize.Small, AstroidSpawnerManager.AstroidRockPosition.BottomCenter);
                break;
            case 9:
                spawnAstroid(AstroidRock.AstroidSize.Large, AstroidSpawnerManager.AstroidRockPosition.TopCenter);
                spawnAstroid(AstroidRock.AstroidSize.Medium, AstroidSpawnerManager.AstroidRockPosition.BottomCenter);
                break;
            case 10:
                spawnAstroid(AstroidRock.AstroidSize.Large, AstroidSpawnerManager.AstroidRockPosition.TopCenter);
                spawnAstroid(AstroidRock.AstroidSize.Medium, AstroidSpawnerManager.AstroidRockPosition.BottomCenter);
                spawnAstroid(AstroidRock.AstroidSize.Medium, AstroidSpawnerManager.AstroidRockPosition.Right);
                break;
            default:
                Debug.LogError("Ran out of levels Dude!");
                break;
        }
    }

    private void spawnAstroid(AstroidRock.AstroidSize astroidSize, AstroidSpawnerManager.AstroidRockPosition astroidRockPosition) {
        AstroidSpawnerManager.Instance.SpawnAstroidAtLocation((int)astroidSize,(int)astroidRockPosition);
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
