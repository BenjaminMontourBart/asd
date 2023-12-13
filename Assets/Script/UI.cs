using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Slider EnnemiHP;
    [SerializeField]
    private Slider PlayerHP;
    [SerializeField]
    private TextMeshProUGUI DeathText;
    [SerializeField]
    private Image Plane;

    private Animator Animator;

    private bool Death = true;

    void Start()
    {
        Animator = Plane.GetComponent<Animator>();
    }


    void Update()
    {
    }
    public void ShowPlayerHP(Player ShowHPPlayer)
    {
        PlayerHP.value = ShowHPPlayer.GetCurrentHPPercentagePlayer();
    }

    public void ShowEnemyHP(Ennemi ShowHP)
    {
        EnnemiHP.gameObject.SetActive(true);
        EnnemiHP.value = ShowHP.GetCurrentHPPercentage();
    }
    public void HideEnemyHP()
    {
        EnnemiHP.gameObject.SetActive(false);
    }
    public void DeathTextPlayer()
    {
        if (Death == true)
        {
            DeathText.transform.Translate(new Vector3(0f, -572.5f, 0f));
            Animator.SetBool("Dead", true);
            Death = false;
        }
    }
}
