using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolArea : MonoBehaviour
{

    [SerializeField] private Bot bot;
    private void OnTriggerEnter(Collider other)
    {   
        if(other.CompareTag(GlobalConstants.Tag.CHARACTER)){
            Bot target=CacheCollider<Bot>.GetCollider(other);
            bot.TrackEnemy(target);
        }
    }

}
