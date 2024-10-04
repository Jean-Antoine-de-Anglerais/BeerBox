using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Config;
using ReflectionUtility;
using System.Reflection;
using UnityEngine.Tilemaps;
using System.IO;
using NCMS;

namespace BeerBoxNCMS
{
    [ModEntry]
    class Main : MonoBehaviour
    {
        void Awake()
        {
            BeerBoxClass.init();
        }
    }
}
