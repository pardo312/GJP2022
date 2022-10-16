using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioType type;
        public AudioAction action;
        public WaitForSeconds delay;
        public AudioFadeInfo fadeIn;
        public AudioFadeInfo fadeOut;

        public AudioJob(AudioType audioType, AudioAction audioAction, float delay, AudioFadeInfo fadeIn, AudioFadeInfo fadeOut)
        {
            this.type = audioType;
            this.action = audioAction;
            this.delay = delay > 0f ? new WaitForSeconds(delay) : null;
            this.fadeIn = fadeIn;
            this.fadeOut = fadeOut;
        }
    }

    public class AudioFadeInfo
    {
        public static AudioFadeInfo defaultfadeInfo = new AudioFadeInfo(false,0);
        public bool fade = ;
        public float fadeDuration;

        public AudioFadeInfo(bool fade, float fadeDuration)
        {
            this.fade = fade;
            this.fadeDuration = fadeDuration;
        }
    }
}