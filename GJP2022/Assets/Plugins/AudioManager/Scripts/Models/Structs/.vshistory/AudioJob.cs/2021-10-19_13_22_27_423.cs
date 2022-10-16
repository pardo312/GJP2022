using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioType type;
        public AudioAction action;
        public AudioJobOptions audioJobExtras;

        public AudioJob(AudioType audioType, AudioAction audioAction, AudioJobOptions audioJobExtras = null)
        {
            this.type = audioType;
            this.action = audioAction;
            this.audioJobExtras = audioJobExtras != null ? audioJobExtras : new AudioJobOptions();
        }
    }
    public class AudioJobOptions
    {
        public AudioFadeInfo fadeIn;
        public AudioFadeInfo fadeOut;
        public WaitForSeconds delay;

        public AudioJobOptions(AudioFadeInfo fadeIn = null, AudioFadeInfo fadeOut = null, float delay = 0f)
        {
            this.fadeIn = fadeIn != null ? fadeIn : new AudioFadeInfo(false, 0);
            this.fadeOut = fadeOut != null ? fadeOut : new AudioFadeInfo(false, 0);
            this.delay = delay > 0f ? new WaitForSeconds(delay) : null;
        }
    }

    public class AudioFadeInfo
    {
        public static AudioFadeInfo defaultfadeInfo = new AudioFadeInfo(false, 0);

        public bool fade;
        public float fadeDuration;

        public AudioFadeInfo(bool fade, float fadeDuration)
        {
            this.fade = fade;
            this.fadeDuration = fadeDuration;
        }
    }
}