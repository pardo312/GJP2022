namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioAction action;
        public AudioType type;
        public bool fade;
        public float delay;

        public AudioJob(AudioAction audioAction, AudioType audioType,bool fade, float fadeTime)
        {
            this.action = audioAction ;
            this.type = audioType;
            this.fade = fade  ;
            this.delay = fadeTime;
        }
    }
}