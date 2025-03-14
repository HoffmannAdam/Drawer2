namespace DrawerOOP2._0
{
    internal class MenuOption
    {
        public string Text { get; }
        private Action Action { get; }

        public MenuOption(string text, Action action)
        {
            Text = text;
            Action = action;
        }

        public void Execute()
        {
            Action.Invoke();
        }
    }
}
