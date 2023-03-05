using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    FlyRocket script;

    public AudioClip crashSound;
    public AudioClip winSound;

    public ParticleSystem crashParticles;
    public ParticleSystem successParticles;
    public float timerToNextLevel = 2f;
    public bool won = false;

    void Start()
    {
      script = GetComponent<FlyRocket>();
      audioSource = GetComponent<AudioSource>();
    }

    void DisableMoviments() {
      if (script.flightEnabled) {
        script.flightEnabled = false;
      }
    }

    void CrashRocket() {
      DisableMoviments();

      audioSource.Stop();
      audioSource.PlayOneShot(crashSound);
      crashParticles.Play();
    }

    void NextScene() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void onFinish() {
      if (!script.flightEnabled) return;
  
      script.flightEnabled = false;
      won = true;
      DisableMoviments();

      audioSource.Stop();
      audioSource.PlayOneShot(winSound);
      successParticles.Play();

      Invoke("NextScene", timerToNextLevel);
    }

    void Restart() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision other) {
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
