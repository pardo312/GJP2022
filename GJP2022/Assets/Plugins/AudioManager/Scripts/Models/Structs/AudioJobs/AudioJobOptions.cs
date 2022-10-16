using System;
using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJobOptions
    {
        public AudioFadeInfo fadeIn = null;
        public AudioFadeInfo fadeOut = null;
        public bool loop = false;
        public float volume = 1;
        public WaitForSeconds delay;

        public AudioJobOptions(AudioFadeInfo _fadeIn = null, AudioFadeInfo _fadeOut = null, bool _loop = false, float _volume = 1, float _delay = 0f)
        {
            this.fadeIn = _fadeIn;
            this.fadeOut = _fadeOut;
            this.loop = _loop;
            this.delay = _delay > 0f ? new WaitForSeconds(_delay) : null;
            SetVolume(_volume);
        }

        public void SetVolume(float volume)
        {
            if (volume >= 1)
                this.volume = 1;
            else if (volume <= 0)
                this.volume = 0;
            else
                this.volume = volume;
        }
    }
}