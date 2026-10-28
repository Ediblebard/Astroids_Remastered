using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private TextMeshProUGUI nextButtonTextMesh;
    [SerializeField] private Button nextButton;

    private Action nextButtonClickAction;

    private void Awake() {
        nextButton.onClick.AddListener(() => {
           nextButtonClickAction();
        });
    }
 
    private void Start() {
        //Lander.Instance.OnLanded += Lander_OnLanded;

        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
        nextButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
