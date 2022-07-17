using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ITPRO.UI
{
    public class Joistick : MonoBehaviour
    {
        [SerializeField]private Transform stick;

        private RectTransform rt;
        private Transform rotater;
        private bool active;
        private float radius;

        private void Awake()
        {
            rt = GetComponent<RectTransform>();
            radius = rt.sizeDelta.x/2;
        }

        public void Enable(bool _active)
        {
            if (_active)
            {
                active = _active;
                StartCoroutine(Moving());
            }
            else
            {
                active = _active;
                StopCoroutine(Moving());
            }
        }

        IEnumerator Moving()
        {
            while (active)
            {
                Vector2 pos;
#if UNITY_ANDROID || UNITY_IOS
                if (Input.touchCount > 0)
                {
                    pos = FindPosTouch();
                }
                else
                {
                    yield return null;
                }
#endif
#if UNITY_EDITOR
                pos = Input.mousePosition;
#endif
                if (Vector2.Distance(transform.position, pos) > radius)
                {
                    
                    float angle;
                    //angle = |a|*|b|*cos
                    //a = (radius,0)
                    //b = (pos.x, pos.y)
                    Vector2 A = new Vector2(radius, 0);
                    Vector2 B = new Vector2(transform.position.x - pos.x, transform.position.y - pos.y);
                    Vector2 D = A - B;
                    //print((pos.x * radius) / (Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y) + Mathf.Sqrt(radius * radius)));
                    angle = Mathf.PI - Mathf.Atan2(D.y, -D.x);
                    //angle = Mathf.Acos((pos.x * radius) / (Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y) + Mathf.Sqrt(radius * radius)));
                    //if (pos.y - transform.position.y < 0)
                    //{
                    //    angle = 360 - angle;
                    //}

                    print((angle * 180) / Mathf.PI);

                    stick.transform.position = new Vector3(transform.position.x + Mathf.Cos(angle) * radius, transform.position.y + Mathf.Sin(angle) * radius);
                }
                else
                {
                    stick.transform.position = pos;
                }
            }
            yield return new WaitForSeconds(0.02f);

        }

        private Vector2 FindPosTouch()
        {
            foreach (Touch touch in Input.touches)
            {
                if(touch.position.x < rt.position.x + radius && touch.position.x > rt.position.x - radius)
                {
                    if (touch.position.y < rt.position.y + radius && touch.position.y > rt.position.y - radius)
                    {
                        return touch.position;
                    }
                }
            }
            return Vector2.zero;
        }
    }
}
