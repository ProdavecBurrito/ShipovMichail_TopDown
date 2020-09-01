using UnityEngine;

public class Timer 
{

    private float _currentTime;
    private float _timeToWait;

    public void InitTimer(float timeToWait)
    {
        _currentTime = 0;
        _timeToWait = timeToWait;
    }

    public void UpdateTimer()
    {
        _currentTime += Time.deltaTime;
    }

    public void NullifyTimer()
    {
        _currentTime = 0;
    }

    public bool IsCanAct()
    {
        return _currentTime <= _timeToWait;
    }
}
