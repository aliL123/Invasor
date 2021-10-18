using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarImage;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
        // Update is called once per frame
        void Update()
    {
        
    }
    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(player.GetComponent<PlayerController>().currentHealth / player.GetComponent<PlayerController>().maxHealth, 0, 1f);
    }
}
