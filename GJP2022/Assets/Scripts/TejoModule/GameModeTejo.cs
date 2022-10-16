using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameModeTejo : MonoBehaviour
{
    public Transform parent;
    public List<Mecha> mechas = new List<Mecha>();
    public CinemachineVirtualCamera NormalCamera,TejoCamera, GameCamera;
    public int puntaje;
    public Tejo tejo;

    private void Start()
    {
        PutMechas();
    }

    public void PutMechas()
    {
        parent.transform.Rotate(transform.up, Random.Range(0, 359));
        foreach (var mecha in mechas)
        {
            mecha.Restart();
        }
    }

    public void Throw()
    {
        tejo.Throw();
        TejoCamera.Priority = 10;
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        yield return new WaitForSeconds(6);
        TejoCamera.Priority = 9;
        GameCamera.Priority = 10;
        yield return new WaitForSeconds(2);
        GameCamera.Priority = 9;
        PutMechas();
        tejo.Restart();
    }
}
