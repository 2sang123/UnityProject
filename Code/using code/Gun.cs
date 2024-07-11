using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire = 1f;

    [SerializeField] Transform gunPoint;    //This is optional. Watch the video fore more information.


    private void Start()
    {
        if(gunPoint == null)
            gunPoint = GetComponentInChildren<GunPoint>().transform;
    }
    public float GetRateOfFire()
    {
        return rateOfFire;   
    }

    public void Fire()
    {
       GameObject bullet =  Instantiate(projectile, gunPoint.position, transform.rotation);
        //you can use transform.position instead of gunPoint.position
        //if this script is attached directly to a gunpoint
        Destroy(bullet, 2f);
    }

}
