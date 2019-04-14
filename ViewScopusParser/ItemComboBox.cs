namespace EmailParserView
{
    public class ItemComboBox
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}