using System.Collections;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour {
        
    // =================================================================================================================
    // VARIABLES 
    // =================================================================================================================
    private PlayerStats _playerStats;          // Used to update player stats 
    private Rigidbody2D _rigidbody2D;          // Used to stop player movement 
    private GameObject _fadeObject;            // Object used to fade in and out 
    private Animator _fadeAnimator;            // 
    private ParticleSystem _particleSystem;    // The particle system that plays when player is invulnerable

    public float invulnerabilityLenght = 6f;   // How long the invulnerability will last for 
    
    // SFX
    public GameObject soundEffect;
    private AudioSource _deathSfx;             
    private AudioSource _coinSfx;
    private AudioSource _checkPointSfx;
    private AudioSource _heartSfx;
    private AudioSource _bouncePadSfx;
    private AudioSource _enemyDeathSfx;

    // Music 
    private AudioSource _mainMusic;
    private AudioSource _powerUpMusic;

    // =================================================================================================================
    // METHODS  
    // =================================================================================================================
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
        
        // Connects Animator 
        if (GameObject.Find("Fade")) { _fadeObject = GameObject.Find("Fade").gameObject; }
        if(_fadeObject != null){ _fadeAnimator = _fadeObject.GetComponent<Animator>();}
        
        // Connects Particle System and turns it off 
        if(GameObject.Find("Particle System"))
        {
            _particleSystem = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
            _particleSystem.Stop();
        }

        // Connects SFX and Music 
        if (GameObject.Find("CoinSFX")){ _coinSfx = GameObject.Find("CoinSFX").GetComponent<AudioSource>(); }
        if (GameObject.Find("DeathSFX")) { _deathSfx = GameObject.Find("DeathSFX").GetComponent<AudioSource>(); }
        if (GameObject.Find("CheckpointSFX")) { _checkPointSfx = GameObject.Find("CheckpointSFX").GetComponent<AudioSource>(); }
        if (GameObject.Find("HeartSFX")) { _heartSfx = GameObject.Find("HeartSFX").GetComponent<AudioSource>(); }
        if (GameObject.Find("BouncePadSFX")) { _bouncePadSfx = GameObject.Find("BouncePadSFX").GetComponent<AudioSource>(); }
        if (GameObject.Find("EnemyDeathSFX")) { _enemyDeathSfx = GameObject.Find("EnemyDeathSFX").GetComponent<AudioSource>(); }
        if (GameObject.Find("LevelMusic")) { _mainMusic = GameObject.Find("LevelMusic").GetComponent<AudioSource>(); }
        if (GameObject.Find("PowerUpMusic")) { _powerUpMusic = GameObject.Find("PowerUpMusic").GetComponent<AudioSource>(); }
    }

    // If player walks into a Trigger collider it check what it is and how the game should react to it 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player walks into a coin, they destroy it, update the coin counter and play the coin SFX 
        if (collision.CompareTag("Coin")){
            Destroy(collision.gameObject);
            _playerStats.coinCounter++;
            _playerStats.UpdateCoinText();
            if (_coinSfx != null) { SpawnAudioSource(_coinSfx); }
        }
        // If player walks int oan enemy or a death object while they are vulnerable they die and start the fade sequence 
        else if ((collision.CompareTag("Enemy") && !_playerStats.invulnerable) || collision.CompareTag("Death")) {
            StartCoroutine(WaitForFadeInAndOut());
            _playerStats.invulnerable = false;
            if (_deathSfx != null) { SpawnAudioSource(_deathSfx); }
        }
        // If the player walks into the enemy while invulnerable they kill the enemy and play kill SFX 
        else if (collision.CompareTag("Enemy") && _playerStats.invulnerable) {
            Destroy(collision.gameObject);
            if (_enemyDeathSfx != null) { SpawnAudioSource(_enemyDeathSfx); }
        }
        // If player walks into a power up, it gets used and start the power up and down sequence 
        else if (collision.CompareTag("PowerUp")) {
            StartCoroutine(PowerUpTurnOff(collision.gameObject));
        }
        // If the player collects a life they destroy the object, update value and play SFX
        else if (collision.CompareTag("Life")) {
            Destroy(collision.gameObject);
            _playerStats.lives++;
            _playerStats.UpdateLivesText();
            if (_heartSfx != null) { SpawnAudioSource(_heartSfx); }
        }
        // Change respawn point to the new location and if it's a different location play SFX 
        else if (collision.CompareTag("CheckPoint")) {
            // Checks if it's a different location if so play SFX 
            if (_playerStats.respawnPoint.transform.position != collision.transform.Find("Point").transform.position
                && _checkPointSfx != null) {
                SpawnAudioSource(_checkPointSfx);
            }
            // Update the location of respawn point 
            _playerStats.respawnPoint.transform.position = collision.transform.Find("Point").transform.position;
        }
        // Set game to Win state 
        else if (collision.CompareTag("LevelGoal")) {
            _playerStats.Victory();
        }  
        // Plays SFX 
        else if (collision.CompareTag("BouncePad")) {
            if (_bouncePadSfx != null) { SpawnAudioSource(_bouncePadSfx); }
        }
    }

    // Spawns a new Sound Effect that will be destroyed after it's played
    private void SpawnAudioSource(AudioSource audioSource) {
        GameObject newSoundEffect = Instantiate(soundEffect, transform.position, Quaternion.identity);
        newSoundEffect.GetComponent<SoundEffectDeath>().SetAudio(audioSource);
    }

    private IEnumerator WaitForFadeInAndOut()
    {
        // Makes player unable to move, be hurt, and zeroes out any velocity the player had before 
        _playerStats.immobile = true;
        _playerStats.invulnerable = true;
        _rigidbody2D.velocity = Vector2.zero;
        
        // Plays the animation 
        if (_fadeObject != null)
        {
            _fadeObject.SetActive(true);
            _fadeAnimator.Play("FadeInAndOut");
        }
        
        // Waits till the screen is black 
        yield return new WaitForSeconds(0.5f);
        
        // Move the player, and update the player life count 
        transform.position = _playerStats.respawnPoint.transform.position;
        _playerStats.lives--;
        _playerStats.UpdateLivesText();
        
        if (_fadeObject != null) { _fadeAnimator.Play("FadeInAndOut");}
        yield return new WaitForSeconds(1f);
        
        if (_fadeObject != null) { _fadeObject.SetActive(false);}

        // If player lost all lives end the game
        if (_playerStats.lives == 0) {
            _playerStats.GameOver();
        }
        // Otherwise allow player to move again 
        else {
            _playerStats.immobile = false;
            _playerStats.invulnerable = false;
        }
    }

    // Turns on and off the power up for the player 
    private IEnumerator PowerUpTurnOff(GameObject powerUp) {
        
        // Turns off the power up object
        powerUp.SetActive(false);
        
        // Pauses main music and starts up powered up music 
        if (_mainMusic != null) { _mainMusic.Pause(); }
        if (_powerUpMusic != null){ _powerUpMusic.Play(); }

        // Turns off the particle effects and makes the player invulnerable
        if(_particleSystem != null){ _particleSystem.Play();}
        _playerStats.invulnerable = true;
        
        // How long the invulnerability lasts 
        yield return new WaitForSeconds(invulnerabilityLenght);
        
        // Turns off the particle effects and makes the player vulnerable
        if (_particleSystem != null) {
            _particleSystem.Stop();
            _particleSystem.Clear();
        }
        _playerStats.invulnerable = false;
        
        // Stops the power up music and unpause main music 
        if (_mainMusic != null) { _mainMusic.UnPause(); }
        if (_powerUpMusic != null) { _powerUpMusic.Stop(); }
        
        // Turns on the power up object 
        powerUp.SetActive(true);
    }
}
