using UnityEngine;
using System.Collections;

public class gravball : MonoBehaviour
{

    public float suckPower = 1f;
    public float pushPower = 1f;
    public float maxDist = 10f;
    private bool suckscale = false;
    private bool pushscale = false;
    private Rigidbody2D player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        suckscale = false;
        if (Input.GetKey("o"))
        {
            suckscale = true;
        }
        pushscale = false;
        if (Input.GetKey("p"))
        {
            pushscale = true;
        }
    }

    void FixedUpdate()
    {
        if (suckscale)
        {
            Vector2 gravPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 suckV = -player.position + gravPos;
            float scale = Mathf.Max(suckV.magnitude, 1.0f);
            suckV.Normalize();
            if (scale < maxDist)
            {
                Debug.Log(scale);
                player.AddForce(suckPower * suckV);
            }
        }
        if (pushscale)
        {
            Vector2 gravPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 pushV = player.position - gravPos;
            float scale = Mathf.Max(pushV.magnitude, 1.0f);
            pushV.Normalize();
            if (scale < maxDist)
            {
                player.AddForce(pushPower * pushV );
            }
        }
       
    }
}
