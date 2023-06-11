// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Linq;
using TestHelper.Input;

namespace InputExample.TestDoubles
{
    public struct SimulateAxis
    {
        public string Name { get; private set; }
        public float Value { get; private set; }

        public SimulateAxis(string name, float value)
        {
            Name = name;
            Value = value;
        }
    }

    public class StubInputAxis : InputWrapper
    {
        public SimulateAxis[] Axes { get; set; } = Array.Empty<SimulateAxis>();

        public override float GetAxis(string axisName)
        {
            return Axes.FirstOrDefault(a => a.Name == axisName).Value;
        }
    }
}
