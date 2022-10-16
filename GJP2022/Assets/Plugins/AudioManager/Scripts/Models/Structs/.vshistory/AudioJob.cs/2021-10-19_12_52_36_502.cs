using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioType type;
        public AudioAction action;
        public WaitForSeconds delay;
        public FadeInfo fadeIn;
        public FadeInfo fadeOut;

        public AudioJob(AudioType audioType, AudioAction audioAction, float delay, FadeInfo fadeIn, FadeInfo fadeOut)
        {
            this.type = audioType;
            this.action = audioAction;
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