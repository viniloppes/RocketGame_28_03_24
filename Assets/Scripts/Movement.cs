
using System.Timers;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public float aceleracaoPrincipal = 0;
    public Rigidbody rb;
    Timer timerTick = new Timer();
    //int toutParaAceleracaoSFX = 0;
    //int toutFimDesaceleracao = 0;
    //const int TOUT_DESACELERACAO_SFX = 2;
    private bool isAccelerating = false;
    private bool hasPlayedAcceleratingAudio = false;
    private const float stopAudioTime = 20f; // Segundo em que o áudio deve parar
    private int toutParaAceleracao = 0;

    [SerializeField] public AudioClip mainEngine;
    [SerializeField] public float rotationSpeed = 0;
    [SerializeField] public ParticleSystem RightParticle;
    [SerializeField] public ParticleSystem LeftParticle;
    [SerializeField] public ParticleSystem MainParticle;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        try
        {
            timerTick.Interval = 100;
            timerTick.Elapsed += timerTick_Elapsed;
            timerTick.Start();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }

    }


    // Update is called once per frame
    void Update()
    {
        AtualizaRotacao();
        AtualizaAceleracao();
    }
    private void AtualizaRotacao()
    {
        try
        {

            if (Input.GetKey(KeyCode.D))
            {
                TurnRight();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                TurnLeft();

            }
            else
            {
                LeftParticle.Stop();
                RightParticle.Stop();

            }
        }
        catch (System.Exception ex)
        {

            Debug.LogError(ex.Message);
        }
    }

    private void TurnLeft()
    {
        AplicarRotacao(rotationSpeed);
        if (!RightParticle.isPlaying)
        {
            RightParticle.Play();

        }
        LeftParticle.Stop();
    }

    private void TurnRight()
    {
        AplicarRotacao(-rotationSpeed);
        if (!LeftParticle.isPlaying)
        {
            LeftParticle.Play();

        }
        RightParticle.Stop();
    }

    private void AplicarRotacao(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;

    }
    void AtualizaAceleracao()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            IniciaPropulsao();
        }
        else
        {
                FinalizaPropulsao();
          
        }
    }

    private void FinalizaPropulsao()
    {
        isAccelerating = false;
        if (!hasPlayedAcceleratingAudio || audioSource.time < 1)
        {
            hasPlayedAcceleratingAudio = false;
            audioSource.Stop();
            MainParticle.Stop();
            return;
        }

        if (audioSource.isPlaying && audioSource.time < stopAudioTime)
        {
            audioSource.time = stopAudioTime;
        }
  
    }

    private void IniciaPropulsao()
    {
        rb.AddRelativeForce(Vector3.up * aceleracaoPrincipal * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.volume = 10;
            audioSource.time = 0;
            audioSource.clip = mainEngine;
            audioSource.Play();
            hasPlayedAcceleratingAudio = true;
            MainParticle.Play();
        }
        else if (audioSource.time > stopAudioTime)
        {
            audioSource.time = 0;

        }
        isAccelerating = true;
    }

    private void timerTick_Elapsed(object sender, ElapsedEventArgs e)
    {
        try
        {
            try
            {
                if (toutParaAceleracao > 0)
                {
                    Debug.Log("Entrou Timer" + toutParaAceleracao);
                    if (toutParaAceleracao % 2 == 0)
                    {
                        audioSource.volume -= 0.05f;
                    }
                    if (--toutParaAceleracao == 0)
                    {
                        hasPlayedAcceleratingAudio = false;
                        audioSource.Stop();
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
            //try
            //{
            //    if (toutParaAceleracaoSFX > 0)
            //    {
            //        if (--toutParaAceleracaoSFX == 0)
            //        {
            //            Debug.Log("Foguete em desaceleração");
            //            audioSource.time = 20;
            //            //audioSource.Play();
            //            toutFimDesaceleracao = 50;

            //        }
            //    }
            //}
            //catch (System.Exception ex)
            //{

            //}

            //try
            //{
            //    if (toutFimDesaceleracao > 0)
            //    {
            //        if (--toutFimDesaceleracao == 0)
            //        {
            //            InvokePump.Instance.Invoke(() =>
            //            {
            //                Debug.Log("Para motor");

            //                //do whatever you do on the main thread
            //                audioSource.Stop();
            //            });

            //        }
            //    }
            //}
            //catch (System.Exception ex)
            //{

            //}



        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }

    }

}
