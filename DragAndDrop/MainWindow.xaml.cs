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
using System.Windows.Forms;
using System.Drawing;
using FileHelpers;
using System.IO;

namespace DragAndDrop
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
            double[] nazvy= new double[5];
            var engine = new FileHelperEngine<Orders>();
            string curFile = "Outputnew.txt";
            if (File.Exists(curFile))
            {
                
            }
            else
            {
                var orders = new List<Orders>();
                engine.WriteFile("Outputnew.txt", orders);
            }
            var records = engine.ReadFile("Output.txt");
            
            foreach (var record in records)
            {
                
                Canvas.SetTop(rect1, record.y);
                Canvas.SetLeft(rect1, record.x);
            }

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
            var engine = new FileHelperEngine<Orders>();
            int ctr1 = 1;
            Console.WriteLine("NAME: " +rectando.Name);
            var orders = new List<Orders>();

            for (int i = 0; i < 1; i++)
            {
                double x = Canvas.GetLeft(rectando);
                int x1 = Convert.ToInt32(x);
                double y = Canvas.GetTop(rectando);
                int y1 = Convert.ToInt32(y);
                orders.Add(new Orders()
                {
                    x = x1,
                    y = y1
                });
                
                Console.WriteLine(" X: " + x + " Y: " + y);
                ctr1++;
            }
            engine.WriteFile("Outputnew.txt", orders);

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
