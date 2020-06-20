using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IndexManager : MonoBehaviour
{

    public void EasyButton()
    {
        GhostMove.speed = 0.05f;
        Weapon.IsShooting = false;
        LoadMain();
    }

    public void MidButton()
    {
        GhostMove.speed = 0.15f;
        Weapon.IsShooting = false;
        LoadMain();
    }

    public void HardButton()
    {
        GhostMove.speed = 0.3f;
        Weapon.IsShooting = false;
        LoadMain();
    }

    public void ShootingButton()
    {
        GhostMove.speed = 0.15f;
        Weapon.IsShooting = true;
        LoadMain();
    }

    public void ScoreButton()
    {
        SceneManager.LoadScene("Score");
    }
    //加载主场景
    private void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}
