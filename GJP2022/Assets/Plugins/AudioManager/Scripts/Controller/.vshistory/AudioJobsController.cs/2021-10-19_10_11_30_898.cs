using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJobsController
    {

        #region 1.Fields
        public Hashtable m_jobsTable;
        #endregion 1.Fields

        #region 2.Methods
        private void Init()
        {
            m_jobsTable = new Hashtable();
        }

        private void AddJob(AudioJob _audioJob)
        {
            RemoveConflictingJobs(_audioJob.type);

            //Add Job
            IEnumerator jobRunner = RunAudioJob(_audioJob);
            m_jobsTable.Add(_audioJob.type, _audioJob);
            Log($"Starting Job {_audioJob.type} with action: {_audioJob.action}");
        }

        private IEnumerator RunAudioJob(AudioJob audioJob)
        {
            throw new NotImplementedException();
        }

        private void RemoveConflictingJobs(AudioType _type)
        {
            if (m_jobsTable.Contains(_type))
            {
                RemoveJob(_type);
            }

            AudioType audioTypeConflict = AudioType.None;

            foreach (DictionaryEntry job in m_jobsTable)
            {
                AudioTrack currentTrack = (AudioTrack)AudioController.Instance.m_audioTable[job.Key];
                AudioTrack newTrack = (AudioTrack)AudioController.Instance.m_audioTable[_type];
                if (currentTrack._audioSource == newTrack._audioSource)
                {
                    LogError($"You have the same audio source for different audioTypes. Please check audioType [{job.Key}] and [{_type}] ");
                    audioTypeConflict = _type;
                }
            }
            if (audioTypeConflict != AudioType.None)
            {
                RemoveJob(audioTypeConflict);
            }
        }

        private void RemoveJob(AudioType _type)
        {
            if (m_jobsTable.ContainsKey(_type))
            {
                LogError($"You are trying to stop a job that doesn't exist in the jobsTable: {_type}");
                return;
            }
            IEnumerator runningJob = (IEnumerator)m_jobsTable[_type];
            StopCoroutine(runningJob);
            m_jobsTable.Remove(_type);
        }
        #endregion 2.Methods
    }
}
