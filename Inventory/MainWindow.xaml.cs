using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileHelpers;
using System.IO;

namespace Inventory
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isRectDragInProg;
        public MainWindow()
        {
            InitializeComponent();
            double[] nazvy = new double[5];
            rect1.SetValue(Grid.RowProperty, 0);

        }

        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = true;
            var rectangle = sender as System.Windows.Shapes.Rectangle;
            rectangle.CaptureMouse();
        }


        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = false;
            var rectando = sender as System.Windows.Shapes.Rectangle;
            rectando.ReleaseMouseCapture();
            

        }

        private void rect_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        { 
            var rectando = sender as System.Windows.Shapes.Rectangle;
            if (!_isRectDragInProg) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(canvas);

            // center the rect on the mouse
            double left = mousePos.X - (rectando.ActualWidth / 2);
            double top = mousePos.Y - (rectando.ActualHeight / 2);
            Canvas.SetLeft(rectando, left);
            Canvas.SetTop(rectando, top);
            

        }
        [DelimitedRecord("|")]
        public class Orders
        {
            public int x;
            public int y;


        }


    }
}
