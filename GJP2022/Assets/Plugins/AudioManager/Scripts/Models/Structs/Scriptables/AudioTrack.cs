
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    [CreateAssetMenu(fileName = "AudioTableScriptable.asset", menuName = "Jiufen/Audio/AudioTableScriptable")]
    [System.Serializable]
    public class AudioTrack : ScriptableObject
    {
        [HideInInspector] public AudioSource audioSource;
        public bool activateAudioTrack = true;
        public List<AudioObject> audioObjects = new List<AudioObject>();
    }

    [System.Serializable]
    public class AudioObject
    {
        [HideInInspector] public string key = "";
        [HideInInspector] public AudioClip audioClip = default;

    }
}