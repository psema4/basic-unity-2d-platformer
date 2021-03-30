using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip[] audioClips;
	public int currentClip = 0;			// music tracks start at index 0
	public int effectsStartAtId = 1;	// sound effects start at this index
	private AudioSource audioSource;
	
    void Start() {
		audioSource = GetComponent<AudioSource>();
	}
    
	public void Play(int clipNumber) {
		audioSource.clip = audioClips[clipNumber];
		audioSource.Play();
		StartCoroutine(WaitForClipEnded(clipNumber));
	}
	
	public IEnumerator WaitForClipEnded(int clipNumber) {
		yield return new WaitUntil(() => audioSource.isPlaying == false);

		// FIXME: focusing a window other than the unity editor triggers
		// Debug.Log("Clip " + clipNumber + " has finished!");

		currentClip++;
		if (currentClip >= effectsStartAtId) {
			currentClip = 0;
		}
		
		Play(currentClip);
	}
	
	public void PlayOneShot(int clipNumber) {
		audioSource.PlayOneShot(audioClips[clipNumber], 0.5f);
	}

}
