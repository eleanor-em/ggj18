using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {
	void Start () {
        StartCoroutine(ExitGame());
	}

    private IEnumerator ExitGame() {
        yield return new WaitForSeconds(10);
        Application.Quit();
    }
}
