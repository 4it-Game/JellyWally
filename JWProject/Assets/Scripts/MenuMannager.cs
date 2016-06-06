using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuMannager : MonoBehaviour {

	public void ToGame(){
		SceneManager.LoadScene ("Scene");
	}

	public void Exit(){
		Application.Quit ();
	}
}
