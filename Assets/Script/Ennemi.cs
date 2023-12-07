using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Ennemi : MonoBehaviour
{
    [SerializeField]
    private Vector3 Direction;
    [SerializeField]
    private int Speed;

    private Animator Animation;

    void Start()
    {
        Animation = GetComponent<Animator>();
    }


    void Update()
    {
        Animation.SetBool("IsMoonWalk", true);
        transform.Translate(Direction * Speed * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.right * 2, Color.red);
        RaycastHit raycastHitInfo;
        if (Physics.Raycast(transform.position, transform.right, out raycastHitInfo, 2f))
        {
            if (raycastHitInfo.collider.CompareTag("Wall"))
            {
                transform.Rotate(0, 180, 0);
                Debug.Log("Hello");
            }
        }
    }
}
