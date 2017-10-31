using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Text.RegularExpressions;

namespace HiLib
{
    public static class Extensions
    {
        public static T FindAncestor<T>(this DependencyObject depObj) where T : class 
        {
            var target = depObj;

            try {
                do {
                    target = System.Windows.Media.VisualTreeHelper.GetParent(target);

                } while (target != null && !(target is T));

                return target as T;
            } finally {
                target = null;
                depObj = null;
            }
        }
    }

    public class DataGridTextColumnEx : DataGridTextColumn
    {

        public bool MultiLine
        {
            get { return (bool)GetValue(MultiLineProperty); }
            set { SetValue(MultiLineProperty, value); }
        }

        /// <summary>複数行入力？</summary>
        public static readonly DependencyProperty MultiLineProperty =
            DependencyProperty.Register("MultiLine", typeof(bool),
                                typeof(DataGridTextColumnEx), new UIPropertyMetadata(false));


        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var textBox = (TextBox)base.GenerateEditingElement(cell, dataItem);

            //文字寄せを指定します。
            //textBox.TextAlignment = this.GetTextAlignment();
            //textBox.VerticalAlignment = this.VerticalAlignment;
            //textBox.MaxLength = this.MaxLength;

            if (this.MultiLine)
            {
                textBox.AcceptsReturn = false;
                textBox.PreviewKeyDown += new KeyEventHandler(TextBox_PreviewKeyDown);
                textBox.Unloaded += new RoutedEventHandler(TextBox_Unloaded);
                //エンターキー押下によるフォーカス移動から除外
                //FocusMoveBehavior.SetExcluded(textBox, true);
            }

            return textBox;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (Keyboard.IsKeyDown(Key.Enter) || Keyboard.IsKeyDown(Key.Return))
            {
                var val = ModifierKeys.Alt & e.KeyboardDevice.Modifiers;

                if (val > 0)
                {
                    //①Alt+Enterで改行を挿入しましょう。
                    var textBox = sender as TextBox;
                    var caret = textBox.CaretIndex;
                    textBox.Text = textBox.Text.Insert(caret, Environment.NewLine);
                    textBox.CaretIndex = caret + 1;

                    //②行の高さを調整しましょう。
                    //var cellsPresenter = textBox.FindAncestor<DataGridCellsPresenter>();

                    //if (cellsPresenter != null)
                    //{
                    //    //改行コードの数をカウントします。
                    //    var lineCnt = GetMatchStringCount(
                    //                            textBox.Text, Environment.NewLine) + 1;

                    //    //標準で設定されている行の高さを取得します。
                    //    //DataGridRowに対する高さ(Height)が未設定の場合のみ、
                    //    //DataGridのRowHeightプロパティを取得します。
                    //    var dataGridRow = cellsPresenter.FindAncestor<DataGridRow>();
                    //    var height = dataGridRow.Height;

                    //    if (double.IsNaN(height))
                    //    {
                    //        var ownerDataGrid = dataGridRow.FindAncestor<DataGrid>();
                    //        height = ownerDataGrid.RowHeight;

                    //        if (double.IsNaN(height))
                    //        {
                    //            height = 22D;//みつからなければ、強制的に22で
                    //        }
                    //    }

                    //    cellsPresenter.Height = (double)(lineCnt * height);
                    //}

                    e.Handled = true;//これにて処理は終了
                }
            }

            return;
        }
        private void TextBox_Unloaded(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            textBox.PreviewKeyDown -= new KeyEventHandler(TextBox_PreviewKeyDown);
            textBox.Unloaded -= new RoutedEventHandler(TextBox_Unloaded);
            //BindingOperations.ClearBinding(textBox,
            //                        FocusMoveBehavior.ExcludedProperty);

            return;
        }

        public static int GetMatchStringCount(string source, string search, bool ignoreCase = false)
        {
            string resultString = string.Empty;

            //using System.Text.RegularExpressions;が必要。
            Regex reg = null;

            if (ignoreCase)
            {
                reg = new Regex(search, RegexOptions.IgnoreCase);
            }
            else
            {
                reg = new Regex(search);
            }

            var m = reg.Match(source);
            var counter = TawamureClosure.CountUp();

            while (m.Success)
            {
                counter();
                m = m.NextMatch();
            }

            return counter();
        }
    }

    public static class TawamureClosure 
    {


        public static Func<int> CountUp(int start = 0, int step = 1) {
            var i = start;

            return () => {
                try {
                    return i;
                } finally {
                    i = i + step;
                }
            };
        }
    }
}
