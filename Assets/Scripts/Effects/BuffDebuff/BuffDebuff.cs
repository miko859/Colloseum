using UnityEngine;

public abstract class BuffDebuff : MonoBehaviour
{
    protected BuffDebuffConfig data;
    protected Health health;
    protected PlayerMovement playerMovement;
    protected GameObject gameObject; // I don't know what all will be needed

    public bool effectEnded = false;
    private bool isApplied = false;

    private float elapsedTime = 0f;
    private float elapsedTickTime = 0f;

    public BuffDebuffConfig GetData() {  return data; }
    public bool IsEffectEnded() { return effectEnded; }

    /// <summary>
    /// Add needed data for effect
    /// </summary>
    /// <param name="config"></param>
    protected void Initialize(BuffDebuffConfig config)
    {
        data = config;
        effectEnded = false;
        elapsedTime = 0f;
        elapsedTickTime = 0f;
    }

    /// <summary>
    /// Counting time of effect, starting function and killing this effect
    /// </summary>
    /// 
    
    public void TimerEffect()
    {
        elapsedTime += Time.deltaTime;
        Debug.Log($"zaèiatok TimerEffect, elapsedTime: {elapsedTime}");
        if (elapsedTime < data.Duration)
        {
            Debug.Log($"ElapsedTime {elapsedTime} < data.Duration {data.Duration}");

            if (data.Frequency > 0)
            {
                Debug.Log($"{data.Frequency} data.Frequency, prešlo na kotroli ticku");
                elapsedTickTime += Time.deltaTime;

                if (elapsedTickTime >= data.Frequency)
                {
                    Debug.Log("elapsedTickTime prešlo data.frequency");
                    Debug.Log("Apply timer effect");
                    Functionality();
                    elapsedTickTime = 0f;
                }
            }
            else if (!isApplied)
            {
                Debug.Log("Apply effect");
                Functionality();
                isApplied = true;

            }
            
        }
        else
        {
            Debug.Log($"MAŽEM EFFECT {elapsedTime}");
            if (data.Frequency <= 0)
            {

            }

            DestroyObject();
        }
    }

    /// <summary>
    /// Set boolean 'true' to remove effect from entity
    /// </summary>
    private void DestroyObject()
    {
        effectEnded = true;
    }

    /// <summary>
    /// Create this effect with data
    /// </summary>
    /// <param name="entity"></param>
    public abstract void CreateObject(GameObject entity);

    /// <summary>
    /// Function of this effect
    /// </summary>
    public abstract void Functionality();

    /// <summary>
    /// Function to reverse changes of effect
    /// </summary>
    public virtual void ReverseEffect() { }
}