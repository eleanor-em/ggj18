using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
	void Start () {
        StartCoroutine(ExitGame());
	}

    private IEnumerator ExitGame() {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}
