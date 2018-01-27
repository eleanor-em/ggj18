using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour {
    public void Next() {
        SceneManager.LoadScene(1);
    }
}
