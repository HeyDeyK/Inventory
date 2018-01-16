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
        int[,] poleveci = new int[5,7];
        double puvodTop;
        double puvodLeft;
        double xSnap = 0;
        double ySnap = 0;
        public MainWindow()
        {
            InitializeComponent();
            GetRectangle();
            AddRectangle();
            

        }
        private void SnapToGrid(UIElement element, double vyska,double sirka)
        {
            int GRID_SIZE = 50;
            xSnap = Canvas.GetLeft(element) % GRID_SIZE;
            ySnap = Canvas.GetTop(element) % GRID_SIZE;

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

            KontrolaPresahu(sirka, vyska);

            int xpozice = Convert.ToInt32(xSnap)/GRID_SIZE;
            int ypozice = Convert.ToInt32(ySnap)/GRID_SIZE;

            
            
            int zakazat = PoleVyska(vyska, xpozice, ypozice);
            int zakazat2 = PoleSirka(sirka, xpozice, ypozice);

            if (zakazat==0 && zakazat2==0)
            {
                Canvas.SetLeft(element, xSnap);
                Canvas.SetTop(element, ySnap);

                int xpozicenula = Convert.ToInt32(puvodLeft) / GRID_SIZE;
                int ypozicenula = Convert.ToInt32(puvodTop) / GRID_SIZE;

                PoleVyskaSet(vyska, xpozicenula, ypozicenula, 0);
                PoleSirkaSet(sirka, xpozicenula, ypozicenula, 0);
                
                PoleVyskaSet(vyska, xpozice,ypozice,1);
                PoleSirkaSet(sirka, xpozice, ypozice,1);
                

            }
            else
            {
                Canvas.SetLeft(element, puvodLeft);
                Canvas.SetTop(element, puvodTop);

                int xpozicenula = Convert.ToInt32(puvodLeft) / GRID_SIZE;
                int ypozicenula = Convert.ToInt32(puvodTop) / GRID_SIZE;

                PoleVyskaSet(vyska, xpozicenula, ypozicenula, 1);
                PoleSirkaSet(sirka, xpozicenula, ypozicenula, 1);
            }
        }
        private void GetRectangle()
        {
            var engine = new FileHelperEngine<Objekt>();

            var objekty = new List<Objekt>();
            foreach (Rectangle rect in canvas.Children.OfType<Rectangle>())
            {
                double leftpos = Canvas.GetLeft(rect);
                double toppos = Canvas.GetTop(rect);
                objekty.Add(new Objekt()
                {
                    name = rect.Name,
                    LeftPos = leftpos,
                    TopPos = toppos,
                    height = rect.Height,
                    width = rect.Width
                });

                engine.WriteFile("Output.Txt", objekty);
            }
        }
        public void AddRectangle()
        {
            

            var engine = new FileHelperEngine<Objekt>();
            var records = engine.ReadFile("Output.txt");

            foreach (var record in records)
            {
                Rectangle r = new Rectangle();
                r.Width = record.width;
                r.Height = record.height;
                if(r.Width == 50 && r.Height ==50)
                {
                    r.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"D:\ViletaJeBuh\Inventory\img\dia.png", UriKind.Absolute))
                    };
                }
                else if(r.Width == 100 && r.Height == 50)
                {
                    r.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"D:\ViletaJeBuh\Inventory\img\gun.png", UriKind.Absolute))
                    };
                }
                else if (r.Width == 50 && r.Height == 100)
                {
                    r.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"D:\ViletaJeBuh\Inventory\img\axe.png", UriKind.Absolute))
                    };
                }
                else
                {
                    r.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"D:\ViletaJeBuh\Inventory\img\dia.png", UriKind.Absolute))
                    };
                }

                r.MouseLeftButtonDown += rect_MouseLeftButtonDown;
                r.MouseLeftButtonUp += rect_MouseLeftButtonUp;
                r.MouseMove += rect_MouseMove;
                Canvas.SetLeft(r, record.LeftPos);
                Canvas.SetTop(r, record.TopPos);

                canvas.Children.Add(r);
                int GRID_SIZE = 50;
                int xpozice = Convert.ToInt32(record.LeftPos) / GRID_SIZE;
                int ypozice = Convert.ToInt32(record.TopPos) / GRID_SIZE;
                PoleVyskaSet(r.Height,xpozice , ypozice,1);
                PoleSirkaSet(r.Width, xpozice, ypozice,1);
            }
        }
        public void KontrolaPresahu(double sirka, double vyska)
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
        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            _isRectDragInProg = true;
            int GRID_SIZE = 50;
            var rectangle = sender as System.Windows.Shapes.Rectangle;
            puvodLeft = Canvas.GetLeft(rectangle);
            puvodTop = Canvas.GetTop(rectangle);
            double vyska = rectangle.Height;
            double sirka = rectangle.Width;
            int xpozicenula = Convert.ToInt32(puvodLeft) / GRID_SIZE;
            int ypozicenula = Convert.ToInt32(puvodTop) / GRID_SIZE;
            KontrolaPresahu(sirka, vyska);
            
            PoleVyskaSet(vyska, xpozicenula, ypozicenula, 0);
            PoleSirkaSet(sirka, xpozicenula, ypozicenula, 0);
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
            GetRectangle();
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

        public int PoleVyska(double vyska, int xpozice, int ypozice)
        {
            int ctr = 0;
            int zakazat = 0;
            while (true)
            {
                if (ctr == vyska / 50)
                {
                    break;
                }
                else
                {
                    if (poleveci[xpozice, ypozice + ctr] == 0)
                    {

                    }
                    else
                    {

                        zakazat = 1;
                        break;
                    }
                    ctr++;
                }
            }
            return zakazat;
        }
        public void PoleVyskaSet(double vyska, int xpozice, int ypozice, int hodnota)
        {
            int ctr = 0;
            while (true)
            {
                if (ctr == vyska / 50)
                {
                    break;
                }
                else
                {
                    poleveci[xpozice, ypozice + ctr] = hodnota;
                    if (hodnota == 0)
                    {
                    }
                    else
                    {
                    }

                    ctr++;
                }
            }
        }
        public void PoleSirkaSet(double sirka, int xpozice, int ypozice, int hodnota)
        {
            int ctr = 0;
            while (true)
            {
                if (ctr == sirka / 50)
                {
                    break;
                }
                else
                {
                    poleveci[xpozice + ctr, ypozice] = hodnota;
                    if (hodnota == 0)
                    {
                    }
                    else
                    {
                    }
                    ctr++;
                }
            }
        }
        public int PoleSirka(double sirka, int xpozice, int ypozice)
        {
            int ctr = 0;
            int zakazat = 0;
            while (true)
            {
                if (ctr == sirka / 50)
                {
                    break;
                }
                else
                {
                    if (poleveci[xpozice + ctr, ypozice] == 0)
                    {
                    }
                    else
                    {
                        zakazat = 1;
                        break;
                    }
                    ctr++;
                }
            }
            return zakazat;
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
            public string name;
            public int LeftPos;
            public int TopPos;


        }


    }
}
