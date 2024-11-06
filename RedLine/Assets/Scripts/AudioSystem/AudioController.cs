using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer redlineMixer;
    [SerializeField] private AudioMixerGroup SFXGROUP;
    [SerializeField] private AudioMixerGroup BGMGROUP;

    [SerializeField] private List<AudioClip> SFXClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> BGMClips = new List<AudioClip>();

    public IEnumerator SetAndPlayNewClip(AudioSource audioSource, int audioIndex = 0, int audioType = 0)
    {
        audioSource.Stop();
        AudioClip newClip = null;

        if (audioType > 0)
        {
            if (audioType == 1)
            {
                audioSource.outputAudioMixerGroup = SFXGROUP;
                newClip = SFXClips[audioIndex];
            }
            if (audioType == 2)
            {
                audioSource.outputAudioMixerGroup = BGMGROUP;
                newClip = BGMClips[audioIndex];
            }

            newClip.LoadAudioData();
            audioSource.clip = newClip;
            audioSource.Play();
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine(SetAndPlayNewClip(audioSource, 0, 0));
    }
}
