using UnityEngine;

public class Asteriod : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 50.0f;

    [SerializeField]
    private float _speed = 3f;

    private Player _player;


    [SerializeField]
    private GameObject _asteriodExplosion;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime, Space.World);
        // move down the enemy with the speed of 4
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -4)
        {
            float randomX = Random.Range(-9, 9);
            transform.position = new Vector3(randomX, 7, 0);
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_asteriodExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            other.transform.GetComponent<Player>();
            _player.Damage();
            Instantiate(_asteriodExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
