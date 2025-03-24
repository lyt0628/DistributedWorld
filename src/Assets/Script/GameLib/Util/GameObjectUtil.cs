using System.Collections.Generic;
using UnityEngine;

namespace QS.Common.Util
{
    public static class GameObjectUtil
    {
        public static Transform FindChild(Transform parent, string name)
        {
            // ����ҵ���ƥ������壬ֱ�ӷ���
            Transform found = parent.Find(name);
            if (found != null)
                return found;

            // ����������������岢�ݹ����
            foreach (Transform child in parent)
            {
                found = FindChild(child, name);
                if (found != null)
                    return found;
            }

            // ���û���ҵ�������null
            return null;
        }
   
        public static void ReplaceChild(Transform root,  string name, Transform replacement)
        {
            var target = FindChild(root, name);
            var parent = target.parent;

            target.parent = null;
            replacement.parent = parent;
            replacement.name = name;
            GameObject.Destroy(target.gameObject);
        }

        public static Transform[] CollectChildren(Transform root)
        {
            List<Transform> all = new();

            foreach (Transform t in root)
            {
                all.Add(t);
                all.AddRange(CollectChildren(t));
            }

            return all.ToArray();
        }
    }
}