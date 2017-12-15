using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Describes a type that can take damage.
interface IDamageable
{
    // Estimates the amount of health lost if damage is dealt. 
    float EstimatedDamageTaken(float damageDealt);

    // Applies damage to this object.
    void TakeDamage(float damageDealt);
}


public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float currentHealth;
    public float healthValue = 100;
    //public float armorValue = 20;
    public bool isDead = false;

    public Image Health;
    public Image damageImage;

    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    public GameObject healEffect;

    public float flashSpeed = 5f;
    bool damaged;

    public bool knockedBack;

    void Start()
    {
        currentHealth = healthValue;
        Health = GameObject.FindGameObjectWithTag("HP").GetComponent<Image>();
    }

    public float EstimatedDamageTaken(float damageDealt)
    {
        return damageDealt;
    }


    public void TakeDamage(float damageDealt)
    {
        healthValue -= damageDealt;

        damaged = true;



        if (Health.fillAmount <= 0)
        {
            Death();
        }
    }

    public void Update()
    {
        Health.fillAmount = healthValue / 100;
        if (isDead)
        {
            //Application.LoadLevel(Application.loadedLevel);
        }

        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
           //damageImage.color = flashColor;
        }
        else
        {
            // ... transition the colour back to clear.
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    void Death()
    {
        isDead = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Health")
        {
            if (healthValue < 100)
            {
                //healthValue += 10;
                //Destroy(Instantiate(healEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject, 2);
            }
        }
    }
}