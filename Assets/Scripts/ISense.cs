using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bu bir class olmad��� i�in monobehavior'dan �ekmedik bir�ey
public interface ISense 
{
    //ve yaz�ca��m�z fonksiyonlar concrad oldu�u i�in body'e sahip olamazlar

    //her hangi bir class bu aray�z� uygularsa a�a��daki fonksiyonlar� doldurmak zorunda
    void InitializeSense();
    void UpdateSense();

}
