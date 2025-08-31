using UnityEngine;
using UnityEngine.UI;

public class SuccessCounter : MonoBehaviour
{
    [SerializeField] private Text successCounter;
    [SerializeField] private Text perfectCounter;
    [SerializeField] private Text failedCounter;

    public int success, failed, perfect;

    public void addSuccess()
    {
        success++;
        successCounter.text = $"sukses = {success}";
    }
    public void addPerfect()
    {
        perfect++;
        perfectCounter.text = $"perfect = {perfect}";
    }

    public void addFailed()
    {
        failed++;
        failedCounter.text = $"gagal = {failed}";
    }
}
