
using UnityEngine;

public class Oscilacao : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    float movementFactor;

    [SerializeField] float period = 2f;
    void Start()
    {
        startingPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;
        //Debug.Log(cycles);
        const float tau = Mathf.PI * 2; // Valor constante de 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos +  offset;
    }
}
