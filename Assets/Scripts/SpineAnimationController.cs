using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class SpineAnimationController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnim;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset[] directionAnim;
    public AnimationReferenceAsset[] PushBoxAnim;
    public string currentState;

    private void Start()
    {
        currentState = "Idle";
        SetCharacterIdle();
    }
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        skeletonAnim.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }
    public void SetParameter(SkeletonAnimation skeletonAnimation,AnimationReferenceAsset setIdle, string setCurrentState)
    {
        skeletonAnim = skeletonAnimation;
        idle = setIdle;
        currentState = setCurrentState;
    }
    public void SetCharacterIdle()
    {
       SetAnimation(idle, true, 1f);
    }
    public void SetCharacterByDirection(Vector2 direction)
    { 
        if(direction == new Vector2(-1,0) || direction == new Vector2(0, 1))
        {
            SetAnimation(directionAnim[0], false, 1f);
        }
        if (direction == new Vector2(1, 0) || direction == new Vector2(0, -1))
        {
            SetAnimation(directionAnim[1], false, 1f);
        }
    }

    public void playWalkAnimation(Vector2 direction)
    {
        StartCoroutine(playWalkAnim(direction));
    }
    public void playPushAnimation(Vector2 direction)
    {
        StartCoroutine(PushAnim(direction));
    }
    IEnumerator playWalkAnim(Vector2 direction)
    {
        SetCharacterByDirection(direction);
        yield return new WaitForSeconds(0.3f);
        SetCharacterIdle();
    }
    IEnumerator PushAnim(Vector2 direction)
    {
        SetAnimation(PushBoxAnim[0], false, 1f);
        yield return new WaitForSeconds(0.45f);
        SetCharacterIdle();
    }
}
