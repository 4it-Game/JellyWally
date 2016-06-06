using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager Instance{ set; get; }

	private int hitPoint = 3;
	private int score = 0;

	public Transform spawnPosition;
	public Transform player;

	public Text scoreText;
	public Text hitPointText;

	void Awake(){
		Instance = this;
		scoreText.text = "Score : " + score.ToString ();
		hitPointText.text =hitPoint.ToString ();
	}

	private void Update(){
		if (player.position	.y < -10) {
			player.position = spawnPosition.position;
			hitPoint--;
			hitPointText.text =hitPoint.ToString ();
			if (hitPoint <= 0) {
				SceneManager.LoadScene ("Menu");
			}

		}
	}

	public void Win(){
		Debug.Log ("Victory");
	}

	public void CollectCoin(){
		score++;
		scoreText.text = "Score : " + score.ToString ();
	}
}
