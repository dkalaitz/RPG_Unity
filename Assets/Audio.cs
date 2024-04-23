using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    public AudioSource battleAudio, gameAudio;
    public bool isBattleAudioPlaying = false;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        Debug.Log(enemy.name);
        battleAudio.Stop();
        gameAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAudio();
    }

    private void HandleAudio()
    {
        if (enemy.isInAggroRange && !enemy.isDead)
        {
            if(!isBattleAudioPlaying)
                PlayBattleAudio();
        }
        else
        {
            if (isBattleAudioPlaying)
                StopBattleAudio();
        }
    }

    private void PlayBattleAudio()
    {
        gameAudio.Pause();
        battleAudio.Play();
        isBattleAudioPlaying = true;
    }

    private void StopBattleAudio()
    {
        battleAudio.Pause();
        gameAudio.Play();
        isBattleAudioPlaying = false;
    }

    
}
