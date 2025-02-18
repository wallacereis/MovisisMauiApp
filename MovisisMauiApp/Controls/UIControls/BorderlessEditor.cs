namespace MovisisMauiApp.Controls.UIControls

{
    public class BorderlessEditor : Editor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorderlessEditor"/> class.
        /// </summary>
        public BorderlessEditor()
        {
            this.TextChanged += BorderlessEditor_TextChanged;
        }

        #region METHODS

        /// <summary>
        /// Invoked when editor text is changed.
        /// </summary>
        /// <param name="sender">The editor</param>
        /// <param name="e">Text changed event args</param>
        private void BorderlessEditor_TextChanged(object? sender, TextChangedEventArgs e)
        {
            this.InvalidateMeasure();
        }

        #endregion
    }
}
