using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	public static AudioManager instance;

	// Initialization, this allows us to play sounds on start
	void Awake () {
		
		// Keeps the audio manager consistent through scenes, and keeps only on instance, we may or may not need this, but I figured it would be handy
		if(instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		
		// Take our array of sounds and create an audio source for each
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}
	
	// Plays music at the start
	void Start()
	{
		Play("Battle Theme");
	}

	// This finds the sound in our array of sounds and then plays the audio clip attached to it
	public void Play (string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) 
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;}
		s.source.Play();
	}
	
	// Same as Play() but Stops the sound
	public void Stop (string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) 
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;}
		s.source.Stop();
	}
}
