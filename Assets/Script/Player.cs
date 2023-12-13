using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private NavMeshAgent Agent;
    private Animator Animation;
    private Animator AnimationLevier;
    private Animator AnimationBridge;
    private float CurrentHpPlayer;
    private float HPLoosePlayer = 20;
    private float Timer;
    public bool Death = false;

    [SerializeField]
    private Ennemi Ennemi;
    [SerializeField]
    private GameObject Levier;
    [SerializeField]
    private Transform Bridge;
    [SerializeField]
    private UI HUD;
    [SerializeField]
    private float MaxHPPlayer;
    [SerializeField]
    private GameObject Prefabs;
    [SerializeField]
    private GameObject Arm;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animation = GetComponent<Animator>();
        AnimationLevier = GameObject.FindGameObjectWithTag("Levier").GetComponent<Animator>();
        AnimationBridge = GameObject.FindGameObjectWithTag("Bridge").GetComponent<Animator>();
        CurrentHpPlayer = MaxHPPlayer;
    }

    void Update()
    {
        if (Death == false)
        {

            Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHitInfo;
            float velocity = Agent.velocity.magnitude;
            if (Physics.Raycast(rayFromCamera, out raycastHitInfo))
            {
                if (raycastHitInfo.collider.CompareTag("Ennemi"))
                {
                    HUD.ShowEnemyHP(Ennemi);
                }
                else
                {
                    HUD.HideEnemyHP();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Animation.SetBool("IfWalk", true);
                    if (raycastHitInfo.collider.CompareTag("Ennemi"))
                    {
                        if (Ennemi.IsDead == false)
                        {
                            transform.rotation = Quaternion.LookRotation(Ennemi.transform.position - transform.position).normalized;
                            Agent.stoppingDistance = 10;
                            Agent.SetDestination(raycastHitInfo.collider.transform.position);
                            if (Vector3.Distance(transform.position, raycastHitInfo.transform.position) <= 10)
                            {
                                Animation.SetBool("Attack", true);
                                Instantiate(Prefabs, Arm.transform.position, transform.rotation);
                            }
                        }
                    }
                    else if (raycastHitInfo.collider.CompareTag("Levier") && Vector3.Distance(transform.position, raycastHitInfo.transform.position) <= 5)
                    {
                        Animation.SetBool("Attack", false);
                        if (Vector3.Distance(transform.position, Levier.transform.position) <= 5)
                        {
                            AnimationLevier.SetBool("Open", true);
                            AnimationBridge.SetBool("BridgeDown", true);
                            Bridge.Translate(new Vector3(0, 8, 0));
                        }
                    }
                    else if (raycastHitInfo.collider.CompareTag("Obstacle") && Vector3.Distance(transform.position, raycastHitInfo.transform.position) <= 5)
                    {
                        Animation.SetBool("Attack", false);
                        Destroy(raycastHitInfo.collider.gameObject);
                    }
                    else
                    {
                        Animation.SetBool("Attack", false);
                        Agent.stoppingDistance = 0;
                        Agent.SetDestination(raycastHitInfo.point);
                    }
                }
                else if (velocity == 0)
                {
                    Animation.SetBool("IfWalk", false);
                }
                else
                {
                    Animation.SetBool("IfWalk", true);
                }
            }
        }
        if (CurrentHpPlayer <= 0)
        {
            Timer += Time.deltaTime;
            HUD.DeathTextPlayer();
            Death = true;
            if (Timer >= 5)
            {
                SceneManager.LoadScene("InGame");
            }
        }
    }
    public float GetCurrentHPPercentagePlayer()
    {
        return CurrentHpPlayer / MaxHPPlayer;
    }

    public void HPLostPlayer()
    {
        CurrentHpPlayer -= HPLoosePlayer;
        HUD.ShowPlayerHP(this);
    }
}
