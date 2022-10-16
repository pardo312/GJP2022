namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioAction action;
        public AudioType type;
        public bool fadeOutPrevious;
        public float delay;

        public AudioJob(AudioAction audioAction, AudioType audioType,bool fade, float delay)
        {
            this.action = audioAction ;
            this.type = audioType;
            this.fadeOutPrevious = fade  ;
            this.delay = delay;
        }
    }
}