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
        int[,] poleveci = new int[6, 5];
        double puvodTop;
        double puvodLeft;
        public MainWindow()
        {
            InitializeComponent();
            
            poleveci[0, 0] = 1;
            poleveci[0, 1] = 1;
            poleveci[0, 2] = 1;
            poleveci[1, 0] = 1;
            poleveci[2, 0] = 1;
        }
        private void SnapToGrid(UIElement element, double vyska,double sirka)
        {
            int GRID_SIZE = 50;
            
            double xSnap = Canvas.GetLeft(element) % GRID_SIZE;
            double ySnap = Canvas.GetTop(element) % GRID_SIZE;
            

            // If it's less than half the grid size, snap left/up 
            // (by subtracting the remainder), 
            // otherwise move it the remaining distance of the grid size right/down
            // (by adding the remaining distance to the next grid point).
            if (xSnap <= GRID_SIZE / 2.0)
            {
                xSnap *= -1;
            }
            else
            {
                xSnap = GRID_SIZE - xSnap;
            }
                
            if (ySnap <= GRID_SIZE / 2.0)
            {
                ySnap *= -1;
            }  
            else
            {
                ySnap = GRID_SIZE - ySnap;
            }

            xSnap += Canvas.GetLeft(element);
            ySnap += Canvas.GetTop(element);

            int xpozice = Convert.ToInt32(xSnap)/GRID_SIZE;
            int ypozice = Convert.ToInt32(ySnap)/GRID_SIZE;
            if (poleveci[xpozice,ypozice]==0)
            {
                Console.WriteLine("MUZEME");
            }

            if (xSnap + sirka > 200)
            {
                xSnap = 250 - sirka;
            }
            else if (xSnap < 0)
            {
                xSnap = 0;
            }
            if (ySnap + vyska > 250)
            {
                ySnap = 300 - vyska;
            }
            else if (ySnap < 0)
            {
                ySnap = 0;
            }

            Canvas.SetLeft(element, xSnap);
            Canvas.SetTop(element, ySnap);

            Console.WriteLine("X: " + xSnap);
            Console.WriteLine("Sirka: " + sirka);
            Console.WriteLine("Y: " + ySnap);
            Console.WriteLine("Vyska: " + vyska);
            Console.WriteLine(poleveci[0,0]);
        }
        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            _isRectDragInProg = true;
            var rectangle = sender as System.Windows.Shapes.Rectangle;
            puvodLeft = rectangle.Width;
            puvodTop = rectangle.Height;
            rectangle.CaptureMouse();
            
        }


        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = false;
            var rectando = sender as System.Windows.Shapes.Rectangle;
            rectando.ReleaseMouseCapture();
            double vyskaRec = rectando.Height;
            double sirkaRec = rectando.Width;
            SnapToGrid(rectando,vyskaRec,sirkaRec);

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
        public void KontrolaPresahu(double xSnap,double ySnap,double sirka,double vyska)
        {
            if (xSnap + sirka > 200)
            {
                xSnap = 250 - sirka;
            }
            else if (xSnap < 0)
            {
                xSnap = 0;
            }
            if (ySnap + vyska > 250)
            {
                ySnap = 300 - vyska;
            }
            else if (ySnap < 0)
            {
                ySnap = 0;
            }
        }

        [DelimitedRecord("|")]
        public class Orders
        {
            public int x;
            public int y;


        }


    }
}
