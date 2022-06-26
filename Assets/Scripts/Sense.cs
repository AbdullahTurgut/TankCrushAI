using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Burdaki sense Isense'den t�retilme olucak
public abstract class Sense : MonoBehaviour,ISense
{
    // ama bu fonksiyonlar� burda doldurmuycaz yavru class'larda doldurucaz
    //fakat bu �ekilde de b�rakamay�z o y�zden bu script'tide abstract yap�caz
    public abstract void InitializeSense();

    public abstract void UpdateSense();

    
    //a�a��da start ve update k�s�mlar�na 'da fonksiyonlar� ekledikten sonra art�k di�er classlardan
    //doldurmam�z gereken k�s�m kald� 
    void Start()
    {
        InitializeSense();   
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSense();
    }
}
