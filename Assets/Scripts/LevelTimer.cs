using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public Text time;
    public int min =0;
    public int sec=0;
    public float totaltime;
    bool gamepause = false; 

    public int getMin()
    {
        return min;
    }

    public int getSec()
    {
        return sec;
    }
    // Start is called before the first frame update
    void Start()
    {
        time.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        if(min <= 99 && sec <= 59 && gamepause ==false)
        {
            totaltime += Time.deltaTime;
            min = (int)(totaltime / 60);
            sec = (int)(totaltime % 60);
        }
        time.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    public void changegamepause()
    {
        if(gamepause) {
            gamepause = false;
        }
        else
        {
            gamepause = true;
        }
    }

}
