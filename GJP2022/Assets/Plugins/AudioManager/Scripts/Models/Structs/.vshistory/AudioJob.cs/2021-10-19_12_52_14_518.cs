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

        public AudioJob(AudioAction audioAction, float delay, AudioType audioType, FadeInfo fadeIn, FadeInfo fadeOut)
        {
            this.action = audioAction;
            this.type = audioType;
            this.delay = delay > 0f ? new WaitForSeconds(delay) : null;
            this.fadeIn = fadeIn;
            this.fadeOut = fadeOut;
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