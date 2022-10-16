using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public partial class AudioController : MonoBehaviour
    {

        #region 1.Fields
        public bool _debug;
        public AudioTrack[] _audioTracks;
        public Hashtable _audioTable;
        public Hashtable _jobsTable;

        public AudioController Instace;
        #endregion 1.Fields

        #region 2.Methods

        #region 2.1.UnityEvents
        public void Awake()
        {
            if (Instace != null) return;
            else
            {
                Configure();
            }
        }
        public void OnDisable()
        {
            Dispose();
        }

        #endregion 2.1.UnityEvents

        #region 2.2.Audio Behaviours
        public void PlayAudio(AudioType audioType)
        {
            AddJob(new AudioJob(AudioAction.START, audioType));
        }
        public void StopAudio(AudioType audioType)
        {
            AddJob(new AudioJob(AudioAction.STOP, audioType));
        }
        public void RestartAudio(AudioType audioType)
        {
            AddJob(new AudioJob(AudioAction.RESTART, audioType));
        }
        #endregion 2.2.Audio Behaviours

        #region 2.3.Jobs Handling
        private void AddJob(AudioJob audioJob)
        {
            RemoveConflictingJobs(audioJob.type);
        }

        private void RemoveConflictingJobs(AudioType audioType)
        {
            if (_jobsTable.Contains(audioType))
            {
                RemoveJob(audioType);
            }

            AudioType audioTypeConflict = AudioType.None;

            foreach (DictionaryEntry job in _jobsTable)
            {
                AudioType jobType = (AudioType)job.Key;
                if (audioType  == jobType )
                    audioTypeConflict = audioType;
            }

            if(audioTypeConflict != AudioType.None)
            {
                RemoveJob(audioTypeConflict);
            }
        }

        private void RemoveJob(AudioType audioType)
        {
            throw new NotImplementedException();
        }

        #endregion 2.3.Jobs Handling

        #region 2.4.Helpers
        private void Configure()
        {
            Instace = this;
            _audioTable = new Hashtable();
            _jobsTable = new Hashtable();
            ConfigureAudioTable();
        }

        private void ConfigureAudioTable()
        {
            foreach (AudioTrack tracks in _audioTracks)
            {
                foreach (AudioObject audioObj in tracks._audioObject)
                {
                    if (_audioTable.Contains(audioObj._audioType))
                    {
                        LogError($"AudioTable is registering an already registered audio type: {audioObj._audioType}");
                    }
                    else
                    {
                        _audioTable.Add(audioObj._audioType, tracks);
                        Log($"Audio type {audioObj._audioType} has been registered in the audioTable");
                    }
                }
            }

        }

        private void Dispose()
        {

        }

        private void Log(string message)
        {
            if (!_debug) return;
            Debug.Log($"<color=yellow>Audio Controller message:</color> {message}");
        }
        private void LogError(string message)
        {
            if (!_debug) return;
            Debug.Log($"<color=red>Audio Controller error message:</color> {message}");
        }
        #endregion 2.4.Helpers

        #endregion 2.Methods
    }
}
