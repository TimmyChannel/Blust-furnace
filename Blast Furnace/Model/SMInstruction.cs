namespace ViewLab10.Model
{
    public class SMInstruction<T, F>
    {
        public T Before_state { get; private set; }
        public F Input_symbol { get; private set; }
        public T After_state { get; private set; }

        public SMInstruction(T before_state, F input_symbol, T after_state)
        {
            Before_state = before_state;
            Input_symbol = input_symbol;
            After_state = after_state;
        }

        public override string ToString()
        {
            return string.Format($"({Before_state}, {Input_symbol}) -> {After_state}");
        }
    }
}
