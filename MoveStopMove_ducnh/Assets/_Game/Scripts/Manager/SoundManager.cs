using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager ins;
    public static SoundManager Ins=>ins;

    [Header("Audio Source")]
    [SerializeField]private SFXMusic sfxSourcePrefab;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioMixerGroup mixerGroup;
    public Camera MainCamera;
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
        SFXMusic sFXMusic=SimplePool.Spawn<SFXMusic>(sfxSourcePrefab);
        sFXMusic.OnInit(mixerGroup);
        AudioSource sfxSource=sFXMusic.SfxSource;
        AudioClip audioClip=null;
        switch(eSound){
            case ESound.ATTACK:
                audioClip=attackSound;
                sfxSource.PlayOneShot(attackSound);
                break;
            case ESound.LOSE:
                audioClip=loseSound;
                sfxSource.PlayOneShot(loseSound);
                break;
            case ESound.PLAYER_DEATH:
                audioClip=playerDeathSound;
                sfxSource.PlayOneShot(playerDeathSound);
                break;
            case ESound.WEAPON_HIT:
                audioClip=weaponHitSound;
                sfxSource.PlayOneShot(weaponHitSound);
                break;
            case ESound.CLICK:
                audioClip=clickSound;
                sfxSource.PlayOneShot(clickSound);
                break;
            case ESound.VICTORY:
                audioClip=victorySound;
                sfxSource.PlayOneShot(victorySound);
                break;
        }
        if(audioClip==null) return;
        float clipLength=audioClip.length;
        sFXMusic.OnDespawn(clipLength);
    }

    /// <summary>
    /// Use this when you want to play music when target in view
    /// </summary>
    /// <param name="target"></param>
    /// <param name="eSound"></param>
    public void PlaySFX(Transform target,ESound eSound){
        if(!IsInCameraView(target)) return;
        PlaySFX(eSound);
    }

    
    

    private bool IsInCameraView(Transform target)
    {
        Vector3 screenPoint = MainCamera.WorldToViewportPoint(target.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0;
    }
}
