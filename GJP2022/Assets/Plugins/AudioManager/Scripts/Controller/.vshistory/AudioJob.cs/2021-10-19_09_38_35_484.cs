namespace Jiufen.Audio
{
    internal class AudioJob
    {
        private AudioAction audioAction;
        private AudioType type;

        public AudioJob(AudioAction audioAction, AudioType audioType)
        {
            this.audioAction = audioAction ;
            this.type = audioType;
        }
    }
}