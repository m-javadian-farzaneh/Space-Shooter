using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _triplePowerup;

    [SerializeField]
    private GameObject _speedPoweup;

    [SerializeField]
    private GameObject _sheildPowerup;

    [SerializeField]
    private GameObject _asteriodPrefab;

    [SerializeField]
    private bool _continueSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        ChageSpawningCondition();
        StartCoroutine(SpawEnemyRoutine());
        StartCoroutine(SpawnTriplePowerupRoutine());
        StartCoroutine(SpawnSpeedPowerupRoutine());
        StartCoroutine(SpawnSheildPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawEnemyRoutine()
    {
        while (_continueSpawning == true)
        {
            Vector3 enemyPose = new Vector3(Random.Range(-9, 9), 9, 0);
            GameObject temp = Random.Range(0, 2) == 0 ? _enemyPrefab : _asteriodPrefab;
            GameObject enemyInstance = Instantiate(temp, enemyPose, Quaternion.identity);
            enemyInstance.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator SpawnTriplePowerupRoutine()
    {
        while (_continueSpawning == true)
        {
            Vector3 triplePowerupPose = new Vector3(Random.Range(-9, 9), 9, 0);
            Instantiate(_triplePowerup, triplePowerupPose, Quaternion.identity);
            yield return new WaitForSeconds(8.0f);
        }
    }


    IEnumerator SpawnSpeedPowerupRoutine()
    {
        while (_continueSpawning == true)
        {
            Vector3 SpeedPowerupPose = new Vector3(Random.Range(-9, 9), 10, 0);
            Instantiate(_speedPoweup, SpeedPowerupPose, Quaternion.identity);
            yield return new WaitForSeconds(15.0f);
        }
    }


    IEnumerator SpawnSheildPowerupRoutine()
    {
        while (_continueSpawning == true)
        {
            Vector3 PowerupPose = new Vector3(Random.Range(-9, 9), 8, 0);
            Instantiate(_sheildPowerup, PowerupPose, Quaternion.identity);
            yield return new WaitForSeconds(12.0f);
        }
    }

    public void ChageSpawningCondition()
    {
        _continueSpawning = !_continueSpawning;
        Debug.Log(_continueSpawning);
    }

    public bool SpawningStatus()
    {
        return _continueSpawning;
    }
}
