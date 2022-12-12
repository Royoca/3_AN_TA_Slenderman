using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    
    public SoundAudioClip[] soundAudioClipArray_;

    private AudioSource audioSource1_, audioSource2_;

    private static Dictionary<Sound, float> soundTimerDictionary_;

    private float time_ = 2.0f;

    public enum Sound {
        PlayerStep,
        DrippingWater,
        SlendermanScream
    }

    [System.Serializable]
    public class SoundAudioClip {
        public Sound soundClip;
        public AudioClip audioClip;
    }

    private void Start() {
        audioSource1_ = gameObject.AddComponent<AudioSource>();
        audioSource2_ = gameObject.AddComponent<AudioSource>();
    }

    public void InitDictionary() {
        soundTimerDictionary_ = new Dictionary<Sound, float>();
        soundTimerDictionary_[Sound.PlayerStep] = 0;
    }

    public void PlaySound(Sound sound, bool is_music) {
       //bool debug_can = CanPlaySound(sound);

        //if (debug_can) {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            if(!is_music) {
                //audioSource.PlayOneShot(GetAudioClip(sound));
            } else {
                audioSource.clip = GetAudioClip(sound);
                //audioSource.Play();
            }
        //}
    }

    private AudioClip GetAudioClip(Sound sound) {
        
        foreach (SoundAudioClip sound_clip in soundAudioClipArray_) {
            if(sound_clip.soundClip == sound) {
                return sound_clip.audioClip;
            }
        }

        Debug.LogError("Sound " + sound + " not found on dictionary");
        return null; 
    }

    private static bool CanPlaySound(Sound sound) {
        switch (sound) {
        default:
            return true;
        case Sound.PlayerStep:
            if (soundTimerDictionary_.ContainsKey(sound)) {
                float lasTimePlayed = soundTimerDictionary_[sound];
                float playerStepTimerMax = 0.5f;
                if (lasTimePlayed + playerStepTimerMax < Time.deltaTime) {
                    soundTimerDictionary_[sound] = Time.deltaTime;
                    return true;
                } else {
                    return false;
                }
            } else {
                return true;
            }
        }
    }

    
}
