using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public Text healthText;
    private float health = 100f;
    Transform camTransform;
    public Transform ui;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (ui.position - camTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection); //Burda Bakma y�n�n� verdik ve a��y� kendisi ayarlad�
        //ui.rotation = lookRotation;
        //Biraz daha yava� animasyon �eklinde olmas�n� istersek �u �ekilde kodu de�i�tiriyoruz
        ui.rotation = Quaternion.Lerp(ui.rotation, lookRotation, Time.deltaTime * 10f);
    }

    public void TakeDamage(int amount)
    {
        if (health < amount) return;
        StartCoroutine(TakeDamageSmoothly(amount));

    }
    private IEnumerator TakeDamageSmoothly(int amount)
    {
        //Azalan can� braz daha animasyon �eklinde yava� bir �ekilde azalmas�n� sa�lamak i�in
        for(int i = 0; i < amount; i++){
            health--;
            healthText.text = health.ToString();
            healthBar.fillAmount = health / 100f;//maximum hali 1 oldu�u i�in health/100 ile 1 ile e�itledik
            yield return null;
        }
       
    }
}
