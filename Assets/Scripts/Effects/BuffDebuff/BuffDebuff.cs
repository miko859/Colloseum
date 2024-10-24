using UnityEngine;

public abstract class BuffDebuff : MonoBehaviour
{
    protected BuffDebuffConfig data;
    protected Health health;
    protected PlayerMovement playerMovement;
    protected GameObject gameObject; // I don't what all will be needed

    private bool effectEnded = false;

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
    }

    /// <summary>
    /// Counting time of effect, starting function and killing this effect
    /// </summary>
    public void TimerEffect()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < data.Duration)
        {
            elapsedTickTime += Time.deltaTime;

            if (elapsedTickTime >= data.Frequency)
            {
                Functionality();
                elapsedTickTime = 0f;
            }
        }
        else
        {
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
}