using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Tween
{
    public enum EasingStyle
    {
        Linear,
        Quad,
        Cubic,
        Quart,
        Exponential,
        Sine,
        Bounce,
        Elastic,
        Back
    }
    public enum EasingDirection
    {
        In,
        Out,
        InOut
    }
    public class Tween
    {
        public Transform transform;
        public Vector3 startSize;
        public Vector3 startPosition;
        public Vector3 goalPosition;
        public Quaternion startRotation;
        public Quaternion goalRotation;
        public float endTime;
        public float startTime;
        public float duration;
        public EasingStyle style;
        public EasingDirection direction;
        public Tween(Transform transform, float duration, EasingStyle style, EasingDirection direction)
        {
            this.transform = transform;
            this.duration = duration;
            this.style = style;
            this.direction = direction;
        }
        // Update is called once per frame
        public void TweenFrame()
        {
            if (Time.time < endTime)
            {
                if (direction == EasingDirection.In)
                {
                    switch (style)
                    {
                        case EasingStyle.Linear:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, linear(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, linear(Time.time - startTime));
                            break;
                        case EasingStyle.Quad:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, QuadIn(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, QuadIn(Time.time - startTime));
                            break;
                        case EasingStyle.Cubic:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, CubeIn(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, CubeIn(Time.time - startTime));
                            break;
                        case EasingStyle.Quart:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, QuartIn(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, QuartIn(Time.time - startTime));
                            break;
                        case EasingStyle.Exponential:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, expoIn(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, expoIn(Time.time - startTime));
                            break;
                        case EasingStyle.Sine:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, SineIn(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, SineIn(Time.time - startTime));
                            break;
                        case EasingStyle.Bounce:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, BounceIn(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, BounceIn(Time.time - startTime));
                            break;
                        case EasingStyle.Elastic:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, elasticIn(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, elasticIn(Time.time - startTime));
                            break;
                        case EasingStyle.Back:
                            transform.position = Vector3.Lerp(startPosition,goalPosition,backIn(Time.time-startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, backIn(Time.time - startTime));
                            break;                    
                    }
                }
                else if (direction == EasingDirection.Out)
                {
                    switch (style)
                    {
                        case EasingStyle.Linear:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, linear(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, linear(Time.time - startTime));
                            break;
                        case EasingStyle.Quad:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, QuadOut(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, QuadOut(Time.time - startTime));
                            break;
                        case EasingStyle.Cubic:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, CubeOut(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, CubeOut(Time.time - startTime));
                            break;
                        case EasingStyle.Quart:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, QuartOut(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, QuartOut(Time.time - startTime));
                            break;
                        case EasingStyle.Exponential:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, expoOut(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, expoOut(Time.time - startTime));
                            break;
                        case EasingStyle.Sine:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, SineOut(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, SineOut(Time.time - startTime));
                            break;
                        case EasingStyle.Bounce:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, BounceOut(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, BounceOut(Time.time - startTime));
                            break;
                        case EasingStyle.Elastic:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, elasticOut(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, elasticOut(Time.time - startTime));
                            break;
                        case EasingStyle.Back:
                            transform.position = Vector3.Lerp(startPosition, goalPosition, backOut(Time.time - startTime));
                            transform.rotation = Quaternion.Lerp(startRotation, goalRotation, backOut(Time.time - startTime));
                            break;
                    }
                }
            }
        }
        //Easing Functions
        //Linear x
        float linear(float time)
        {
            return time / duration;
        }
        //Quadratic x^2
        float QuadIn(float time)
        {
            return (time / duration) * (time / duration);
        }
        float QuadOut(float time)
        {
            return 1 - (1 - time / duration) * (1 - time / duration);
        }
        //Cubic x^3
        float CubeIn(float time)
        {
            return (time / duration) * (time / duration) * (time / duration);
        }
        float CubeOut(float time)
        {
            return 1 - (1 - time / duration) * (1 - time / duration) * (1 - time / duration);
        }
        //Quartic x^4
        float QuartIn(float time)
        {
            return (time / duration) * (time / duration) * (time / duration) * (time / duration);
        }
        float QuartOut(float time)
        {
            return 1 - (1 - time / duration) * (1 - time / duration) * (1 - time / duration) * (1 - time / duration);
        }
        //Exponential x^x
        float expoIn(float time)
        {
            float x = time / duration;
            return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
        }
        float expoOut(float time)
        {
            float x = time / duration;
            return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
        }
        //Sine Sin(x)
        float SineIn(float time)
        {
            float x = time / duration;
            return 1 - Mathf.Cos((x / Mathf.PI) * 2);
        }
        float SineOut(float time)
        {
            float x = time / duration;
            return Mathf.Cos((x / Mathf.PI) * 2);
        }
        //Bounce (Random)
        float BounceIn(float time)
        {
            return 1 - BounceOut(time, true);
        }
        float BounceOut(float time, bool i = false)
        {
            float x = time / duration;
            if (i)
            {
                x = 1 - x;
            }
            float n1 = 7.5625f;
            float d1 = 2.75f;
            if (x < 1 / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2.5 / d1)
            {
                return n1 * (x -= 1.5f / d1) * x + .75f;
            }
            else if (x < 2.5f / d1)
            {
                return n1 * (x -= 2.25f / d1) * x + .9375f;
            }
            else
            {
                return n1 * (x -= 2.625f / d1) * x + .984375f;
            }
        }
        //Elastic
        float elasticIn(float time)
        {
            float x = time / duration;
            float c4 = (2 * Mathf.PI) / 3;
            return x == 0 ? 0 : x == 1 ? 1 : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
        }
        float elasticOut(float time)
        {
            float x = time / duration;
            float c4 = (2 * Mathf.PI) / 3;
            return x == 0 ? 0 : x == 1 ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - .75f) * c4) + 1;
        }

        //Back
        float backIn(float time)
        {
            float x = time/duration;
            return 2.70158f * x * x * x - 1.70158f * x * x;
        }
        float backOut (float time) 
        { 
            float x = time / duration;
            return 1 + 2.70158f * Mathf.Pow(x - 1, 3) + 1.70158f * Mathf.Pow(x - 1, 2);
        }

        //Play Tween
        public void TweenPosition(Vector3 goal)
        {
            startTime = Time.time;
            startPosition = transform.position;
            goalPosition = goal;
            startRotation = transform.rotation;
            goalRotation = transform.rotation;
            endTime = Time.time + duration;
        }
        public void TweenRotation(Quaternion goal)
        {
            startTime = Time.time;
            startRotation = transform.rotation;
            goalRotation = goal;
            startPosition = transform.position;
            goalPosition = transform.position;
            endTime = Time.time + duration;
        }
        public void TweenPositionRotation(Vector3 goalPos, Quaternion goalRot)
        {
            startTime = Time.time;
            startRotation = transform.rotation;
            goalRotation = goalRot;
            startPosition = transform.position;
            goalPosition = goalPos;
            endTime = Time.time + duration;
        }
    }
}