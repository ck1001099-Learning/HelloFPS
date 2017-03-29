using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {

	public Image blackCover;
	public Image bloodBlur;
	public Text HPText;

	// Use this for initialization
	void Start () {
		blackCover.color = Color.black;
		DOTween.To (() => blackCover.color, (x) => blackCover.color = x, new Color (0, 0, 0, 0), 1f);
	}

	Tweener tweenAnimation;

	public void PlayHitAnimation(){
		if (tweenAnimation != null) {
			tweenAnimation.Kill ();
		}
		bloodBlur.color = Color.white;
		tweenAnimation = DOTween.To (() => bloodBlur.color, (x) => bloodBlur.color = x, new Color (1, 1, 1, 0), 0.5f);
	}

	public void PlayerDiedAnimation(){
		bloodBlur.color = Color.white;
		DOTween.To (() => blackCover.color, (x) => blackCover.color = x, new Color (0, 0, 0, 1), 1f).SetDelay (3).OnComplete (() => {
			DOTween.To (() => bloodBlur.color, (x) => bloodBlur.color = x, new Color (1, 1, 1, 0), 0.5f).SetDelay (3).OnComplete (() => {
				SceneManager.LoadScene ("PrewScene");
			});
		});
	}

	public void SetHP(int hp){
		HPText.text = "HP: " + hp;
	}
}
