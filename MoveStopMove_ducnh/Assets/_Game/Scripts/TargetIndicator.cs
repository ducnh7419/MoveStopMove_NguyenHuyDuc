using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : GameUnit
{
    [SerializeField] RectTransform rect;
    [SerializeField] Image iconImg;
    [SerializeField] Image directImg;
    [SerializeField] RectTransform direct;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI scoreTxt;

    [SerializeField] CanvasGroup canvasGroup;

    Transform target;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;

    Vector3 viewPoint;

    Vector2 viewPointX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointY = new Vector2(0.05f, 0.85f);

    Vector2 viewPointInCameraX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointInCameraY = new Vector2(0.05f, 0.95f);

    Camera Camera => CameraFollow.Ins.Camera;

    private bool IsInCamera => viewPoint.x > viewPointInCameraX.x && viewPoint.x < viewPointInCameraX.y && viewPoint.y > viewPointInCameraY.x && viewPoint.y < viewPointInCameraY.y;

    private void LateUpdate()
    {
        viewPoint = Camera.WorldToViewportPoint(target.position);
        Vector3 direction = (target.position - UserDataManager.Ins.Player.TF.position).normalized;
        if (!IsInCamera){
            if (direction.z < 0)
            {
                viewPoint.y *= -1;
            }
        }
        direct.gameObject.SetActive(!IsInCamera);
        nameTxt.gameObject.SetActive(IsInCamera);
        viewPoint.x = Mathf.Clamp(viewPoint.x, viewPointX.x, viewPointX.y);
        viewPoint.y = Mathf.Clamp(viewPoint.y, viewPointY.x, viewPointY.y);
        Vector3 targetSPoint = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
        Vector3 playerSPoint = Camera.WorldToScreenPoint(UserDataManager.Ins.Player.TF.position) - screenHalf;
        rect.anchoredPosition = targetSPoint;
        direct.up = (targetSPoint - playerSPoint).normalized;
    }

    private void OnInit()
    {
        SetScore(0);
        SetColor(new Color(Random.value, Random.value, Random.value, 1));
        
    }

    private void Update(){
        SetAlpha((GameManager.Ins.CurrState >= GameManager.State.StartGame)?1:0);
    }

    public void TurnOn()
    {
        SetAlpha(1);
    }

    public void SetTarget(Transform target, Material material)
    {
        this.target = target;
        OnInit();

    }

    public void SetScore(int score)
    {
        scoreTxt.SetText(score.ToString());
    }

    public void SetName(string name)
    {
        nameTxt.SetText(name);
    }

    private void SetColor(Color color)
    {
        iconImg.color = color;
        nameTxt.color = color;
    }

    public void SetColor(Material material)
    {
        iconImg.material = material;
        nameTxt.material = material;
    }

    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public void OnDesPawn()
    {
        SetAlpha(0);
        target = null;
        SimplePool.Despawn(this);
    }
}
