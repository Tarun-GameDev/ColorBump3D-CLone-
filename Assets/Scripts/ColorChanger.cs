using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] Material[] _mats;
    [SerializeField] LayerMask collideWithMask;

    void Update()
    {
        collisiionDetection();
    }

    void collisiionDetection()
    {
        //better collision
        Collider[] collider = Physics.OverlapBox(transform.position, new Vector3(5.5f, 1.5f, .2f),Quaternion.identity,collideWithMask);
        //Collider[] collider = Physics.OverlapSphere(transform.position, detectingRadius, collideWithBulletMask);
        foreach (Collider nearbyObject in collider)
        {
            Player _player = nearbyObject.GetComponent<Player>();

            if(_player != null)
            {
                _player.ChangeEnemy(_mats[1]);
            }

            if(nearbyObject.CompareTag("Enemy"))
            {
                nearbyObject.gameObject.GetComponent<Renderer>().material = _mats[0];
                nearbyObject.gameObject.tag = "Enemy1";
            }
            if(nearbyObject.CompareTag("Enemy1"))
            {
                nearbyObject.gameObject.GetComponent<Renderer>().material = _mats[1];
                nearbyObject.gameObject.tag = "Enemy";
            }
        }
    }
}
