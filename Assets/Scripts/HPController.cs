using System.Collections;
using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] private int HP = 3;
    private int currentHP;
    private Coroutine runable;
    [SerializeField] private GameObject prefabHP;

    private void initHP()
    {
        currentHP = HP;

        float offsetStep = 0.5f;
        float startX = -offsetStep * (HP - 1) / 2f;

        for (int i = 0; i < HP; i++)
        {
            GameObject hp = Instantiate(prefabHP, transform);
            hp.transform.localPosition = new Vector3(startX + i * offsetStep, 0.7f, 0);
        }
    }

    private void clearHP()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i) != this.transform)
                Destroy(transform.GetChild(i).gameObject);
        }
    }

    void Start()
    {
        initHP();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DeadZone")
        {
            DieAndRespawn();
            return;
        }

        if ((collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy")) && runable == null && currentHP > 0)
        {
            int lastHpIndex = transform.childCount - 1;

            GameObject lastHP = transform.GetChild(lastHpIndex).gameObject;

            HPAnimationController anim = lastHP.GetComponent<HPAnimationController>();
            if (anim != null)
            {
                anim.DestroyAnim();
            }

            runable = StartCoroutine(destroyHp(lastHP));
            Debug.Log("HP lost. Current HP: " + currentHP);
        }
    }

    IEnumerator destroyHp(GameObject obj)
    {
        currentHP--;
        yield return new WaitForSeconds(0.6f);
        Destroy(obj);
        runable = null;

        if (currentHP <= 0)
        {
            DieAndRespawn();
        }
    }

    private void DieAndRespawn()
    {
        Debug.Log("You died. Respawning");
        clearHP();
        transform.position = GetComponent<PlayerCheckpointController>().lastCheckpoint.transform.position + Vector3.up;
        initHP();
        runable = null;
    }
}
