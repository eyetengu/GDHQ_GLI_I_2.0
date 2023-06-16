using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelScript : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    

    public void TriggerBarrelExplosion()
    {       
        if(this.gameObject.activeSelf)
             StartCoroutine(BarrelExplosion());

        var colliders = Physics.OverlapSphere(transform.position, 2);
        foreach (var collider in colliders)
        {
            EnemyControl enemyScript = collider.GetComponent<EnemyControl>();
            Debug.Log("Present And Accounted For");
            if(enemyScript != null)
                enemyScript.SwitchAnimation(4);
        }
    }

    IEnumerator BarrelExplosion()
    {
        _explosion.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        _explosion.SetActive(false);
        gameObject.SetActive(false);
    }
}
