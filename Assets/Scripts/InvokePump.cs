
using UnityEngine;

public class InvokePump : MonoBehaviour
{
    #region Singleton Interface
    //implement this however you please, I personally do this different, this is here for example only
    private static InvokePump _instance;
    public static InvokePump Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    private object _lock = new object();
    private System.Action _callbacks;

    public void Invoke(System.Action callback)
    {
        lock (_lock)
        {
            _callbacks += callback;
        }
    }

    private void Update()
    {
        //sure, this updates constantly, again, for demo purposes... implement the actual update hook however you please


        //here we pull the delegates out, incase the callback calls 'Invoke'
        //if Invoke is called, the lock would create a deadlock, and freeze the game
        System.Action a = null;
        lock (_lock)
        {
            if (_callbacks != null)
            {
                a = _callbacks;
                _callbacks = null;
            }
        }
        if (a != null) a();
    }
}
