using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Map
{
    [CreateAssetMenu(fileName = "MapSO", menuName = "ScriptableObjects/MapSO")]
    public class MapSO : SerializedScriptableObject
    {
        [Title("SECTION OF MAP", bold: true, horizontalLine: true), Space(2)]
        public Dictionary<Section, List<Level>> sectionsOfMap = new Dictionary<Section, List<Level>>();
    }

    [Serializable]
    public class Level
    {
        [Title("LEVEL PROPERTY", bold: true, horizontalLine: true), Space(2)]
        public GameObject levelPrefab;
        public bool isUnlock;
    }

    public enum Section {
        Section_01, Section_02, Section_03
    }
}


