using System.Collections.Generic;

namespace Systems.Stage
{
    public class SM_StageData
    {
        public int StageId;
        public int CurrentModeIndex;
        public List<int> ModeSequence;
        public float ElapsedTime;
        public bool IsCompleted;
    }
}