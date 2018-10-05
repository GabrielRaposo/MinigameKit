using UnityEngine;
using DG.Tweening;
namespace Fevdo{
    public class WitchAI: MonoBehaviour{
        enum State{Flying, Vulnerable}
        State state;
        Tweener tween;

        void Start(){
        }

        void Update(){
            if(tween == null)
                tween = transform.DOPath(generatePath(), Random.Range(5,8), PathType.CatmullRom, PathMode.TopDown2D, 10, Color.yellow).SetEase(Ease.InSine);

        }

        Vector3[] generatePath(){
            return new Vector3[]{transform.position, randomPos(), randomPos()};

        }



        Vector2 randomPos(){
            return new Vector2(
                Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 0.65f)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y)
            );
        }
    }
}