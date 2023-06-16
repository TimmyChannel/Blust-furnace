using System.Collections.Generic;

namespace ViewLab10.Model
{
    public enum FurnaceStates
    {
        Offline = 0,
        Heating,
        Maintaining,
        Cooling
    }

    public enum FurnaceControls
    {
        Backward = 0,
        Forward
    }

    public static class BlastFurnaceInstructions
    {
        private static readonly List<SMInstruction<int, int>> _BlastFurnaceInstructions = new()
        {
            new SMInstruction<int, int>((int)FurnaceStates.Offline, (int)FurnaceControls.Forward, (int)FurnaceStates.Heating),
            new SMInstruction<int, int>((int)FurnaceStates.Heating, (int)FurnaceControls.Forward, (int)FurnaceStates.Maintaining),
            new SMInstruction<int, int>((int)FurnaceStates.Heating, (int)FurnaceControls.Backward, (int)FurnaceStates.Cooling),
            new SMInstruction<int, int>((int)FurnaceStates.Maintaining, (int)FurnaceControls.Backward, (int)FurnaceStates.Cooling),
            new SMInstruction<int, int>((int)FurnaceStates.Cooling, (int)FurnaceControls.Forward, (int)FurnaceStates.Heating),
            new SMInstruction<int, int>((int)FurnaceStates.Cooling, (int)FurnaceControls.Backward, (int)FurnaceStates.Offline)
        };

        public static List<SMInstruction<int, int>> instructions { get => _BlastFurnaceInstructions; }
    }
}
