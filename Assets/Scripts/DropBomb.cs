using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float force;

    [SerializeField]
    private float impactRange;

    [SerializeField]
    private LayerMask layerToHit;

    private Animator anim;

    private void Start()
    {
        StartCoroutine(Explode());
        anim = GetComponent<Animator>();
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.5f);

        anim.SetTrigger("Explose");

        yield return new WaitForSeconds(1);

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactRange, layerToHit);
        foreach (Collider2D obj in objects)
        {
            if (obj.tag == "Player")
            {
                MyPlayer.MyInstance.Stun();
            }
            if (obj.tag == "Enemy")
            {
                obj.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        Destroy(gameObject, 1.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRange);
    }
}
