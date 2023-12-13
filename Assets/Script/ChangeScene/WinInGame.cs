using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinInGame : MonoBehaviour
{
    [SerializeField]
    private string Scene;
    void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(Scene);
        }
    }
}
