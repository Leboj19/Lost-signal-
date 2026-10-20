using UnityEngine;
using UnityEngine.Events;

public class WireRepairInteractable : MonoBehaviour, IInteractable
{
    [Header("Mini-Game")]
    public WireRepairMinigame minigame;

    [Header("Screen Visuals")]
    public GameObject brokenScreenVisual;
    public GameObject repairedScreenVisual;



    [Header("Time Reward")]
    public UnityEvent<float> onTimeReward;
    public float rewardSeconds = 60f;

    [Header("Repair Audio")]
    public AudioSource repairAudio;
    public AudioClip repairCompleteSound;

    private bool repaired = false;


    public void Interact()
    {
        if (repaired)
            return;

        if( minigame != null)
        {
            minigame.Open(this);
        }
        else
        {
            Debug.LogWarning("Wire repair mini-game is nit assigned.", this);
        }
    }

    public void RepairCompleted()
    {
        if (repaired)
            return;
        repaired = true;
        
        if (brokenScreenVisual != null)
            brokenScreenVisual.SetActive(false);

        if (repairedScreenVisual != null)
            repairedScreenVisual.SetActive(true);

        if (repairAudio != null && repairCompleteSound != null)
            repairAudio.PlayOneShot(repairCompleteSound);

        onTimeReward.Invoke(rewardSeconds);

        Debug.Log("Screen repaired! Added " +
           rewardSeconds + " seconds.");
    }
}
