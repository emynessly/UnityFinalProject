using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    private bool canShoot = true;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float delayShoot = 1f;
    private bool direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //direction = GameObject.Find("Player").GetComponent<MovementController>().flipX; надо доработать
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot & Input.GetKey(KeyCode.F))
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        canShoot = false;

        bool direction = GameObject.Find("Player").GetComponent<MovementController>().flipX;

        // создание пули
        GameObject b = Instantiate(
            bullet,
            transform.position + (direction ? Vector3.left : Vector3.right) * 0.5f,
            Quaternion.identity
        );

        b.GetComponent<BulletController>().SetDirection(direction);

        yield return new WaitForSeconds(delayShoot);
        canShoot = true;
    }
}
