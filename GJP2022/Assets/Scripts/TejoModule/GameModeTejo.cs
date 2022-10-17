using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameModeTejo : MonoBehaviour
{
    public Transform parent;
    public List<Mecha> mechas = new List<Mecha>();
    public CinemachineVirtualCamera NormalCamera,TejoCamera, GameCamera;
    [SerializeField] TextMeshProUGUI TurnText, PointText;
    public int puntaje;
    int currentTurn = 0;
    public Tejo tejo;
    public Thrower thrower;
    public GameObject winPage;

    private void Start()
    {
        PutMechas();
        foreach (var mecha in mechas)
            mecha.OnExplode += () => { puntaje++; PointText.text = puntaje.ToString("00"); };
    }

    public void PutMechas()
    {
        currentTurn += 1;
        if (currentTurn > 7)
        {
            thrower.gameObject.gameObject.SetActive(false);
            winPage.SetActive(true);
        }
        TurnText.text = (7 - currentTurn).ToString("00");
        parent.transform.Rotate(transform.up, Random.Range(0, 359));
        foreach (var mecha in mechas)
        {
            mecha.Restart();
        }
        thrower.turn = true;
    }

    public void Throw()
    {
        tejo.Throw();
        TejoCamera.Priority = 10;
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        yield return new WaitForSeconds(4);
        TejoCamera.Priority = 9;
        GameCamera.Priority = 10;
        yield return new WaitForSeconds(2);
        GameCamera.Priority = 9;
        PutMechas();
        tejo.Restart();
    }

    public void ReclaimAwards(int index)
    {
        PlayerPrefs.SetInt("Arm" + index.ToString(), 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
