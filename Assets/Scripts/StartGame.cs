using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour {
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Next();
        }
    }

    public void Next() {
        SceneManager.LoadScene(1);
    }

    public void Exit() {
        Application.Quit();
    }
}
