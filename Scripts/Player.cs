using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 800f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShootPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShootActive = false;
    [SerializeField]
    private bool _isSpeedActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    GameObject _shieldVisual;
    [SerializeField]
    private int _score = 0;

    private UI_Manager _uiManager;
    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        
        if (_spawnManager == null)
        {
            _spawnManager.isDead();
            Debug.LogError("Spawn manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovment();

        if (Input.GetKeyDown(KeyCode.Space)&&Time.time>_canFire)
        {
            Shoot();
        }

    }

    void CalculateMovment()
    {
        float horizentalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");
        Vector3 place = new Vector3(horizentalinput, verticalinput, 0);

        transform.Translate(place * _speed * Time.deltaTime);

        float horizentallimit = Mathf.Clamp(transform.position.y, -5.8f, 0);
        transform.position = new Vector3(transform.position.x, horizentallimit, 0);
        
        if (transform.position.x >= 12.8f)
        {
            transform.position = new Vector3(-14.1f, transform.position.y, 0);
        }
        else if (transform.position.x <= -14.1f)
        {
            transform.position = new Vector3(12.8f, transform.position.y, 0);
        }
    }

    void Shoot()
    {
        _canFire = Time.time + _fireRate;

        if(_isTripleShootActive)
        {
            Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);
        }

        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
        }
        
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisual.SetActive(false);
            return;
        }
            _lives--;
        _uiManager.UpdateLives(_lives);
            if (_lives < 1)
            {
                _spawnManager.isDead();
                Destroy(this.gameObject);
            }
    }

    public void TripleShotActive()
    {
        _isTripleShootActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        _speed = 2 * _speed;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisual.SetActive(true);
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShootActive = false;
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedActive = false;
        _speed = _speed / 2;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
