using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio.Test
{
    public class TestAudio : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AudioJobOptions audioJobExtras = new AudioJobOptions(new AudioFadeInfo(1), null, true, 1);
                AudioManager.PlayAudio("OST_MAIN_THEME", audioJobExtras);
                Debug.Log("Pressed q");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.PlayAudio("SFX_1");
                Debug.Log("Pressed e");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                AudioManager.PlayAudio("SFX_2");
                Debug.Log("Pressed r");
            }

            //STOPS
            if (Input.GetKeyDown(KeyCode.A))
            {
                AudioJobOptions audioJobExtras = new AudioJobOptions(null, new AudioFadeInfo(1));
                AudioManager.StopAudio("OST_MAIN_THEME", audioJobExtras);
                Debug.Log("Pressed a");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                AudioManager.StopAudio("SFX_1");
                Debug.Log("Pressed d");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioManager.StopAudio("SFX_2");
                Debug.Log("Pressed f");
            }
        }
#endif
    }
}
