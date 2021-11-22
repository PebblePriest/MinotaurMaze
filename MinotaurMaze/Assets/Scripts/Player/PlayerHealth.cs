using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerHealth : MonoBehaviourPun, IPunObservable
{
    public static PlayerHealth instance;
    public int maxHealth = 100;
    public static int currentHealth;
    public int knockBackForce;
    private SpriteRenderer theSR;

    public float invincibleLength;
    private float invincibleCounter;

    public Slider healthSlider;
    public GameObject HealthBar;
    public PhotonView PV;
    private Vector3 smoothMove;

    public GameObject myCamera;

    private void Awake()
    {
        instance = this;
        PV = GetComponent<PhotonView>();
        //HealthBar = GameObject.FindGameObjectWithTag("HealthSlider");
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<Slider>();
        theSR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        healthSlider.value = currentHealth;
        if (PV.IsMine)
        {
            Debug.Log("I activated my camera");
            myCamera.SetActive(true);
            if (invincibleCounter > 0)
            {
                invincibleCounter -= Time.deltaTime;

                if (invincibleCounter <= 0)
                {
                    theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
                }
            }
        }
     
       
    }
   

    public void TakeDamage(int damage)
    {
        if(invincibleCounter <= 0)
        {

            PV.RPC("PTD", RpcTarget.AllBuffered, damage);
            knockBackForce = damage;
            PlayerController.instance.KnockBack();
            PlayerController.instance.anim.SetTrigger("isHurt");
            if (currentHealth <= 0)
            {
                Debug.Log("Supposed to be dead!");
                Die();
            }
            else
            {
                invincibleCounter = invincibleLength;

                //unity handles color values as a float from 0-1
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);
            }
        }
        
       
    }
    [PunRPC]
    public void PTD(int damage)
    {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            Debug.Log(currentHealth + " player");     
    }
    void Die()
    {
        GameManager.instance.RespawnPlayer();
        Debug.Log("Player Died");
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }
    public void smoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
        }
    }
}
