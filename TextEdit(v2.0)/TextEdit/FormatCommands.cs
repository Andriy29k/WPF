using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TextEdit
{
    class FormatCommands
    {
        private static RoutedUICommand fontcol;
        private static RoutedUICommand backcol;
        static FormatCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.F, ModifierKeys.Control | ModifierKeys.Shift, "Ctrl+Shift+F"));
            fontcol = new RoutedUICommand("Font Color...", "FontColor", typeof(FormatCommands), inputs);
            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.B, ModifierKeys.Control | ModifierKeys.Shift, "Ctrl+Shift+B"));
            backcol = new RoutedUICommand("Back Color...", "BackColor", typeof(FormatCommands), inputs);
        }

        public static RoutedUICommand FontColor { get { return fontcol; } }

        public static RoutedUICommand BackColor { get { return backcol; } }
    }
}
