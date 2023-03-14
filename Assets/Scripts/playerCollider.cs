using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    FlyRocket script;

    Rigidbody rd;

    public AudioClip crashSound;
    public AudioClip winSound;

    public ParticleSystem crashParticles;
    public ParticleSystem successParticles;
    public float timerToNextLevel = 2f;
    public bool won = false;

    public bool collisionsEnabled = true;

    void Start()
    {
      script = GetComponent<FlyRocket>();
      audioSource = GetComponent<AudioSource>();
      rd = GetComponent<Rigidbody>();
    }

    void DisableMoviments() {
      if (script.flightEnabled) {
        script.flightEnabled = false;
      }
    }

    void CrashRocket() {
      if(!collisionsEnabled) return;
      collisionsEnabled = false;
      rd.constraints = RigidbodyConstraints.None;
      DisableMoviments();

      audioSource.Stop();
      audioSource.PlayOneShot(crashSound, 1f);
      crashParticles.Play();
    }

    public void NextScene() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void onFinish() {
      if (!script.flightEnabled || !collisionsEnabled) return;
      collisionsEnabled = false;
      script.flightEnabled = false;
      won = true;
      DisableMoviments();
      audioSource.Stop();
      audioSource.PlayOneShot(winSound, 1f);
      successParticles.Play();

      Invoke("NextScene", timerToNextLevel);
    }

    void Restart() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision other) {
      if (!collisionsEnabled) return;

      switch (other.gameObject.tag) {
        case "finish":
          onFinish();
          break;
        case "init":
          break;
        default:
          CrashRocket();
          break;
      }
    }

    // Update is called once per frame
    void Update()
    {
        if (!script.flightEnabled && !won && Input.anyKeyDown) {
          Restart();
        }
    }
}
