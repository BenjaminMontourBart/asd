using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 Offset;
    [SerializeField]
    private Transform Player;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Player.position + Offset;
    }
}
