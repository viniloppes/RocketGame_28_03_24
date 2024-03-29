
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] public float toutFinaliza = 2f;
    [SerializeField] public AudioClip successAudio;
    [SerializeField] public AudioClip failureAudio;
    [SerializeField] public ParticleSystem successParticle;
    [SerializeField] public ParticleSystem failureParticle;
    private AudioSource audioSource;

    bool isTransitioning = false;
    bool isDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondDebugKeys();
    }

    private void RespondDebugKeys()
    {
        if(Input.GetKeyUp(KeyCode.L))
        {
            LoadNextLevel();
        } else if(Input.GetKeyUp(KeyCode.C))
        {
            isDisabled = !isDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning == true || isDisabled == true) return;
        switch (collision.gameObject.tag)
        {

            case "Fuel":
                Debug.Log("The rocket get the fuel");
                break;
            case "Friendly":
                Debug.Log("The rocket parked in a safe place");
                break;
            case "Finish":
           
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }


    }

    void StartSuccessSequence()
    {
     successParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), toutFinaliza);
    }

    void StartCrashSequence()
    {
        failureParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(failureAudio);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), toutFinaliza);

    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int indexNextLevel = currentSceneIndex + 1;
        if (indexNextLevel == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);

        }
        else
        {
            SceneManager.LoadScene(indexNextLevel);

        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
}
