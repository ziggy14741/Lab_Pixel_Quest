using System.Collections;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    private PlayerStats _playerStats;
    private Rigidbody2D _rigidbody2D;
    private GameObject _fadeObject;
    private Animator _fadeAnimator;
    private ParticleSystem _particleSystem;

    private AudioSource _deathSFX;
    private AudioSource _coinSFX;
    private AudioSource _checkPointSFX;
    private AudioSource _heartSFX;
    private AudioSource _bouncePadSFX;
    private AudioSource _enmyDeathSFX;

    private AudioSource _mainMusic;
    private AudioSource _powerUpMusic;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
        if (GameObject.Find("Canvas")){_fadeAnimator = _fadeObject.GetComponent<Animator>();  }
        if(GameObject.Find("Particle System"))
        {
            _particleSystem = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
            _particleSystem.enableEmission = false;
        }

        if (GameObject.Find("SFX"))
        {
            _coinSFX = GameObject.Find("CoinSFX").GetComponent<AudioSource>();
            _deathSFX = GameObject.Find("DeathSFX").GetComponent<AudioSource>();
            _checkPointSFX = GameObject.Find("CheckpointSFX").GetComponent<AudioSource>();
            _heartSFX = GameObject.Find("HeartSFX").GetComponent<AudioSource>();
            _bouncePadSFX = GameObject.Find("BouncePadSFX").GetComponent<AudioSource>();
            _enmyDeathSFX = GameObject.Find("EnemyDeathSFX").GetComponent<AudioSource>();
        }

        if (GameObject.Find("Music"))
        {
            _mainMusic = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
            _powerUpMusic = GameObject.Find("PowerUpMusic").GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin")){
            Destroy(collision.gameObject);
            _playerStats.coinCounter++;
            _playerStats.UpdateCoinText();
            _coinSFX.Play();
        }
        else if ((collision.CompareTag("Enemy") && !_playerStats.invurnrable) || collision.CompareTag("Death"))
        {
            StartCoroutine(WaitForFadeInAndOut());
            _playerStats.invurnrable = false;
            _deathSFX.Play();

        }
        else if (collision.CompareTag("Enemy") && _playerStats.invurnrable)
        {
            Destroy(collision.gameObject);
            _enmyDeathSFX.Play();
        }
        else if (collision.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(PowerUpTurnOff());
        }
        else if (collision.CompareTag("Life"))
        {
            Destroy(collision.gameObject);
            _playerStats.lives++;
            _playerStats.UpdateLivesText();
            _heartSFX.Play();
        }
        else if (collision.CompareTag("CheckPoint"))
        {
            if (_playerStats.respawnPoint.transform.position != collision.transform.Find("Point").transform.position)
            {
                _checkPointSFX.Play();
            }
            _playerStats.respawnPoint.transform.position = collision.transform.Find("Point").transform.position;
        }
        else if (collision.CompareTag("LevelGoal"))
        {
            _playerStats.Victory();
        }  
        else if (collision.CompareTag("BouncePad"))
        {
            _bouncePadSFX.Play();   
        }
    }

    private IEnumerator WaitForFadeInAndOut()
    {
        _playerStats.immoblie = true;
        _playerStats.invurnrable = true;
        _rigidbody2D.velocity = Vector2.zero;
        if (GameObject.Find("Canvas"))
        {
            _fadeObject.SetActive(true);
            _fadeAnimator.Play("FadeInAndOut");
        }
        yield return new WaitForSeconds(0.5f);
        transform.position = _playerStats.respawnPoint.transform.position;
        _playerStats.lives--;
        _playerStats.UpdateLivesText();
        if (GameObject.Find("Canvas")) { _fadeAnimator.Play("FadeInAndOut");}
        yield return new WaitForSeconds(1f);
        _playerStats.immoblie = false;
        _playerStats.invurnrable = false;
        if (GameObject.Find("Canvas")) { _fadeObject.SetActive(false);}
        if (_playerStats.lives == 0) { _playerStats.GameOver(); }
    }

    private IEnumerator PowerUpTurnOff()
    {
        if (GameObject.Find("Music"))
        {
            _mainMusic.Pause();
            _powerUpMusic.Play();
        }
        _playerStats.invurnrable = true;
        if(_particleSystem != null){ _particleSystem.enableEmission = true; }
        yield return new WaitForSeconds(5f);
        if (_particleSystem != null) { _particleSystem.enableEmission = false; }
        _playerStats.invurnrable = false;
        if (GameObject.Find("Music"))
        {
            _powerUpMusic.Stop();
            _mainMusic.UnPause(); // Resume playback.
        }

    }
}
