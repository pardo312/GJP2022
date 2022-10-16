namespace Jiufen.Audio
{
    internal class AudioJob
    {
        private AudioController.AudioAction rESTART;
        private AudioType audioType;

        public AudioJob(AudioController.AudioAction rESTART, AudioType audioType)
        {
            this.rESTART = rESTART;
            this.audioType = audioType;
        }
    }
}