using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LabelAnimator : MonoBehaviour
{

    enum State
    {
        Ex_DiagLine,
        Ex_HorizLine,
        Ex_Text,
        Expanded,
        Wait,
        Co_Text,
        Co_HorizLine,
        Co_DiagLine,
        Collapsed
    }

    public Text text;
    public Image diagLine;
    public Image horizLine;
    public string labelText;

    public float appearDelay;

    State state = State.Collapsed;
    Vector3 targetDiagScale;
    Vector3 targetHorizScale;


    // Use this for initialization
    void Start()
    {
        targetDiagScale = new Vector3(diagLine.rectTransform.localScale.x, 10, diagLine.rectTransform.localScale.z);
        Update();
        state = State.Collapsed;

        //Expand();
        Invoke("Expand", 1f);
        //targetDiagSize = new Vector2(Mathf.Cos(Mathf.PI / 4) * DIAG_LENGTH, Mathf.Sin(Mathf.PI / 4) * DIAG_LENGTH);
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(appearDelay);
        Collapse();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Ex_DiagLine:
                diagLine.gameObject.SetActive(true);
                diagLine.rectTransform.localScale = Vector3.Lerp(diagLine.rectTransform.localScale, targetDiagScale, 0.2f);
                if (Vector3.Distance(diagLine.rectTransform.localScale, targetDiagScale) < 0.05f)
                {
                    state = State.Ex_Text;
                }
                break;
            case State.Ex_Text:
                text.gameObject.SetActive(true);
                Color c = text.color;
                c.a = 0f;
                text.color = c;
                text.text = labelText;
                state = State.Ex_HorizLine;
                break;
            case State.Ex_HorizLine:
                horizLine.gameObject.SetActive(true);
                horizLine.rectTransform.localPosition = new Vector3(diagLine.rectTransform.localPosition.x + Mathf.Cos(Mathf.PI / 4)*0.98f, diagLine.rectTransform.localPosition.y + Mathf.Sin(Mathf.PI / 4)*0.98f, 0);
                text.rectTransform.localPosition = horizLine.rectTransform.localPosition;
                targetHorizScale = new Vector3(1,text.rectTransform.sizeDelta.x*2f/100f,1);
                horizLine.rectTransform.localScale = Vector3.Lerp(horizLine.rectTransform.localScale, targetHorizScale, 0.1f);
                if (Vector3.Distance(horizLine.rectTransform.localScale, targetHorizScale) < 0.05f)
                {
                    Color d = text.color;
                    d.a = 1.0f;
                    text.color = d;
                    state = State.Expanded;
                }
                else
                {
                    Color e = text.color;
                    e.a = horizLine.rectTransform.localScale.magnitude / targetHorizScale.magnitude;
                    text.color = e;
                }
                break;
            case State.Expanded:
                if (appearDelay != 0)
                {
                    StartCoroutine(Disappear());
                    state = State.Wait;
                }
                break;
            case State.Co_HorizLine:
                horizLine.rectTransform.localScale = Vector3.Lerp(horizLine.rectTransform.localScale, new Vector3(1, 0.1f, 1), 0.1f);
                if (Vector3.Distance(horizLine.rectTransform.localScale, new Vector3(1, 0.1f, 1)) < 0.3f)
                {
                    Color d = text.color;
                    d.a = 0.1f;
                    text.color = d;
                    state = State.Co_Text;
                    horizLine.gameObject.SetActive(false);
                }
                else
                {
                    Color e = text.color;
                    e.a = horizLine.rectTransform.localScale.y / targetHorizScale.magnitude;
                    text.color = e;
                }
                break;
            case State.Co_Text:
                text.gameObject.SetActive(false);
                state = State.Co_DiagLine;
                break;
            
            case State.Co_DiagLine:
                diagLine.rectTransform.localScale = Vector3.Lerp(diagLine.rectTransform.localScale, new Vector3(1, 0.1f, 1), 0.2f);
                if (Vector3.Distance(diagLine.rectTransform.localScale, new Vector3(1, 0.1f, 1)) < 0.05f)
                {
                    state = State.Collapsed;
                }
                break;
            case State.Collapsed:
                text.gameObject.SetActive(false);
                diagLine.gameObject.SetActive(false);
                horizLine.gameObject.SetActive(false);
                if (messageQueue.Count > 0)
                {
                    labelText = (string)messageQueue.Dequeue();
                    Expand();
                }
                break;
        }
    }


    public void Expand()
    {
        if (state == State.Collapsed)
        {
            state = State.Ex_DiagLine;
            print("Expand");
        }
    }

    public void Collapse()
    {
        if (state == State.Wait || state == State.Expanded)
        {
            state = State.Co_HorizLine;
        }
    }

    public bool Busy()
    {
        return state != State.Collapsed;
    }

    private Queue messageQueue = new Queue();
    public void QueueMessage(string message)
    {
        messageQueue.Enqueue(message);
    }
}
