using UnityEngine;

public abstract class BuffDebuff : MonoBehaviour
{
    protected BuffDebuffConfig data;
    protected Health health;
    protected PlayerMovement playerMovement;
    protected GameObject gameObject; // I don't know what all will be needed

    private bool effectEnded = false;
    private bool isApplied = false;
    protected int stacks = 1;

    private float elapsedTime = 0f;
    private float elapsedTickTime = 0f;

    public BuffDebuffConfig GetData() { return data; }
    public bool IsEffectEnded() { return effectEnded; }
    public void SetEffectEnded(bool value)
    {
        effectEnded = value;
    }
    public void SetIsApplied(bool value) { isApplied = value; }

    private void ResetElapsedTickTime()
    {
        elapsedTickTime = 0f;
    }
    
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
        stacks = 1;
    }

    /// <summary>
    /// Counting time of effect, starting function and killing this effect
    /// </summary>
    public void TimerEffect()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < data.Duration * stacks)
        {
            if (data.Frequency > 0)
            {
                elapsedTickTime += Time.deltaTime;

                if (elapsedTickTime >= data.Frequency)
                {
                    elapsedTickTime = 0f;
                    Functionality();
                    
                }
            }
            if (!isApplied)
            {
                Debug.Log($"Apply Effect {data.Name}");
                isApplied = true;
                Functionality();
            }
            
        }
        else
        {
            if (isApplied)
            {
                
                ReverseEffect();
                isApplied = false;
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
        data = null;
        elapsedTime = 0f;
        elapsedTickTime = 0f;
        stacks = 1;
    }

    /// <summary>
    /// if this effect is already on entity, stack will be added to multiply Duration and Damage
    /// </summary>
    public void AddStack() 
    {
        if (data.MaxStacks < stacks)
        {
            stacks++;

            if (isApplied)
            {
                isApplied = false;
            }
        }
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
    public abstract void ReverseEffect();
}