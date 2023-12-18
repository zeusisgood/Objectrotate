using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Cube : UdonSharpBehaviour
{
    // 変数宣言
    [SerializeField] Vector3 axis;
    [SerializeField] float speed;
    [UdonSynced] bool _isActive;

    //インタラクト時に起動する
    public override void Interact()
    {
        // オーナ権限を委譲
        if (!Networking.IsOwner(this.gameObject))
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);

        // UdonSynced変数を更新
        _isActive = !_isActive;

        // 同期をリクエスト
        RequestSerialization();
    }

    //毎フレーム実行
    void Update()
    {
        // インタラクトされたら
        if (_isActive)
        {
            //speed*最後のフレームから現在のフレームまでの秒単位の間隔*角度
            transform.rotation *= Quaternion.AngleAxis(
                speed * Time.deltaTime,
                axis
            );
        }
        else
        {
            // デフォルトの角度にリセット
            transform.rotation = Quaternion.identity;
        }
    }
}
