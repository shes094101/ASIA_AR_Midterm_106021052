using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator animatorRed, animatorBlue;
    public Button re;
    public int blood_Red = 100;
    public int blood_Blue = 100;
    private bool defend_Blue;
    private bool defend_Red;
    private float delay = 1;

    private bool die_Red = false;
    private bool die_Blue = false;


    [Header("介面")] //告訴我們這是介面
    public Text blood_R;
    public Text blood_B;
    public Text title;

    void Start()
    {
        defend_Blue = false;
        defend_Red = false;
        die_Red = false;
        die_Blue = false;
}
    public AudioSource music;
    public AudioClip attack_music, attack_null;

    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        attack_music = Resources.Load<AudioClip>("music/attack");
        attack_null = Resources.Load<AudioClip>("music/defend");
    }


    public void attackBlue() {
        if (die_Red == false && die_Blue != true) {
            animatorBlue.SetTrigger("attack");
            if (defend_Red != true)
            {
                blood_Red -= 20;
                blood_R.text = "血量:" + blood_Red.ToString() + "%";
                title.text = "紅方被擊中了!";
                Invoke("title_none", delay);
                music.clip = attack_music;
                music.Play();
            }
            else {
                title.text = "藍方攻擊無效!";
                music.clip = attack_null;
                music.Play();
            }
            die_R();
        }
    }

    public void attackRed()
    {
        if (die_Blue == false && die_Red != true)
        {
            animatorRed.SetTrigger("attack");
            if (defend_Blue != true)
            {
                blood_Blue -= 20;
                blood_B.text = "血量:" + blood_Blue.ToString() + "%";
                title.text = "藍方被擊中了!";
                Invoke("title_none", delay);
                music.clip = attack_music;
                music.Play();
            }
            else
            {
                title.text = "紅方攻擊無效!";
                music.clip = attack_null;
                music.Play();
            }
            die_B();
        }
    }

    public void defendBlue()
    {
        if (die_Blue == false)
        {
            animatorBlue.SetTrigger("defend");
            defend_Blue = true;
            title.text = "藍方防禦!!";
            Invoke("denfend_B", delay);
        }
    }

    public void defendRed()
    {
        if (die_Red == false)
        {
            animatorRed.SetTrigger("defend");
            defend_Red = true;
            title.text = "紅方防禦!!";
            Invoke("denfend_R", delay);
        }
    }

    void die_R()
    {
        if (blood_Red <= 0)
        {
            animatorRed.SetTrigger("die");
            die_Red = true;
            animatorBlue.SetTrigger("taunt");
            title.text = "藍方獲勝";
            re.gameObject.SetActive(true);
        }
    }
    void die_B()
    {
        if (blood_Blue <= 0)
        {
            animatorBlue.SetTrigger("die");
            die_Blue = true;
            animatorRed.SetTrigger("taunt");
            title.text = "紅方獲勝";
            re.gameObject.SetActive(true);
        }
    }
    void title_none()
    {
        title.text = "";
    }

    void denfend_R() {
        defend_Red = false;
        title.text = "";
        }
    
    void denfend_B() {
        defend_Blue = false;
        title.text = "";
    }

    public void restart() {
        SceneManager.LoadScene(0);
    }
    
    

}
