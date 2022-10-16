using System;
using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJobStop : AudioJob
    {
        public AudioJobStop(string key, AudioJobOptions audioJobExtras = null) : base(key, audioJobExtras)
        {
            action = AudioAction.STOP;
        }

        public override IEnumerator RunAudioJob(AudioTrack track, AudioClip clip)
        {
            yield return base.RunAudioJob(track, clip);

            if (options.fadeOut == null)
                track.audioSource.Stop();
            else if (options.fadeOut != null)
            {
                var initVolume = options.fadeOut.initVolume != -1 ? options.fadeOut.initVolume : track.audioSource.volume;
                var endVolume = options.fadeOut.endVolumen != -1 ? options.fadeOut.endVolumen : 0;

                yield return FadeAudio(track, options.fadeOut.fadeDuration, initVolume, endVolume,options.fadeOut.callback);
            }
        }

        private protected override IEnumerator FadeAudio(AudioTrack track, float durationFade, float initialVolume, float targetVolume, Action onFinishFadeCallback = null)
        {
            yield return base.FadeAudio(track, durationFade, initialVolume, targetVolume, () =>
            {
                track.audioSource.Stop();
                onFinishFadeCallback?.Invoke();
            });
        }
    }
}