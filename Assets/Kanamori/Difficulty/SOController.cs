using UnityEngine;
using System.IO;
using System;

namespace Kanamori.Difficulty
{
    public class SOController : MonoBehaviour
    {
        void Start()
        {
            Debug.Log("start save Object...");

            ObjectA a1 = new ObjectA();
            a1.position = Vector3.zero;
            a1.eulerAngles = new Vector3(45f, 30f, 60f);
            a1.name = "obj-1";

            string json = JsonUtility.ToJson(a1);
            Debug.Log(json);

            string path = Application.persistentDataPath + "/saveobject.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(json);
                }
                Debug.Log("save end." + path);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            string readJson = "";

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    readJson = reader.ReadLine();
                }
                Debug.Log(readJson);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            var objGet = JsonUtility.FromJson<ObjectA>(readJson);
            Debug.Log(objGet);
            Debug.Log(objGet.name);
        }
    }

    public class ObjectA
    {
        public Vector3 position;
        public Vector3 eulerAngles;
        public string name;
    }
}

