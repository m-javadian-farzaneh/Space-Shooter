using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

   private float _horizontal_input = 0;
   private float _vertical_input = 0;
   [SerializeField]
   private float _speed = 4f;
   // Start is called before the first frame update

   [SerializeField]
   private GameObject _laser;

   [SerializeField]
   private GameObject _tripleLaser;

   [SerializeField]
   private GameObject _sheild;

   [SerializeField]
   private float _fireRate = 0.25f;

   [SerializeField]
   private float _canFire = -1f;

   [SerializeField]
   private int _lives = 3;

   // Powerup controller on player

   [SerializeField]
   private bool _isTripleShotActive = false;

   [SerializeField]
   private int _boost = 1;

   [SerializeField]
   private bool _isSheildActive = false;


   [SerializeField]
   private int _score = 0;

   [SerializeField]
   private GameObject[] _damagedEngines;

   private SpawnManager _spawnManager;
   private UI_Manager _ui_manager;

   private AudioSource _audioSource;

   private Gamepad _gamepad;

   void Start()
   {
      _gamepad = Gamepad.current;
      transform.position = new Vector3(0, 0, 0);
      _sheild.SetActive(false);

      for (int i = 0; i < 2; i++)
      {
         _damagedEngines[i].SetActive(false);
      }

      _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
      _ui_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

      _audioSource = GetComponent<AudioSource>();
   }

   void CheckBoundaries()
   {
      float user_position_x = transform.position.x;
      float user_position_y = transform.position.y;
      float user_position_z = transform.position.z;

      transform.position = new Vector3(Mathf.Clamp(user_position_x, -9, 9),
         Mathf.Clamp(user_position_y, -4, 0)
         , user_position_z);
   }

   void MovePlayer()
   {
       Vector2 move = _gamepad.leftStick.ReadValue();
      _horizontal_input = move.x;
      _vertical_input = move.y;
   

      Vector3 direction = new Vector3(_horizontal_input, _vertical_input, 0);
      transform.Translate(direction * _speed * _boost * Time.deltaTime);
   }

   public void FireLaser()
   {
      if (Time.time > _canFire)
      {
         _canFire = Time.time + _fireRate;
         if (!_isTripleShotActive)
            Instantiate(_laser, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
         else
            Instantiate(_tripleLaser, transform.position + new Vector3(-1.76f, 0.5f, 0), Quaternion.identity);
      }
      // play the laser audio
      _audioSource.Play();
      
   }

   // Update is called once per frame
   void Update()
   {
      MovePlayer();
      CheckBoundaries();
   }

   public void Damage()
   {
      if (_isSheildActive)
      {
         _isSheildActive = false;
         _sheild.SetActive(false);
         return;
      }
      _lives--;

      _ui_manager.ChangeDamageImg(_lives);

      if (_lives <= 0)
      {
         _spawnManager.ChageSpawningCondition();
         _ui_manager.ShowGameOver(_score);

         Destroy(this.gameObject);
      }
      else if (_lives <= 2)
      {
         _damagedEngines[_lives - 1].SetActive(true);
      }
   }

   public void IncreaseScore(int Score)
   {
      _score = _score + Score;

      _ui_manager.ChangeScoreText(_score);
   }

   // Powerups Activation/Deactivation Properties
   public void ActivateTripleShot()
   {
      _isTripleShotActive = true;
      StartCoroutine(DeactivateTripleShot());
   }

   IEnumerator DeactivateTripleShot()
   {
      yield return new WaitForSeconds(5.0f);
      _isTripleShotActive = false;
   }

   public void ActivateSheild()
   {
      _isSheildActive = true;
      _sheild.SetActive(true);
      StartCoroutine(DeactivateSheild());
   }

   IEnumerator DeactivateSheild()
   {
      yield return new WaitForSeconds(5.0f);
      _sheild.SetActive(false);
      _isSheildActive = false;
   }



   public void IncreaseBoost()
   {
      _boost = 2;
      StartCoroutine(DecreaseBoost());
   }

   IEnumerator DecreaseBoost()
   {
      yield return new WaitForSeconds(5.0f);
      _boost = 1;
   }
}
