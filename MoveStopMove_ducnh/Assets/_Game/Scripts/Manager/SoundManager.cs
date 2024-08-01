using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager ins;
    public static SoundManager Ins=>ins;

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    

    [Header("Audio Clip")]
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip weaponHitSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip victorySound;
    private void Awake() {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }


    public void PlaySFX(ESound eSound){
        switch(eSound){
            case ESound.ATTACK:
                sfxSource.PlayOneShot(attackSound);
                break;
            case ESound.LOSE:
                sfxSource.PlayOneShot(loseSound);
                break;
            case ESound.PLAYER_DEATH:
                sfxSource.PlayOneShot(playerDeathSound);
                break;
            case ESound.WEAPON_HIT:
                sfxSource.PlayOneShot(weaponHitSound);
                break;
            case ESound.CLICK:
                sfxSource.PlayOneShot(clickSound);
                break;
            case ESound.VICTORY:
                sfxSource.PlayOneShot(victorySound);
                break;
        }
    }
}
