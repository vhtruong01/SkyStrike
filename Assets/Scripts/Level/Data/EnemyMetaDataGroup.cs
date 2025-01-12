using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        [CreateAssetMenu(fileName = "EnemyMetaDataGroup", menuName = "Data/EnemyMetaDataGroup")]
        public class EnemyMetaDataGroup: ScriptableObject
        {
            public List<EnemyMetaData> enemiesData;
        }
    }
}