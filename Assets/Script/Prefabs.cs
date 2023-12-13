using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    private float Timer;

    [SerializeField]
    private float Speed;

    void Start()
    {
    }


    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        Timer += Time.deltaTime;
        if (Timer >= 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ennemi"))
        {
            collision.collider.GetComponent<Ennemi>().HPLost();
            Destroy(gameObject);
        }    
    }
}
