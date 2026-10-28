using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    //[SerializeField] private AstroidSpawnerManager astroidSpawnerManager;

    private static int levelNumber = 1;
    private static int totalScore = 0;

    public static void ResetStaticData() {
        levelNumber = 1;
        totalScore = 0;
    }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private int score;
    private float time;
    private bool isTimerActive;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        //Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;

        GameInput.Instance.OnMenuButtonPressed += GameInput_OnMenuButtonPressed;
        LoadCurrentLevel();
    }

    private void GameInput_OnMenuButtonPressed(object sender, EventArgs e) {
        PauseUnPauseGame();
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e) {
        isTimerActive = e.state == Lander.State.Normal;

        if (e.state == Lander.State.Normal) {
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.SetNormalOrthographicSize();
        }
    }

    private void Update() {
        if (isTimerActive) {
            time += Time.deltaTime;
        }
    }

    public void RegisterAstroid(AstroidRock astroidRock) {
        //Debug.Log("GameManager: Added " + astroidRock + " to the list.");
        astroidRock.OnDestroyed += HandleAstroidDestroyed;
    }

    public void UnRegisterAstroid(AstroidRock astroidRock) {
        //Debug.Log("GameManager: Removed " + astroidRock.GetRockName() + " from the list.");
        astroidRock.OnDestroyed -= HandleAstroidDestroyed;
    }

    private void HandleAstroidDestroyed(object sender, AstroidRock.OnDestroyedEventArgs e) {
        Debug.Log("GameManager: Reporting that *" + e.RockName + "*  *" + e.AstroidSize + "* *" + e.Location + " was destoryed");
        switch (e.AstroidSize) {
            case AstroidRock.AstroidSize.Large:
                
                AstroidSpawnerManager.Instance.SpawnAstroidAtPosition((int)AstroidRock.AstroidSize.Medium, e.Location, e.Rotation);
                AstroidSpawnerManager.Instance.SpawnAstroidAtPosition((int)AstroidRock.AstroidSize.Medium, e.Location, e.Rotation);
                AddScore(150);
                break;
            case AstroidRock.AstroidSize.Medium:
                AstroidSpawnerManager.Instance.SpawnAstroidAtPosition((int)AstroidRock.AstroidSize.Small, e.Location, e.Rotation);
                AstroidSpawnerManager.Instance.SpawnAstroidAtPosition((int)AstroidRock.AstroidSize.Small, e.Location, e.Rotation);
                AddScore(100);
                break;
            case AstroidRock.AstroidSize.Small:
                AddScore(50);
                break;
            default:
                Debug.LogError("GameManager.cs | Error: Failed to handle Astroid Destroyed!");
                break;
        }
    }


    private void LoadCurrentLevel() {
        GameLevel gameLevel = GetGameLevel();
        GameLevel spawnedGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
        Lander.Instance.transform.position = spawnedGameLevel.GetLanderStartPosition();
        cinemachineCamera.Target.TrackingTarget = spawnedGameLevel.GetCameraStartTargetTransform();
        CinemachineCameraZoom2D.Instance.SetTargetOrthographicSize(spawnedGameLevel.GetZoomedOutOrthographicSize());
    }

    private GameLevel GetGameLevel() {
        foreach (GameLevel gameLevel in gameLevelList) {
            if (gameLevel.GetLevelNumber() == levelNumber) {
                return gameLevel;
            }
        }
        return null;
    }

    private void Lander_OnCoinPickup(object sender, EventArgs e) {
        AddScore(500);
    }

    public void AddScore(int addScoreAmount) {
        score += addScoreAmount;
        Debug.Log("SCORE: " + score);
    }

    public int GetScore() {
        return score;
    }

    public float GetTime() {
        return time;
    }

    public int GetTotalScore() {
        return totalScore;
    }

    public void GoToNextLevel() {
        levelNumber++;
        totalScore += score;

        if (GetGameLevel() == null) {
            // No More Levels
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        } else {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        }
    }

    public void RetryLevel() {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }

    public int GetLevelNumber() {
        return levelNumber;
    }

    public void PauseUnPauseGame() {
        if (Time.timeScale == 1f) {
            PauseGame();
        } else {
            UnPauseGame();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    public void UnPauseGame() {
        Time.timeScale = 1f;
        OnGameUnPaused?.Invoke(this, EventArgs.Empty);
    }
}
