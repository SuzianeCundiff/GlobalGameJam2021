﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Serializable]
    class LevelMusic
    {
        public string name;
        public AudioClip music;
    }

    [SerializeField] LevelMusic[] musics;

    private AudioSource audioSource;

    private void Awake()
    {
        MusicManager[] objects = FindObjectsOfType<MusicManager>();

        if (objects.Length > 1)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].gameObject == gameObject)
                {
                    Destroy(objects[i].gameObject);
                }
            }
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();
    }

    public void changeMusic(string name)
    {
        for (int i = 0; i < musics.Length; i++)
        {
            if(musics[i].name == name)
            {
                audioSource.clip = musics[i].music;
                break;
            }
        }
    }

    public void FadeOutMusic(float duration)
    {
        StartCoroutine(MusicFadeOut(0, duration));
    }

    public void FadeInMusic(float duration)
    {
        StartCoroutine(MusicFadeIn(1, duration));
    }

    IEnumerator MusicFadeIn(int goTo, float duration)
    {
        audioSource.Play();

        while (audioSource.volume < .5f)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, .5f, Time.deltaTime / duration);
            yield return null;
        }
    }

    IEnumerator MusicFadeOut(int goTo, float duration)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0, Time.deltaTime / duration);
            yield return null;
        }
    }
}
