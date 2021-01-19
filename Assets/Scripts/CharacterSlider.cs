    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using System;

    public class CharacterSlider : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        // private float speed = 0.5f;
        // private float friction = 0.2f;   // ...or whatever gives the right effect.
        // private float velocity = 0f;
        private float delta = 0f;

        private bool sliding = false;
        private int selectedIndex = 0;
        private int targetIndex = 0;
        private float startX = 0;
        private Vector3 startPosition;
        private Vector3 endPosition;
        private float startTime;


    public int CINDEX;

        const int maxIndex = 4;
        const int threshold = 5;

        const float charWidth = 51f;
        const float gap = 29f;

        private float EaseInOutQuint(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end * 0.5f * value * value * value * value * value + start;
            value -= 2;
            return end * 0.5f * (value * value * value * value * value + 2) + start;
        }

        public void OnBeginDrag(PointerEventData pointerEventData) {
            if(!sliding) {
                if(Input.touchCount > 0) {
                    var touch = Input.GetTouch(0);
                    startX = touch.position.x;
                } else {
                    startX = Input.mousePosition.x;
                }
            }
        }

        public void OnDrag(PointerEventData data) {
            if(!sliding) {
                if(Input.touchCount > 0) {
                    var touch = Input.GetTouch(0);
                    delta = touch.position.x - startX;
                } else {
                    delta = Input.mousePosition.x - startX;
                }
                if(Math.Abs(delta) > threshold) {
                    if(delta > 0 && selectedIndex > 0) {
                        targetIndex -= 1;
                    }
                    if(delta < 0 && selectedIndex < maxIndex) {
                        targetIndex += 1;
                    }
                    if(selectedIndex != targetIndex) {
                        sliding = true;
                        startTime = Time.time;
                        Vector3 pos = gameObject.transform.localPosition;
                        startPosition = new Vector3(pos.x, pos.y, pos.z);
                        float targetX = -charWidth/2 - targetIndex * (charWidth + gap);
                        endPosition = new Vector3(targetX, pos.y, pos.z);
                    }
                }
            }
        }

        public void SelectCharacter(int index) {
            if (index < 0 || index > maxIndex) {
                return;
            }

            if (index == selectedIndex) {
                return;
            }
        CINDEX = index;
            targetIndex = index;
            sliding = true;
            startTime = Time.time;
            Vector3 pos = gameObject.transform.localPosition;
            startPosition = new Vector3(pos.x, pos.y, pos.z);
            float targetX = -charWidth/2 - targetIndex * (charWidth + gap);
            endPosition = new Vector3(targetX, pos.y, pos.z);
        }

        void Update () {
            if(selectedIndex != targetIndex) {
                float t = EaseInOutQuint(0, 1, (Time.time - startTime) / 0.4f);
                gameObject.transform.localPosition = Vector3.LerpUnclamped(startPosition, endPosition, t);

                animateChars(t);

                if(t >= 1) {
                    sliding = false;
                    selectedIndex = targetIndex;
                }
            }
        }

        void animateChars(float t) {
            GameObject selectedChar = gameObject.transform.GetChild(selectedIndex).gameObject;
            GameObject targetChar = gameObject.transform.GetChild(targetIndex).gameObject;

            animateChar(selectedChar, 1f - t, 1f - 0.58f * t, 1f - 0.268f * t);
            animateChar(targetChar, t, 0.42f + 0.58f * t, 0.732f + 0.268f * t);

        }

        void animateChar(GameObject obj, float alphaSel, float alphaChar, float scale) {
            GameObject sel = obj.transform.GetChild(0).gameObject;

            sel.SetActive(true);
            Image selImage = sel.GetComponent<Image>();
            selImage.color = new Color(1f, 1f, 1f, alphaSel);

            GameObject icon = obj.transform.GetChild(1).gameObject;
            Image iconImage = icon.GetComponent<Image>();
            iconImage.color = new Color(1f, 1f, 1f, alphaChar);

            icon.transform.localScale = new Vector3(scale, scale, scale);
        }

        public int getSelectedIndex() {
            return selectedIndex;
        }
    }
