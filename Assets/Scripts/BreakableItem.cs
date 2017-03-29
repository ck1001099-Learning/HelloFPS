﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BreakableItem : MonoBehaviour {

	[System.Serializable]
	public class BreakingEntry{
		public GameObject breakNode;
		public float breakingHP;
	}

	public float currentHP;
	public List<BreakingEntry> breakingSettings;
	public GameObject destroyEffect;
	public void Hit(float hitValue){
		if (currentHP > 0) {
			currentHP -= hitValue;
			if (currentHP <= 0) {
				destroyEffect.gameObject.SetActive (true);
				this.transform.DOScale (new Vector3 (0.0f, 0.0f, 0.0f), 0.01f).SetDelay (0.1f).OnComplete (() => {
					Invoke ("DisableParticleSystems", 10);
				});
			} else {
				foreach (BreakingEntry entry in breakingSettings) {
					if (currentHP < entry.breakingHP) {
						entry.breakNode.SetActive (true);
					}
				}
			}
		}
	}

	public void DisableParticleSystems(){
		ParticleSystem[] particles = this.GetComponentsInChildren<ParticleSystem> ();
		foreach (ParticleSystem p in particles) {
			p.Stop ();
		}
		Invoke ("KillYourself", 5);
	}

	public void KillYourself(){
		GameObject.Destroy (this.gameObject);
	}
}