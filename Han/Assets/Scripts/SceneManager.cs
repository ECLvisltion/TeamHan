using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    /// <summary>
    /// 씬만 바꾸는 용도
    /// </summary>
    /// <param name="sceneNumber">씬 번호</param>
    public void SceneChange(int sceneNumber)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNumber);
    }

    /// <summary>
    /// 씬을 바꿀 때 플레이어 위치도 바꾸는 용도
    /// </summary>
    /// <param name="sceneNumber">씬 번호</param>
    /// <param name="player">플레이어로 지정된 게임 오브젝트</param>
    /// <param name="position">이동할 위치</param>
    public void SceneChange(int sceneNumber, GameObject player, Vector3 position)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNumber);
        player.transform.position = position;
    }
}
