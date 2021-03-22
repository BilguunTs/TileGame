using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Game unit per seconds")]
    [SerializeField] float scrollRate = 0.3f;
 
    // Update is called once per frame
    void Update()
    {
        float yMove = scrollRate * Time.deltaTime;
        transform.Translate(new Vector2(0f,yMove));
    }
}
