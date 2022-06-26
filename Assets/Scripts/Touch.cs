using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Sense
{
    string tankName= " ";

    public override void InitializeSense()
    {
      
    }

    public override void UpdateSense()
    {
        if (tankName.Equals("playerTank"))
        {
            Debug.Log("Player has been detected");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        tankName = other.name;
    }
    private void onTriggerExit(Collider other)
    {
        tankName =" ";
    }
}
