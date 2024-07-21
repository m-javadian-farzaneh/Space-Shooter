using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4f;

    private Player _player;


    private Animator _anim;


    private AudioSource _enemyAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _enemyAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // move down the enemy with the speed of 4
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -4)
        {
            float randomX = UnityEngine.Random.Range(-9, 9);
            transform.position = new Vector3(randomX, 7, 0);
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Laser")
        {
            // Destroy Enemy and laser
            _player.IncreaseScore(10);
            Destroy(other.gameObject);
            DestroyEnemyItself();
        }
        else if (other.tag == "Player")
        {
            _player.Damage();
            DestroyEnemyItself();
        }
    }

    private void DestroyEnemyItself()
    {
        _speed = 0;
        _anim.SetTrigger("OnEnemyDeath");
        _enemyAudioSource.Play();
        Destroy(this.gameObject, 0.5f);
    }
}
