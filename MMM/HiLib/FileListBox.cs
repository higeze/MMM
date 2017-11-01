using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HiLib
{
    public class FileListBox:ListBox
    {
        public FileListBox()
            : base()
        {
            AllowDrop = true;
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        protected override void OnDragOver(System.Windows.DragEventArgs e)
        {
            base.OnDragOver(e);

            if (e.Data.GetDataPresent(typeof(ListBoxItem)))
            {
                e.Effects = DragDropEffects.Move;
            }
        }

        protected override void OnDrop(System.Windows.DragEventArgs e)
        {
            base.OnDrop(e);

            if (e.Data.GetFormats().Contains(DataFormats.FileDrop))
            {
                string[] droppedFiles = null;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    droppedFiles = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                }

                if ((null == droppedFiles) || (!droppedFiles.Any())) { return; }

                foreach (string s in droppedFiles)
                {
                    this.Items.Add(s);
                }
            }
            else
            {
                var item = (string)e.Data.GetData(typeof(string));
                if (this == e.Source) { return; }

                if (this.SelectedItem == null)
                {
                    this.Items.Add(item);
                }
                else
                {
                    this.Items.Insert(this.SelectedIndex, item);
                }
            }


        }
        private Point _startPoint;

        protected override void OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            _startPoint = e.GetPosition(null);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            Point mousePos = e.GetPosition(null);
            Vector diff = _startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
          (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
           Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                DragDrop.DoDragDrop(this, this.SelectedItem, DragDropEffects.Copy);
            }
        }
    }
}
