using UnityEngine;
using UnityEngine.AI;

public class Ennemi : MonoBehaviour
{
    [SerializeField]
    private Vector3 Direction;
    [SerializeField]
    private int Speed;
    [SerializeField]
    private float MaxHP;
    [SerializeField]
    private Player Player;
    [SerializeField]
    private AudioClip[] AudioClip;

    private float CurrentHp;
    private float HPLoose = 22;

    private AudioSource Audio;
    private Animator Animation;
    private NavMeshAgent Agent;
    private float Timer = 4;
    public bool IsDead;

    void Start()
    {
        IsDead = false;
        Audio = GetComponent<AudioSource>();
        Agent = GetComponent<NavMeshAgent>();
        Animation = GetComponent<Animator>();
        CurrentHp = MaxHP;

        Audio.PlayOneShot(AudioClip[0]);
        Audio.loop = true;
    }


    void Update()
    {
        if (GetCurrentHPPercentage() < 1f && Player.Death != true)
        {
            Charge();
        }
        else
        {
            Move();
        }
        if (IsDead == true)
        {
            Destroy(gameObject, 2);
        }
        if (Player.Death == true)
        {
            Audio.Stop();
        }
    }
    private void Move()
    {
        Animation.SetBool("IsMoonWalk", true);
        transform.Translate(Direction * Speed * Time.deltaTime);
        Debug.DrawRay(transform.position + new Vector3(0f, 1.5f, 0f), transform.right * 2, Color.red);
        RaycastHit raycastHitInfo;
        if (Physics.Raycast(transform.position + new Vector3(0f, 1.5f, 0f), transform.right, out raycastHitInfo, 2f))
        {
            if (raycastHitInfo.collider.CompareTag("Wall"))
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
    private void Charge()
    {
        if (IsDead == false)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) <= 5)
            {
                transform.rotation = Quaternion.LookRotation(Player.transform.position - transform.position).normalized;
                Animation.SetBool("WalkForFight", false);
                Animation.SetBool("IsMoonWalk", false);
                Timer += Time.deltaTime;
                if (Timer >= 3)
                {
                    Animation.SetBool("Attack", true);
                    Player.HPLostPlayer();
                    Timer = 1;
                }
            }
            else if (Vector3.Distance(transform.position, Player.transform.position) >= 5)
            {
                Animation.SetBool("IsMoonWalk", false);
                Animation.SetBool("Attack", false);
                Animation.SetBool("WalkForFight", true);
                Agent.SetDestination(Player.transform.position);
                Agent.stoppingDistance = 5;
            }
        }
        else
        {
            Animation.SetBool("IsMoonWalk", false);
            Animation.SetBool("Attack", false);
            Animation.SetBool("WalkForFight", false);
        }
    }

    public float GetCurrentHPPercentage()
    {
        return CurrentHp / MaxHP;
    }

    public void HPLost()
    {
        if (CurrentHp == MaxHP)
        {
            Audio.Stop();
            Audio.PlayOneShot(AudioClip[1]);
        }
        else if (CurrentHp <= 20)
        {
            Audio.Stop();
            Audio.PlayOneShot(AudioClip[2]);
            IsDead = true;
        }
        CurrentHp -= HPLoose;
    }
}
