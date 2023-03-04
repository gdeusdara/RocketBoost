using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    FlyRocket script;

    public AudioClip crashSound;
    public AudioClip winSound;
    public float timerToNextLevel = 2f;
    public bool won = false;

    void Start()
    {
      script = GetComponent<FlyRocket>();
      audioSource = GetComponent<AudioSource>();
    }

    void DisableMoviments() {
      if (script.enabled) {
        script.enabled = false;
      }
    }

    void CrashRocket() {
      DisableMoviments();

      audioSource.Stop();
      audioSource.PlayOneShot(crashSound);
    }

    void NextScene() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void onFinish() {
      if (!script.enabled) return;
  
      script.enabled = false;
      won = true;
      DisableMoviments();

      audioSource.Stop();
      audioSource.PlayOneShot(winSound);

      Invoke("NextScene", 1f);
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
        if (!script.enabled && !won && Input.anyKeyDown) {
          Restart();
        }
    }
}
