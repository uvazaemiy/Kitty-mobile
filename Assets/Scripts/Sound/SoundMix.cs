using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMix : MonoBehaviour
{
    [SerializeField] private List<SoundSettings> _soundSettings = new List<SoundSettings>();
    [SerializeField] private bool _stopOnPause;
    private Coroutine _coroutine;

    private int _index;

    public void Play()
    {
        _index = 0;
        if (_soundSettings.Count > 0)
            _coroutine = StartCoroutine(Wait());
    }

    public void Stop()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        for (int i = 0; i < _soundSettings.Count; i++)
            _soundSettings[i].Sound.Stop();
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_soundSettings[_index].Time);
        _soundSettings[_index].Sound.Play();
        _index++;
        if (_index < _soundSettings.Count)
            _coroutine = StartCoroutine(Wait());
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && _stopOnPause)
        {
            Stop();
        }
    }

    public void VolumeChange(float volume)
    {
        _soundSettings[0].Sound.volume = volume;
    }

}

[System.Serializable]
public class SoundSettings
{
    public float Time;
    public AudioSource Sound;
}