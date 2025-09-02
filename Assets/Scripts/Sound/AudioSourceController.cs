using System.Collections;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;

    SoundType _soundType;
    bool _isPaused = false;

    public void Initialize(SoundType type)
    {
        _soundType = type;

        if(_soundType.IsPersistent) { DontDestroyOnLoad(gameObject); }
        if(_soundType.IsLooping) { _audioSource.loop = true; }
        if(_soundType.IsRandomized) { SetPitch(Random.Range(_soundType.PitchRange.Item1, _soundType.PitchRange.Item2)); }
    }

    public void Play(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
        if(!_soundType.IsLooping) { StartCoroutine(DestroyAfterPlaying()); }
    }

    private void Update()
    {
        if(!_soundType.IsPausable) { return; }
        if (Time.timeScale == 0f && _audioSource.isPlaying)
        {
            _isPaused = true;
            _audioSource.Pause();
        }
        else if (Time.timeScale > 0f && !_audioSource.isPlaying)
        {
            _isPaused = false;
            _audioSource.UnPause();
        }
    }

    public void SetMute(bool mute)
    {
        if (_soundType.IsMutable) { _audioSource.mute = mute; }
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume * _soundType.VolumeMultiplier;
    }

    public void SetPitch(float pitch)
    {
        _audioSource.pitch = pitch;
    }

    public void SetSpatial(float spatial)
    {
        _audioSource.spatialBlend = spatial;
    }

    IEnumerator DestroyAfterPlaying()
    {
        while(_audioSource.isPlaying || _isPaused)
        {
            yield return null;
        }
        SoundManager.Instance.DestroyAudioSource(this);
    }
}
