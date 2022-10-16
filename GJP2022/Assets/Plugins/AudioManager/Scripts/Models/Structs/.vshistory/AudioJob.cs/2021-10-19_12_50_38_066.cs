using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioAction action;
        public AudioType type;
        public FadeInfo fadeIn;
        public FadeInfo fadeOut;
        public WaitForSeconds delay;

        public AudioJob(AudioAction audioAction, AudioType audioType, FadeInfo fadeIn, FadeInfo fadeOut, float delay)
        {
            this.action = audioAction;
            this.type = audioType;
            this.fadeIn = fadeIn;
            this.fadeOut = fadeOut;
            this.delay = delay > 0f ? new WaitForSeconds(delay) : null;
        }
    }

    public class FadeInfo
    {
        public bool fade;
        public float fadeDuration;

        public FadeInfo(bool fade, float fadeDuration)
        {
            this.fade = fade;
            this.fadeDuration = fadeDuration;
        }
    }
}