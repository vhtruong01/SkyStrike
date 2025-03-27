using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace UI
    {
        public class LoadingScene : MonoBehaviour
        {
            [SerializeField] private Slider progressBar;
            [SerializeField] private Image icon;
            [SerializeField] private float disappearTime;

            public IEnumerator ShowLoadingProgess(List<AsyncOperation> scensLoading)
            {
                float totalProgress = 0;
                for (int i = 0; i < scensLoading.Count; i++)
                {
                    while (!scensLoading[i].isDone)
                    {
                        foreach (AsyncOperation operation in scensLoading)
                            totalProgress += operation.progress;
                        progressBar.value = totalProgress / scensLoading.Count;
                        yield return null;
                    }
                }
            }
        }
    }
}