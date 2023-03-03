using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    FlyRocket script;

    public AudioClip crashSound;
    public AudioClip winSound;
    public float timerToNextLevel = 2;

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

      audioSource.PlayOneShot(crashSound);
    }

    void NextScene() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void onFinish() {
      DisableMoviments();
      audioSource.PlayOneShot(winSound);

      Invoke("NextScene", timerToNextLevel);
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
        
    }
}
