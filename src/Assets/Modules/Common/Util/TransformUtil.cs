

using UnityEngine;

namespace QS.Common.Util
{
    public static class TransformUtil
    {
        public static bool IsInSectorArea(Transform self, Transform taret, RelativeDir dir, float deg = 180, float distance = 10)
        {

            var targetPos = new Vector2(taret.position.x, taret.position.z);
            var selfPos = new Vector2(self.transform.position.x, self.transform.position.z);
            if (Vector2.Distance(selfPos, targetPos) > distance) return false;

            var baseDir = dir switch
            {
                RelativeDir.Forward => self.transform.forward,
                RelativeDir.Back => -self.transform.forward,
                RelativeDir.Left => -self.transform.right,
                RelativeDir.Right => self.transform.right,
                _ => throw new System.NotImplementedException(),
            };

            var self2Target = taret.position - self.transform.position;
            self2Target = self2Target.normalized;
            if (Vector3.Angle(self2Target, baseDir) > deg / 2) return false;

            return true;
        }
    }
}