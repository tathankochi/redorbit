using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            Invoke("stop", 36.0f);
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }

    public void start()
    {
        hasStarted = true;
    }

    public void stop()
    {
        hasStarted = false;
    }
}
