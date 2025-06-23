using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    private Camera camera;
    private bool direction = false;
    public void SetDirection(bool dir)
    {
        direction = dir;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(bulletSpeed * (direction ? -1 : 1), 0, 0) * Time.deltaTime);

        Vector3 viewPos = camera.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0 || viewPos.x > 1)
        {
            Destroy(gameObject);
        }
    }
}
