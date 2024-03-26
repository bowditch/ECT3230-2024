using GRIDLabGames.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

[RequireComponent(typeof(AudioSource))]   
public class AudioManager : Singleton<AudioManager>
{

    [Header("Background Music Track")]
    [SerializeField]
    private AudioClip[] tracks; //Array of clips to randomize

    [SerializeField]
    private AudioSource audioSource; //Needed to play audio

    [Header("Events")]

    public Action onCurrentTrackEnd;

    public void Awake()
    {
     
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShuffleWhenStopPlaying());
        ShuffleAndPlay();
       
    }

    public void ShuffleAndPlay(GameState gameState = GameState.Playing) //reference enum class
    {
        if (tracks.Length > 0)
        {
            //UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            audioSource.clip = tracks[UnityEngine.Random.Range(0,tracks.Length -1)];
            audioSource.Play();
        }
    }

    private IEnumerator ShuffleWhenStopPlaying() //Another track starts playing when current one ends
    { 
    
    while (true) 
        { 
            yield return new WaitUntil(() => !audioSource.isPlaying);
            ShuffleAndPlay();
            onCurrentTrackEnd?.Invoke(); //Invokes this action to anything that is listening
        }
    
    }

}
