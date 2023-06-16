using System;
using System.Collections.Generic;

namespace ViewLab10.Model
{
    public class SimpleStateMachine<T, F> where T : IEquatable<T>
                                          where F : IEquatable<F>
    {
        public List<SMInstruction<T, F>> _instructions { get; set; }
        public T _current_state { get; set; }

        public SimpleStateMachine(List<SMInstruction<T, F>> instructions, T current_state)
        {
            _instructions = instructions;
            _current_state = current_state;
        }

        public void InputSymbol(F symbol)
        {
            SMInstruction<T, F> current_instruction = _instructions.Find(i => Equals(i.Before_state, _current_state) && Equals(i.Input_symbol, symbol));
            if (current_instruction == null)
            {
                return;
            }
            _current_state = current_instruction.After_state;
        }
    }
}
