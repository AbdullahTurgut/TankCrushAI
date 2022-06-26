using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Burdaki sense Isense'den türetilme olucak
public abstract class Sense : MonoBehaviour,ISense
{
    // ama bu fonksiyonlarý burda doldurmuycaz yavru class'larda doldurucaz
    //fakat bu þekilde de býrakamayýz o yüzden bu script'tide abstract yapýcaz
    public abstract void InitializeSense();

    public abstract void UpdateSense();

    
    //aþaðýda start ve update kýsýmlarýna 'da fonksiyonlarý ekledikten sonra artýk diðer classlardan
    //doldurmamýz gereken kýsým kaldý 
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
