using System;
using UnityEngine;

public class WelcomeUI : MonoBehaviour
{
    private void Start() {
        Lander.Instance.OnAfterForce += Lander_OnAfterForce; 
    }

    private void Lander_OnAfterForce(object sender, EventArgs e) {
        Hide();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
