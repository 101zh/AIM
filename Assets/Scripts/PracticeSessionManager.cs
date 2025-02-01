using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PracticeSessionManager : MonoBehaviour
{

    public static PracticeSessionManager instance;
    [SerializeField] int numToShoot = 25;
    [SerializeField] int numShot = 0;

    [SerializeField] private GameObject target;

    [SerializeField] private Transform topRight;
    [SerializeField] private Transform bottomLeft;

    private List<float> shotDelays = new List<float>();
    private List<int> missFires = new List<int>();
    private List<float> overShootValues = new List<float>();
    private List<float> timeToKillTarget = new List<float>();

    public delegate void PracticeSessionEvent();
    public PracticeSessionEvent onTargetInView;

    private void Awake()
    {
        instance = this;
    }

    public void createNewTarget()
    {
        numShot++;

        if (numShot >= numToShoot)
        {
            GameManager.instance.endPracticeSession();
            return;
        }
        Vector3 position = new Vector3(Random.Range(bottomLeft.position.x, topRight.position.x), Random.Range(bottomLeft.position.y, topRight.position.y), Random.Range(bottomLeft.position.z, topRight.position.z));

        Instantiate(target, position, target.transform.rotation);
    }

    public void addNewTimeToKill(float hitTime)
    {
        timeToKillTarget.Add(hitTime);
    }

    public void addNewShotDelay(float delay)
    {
        shotDelays.Add(delay);
    }

    public void addNewMissCount(int missFireCount)
    {
        missFires.Add(missFireCount);
    }

    public void addNewOverShootValue(float overshoot)
    {
        overShootValues.Add(overshoot);
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

    public void addNewPraticeDataSet(PracticeStats newPracticeStatsSet)
    {
        addNewTimeToKill(newPracticeStatsSet.timeToKillTarget);
        addNewShotDelay(newPracticeStatsSet.shotDelay);
        addNewMissCount((int)newPracticeStatsSet.missFires);
        addNewOverShootValue(newPracticeStatsSet.overShoot);
    }

    public PracticeStats GetAveragePracticeStats()
    {
        return new PracticeStats(getAverageShotDelay(), getAverageMisses(), getAverageOverShoot(), getAverageTimeToKillTarget());
    }
}

public class PracticeStats
{
    public float shotDelay { get; private set; }
    public float missFires { get; private set; }
    public float overShoot { get; private set; }
    public float timeToKillTarget { get; private set; }

    public PracticeStats(float shotDelay, float missFires, float overShoot, float timeToKillTarget)
    {
        this.shotDelay = shotDelay;
        this.missFires = missFires;
        this.overShoot = overShoot;
        this.timeToKillTarget = timeToKillTarget;
    }
}
