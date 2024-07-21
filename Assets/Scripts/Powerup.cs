using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3;

    [SerializeField]
    private AudioClip _audioClip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();

            if (player == null) return;

            AudioSource.PlayClipAtPoint(_audioClip, new Vector3(0,0,-7));

            switch (transform.tag)
            {
                case "SpeedPowerup":
                    player.IncreaseBoost();
                    break;
                case "TripleShotPowerup":
                    player.ActivateTripleShot();
                    break;
                case "SheildPowerup":
                    player.ActivateSheild();
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
