using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJobsController : MonoBehaviour
    {

        #region 1.Fields
        public Hashtable m_jobsTable;
        #endregion 1.Fields

        #region 2.Methods
        #region 2.0.Init
        public void Init()
        {
            m_jobsTable = new Hashtable();
        }
        #endregion 2.0.Init

        #region 2.1.Add & Execute Jobs
        public void AddJob(AudioJob _audioJob)
        {
            RemoveConflictingJobs(_audioJob.type);

            //Add Job
            IEnumerator jobRunner = RunAudioJob(_audioJob);
            m_jobsTable.Add(_audioJob.type, jobRunner);
            StartCoroutine(jobRunner);
            AudioLogger.Log($"Starting Job {_audioJob.type} with action: {_audioJob.action}");
        }

        private IEnumerator RunAudioJob(AudioJob _audioJob)
        {
            yield return new WaitForSeconds(_audioJob.delay); ;
            AudioTrack track = (AudioTrack)AudioController.Instance.m_audioTable[_audioJob.type];
            track._audioSource.clip = GetAudioClipFromAudioTrack(_audioJob.type, track);

            switch (_audioJob.action)
            {
                case AudioAction.START:
                    track._audioSource.Play();
                    break;
                case AudioAction.STOP:
                    if (_audioJob.fadeOutPrevious)
                    {
                        track._audioSource.Stop();
                    }
                    break;
                case AudioAction.RESTART:
                    track._audioSource.Stop();
                    track._audioSource.Play();
                    break;
            }

            m_jobsTable.Remove(_audioJob.type);
            AudioLogger.Log($"Job count: {m_jobsTable.Count}");
        }
        private AudioClip GetAudioClipFromAudioTrack(AudioType type, AudioTrack track)
        {
            foreach(AudioObject obj in track._audioObject)
            {
                if(obj._audioType == type)
                {
                    return obj._audioClip;
                }
            }
            return null;
        }
        #endregion 2.1.Add & Execute Jobs

        #region 2.2.Remove and Dispose Jobs
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
                    AudioLogger.LogError($"You have the same audio source for different audioTypes. Please check audioType [{job.Key}] and [{_type}] ");
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
                AudioLogger.LogError($"You are trying to stop a job that doesn't exist in the jobsTable: {_type}");
                return;
            }
            IEnumerator runningJob = (IEnumerator)m_jobsTable[_type];
            StopCoroutine(runningJob);
            m_jobsTable.Remove(_type);
        }
        public void Dispose()
        {
            foreach(DictionaryEntry entry in m_jobsTable)
            {
                IEnumerator job = (IEnumerator)entry.Value;
                StopCoroutine(job);
            }
        }

        #endregion 2.2.Remove and Dispose Jobs
        #endregion 2.Methods
    }
}
