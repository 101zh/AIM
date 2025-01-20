using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PracticeSessionManager : MonoBehaviour
{

    public static PracticeSessionManager instance;

    [SerializeField] private GameObject target;

    private List<float> shotDelays = new List<float>();
    private List<int> missFires = new List<int>();
    private List<float> overShootValues = new List<float>();
    private List<float> timeToKillTarget = new List<float>();

    public delegate void PracticeSessionEvent();
    public PracticeSessionEvent onTargetInView;

    private void Awake()
    {
        instance = this;
        createNewTarget();
    }

    public void createNewTarget()
    {
        Instantiate(target, new Vector3(Random.Range(0.5f, 2f), Random.Range(0.5f, 2f), 9), target.transform.rotation);
    }

    public void addNewHitTime(float hitTime)
    {
        timeToKillTarget.Add(hitTime);
    }

    public void addNewMissCount(int missFireCount)
    {
        missFires.Add(missFireCount);
    }

    public void addNewOverShootValue(float overshoot)
    {
        overShootValues.Add(overshoot);
    }

    public void addNewShotDelay(float delay)
    {
        shotDelays.Add(delay);
    }

    public float getAverageTimeToKillTarget()
    {
        return timeToKillTarget.Sum() / timeToKillTarget.Count();
    }

    public float getAverageShotDelay()
    {
        return shotDelays.Sum() / shotDelays.Count;
    }

    public float getAverageMisses()
    {
        return (float)missFires.Sum() / missFires.Count;
    }

    public float getAverageOverShoot()
    {
        return overShootValues.Sum() / overShootValues.Count;
    }
}
