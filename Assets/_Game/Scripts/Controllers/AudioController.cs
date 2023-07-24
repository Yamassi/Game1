using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    private List<EventInstance> _eventInstances;
    private EventInstance _musicEventInstance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        _eventInstances = new List<EventInstance>();
    }
    public void StartMusic(EventReference musicEventReference)
    {
        _musicEventInstance = CreateInstance(musicEventReference);
        // _musicEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
        _musicEventInstance.start();
    }
    public void StopMusic()
    {
        _musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPosition)
    => RuntimeManager.PlayOneShot(sound, worldPosition);
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        _eventInstances.Add(eventInstance);
        return eventInstance;
    }

}
